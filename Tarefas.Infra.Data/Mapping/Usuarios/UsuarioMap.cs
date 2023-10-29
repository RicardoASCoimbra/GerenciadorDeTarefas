using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Models.Usuario;

namespace Tarefas.Infra.Data.Mapping.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
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
            builder.Property(x => x.Excluido).HasDefaultValue(false);
            builder.HasKey(x => x.Id);
        }
    }
}
