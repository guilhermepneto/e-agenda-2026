using System;
using eAgenda.Dominio.Modulos.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Modulos.Despesas;

public class DespesaConfiguration : IEntityTypeConfiguration<Despesa>
{
    public void Configure(EntityTypeBuilder<Despesa> builder)
    {
        builder.ToTable("TBDespesas");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        builder.Property(d => d.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.DataOcorrencia)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(d => d.Valor)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(d => d.FormaPagamento)
            .HasConversion<string>()
            .IsRequired();

        builder.HasMany(d => d.Categorias)
            .WithMany(c => c.Despesas)
            .UsingEntity("TBDespesasCategorias");
    }
}
