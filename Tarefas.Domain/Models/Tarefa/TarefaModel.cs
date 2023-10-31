using Tarefas.Domain.Core.Models;
using Tarefas.Domain.Enuns;

namespace Tarefas.Domain.Models.Tarefa
{
    public class TarefaModel : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public virtual Status Status { get; private set; }
        public virtual Prioridades Prioridades { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public string Responsavel { get; private set; }

        public TarefaModel() { }

        public TarefaModel(
            Guid id,
            string nome,
            string descricao,
            Status status,
            Prioridades prioridades,
            DateTime dataCadastro,
            DateTime? dataInicio,
            DateTime? dataFim,
            string Responsavel)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Status = status;
            this.Prioridades = prioridades;
            this.DataCadastro = dataCadastro;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Responsavel = Responsavel;
        }

        public void SetDados(
             string nome,
            string descricao,
            Status status,
            Prioridades prioridades,
            DateTime dataCadastro,
            DateTime? dataInicio,
            DateTime? dataFim,
            string Responsavel)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Status = status;
            this.Prioridades = prioridades;
            this.DataCadastro = dataCadastro;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Responsavel = Responsavel;
        }
    }
}
