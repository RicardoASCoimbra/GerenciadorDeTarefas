using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.Usuarios;

namespace Tarefas.Domain.Validation.Usuarios
{
    public class UsuarioEditCommandValidation : CommandValidation<UsuarioEditCommand>
    {
        public UsuarioEditCommandValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome do usuário é obrigatório!");
            RuleFor(x => x.Nome)
                .MaximumLength(255).WithMessage("O campo Nome não pode conter mais que 255 caracteres!");

            RuleFor(x => x.Perfil)
              .NotEmpty().WithMessage("O Perfil do usuário é obrigatório!");
            RuleFor(x => x.Perfil)
                .IsInEnum().WithMessage("O Perfil deve ser válido");
        }
    }
}
