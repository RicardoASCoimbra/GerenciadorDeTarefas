using System.ComponentModel.DataAnnotations.Schema;
using Tarefas.Domain.Core.Models;
using Tarefas.Domain.Enuns;

namespace Tarefas.Domain.Models.Usuario
{
    public class UsuarioModel : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public TipoDeAcesso Perfil { get; set; }
        public string Login { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Senha { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Salt { get; set; }

        public bool Ativo { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public bool Excluido { get; set; }
        public UsuarioModel() { }

        public UsuarioModel(Guid id, string nome, string email, string cpf, TipoDeAcesso tipoDeAcesso, string login, bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.CPF = cpf;
            this.Perfil = tipoDeAcesso;
            this.Login = login;
            this.Ativo = ativo;
            this.PrimeiroAcesso = true;
        }

        public void SetDados(string nome, string email, TipoDeAcesso tipoDeAcesso, bool ativo)
        {
            this.Nome = nome;
            this.Email = email;
            this.Ativo = ativo;
            this.Perfil = tipoDeAcesso;
        }

        public void SetSenha(string senha, string salt)
        {
            this.Salt = salt;
            this.Senha = senha;
        }

        public void setPrimeiroAcesso(bool flag)
        {
            this.PrimeiroAcesso = flag;
        }

        public void setStatus(bool status)
        {
            this.Ativo = status;
        }

        public void SetPrimeiroAcesso(bool primeiroAcesso)
        {
            this.PrimeiroAcesso = primeiroAcesso;
        }

        public void SetExcluido(bool flag)
        {
            Excluido = flag;
        }
    }

}