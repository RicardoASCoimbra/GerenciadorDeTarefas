using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Models.Tarefa;

namespace Tarefas.Infra.Data.Mapping.Tarefa
{
    public class TarefaMap : IEntityTypeConfiguration<TarefaModel>
    {
        public void Configure(EntityTypeBuilder<TarefaModel> builder)
        {
            builder.ToTable("Tarefas");
            builder.Property(x => x.Id).IsRequired();
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Descricao).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Status);
            builder.Property(t => t.Prioridades);
            builder.Property(v => v.DataCadastro).IsRequired();
            builder.Property(x => x.DataInicio);
            builder.Property(x => x.DataFim);
            builder.Property(t => t.Responsavel).HasMaxLength(255);

            builder.HasKey(x => x.Id);
        }
    }
}
