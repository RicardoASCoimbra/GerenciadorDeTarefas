using FluentValidation.Results;
using Tarefas.Domain.Core.Events;

namespace Tarefas.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public Guid? UsuarioRequerenteId { get; set; }
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command() => Timestamp = DateTime.Now;

        public abstract bool IsValid();

    }
}
