using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.Login;

namespace Tarefas.Domain.Validation.ComandModels.Login
{
    public class AuthPrimeiroAcessoCommandValidation : CommandValidation<AuthPrimeiroAcessoCommand>
    {
        public AuthPrimeiroAcessoCommandValidation()
        {
            RuleFor(x => x.NovaSenha.Trim()).NotEmpty().WithMessage("A nova senha não deve ser vazia");
            RuleFor(x => x.NovaSenha).Must(x => x.Length >= 4).WithMessage("A nova senha deve conter pelo menos 4 caracteres");
            RuleFor(x => x.NovaSenha).Equal(x => x.ConfirmaSenha).WithMessage("As senhas não coincidem.");

        }
    }
}
