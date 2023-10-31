using Tarefas.Domain.Enuns;

namespace Tarefas.Services.ViewModels.Tarefa
{
    public class TarefaViewModel
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
    }
}
