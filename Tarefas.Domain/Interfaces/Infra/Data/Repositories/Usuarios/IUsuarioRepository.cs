using Tarefas.Domain.Enuns;
using Tarefas.Domain.Interfaces.Domain;
using Tarefas.Domain.Models.Usuario;

namespace Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios
{
    public interface IUsuarioRepository : IRepositoryBase<UsuarioModel>
    {
        Task<IEnumerable<UsuarioModel>> GetAllUsuarios();
        Task<IEnumerable<UsuarioModel>> GetByLogin(string login);
        Task<IEnumerable<UsuarioModel>> GetByCpf(string cpf);
        Task<IEnumerable<UsuarioModel>> VerificaDuplicidade(string login, string cpf);
        Task<IEnumerable<UsuarioModel>> GetAllPerfils();
        Task<IEnumerable<UsuarioModel>> GetByFilter(string login, TipoDeAcesso? perfil);
        Task<UsuarioModel> GetById(Guid id);
        Task<UsuarioModel> GetByUser(string cpf);

    }
}
