using Newtonsoft.Json;
using Tarefas.Domain.Core.Events;
using Tarefas.Domain.Core.Interfaces;

namespace Tarefas.Infra.Data.EventSourcing
{
    public class EventStore : IEventStore
    {
        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(theEvent, serializedData, "_user.Name");
        }
    }
}
