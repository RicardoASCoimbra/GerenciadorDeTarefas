using Tarefas.Domain.Core.Models;
using Tarefas.Domain.Models.Usuario;

namespace Tarefas.Domain.Models.EquipeColaborador
{
    public class EquipeColaboradorModel : Entity
    {
        public string NomeEquipe { get; private set; }
        public string Descricao { get; private set; }

        public Guid IdUsuario { get; private set; } // Chave estrangeira para UsuarioModel
        public UsuarioModel UsuarioPrincipal { get; private set; } // Propriedade de navegação para UsuarioModel

        public EquipeColaboradorModel() { }

        public EquipeColaboradorModel(Guid id, string nomeEquipe, string descricao, Guid idUsuario)
        {
            Id = id;
            NomeEquipe = nomeEquipe;
            Descricao = descricao;
            IdUsuario = idUsuario;
        }

        public void SetInfo(string nomeEquipe, string descricao, Guid idUsuario)
        {
            NomeEquipe = nomeEquipe;
            Descricao = descricao;
            IdUsuario = idUsuario;
        }
    }
}
