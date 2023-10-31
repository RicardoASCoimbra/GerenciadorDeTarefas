using MediatR;
using Tarefas.Domain.Core.Interfaces;
using Tarefas.Domain.Core.Notifications;
using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Tarefa;
using Tarefas.Domain.Models.Tarefa;

namespace Tarefas.Domain.Commads.ComandModels.Tarefa
{
    public class TarefaCommandHandler : CommandHandler, IRequestHandler<TarefaCreateCommand>, IRequestHandler<TarefaEditCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly ITarefaRepository _repository;

        public TarefaCommandHandler(
                ITarefaRepository repository,
                IMediatorHandler bus,
                IUnitOfWork uow,
                INotificationHandler<DomainNotification> notifications)
                : base(uow, bus, notifications)
        {
            _bus = bus;
            _repository = repository;
        }


        public async Task<Unit> Handle(TarefaCreateCommand request, CancellationToken cancellationToken)
        {
            TarefaModel task = new TarefaModel(request.Id, request.Nome, request.Descricao, request.Status, request.Prioridades, request.DataCadastro,
                                            request.DataInicio, request.DataFim, request.Responsavel);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                try
                {
                    _repository.Add(task);
                    await Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await _bus.RaiseEvent(new DomainNotification("Erro", "Erro ao salvar os dados!"));
                }
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(TarefaEditCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                TarefaModel task = await _repository.GetById(request.Id);

                task.SetDados(request.Nome, request.Descricao, request.Status, request.Prioridades, request.DataCadastro,
                                            request.DataInicio, request.DataFim, request.Responsavel);

                try
                {
                    _repository.Update(task);
                    await Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await _bus.RaiseEvent(new DomainNotification("Erro", "Erro ao salvar os dados!"));
                }

            }
            return Unit.Value;
        }
    }
}

