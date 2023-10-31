using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Models.Usuario;

namespace Tarefas.Infra.Data.Mapping.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.ToTable("Usuario");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(255);
            builder.Property(t => t.CPF).IsRequired().HasMaxLength(15);
            builder.Property(t => t.Login).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Senha).HasMaxLength(255);
            builder.Property(t => t.Salt).HasMaxLength(255);
            builder.Property(t => t.Ativo).IsRequired();
            builder.Property(t => t.PrimeiroAcesso).IsRequired();
            builder.Property(t => t.Perfil).IsRequired();
            builder.Property(t => t.Cargo);

            //PrimaryKey
            builder.HasKey(x => x.Id);
        }
    }
}
