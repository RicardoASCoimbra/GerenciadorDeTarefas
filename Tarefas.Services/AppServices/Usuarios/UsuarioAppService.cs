using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tarefas.Domain.Commads.ComandModels.Usuarios;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Services.Interfaces.Usuarios;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.AppServices.Usuarios
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUsuarioRepository _repository;

        public UsuarioAppService(IMediatorHandler bus, IMapper mapper, INotificationHandler<DomainNotification> notifications,
            IUsuarioRepository repository, IHttpContextAccessor httpContext)
        {
            _bus = bus;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _httpContext = httpContext;
        }

        public async Task Create(UsuarioViewModel model)
        {

            UsuarioCreateCommand command = _mapper.Map<UsuarioCreateCommand>(model);
            command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            await _bus.SendCommand(command);
        }

        public async Task Update(UsuarioViewModel model)
        {
            UsuarioEditCommand command = _mapper.Map<UsuarioEditCommand>(model);
            command.UsuarioRequerenteId = Guid.Parse(_httpContext.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            await _bus.SendCommand(command);
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAll()
        {
            var users = (await _repository.GetAllUsuarios());
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(users);
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetByLogin(string login)
        {
            var response = (await _repository.GetByLogin(login));
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(response);
        }

        public async Task<bool> CheckExists(string login, string cpf)
        {
            var response = (await _repository.VerificaDuplicidade(login, cpf));
            return response.Count() > 0;
        }
        public async Task<IEnumerable<UsuarioViewModel>> GetAllPerfils()
        {
            var response = (await _repository.GetAllPerfils());
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(response);
        }
        public async Task<IEnumerable<UsuarioViewModel>> GetByFilter(string login, TipoDeAcesso? perfil)
        {
            var response = (await _repository.GetByFilter(login, perfil));
            return _mapper.ProjectTo<UsuarioViewModel>(response.AsQueryable());
        }

        public async Task<UsuarioViewModel> GetById(Guid id)
        {
            var response = (await _repository.GetById(id));
            return _mapper.Map<UsuarioViewModel>(response);
        }

        public async Task<UsuarioViewModel> GetByUser(string cpf)
        {
            var response = (await _repository.GetByUser(cpf));
            return _mapper.Map<UsuarioViewModel>(response);
        }

        public async Task<bool> Delete(Guid id)
        {
            var usuario = await _repository.GetById(id);
            if (usuario != null)
            {

                try
                {
                    await DeleteFisicamente(usuario);
                }
                catch (Exception e)
                {
                    Console.WriteLine("MESSAGE: " + e.Message + "\n INNER: " + e.InnerException);
                    return false;
                }

                return true;
            }

            return false;

        }
        private async Task DeleteFisicamente(UsuarioModel usuario)
        {
            _repository.Delete(usuario);
            await _repository.SaveChangesAsync();
        }

    }
}
