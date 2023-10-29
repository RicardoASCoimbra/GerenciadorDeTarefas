using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarefas.Domain.Interfaces.Domain;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Configuration
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected TarefaDB _context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(TarefaDB context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }
        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAll(bool @readonly = true)
        {
            if (@readonly)
                return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            else
                return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            if (@readonly)
                return await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
            else
                return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
