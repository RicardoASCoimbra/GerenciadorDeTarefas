using Tarefas.Domain.Interfaces.Infra.Data;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TarefaDB _dbContext;

        public UnitOfWork(TarefaDB dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Commit()
        {
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public void Close()
        {
            Dispose();
        }

        public string GetContextId()
        {
            return _dbContext.GetHashCode().ToString();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

}

