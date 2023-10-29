﻿using Tarefas.Domain.Enuns;

namespace Tarefas.Services.ViewModels.Usuarios
{
    public class UsuarioViewModel
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
    }
}
