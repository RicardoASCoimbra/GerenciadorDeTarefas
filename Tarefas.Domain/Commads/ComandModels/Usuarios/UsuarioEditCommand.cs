using Tarefas.Domain.Core.Commands;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Validation.Usuarios;

namespace Tarefas.Domain.Commads.ComandModels.Usuarios
{
    public class UsuarioEditCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public TipoDeAcesso Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public bool Ativo { get; set; }
        public bool PrimeiroAcesso { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UsuarioEditCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

