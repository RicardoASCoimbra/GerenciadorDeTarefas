using Tarefas.Services.ViewModels.Login;
using Tarefas.Services.ViewModels.Usuarios;

namespace Tarefas.Services.Interfaces.Login
{
    public interface ILoginAppService
    {
        Task<UsuarioViewModel> Autenticar(LoginViewModel loginViewModel);
        Task<UsuarioViewModel> PrimeiroAcesso(PrimeiroAcessoViewModel primeiroAcessoViewModel);
        Task<bool> ResetarSenha(string id);
        Task<bool> EsqueciSenha(string cpf);
        Task<string> AlterarSenha(AlterarSenhaViewModel model);
    }
}
