using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Core.Events;

namespace Tarefas.Domain.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
