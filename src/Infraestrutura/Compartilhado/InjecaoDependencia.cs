using eAgenda.Dominio.Modulos.Categorias;
using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Tarefas;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using eAgenda.Infraestrutura.Modulos.Categorias;
using eAgenda.Infraestrutura.Modulos.Contatos;
using eAgenda.Infraestrutura.Modulos.Compromissos;
using eAgenda.Infraestrutura.Modulos.Despesas;
using eAgenda.Infraestrutura.Modulos.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eAgenda.Infraestrutura.Compartilhado;

public static class InjecaoDependencia
{
    public static void AdicionarCamadaInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EAgendaDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServer");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("A ConnectionStrings:SqlServer não foi configurada.");
            options.UseSqlServer(connectionString, cfg => cfg.EnableRetryOnFailure(3));
        });
        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmOrm>();
        services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmOrm>();
        services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmOrm>();
    }
}
