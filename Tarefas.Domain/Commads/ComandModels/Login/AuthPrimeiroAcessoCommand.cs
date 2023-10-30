using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Validation.ComandModels.Login;

namespace Tarefas.Domain.Commads.ComandModels.Login
{
    public class AuthPrimeiroAcessoCommand : Command
    {
        public string Login { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
        public AuthPrimeiroAcessoCommand() { }

        public override bool IsValid()
        {
            ValidationResult = new AuthPrimeiroAcessoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}