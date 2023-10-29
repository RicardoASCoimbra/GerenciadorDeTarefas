using Tarefas.Domain.Enuns;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.Interfaces.Usuarios
{
    public interface IUsuarioAppService
    {
        Task Create(UsuarioViewModel model);
        Task Update(UsuarioViewModel model);
        Task<IEnumerable<UsuarioViewModel>> GetAll();
        Task<IEnumerable<UsuarioViewModel>> GetByLogin(string login);
        Task<bool> CheckExists(string login, string cpf);
        Task<IEnumerable<UsuarioViewModel>> GetAllPerfils();
        Task<IEnumerable<UsuarioViewModel>> GetByFilter(string login, TipoDeAcesso? perfil);
        Task<UsuarioViewModel> GetById(Guid id);
        Task<UsuarioViewModel> GetByUser(string cpf);
        Task<bool> Delete(Guid id);
    }
}
