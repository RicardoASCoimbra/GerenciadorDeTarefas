using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Domain.Utils;

namespace Tarefas.Domain.Commads.ComandModels.Usuarios
{
    public class UsuarioCommandHandler : CommandHandler, IRequestHandler<UsuarioCreateCommand>, IRequestHandler<UsuarioEditCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _repository;
        private readonly IEquipeColaboradorRepository _equiperepository;


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
            var userId = request.UsuarioRequerenteId.ToGuid();

            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Unit.Value; // Retorna imediatamente se a solicitação não for válida.
            }

            var users = await _repository.VerificaDuplicidade(request.Login, request.CPF);

            if (users.Any())
            {
                await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Registro já existente."));
            }
            else
            {
                var equipeColaborador = CarregaEquipe(request);

                var usuario = new UsuarioModel(Guid.NewGuid(), request.Nome, request.Email, request.CPF, request.Cargo, request.Perfil, request.Login, request.Ativo, equipeColaborador);
                var salt = Cryptography.GetSalt();
                usuario.SetSenha(Cryptography.GetHash(salt, request.CPF), salt);

                _repository.Add(usuario);
                await Commit();
            }

            return Unit.Value;
        }

        private List<EquipeColaboradorModel> CarregaEquipe(UsuarioCreateCommand request)
        {
            var equipeColaborador = new List<EquipeColaboradorModel>();

            foreach (var item in request.EquipeColaborador)
            {
                var idequipeColaborador = Guid.NewGuid();
                var equipe = new EquipeColaboradorModel(idequipeColaborador, item.NomeEquipe, item.Descricao, request.Id);
                equipeColaborador.Add(equipe);
            }

            return equipeColaborador;
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
                    gerenciarEquipe(request, usuarioPrincipal);
                    usuarioPrincipal.SetDados(request.Nome, request.Email, request.Cargo, request.Perfil, request.Ativo, request.EquipeColaborador);
                    _repository.Update(usuarioPrincipal);
                    await Commit();
                }
            }
            return Unit.Value;
        }


        public void gerenciarEquipe(UsuarioEditCommand request, UsuarioModel usuarioPrincipal)
        {
            var equipeExcluidos = usuarioPrincipal.EquipeColaborador.Where(n => !request.EquipeColaborador.Select(a => a.Id).Contains(n.Id));
            var equipeJaCadastrados = usuarioPrincipal.EquipeColaborador.Select(n => n.Id);
            var equipeNaoCadastrados = request.EquipeColaborador.Where(n => !equipeJaCadastrados.Contains(n.Id));
            foreach (var video in equipeExcluidos)
            {
                _equiperepository.Delete(video);
            }
            foreach (var video in equipeNaoCadastrados)
            {
                _equiperepository.Add(video);
            }
        }
    }
}
