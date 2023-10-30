using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Utils;

namespace Tarefas.Domain.Commads.Autenticacao
{
    public class AutenticacaoCommandHandler : CommandHandler,
        IRequestHandler<AutenticacaoLoginCommand, Unit>,
        IRequestHandler<AutenticacaoPrimeiroAcessoCommand, Unit>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoCommandHandler(
            IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications,
            IUsuarioRepository usuarioRepository) : base(uow, bus, notifications)
        {
            _bus = bus;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Unit> Handle(AutenticacaoLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    NotifyValidationErrors(request);
                    return Unit.Value;
                }

                var userByLogin = await _usuarioRepository.GetByLogin(request.Login);
                switch (userByLogin.Count())
                {
                    case 0:
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, "O nome de usuário informado é inválido"));
                        break;
                    case 1:
                        var userQuery = userByLogin.First();
                        string requestSenha = Cryptography.GetHash(userQuery.Salt, request.Senha);
                        if (requestSenha != userQuery.Senha)
                            await _bus.RaiseEvent(new DomainNotification(request.MessageType, "A senha informada é inválida"));
                        break;
                    default:
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Existe mais de um usuário com o mesmo login: {request.Login.ToLower()}"));
                        break;
                }
            }
            catch (Exception ex)
            {
                await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Acesso Negado: {ex.Message}"));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(AutenticacaoPrimeiroAcessoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Unit.Value;
            }

            var usuario = (await _usuarioRepository.GetByLogin(request.Login)).FirstOrDefault();
            if (usuario == null)
            {
                await _bus.RaiseEvent(new DomainNotification(request.MessageType, "O usuário não foi encontrado no banco de dados"));
            }
            else
            {
                usuario.SetSenha(Cryptography.GetHash(usuario.Salt, request.NovaSenha), usuario.Salt);
                usuario.SetPrimeiroAcesso(false);
                _usuarioRepository.Update(usuario);
                await Commit();
            }

            return Unit.Value;
        }
    }
}
