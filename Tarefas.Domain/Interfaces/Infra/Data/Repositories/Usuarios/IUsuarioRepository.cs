using Tarefas.Domain.Enuns;
using Tarefas.Domain.Interfaces.Domain;
using Tarefas.Domain.Models.Usuario;

namespace Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<IEnumerable<Usuario>> GetByLogin(string login);
        Task<IEnumerable<Usuario>> GetByCpf(string cpf);
        Task<IEnumerable<Usuario>> VerificaDuplicidade(string login, string cpf);
        Task<IEnumerable<Usuario>> GetAllPerfils();
        Task<IEnumerable<Usuario>> GetByFilter(string login, TipoDeAcesso? perfil);
        Task<Usuario> GetById(Guid id);
        Task<Usuario> GetByUser(string cpf);

    }
}
