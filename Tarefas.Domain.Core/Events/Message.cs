using MediatR;

namespace Tarefas.Domain.Core.Events
{
    public abstract class Message : IRequest
    {
        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }

        protected Message() => MessageType = GetType().Name;
    }
}
