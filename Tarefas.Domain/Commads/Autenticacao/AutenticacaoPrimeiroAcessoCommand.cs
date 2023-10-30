using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Validation.Autenticacao;

namespace Tarefas.Domain.Commads.Autenticacao
{
    public class AutenticacaoPrimeiroAcessoCommand : Command
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
        public AutenticacaoPrimeiroAcessoCommand() { }

        public override bool IsValid()
        {
            ValidationResult = new AutenticacaoPrimeiroAcessoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
