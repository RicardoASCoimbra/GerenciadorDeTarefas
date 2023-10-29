using FluentValidation;
using Tarefas.Domain.Core.Commands;

namespace Tarefas.Domain.Validation
{
    public class CommandValidation<T> : AbstractValidator<T> where T : Command
    {
    }
}
