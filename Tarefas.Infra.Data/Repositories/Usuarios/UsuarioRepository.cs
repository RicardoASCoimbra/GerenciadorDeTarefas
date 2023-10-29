using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Interfaces.Infra.Data.Repositories.Usuarios;
using Tarefas.Domain.Models.Usuario;
using Tarefas.Infra.Data.Configuration;
using Tarefas.Infra.Data.Context;

namespace Tarefas.Infra.Data.Repositories.Usuarios
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TarefaDB context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetByLogin(string login)
        {
            IQueryable<Usuario> query = DbSet.Where(x => x.Login.ToLower() == login.ToLower());
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetByCpf(string cpf)
        {
            return await DbSet.Where(x => x.CPF.ToLower() == cpf.ToLower() && x.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> VerificaDuplicidade(string login, string cpf)
        {
            var userPorLogin = await GetByLogin(login);
            var userPorCpf = await GetByCpf(cpf);

            return userPorCpf.Count() > 0 ? userPorCpf : userPorLogin;
        }
        public async Task<IEnumerable<Usuario>> GetAllPerfils()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetByFilter(string login, TipoDeAcesso? perfil)
        {
            IQueryable<Usuario> query =
                DbSet.Where(x => (string.IsNullOrEmpty(login) || x.Login.ToLower().Contains(login.ToLower()))
                && (perfil == null || x.Perfil.Equals(perfil.Value)) && !x.Excluido);

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task<Usuario> GetByUser(string cpf)
        {
            return await _context.Set<Usuario>().Where(x => x.CPF == cpf).FirstOrDefaultAsync();
        }


        public async Task<Usuario> GetById(Guid id)
        {
            return await _context.Set<Usuario>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
