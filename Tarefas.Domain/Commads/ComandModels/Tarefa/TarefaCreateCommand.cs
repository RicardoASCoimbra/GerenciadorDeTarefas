using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Validation.ComandModels.Tarefa;

namespace Tarefas.Domain.Commads.ComandModels.Tarefa
{
    public class TarefaCreateCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual Status Status { get; set; }
        public virtual Prioridades Prioridades { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Responsavel { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new TarefaCreateCommandCreateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
