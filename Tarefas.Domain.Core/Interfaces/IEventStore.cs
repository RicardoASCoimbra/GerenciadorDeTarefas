using Tarefas.Domain.Core.Events;

namespace Tarefas.Domain.Core.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
