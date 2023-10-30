using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tarefas.Domain.Commads.ComandModels.Login;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Utils;
using Tarefas.Services.Interfaces.Login;
using Tarefas.Services.ViewModels.Login;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AppServices.Login
{
    public class LoginAppService : ILoginAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUsuarioRepository _repository;
        private readonly IHttpContextAccessor _httpContext;


        public LoginAppService(
            IMediatorHandler bus,
            IMapper mapper,
            INotificationHandler<DomainNotification> notifications,
            IUsuarioRepository repository,
            IHttpContextAccessor httpContext)
        {
            _bus = bus;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _httpContext = httpContext;
        }

        public async Task<UsuarioViewModel> Autenticar(LoginViewModel loginviewmodel)
        {
            UsuarioViewModel viewModel = null;
            var command = _mapper.Map<AuthLoginCommand>(loginviewmodel);
            await _bus.SendCommand(command);
            if (!_notifications.HasNotifications())
            {
                var usuario = (await _repository.GetByLogin(loginviewmodel.Login)).FirstOrDefault();
                viewModel = _mapper.Map<UsuarioViewModel>(usuario);
            }
            return viewModel;
        }

        public async Task<UsuarioViewModel> PrimeiroAcesso(PrimeiroAcessoViewModel primeiroAcessoViewModel)
        {
            UsuarioViewModel userViewModel = null;
            var command = _mapper.Map<AuthPrimeiroAcessoCommand>(primeiroAcessoViewModel);
            await _bus.SendCommand(command);
            if (!_notifications.HasNotifications())
            {
                var usuario = (await _repository.GetByLogin(primeiroAcessoViewModel.Login)).FirstOrDefault();
                userViewModel = _mapper.Map<UsuarioViewModel>(usuario);
            }
            return userViewModel;

        }

        public async Task<string> AlterarSenha(AlterarSenhaViewModel model)
        {
            var id = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var usuario = await _repository.GetById(id);
            var newSenha = Cryptography.GetHash(usuario.Salt, model.NovaSenha);
            if (Cryptography.GetHash(usuario.Salt, model.Senha) != usuario.Senha)
            {
                return "Senha atual não confere";
            }
            if (usuario.Senha == newSenha)
            {
                return "Nova senha precisa ser diferente da senha anterior";
            }
            if (model.NovaSenha.Length < 4)
            {
                return "A nova senha deve conter pelo menos 4 caracteres";
            }
            if (usuario != null)
            {
                string salt = Cryptography.GetSalt();
                usuario.setPrimeiroAcesso(false);
                usuario.SetSenha(Cryptography.GetHash(salt, model.NovaSenha), salt);
                try
                {
                    _repository.Update(usuario);
                    await _repository.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                return "Senha salva com sucesso";
            }

            return "Senha salva com sucesso";
        }

        public async Task<bool> ResetarSenha(string id)
        {
            var usuario = await _repository.GetById(Guid.Parse(id));
            if (usuario != null)
            {
                string salt = Cryptography.GetSalt();
                usuario.setPrimeiroAcesso(true);
                usuario.SetSenha(Cryptography.GetHash(salt, usuario.CPF), salt);
                try
                {
                    _repository.Update(usuario);
                    await _repository.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            }

            return false;
        }

        public async Task<bool> EsqueciSenha(string cpf)
        {
            var usuario = await _repository.GetByUser(cpf);
            if (usuario != null)
            {
                string salt = Cryptography.GetSalt();
                usuario.setPrimeiroAcesso(true);
                usuario.SetSenha(Cryptography.GetHash(salt, usuario.CPF), salt);
                try
                {
                    _repository.Update(usuario);
                    await _repository.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            }

            return false;
        }
    }
}
