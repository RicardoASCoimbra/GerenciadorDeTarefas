using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Tarefa;
using Tarefas.Domain.Models.Tarefa;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Repositories.Tarefa
{
    public class TarefaRepository : RepositoryBase<TarefaModel>, ITarefaRepository
    {
        public TarefaRepository(TarefaDB context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TarefaModel>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }
        public async Task<TarefaModel> GetById(Guid id)
        {
            IQueryable<TarefaModel> query = DbSet.Where(x => x.Id.Equals(id));
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TarefaModel>> GetByFilter(string nome, string responsavel)
        {
            IQueryable<TarefaModel> query =
                DbSet.Where(x => (string.IsNullOrEmpty(nome) || x.Nome.ToLower().Contains(nome.ToLower()))
                && (string.IsNullOrEmpty(responsavel) || x.Responsavel.ToLower().Contains(responsavel.ToLower())));
            return await query.AsNoTracking().ToListAsync();
        }

    }
}
