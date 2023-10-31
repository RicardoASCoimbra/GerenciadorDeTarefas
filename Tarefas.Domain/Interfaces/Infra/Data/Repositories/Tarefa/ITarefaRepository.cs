using Tarefas.Domain.Interfaces.Domain;
using Tarefas.Domain.Models.Tarefa;

namespace Tarefas.Domain.Interfaces.Infra.Data.Repositories.Tarefa
{
    public interface ITarefaRepository : IRepositoryBase<TarefaModel>
    {
        Task<TarefaModel> GetById(Guid id);
        Task<IEnumerable<TarefaModel>> GetAll();
        Task<IEnumerable<TarefaModel>> GetByFilter(string nome, string responsavel);
    }
}