using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.Tarefa;

namespace Tarefas.Domain.Validation.ComandModels.Tarefa
{
    public class TarefaCreateCommandCreateCommandValidation : CommandValidation<TarefaCreateCommand>
    {
        public TarefaCreateCommandCreateCommandValidation()
        {
            RuleFor(x => x.DataCadastro).NotEmpty().WithMessage("A data do Cadastro é obrigatório!");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("A nome da Tarefa é obrigatório!");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição da tarefa é requisito obrigatório");
        }
    }
}
