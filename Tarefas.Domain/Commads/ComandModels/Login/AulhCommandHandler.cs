using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Domain.Utils;

namespace Tarefas.Domain.Commads.ComandModels.Login
{
    public class AulhCommandHandler : CommandHandler, IRequestHandler<AuthLoginCommand>, IRequestHandler<AuthPrimeiroAcessoCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _repository;

        public AulhCommandHandler(IUsuarioRepository repository, IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _repository = repository;
            _bus = bus;
        }


        public async Task<Unit> Handle(AuthPrimeiroAcessoCommand request, CancellationToken cancellationToken)
        {
            //LogHistorico log;
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                var usuario = (await _repository.GetByLogin(request.Login)).FirstOrDefault();
                if (usuario == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "O usuário não foi encontrado no banco de dados"));
                }
                else
                {

                    usuario.SetSenha(Cryptography.GetHash(usuario.Salt, request.NovaSenha), usuario.Salt);
                    usuario.setPrimeiroAcesso(false);
                    _repository.Update(usuario);
                    await Commit();
                }
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) NotifyValidationErrors(request);
            else
            {
                var userByLogin = await _repository.GetByLogin(request.Login);

                switch (userByLogin.Count())
                {
                    case 0:
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"O usuário informado é inválido"));
                        break;
                    case 1:
                        UsuarioModel user = userByLogin.ElementAt(0);
                        if (user.Ativo)
                        {
                            var userQuery = userByLogin.FirstOrDefault();
                            string requestSenha = Cryptography.GetHash(userQuery.Salt, request.Senha);
                            if (requestSenha != userQuery.Senha)
                                await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"A senha informada é inválida"));
                        }
                        else
                        {
                            await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Usuário {user.Login} está inativo, por favor entrar em contato com o Administrador."));
                        }
                        break;
                    default:
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Existe mais de um usuário com o mesmo login: {request.Login.ToLower()}"));
                        break;
                }
            }

            return Unit.Value;
        }
    }
}
