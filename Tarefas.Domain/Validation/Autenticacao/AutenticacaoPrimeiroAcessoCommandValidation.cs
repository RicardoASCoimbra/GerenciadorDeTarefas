using FluentValidation;
using Tarefas.Domain.Commads.Autenticacao;

namespace Tarefas.Domain.Validation.Autenticacao
{
    public class AutenticacaoPrimeiroAcessoCommandValidation : CommandValidation<AutenticacaoPrimeiroAcessoCommand>
    {
        public AutenticacaoPrimeiroAcessoCommandValidation()
        {
            RuleFor(x => x.NovaSenha.Trim()).NotEmpty().WithMessage("A nova senha não deve ser vazia");
            RuleFor(x => x.NovaSenha).Must(x => x.Length >= 4).WithMessage("A nova senha deve conter pelo menos 4 caracteres");
            RuleFor(x => x.NovaSenha).NotEqual(x => x.Senha).WithMessage("A nova senha não deve ser igual a senha anterior.");
            RuleFor(x => x.NovaSenha).Equal(x => x.ConfirmaSenha).WithMessage("As senhas não coincidem.");

        }
    }
}