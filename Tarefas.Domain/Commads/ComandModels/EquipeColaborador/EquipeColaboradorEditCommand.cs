using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Validation.ComandModels.EquipeColaborador;

namespace Tarefas.Domain.Commads.ComandModels.EquipeColaborador
{
    public class EquipeColaboradorEditCommand : Command
    {
        public Guid Id { get; set; }
        public string NomeEquipe { get; set; }
        public string Descricao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new EquipeColaboradorEditCommandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
