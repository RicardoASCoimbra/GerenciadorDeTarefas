using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador;
using Tarefas.Domain.Models.EquipeColaborador;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Repositories.EquipeColaborador
{
    public class EquipeColaboradorRepository : RepositoryBase<EquipeColaboradorModel>, IEquipeColaboradorRepository
    {
        public EquipeColaboradorRepository(TarefaDB context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EquipeColaboradorModel>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }
        public async Task<EquipeColaboradorModel> GetById(Guid id)
        {
            IQueryable<EquipeColaboradorModel> query = DbSet.Where(x => x.Id.Equals(id));
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<EquipeColaboradorModel>> GetByFilter(string nome, string descricao)
        {
            IQueryable<EquipeColaboradorModel> query =
                DbSet.Where(x => (string.IsNullOrEmpty(nome) || x.NomeEquipe.ToLower().Contains(nome.ToLower()))
                && (string.IsNullOrEmpty(descricao) || x.Descricao.ToLower().Contains(descricao.ToLower())));
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EquipeColaboradorModel>> GetEquipe(string nome)
        {
            IQueryable<EquipeColaboradorModel> query = DbSet.Where(x => string.IsNullOrEmpty(nome) || x.NomeEquipe.ToLower().Contains(nome.ToLower()));
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
