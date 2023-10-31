using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Repositories.Usuarios
{
    public class UsuarioRepository : RepositoryBase<UsuarioModel>, IUsuarioRepository
    {
        public UsuarioRepository(TarefaDB context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllUsuarios()
        {
            return await DbSet.AsNoTracking().Include(q => q.EquipeColaborador).ToListAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> GetByLogin(string login)
        {
            IQueryable<UsuarioModel> query = DbSet.Where(x => x.Login.ToLower() == login.ToLower());
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> GetByCpf(string cpf)
        {
            return await DbSet.Where(x => x.CPF.ToLower() == cpf.ToLower() && x.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> VerificaDuplicidade(string login, string cpf)
        {
            var userPorLogin = await GetByLogin(login);
            var userPorCpf = await GetByCpf(cpf);

            return userPorCpf.Count() > 0 ? userPorCpf : userPorLogin;
        }
        public async Task<IEnumerable<UsuarioModel>> GetAllPerfils()
        {
            return await _context.Set<UsuarioModel>().ToListAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> GetByFilter(string login, TipoDeAcesso? perfil)
        {
            IQueryable<UsuarioModel> query =
                DbSet.Where(x => (string.IsNullOrEmpty(login) || x.Login.ToLower().Contains(login.ToLower()))
                && (perfil == null || x.Perfil.Equals(perfil.Value)));

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task<UsuarioModel> GetByUser(string cpf)
        {
            return await _context.Set<UsuarioModel>().Where(x => x.CPF == cpf).Include(q => q.EquipeColaborador).FirstOrDefaultAsync();
        }


        public async Task<UsuarioModel> GetById(Guid id)
        {
            //return await _context.Set<UsuarioModel>().Where(x => x.Id == id).FirstOrDefaultAsync();

            IQueryable<UsuarioModel> query = DbSet.Where(x => x.Id.Equals(id)).Include(q => q.EquipeColaborador);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
