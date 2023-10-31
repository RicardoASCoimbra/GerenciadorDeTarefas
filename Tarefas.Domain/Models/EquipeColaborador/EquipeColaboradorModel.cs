using Tarefas.Domain.Core.Models;

namespace Tarefas.Domain.Models.EquipeColaborador
{
    public class EquipeColaboradorModel : Entity
    {
        public string NomeEquipe { get; private set; }
        public string Descricao { get; private set; }


        public EquipeColaboradorModel() { }

        public EquipeColaboradorModel(Guid id, string nomeEquipe, string descricao)
        {
            Id = id;
            NomeEquipe = nomeEquipe;
            Descricao = descricao;
        }

        public void SetInfo(string nomeEquipe, string descricao)
        {
            NomeEquipe = nomeEquipe;
            Descricao = descricao;
        }

    }
}
