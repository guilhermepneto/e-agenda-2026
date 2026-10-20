using eAgenda.Dominio.Modulos.Categorias;
using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Tarefas;
using eAgenda.Infraestrutura.Modulos.Categorias;
using eAgenda.Infraestrutura.Modulos.Contatos;
using eAgenda.Infraestrutura.Modulos.Compromissos;
using eAgenda.Infraestrutura.Modulos.Despesas;
using eAgenda.Infraestrutura.Modulos.Tarefas;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Compartilhado.Orm;

public sealed class EAgendaDbContext(DbContextOptions<EAgendaDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<ItemTarefa> ItensTarefa => Set<ItemTarefa>();
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Compromisso> Compromissos => Set<Compromisso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new DespesaConfiguration());
        modelBuilder.ApplyConfiguration(new TarefaConfiguration());
        modelBuilder.ApplyConfiguration(new ItemTarefaConfiguration());
        modelBuilder.ApplyConfiguration(new ContatoConfiguration());
        modelBuilder.ApplyConfiguration(new CompromissoConfiguration());
    }
}
