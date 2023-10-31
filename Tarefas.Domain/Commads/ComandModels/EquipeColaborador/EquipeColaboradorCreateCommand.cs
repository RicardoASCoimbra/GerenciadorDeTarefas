using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Validation.ComandModels.EquipeColaborador;

namespace Tarefas.Domain.Commads.ComandModels.EquipeColaborador
{
    public class EquipeColaboradorCreateCommand : Command
    {
        public Guid Id { get; set; }
        public string NomeEquipe { get; set; }
        public string Descricao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new EquipeColaboradorCreateCommandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
