using System.ComponentModel.DataAnnotations;

namespace Tarefas.Services.ViewModels.Login
{
    public class LoginViewModel
    {
        [StringLength(50, ErrorMessage = "O nome de usuário possui um limite máximo de 50 caracteres")]
        [Required(ErrorMessage = "O nome de usuário não foi informado!")]
        public string Login { get; set; }

        [StringLength(50, ErrorMessage = "A senha de usuário possui um limite máximo de 50 caracteres")]
        [Required(ErrorMessage = "A senha não foi informada!")]
        public string Senha { get; set; }
    }
}
