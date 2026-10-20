using eAgenda.Dominio.Modulos.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Modulos.Tarefas;

public sealed class ItemTarefaConfiguration : IEntityTypeConfiguration<ItemTarefa>
{
    public void Configure(EntityTypeBuilder<ItemTarefa> builder)
    {
        builder.ToTable("TBItensTarefa");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Concluido)
            .IsRequired();

        builder.Property(i => i.TarefaId)
            .IsRequired();
    }
}
