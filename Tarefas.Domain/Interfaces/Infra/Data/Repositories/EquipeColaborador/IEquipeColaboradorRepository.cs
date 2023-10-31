using Tarefas.Domain.Interfaces.Domain;
using Tarefas.Domain.Models.EquipeColaborador;

namespace Tarefas.Domain.Interfaces.Infra.Data.Repositories.EquipeColaborador
{
    public interface IEquipeColaboradorRepository : IRepositoryBase<EquipeColaboradorModel>
    {
        Task<EquipeColaboradorModel> GetById(Guid id);
        Task<IEnumerable<EquipeColaboradorModel>> GetAll();
        Task<IEnumerable<EquipeColaboradorModel>> GetByFilter(string nome, string descricao);
        Task<IEnumerable<EquipeColaboradorModel>> GetEquipe(string nome);
    }
}