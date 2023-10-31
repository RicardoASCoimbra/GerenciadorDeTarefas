using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tarefas.Domain.Core.Models;
using Tarefas.Domain.Enuns;
using Tarefas.Domain.Models.EquipeColaborador;

namespace Tarefas.Domain.Models.Usuario
{
    public class UsuarioModel : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public TipoDeFuncao Cargo { get; private set; }
        public TipoDeAcesso Perfil { get; private set; }
        public string Login { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Senha { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Salt { get; set; }

        public bool Ativo { get; set; }
        public bool PrimeiroAcesso { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<EquipeColaboradorModel> EquipeColaborador { get; private set; }

        public UsuarioModel() { }

        public UsuarioModel(Guid id, string nome, string email, string cpf, TipoDeFuncao tipoDeFuncao, TipoDeAcesso tipoDeAcesso, string login, bool ativo, List<EquipeColaboradorModel> equipeColaborador)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.CPF = cpf;
            this.Cargo = tipoDeFuncao;
            this.Perfil = tipoDeAcesso;
            this.Login = login;
            this.Ativo = ativo;
            this.PrimeiroAcesso = true;
            this.EquipeColaborador = equipeColaborador;
        }

        public void AdicionarEquipeColaborador(EquipeColaboradorModel equipeColaborador)
        {
            var novaColecao = new List<EquipeColaboradorModel>(EquipeColaborador);
            novaColecao.Add(equipeColaborador);
            EquipeColaborador = novaColecao;
        }

        public void SetDados(string nome, string email, TipoDeFuncao tipoDeFuncao, TipoDeAcesso tipoDeAcesso, bool ativo, IEnumerable<EquipeColaboradorModel> equipeColaborador)
        {
            this.Nome = nome;
            this.Email = email;
            this.Ativo = ativo;
            this.Cargo = tipoDeFuncao;
            this.Perfil = tipoDeAcesso;
            this.EquipeColaborador = new List<EquipeColaboradorModel>(equipeColaborador);
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

    }

}