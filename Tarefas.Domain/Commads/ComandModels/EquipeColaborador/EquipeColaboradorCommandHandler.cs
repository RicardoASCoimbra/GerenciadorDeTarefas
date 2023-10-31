using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Domain.Models.EquipeColaborador;

namespace Tarefas.Domain.Commads.ComandModels.EquipeColaborador
{
    public class EquipeColaboradorCommandHandler : CommandHandler, IRequestHandler<EquipeColaboradorCreateCommand>, IRequestHandler<EquipeColaboradorEditCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IEquipeColaboradorRepository _repository;



        public EquipeColaboradorCommandHandler(IEquipeColaboradorRepository repository,
                IMediatorHandler bus,
                IUnitOfWork uow,
                INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _repository = repository;
        }

        public async Task<Unit> Handle(EquipeColaboradorCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {

                EquipeColaboradorModel equipe = new EquipeColaboradorModel(Guid.NewGuid(), request.NomeEquipe, request.Descricao);
                try
                {
                    _repository.Add(equipe);
                    await Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return Unit.Value;
        }


        public async Task<Unit> Handle(EquipeColaboradorEditCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);

            else
            {
                EquipeColaboradorModel equipe = await _repository.GetById(request.Id);
                equipe.SetInfo(request.NomeEquipe, request.Descricao);

                try
                {
                    _repository.Update(equipe);
                    await Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return Unit.Value;
        }

    }
}
