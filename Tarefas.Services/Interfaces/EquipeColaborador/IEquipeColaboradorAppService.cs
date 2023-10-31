using Tarefas.Services.ViewModels.EquipeColaborador;

namespace Tarefas.Services.Interfaces.EquipeColaborador
{
    public interface IEquipeColaboradorAppService
    {
        Task Create(EquipeColaboradorViewModel model);
        Task Update(EquipeColaboradorViewModel model);
        Task<object> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<EquipeColaboradorViewModel>> GetAll();
        Task<IEnumerable<EquipeColaboradorViewModel>> GetByFilter(string nome, string descricao);
        Task<IEnumerable<EquipeColaboradorViewModel>> GetEquipe(string nome);
    }
}
