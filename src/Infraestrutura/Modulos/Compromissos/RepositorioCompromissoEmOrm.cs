using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;

public sealed class RepositorioCompromissoEmOrm(EAgendaDbContext dbContext)
    : RepositorioBaseEmOrm<Compromisso>(dbContext), IRepositorioCompromisso
{
    public bool ExisteConflito(DateOnly data, TimeOnly inicio, TimeOnly termino, Guid? idIgnorado = null) =>
        registros.Any(c => c.DataOcorrencia == data
            && (!idIgnorado.HasValue || c.Id != idIgnorado.Value)
            && inicio < c.HoraTermino && termino > c.HoraInicio);

    public override Compromisso? SelecionarPorId(Guid idSelecionado) =>
        registros.Include(c => c.Contato).SingleOrDefault(c => c.Id == idSelecionado);

    public override List<Compromisso> SelecionarTodos() =>
        registros.Include(c => c.Contato)
            .OrderBy(c => c.DataOcorrencia).ThenBy(c => c.HoraInicio).ToList();
}
