using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.Login;

namespace Tarefas.Domain.Validation.ComandModels.Login
{
    public class AuthLoginCommandValidation : CommandValidation<AuthLoginCommand>
    {
        public AuthLoginCommandValidation()
        {
            RuleFor(x => x.Login.Trim()).NotEmpty().WithMessage("O Campo Login não pode estar vazio");
            RuleFor(x => x.Senha.Trim()).NotEmpty().WithMessage("O Campo Senha não pode estar vazio");
        }
    }
}