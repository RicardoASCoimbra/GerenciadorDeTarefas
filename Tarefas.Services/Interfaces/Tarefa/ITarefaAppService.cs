using Tarefas.Services.ViewModels.Tarefa;

namespace Tarefas.Services.Interfaces.Tarefa
{
    public interface ITarefaAppService
    {
        Task Create(TarefaViewModel model);
        Task Update(TarefaViewModel model);
        Task<object> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<TarefaViewModel>> GetAll();
        Task<IEnumerable<TarefaViewModel>> GetByFilter(string nomeUsuario, string sistemaOrigem);
    }
}
