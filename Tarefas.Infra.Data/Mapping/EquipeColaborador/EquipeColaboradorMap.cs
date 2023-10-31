using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Models.EquipeColaborador;

namespace Tarefas.Infra.Data.Mapping.EquipeColaborador
{
    public class EquipeColaboradorMap : IEntityTypeConfiguration<EquipeColaboradorModel>
    {
        public void Configure(EntityTypeBuilder<EquipeColaboradorModel> builder)
        {
            builder.ToTable("EquipeColaborador");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(t => t.NomeEquipe).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Descricao).IsRequired().HasMaxLength(255);

            //PrimaryKey
            builder.HasKey(x => x.Id);
        }
    }
}
