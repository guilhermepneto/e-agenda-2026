using eAgenda.Dominio.Modulos.Despesas;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Modulos.Despesas;

public class RepositorioDespesaEmOrm(EAgendaDbContext dbContext) :
    RepositorioBaseEmOrm<Despesa>(dbContext), IRepositorioDespesa
{
    public override Despesa? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(d => d.Categorias)
            .SingleOrDefault(d => d.Id == idSelecionado);
    }

    public override List<Despesa> SelecionarTodos()
    {
        return registros
            .OrderByDescending(d => d.DataOcorrencia)
            .ThenBy(d => d.Descricao)
            .Include(d => d.Categorias)
            .ToList();
    }
}
