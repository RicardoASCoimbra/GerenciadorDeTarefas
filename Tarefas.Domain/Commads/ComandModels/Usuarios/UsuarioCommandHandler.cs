using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Domain.Utils;

namespace Tarefas.Domain.Commads.ComandModels.Usuarios
{
    public class UsuarioCommandHandler : CommandHandler, IRequestHandler<UsuarioCreateCommand>, IRequestHandler<UsuarioEditCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _repository;


        public UsuarioCommandHandler(IUsuarioRepository repository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications)

            : base(uow, bus, notifications)
        {
            _bus = bus;
            _repository = repository;
        }

        public async Task<Unit> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            Guid userId = request.UsuarioRequerenteId.ToGuid();
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                var user = await _repository.VerificaDuplicidade(request.Login, request.CPF);

                if (user.Count() > 0)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Registro já existente."));
                else
                {
                    UsuarioModel usuario = new UsuarioModel(Guid.NewGuid(), request.Nome, request.Email, request.CPF, request.Perfil, request.Login, request.Ativo);
                    string salt = Cryptography.GetSalt();
                    usuario.SetSenha(Cryptography.GetHash(salt, request.CPF), salt);
                    _repository.Add(usuario);
                    await Commit();
                }
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(UsuarioEditCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
            }
            else
            {
                UsuarioModel usuarioPrincipal = await _repository.GetById(request.Id);
                if (usuarioPrincipal == null)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "O usuário não foi encontrado no banco de dados"));
                else
                {
                    usuarioPrincipal.SetDados(request.Nome, request.Email, request.Perfil, request.Ativo);
                    _repository.Update(usuarioPrincipal);
                    await Commit();
                }
            }
            return Unit.Value;
        }
    }
}
