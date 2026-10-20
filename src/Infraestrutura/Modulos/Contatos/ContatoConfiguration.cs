using eAgenda.Dominio.Modulos.Contatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Modulos.Contatos;

public sealed class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("TBContatos");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(254).IsRequired();
        builder.Property(c => c.Telefone).HasMaxLength(20).IsRequired();
        builder.Property(c => c.Cargo).HasMaxLength(100);
        builder.Property(c => c.Empresa).HasMaxLength(150);
        builder.HasIndex(c => c.Email).IsUnique();
        builder.HasIndex(c => c.Telefone).IsUnique();
    }
}
