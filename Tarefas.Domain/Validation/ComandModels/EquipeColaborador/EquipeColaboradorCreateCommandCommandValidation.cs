using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.EquipeColaborador;

namespace Tarefas.Domain.Validation.ComandModels.EquipeColaborador
{
    public class EquipeColaboradorCreateCommandCommandValidation : CommandValidation<EquipeColaboradorCreateCommand>
    {
        public EquipeColaboradorCreateCommandCommandValidation()
        {
            RuleFor(x => x.NomeEquipe).NotEmpty().WithMessage("O nome da pastoral é obrigatório!");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("O tipo da pastoral  é obrigatório!");
        }

    }
}