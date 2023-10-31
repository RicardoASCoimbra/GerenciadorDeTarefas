using FluentValidation;
using Tarefas.Domain.Commads.ComandModels.Usuarios;

namespace Tarefas.Domain.Validation.ComandModels.Usuarios
{
    public class UsuarioCreateCommandValidation : CommandValidation<UsuarioCreateCommand>
    {
        public UsuarioCreateCommandValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome do usuário é obrigatório!");
            RuleFor(x => x.Nome)
                .MaximumLength(255).WithMessage("O campo Nome não pode conter mais que 255 caracteres!");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O CPF do usuário é obrigatório!");
            RuleFor(x => x.CPF)
                .MaximumLength(11).WithMessage("O campo CPF não pode conter mais de 11 caracteres!");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("O Login do usuário é obrigatório!");
            RuleFor(x => x.Login)
                .MaximumLength(255).WithMessage("O campo Login não pode conter mais de 255 caracteres!");

            RuleFor(x => x.Perfil)
                .NotEmpty().WithMessage("O Perfil do usuário é obrigatório!");
            RuleFor(x => x.Perfil)
                .IsInEnum().WithMessage("O Perfil deve ser válido");

            RuleFor(x => x.Senha)
                .MaximumLength(50)
                .NotEmpty().WithMessage("A Senha do usuário é obrigatória!");

            RuleForEach(x => x.EquipeColaborador)
              //.Must(y => y.PossuiVideo).WithMessage("Obrigatório selecionar um item")
              .Must(y => !String.IsNullOrEmpty(y.NomeEquipe)).WithMessage("O Titulo do vídeo é obrigatório!")
              .Must(y => !String.IsNullOrEmpty(y.Descricao)).WithMessage("O Link do vídeo é obrigatório!");

        }
    }
}
