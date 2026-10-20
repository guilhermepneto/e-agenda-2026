using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Compromissos;

public interface IRepositorioCompromisso : IRepositorio<Compromisso>
{
    bool ExisteConflito(DateOnly data, TimeOnly inicio, TimeOnly termino, Guid? idIgnorado = null);
}
