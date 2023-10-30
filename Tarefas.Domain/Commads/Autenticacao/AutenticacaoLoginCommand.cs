using Tarefas.Domain.Core.Commands;

namespace Tarefas.Domain.Commads.Autenticacao
{
    public class AutenticacaoLoginCommand : Command
    {
        public string Login { get; protected set; }
        public string Senha { get; protected set; }

        public AutenticacaoLoginCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Senha);
        }
    }
}
