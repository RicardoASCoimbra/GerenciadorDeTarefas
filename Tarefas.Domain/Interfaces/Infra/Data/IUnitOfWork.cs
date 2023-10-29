namespace Tarefas.Domain.Interfaces.Infra.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        //Task Commit();
        void Close();
        string GetContextId();
    }
}
