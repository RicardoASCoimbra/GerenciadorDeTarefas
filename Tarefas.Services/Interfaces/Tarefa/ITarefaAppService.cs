using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Services.ViewModels.Tarefa;

namespace Tarefas.Services.Interfaces.Tarefa
{
    public interface ITarefaAppService
    {
        Task<TarefaViewModel> Create(TarefaViewModel model);
        Task Update(TarefaViewModel model);
        Task<TarefaViewModel> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<TarefaViewModel>> GetAll();
        Task<IEnumerable<TarefaViewModel>> GetByFilter(string nomeUsuario, string sistemaOrigem);
    }
}
