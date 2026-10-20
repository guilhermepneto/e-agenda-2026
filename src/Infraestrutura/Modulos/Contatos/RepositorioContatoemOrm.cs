using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Modulos.Contatos;

public sealed class RepositorioContatoEmOrm(EAgendaDbContext dbContext)
    : RepositorioBaseEmOrm<Contato>(dbContext), IRepositorioContato
{
    public bool ExisteEmail(string email, Guid? idIgnorado = null) =>
        registros.Any(c => c.Email.ToLower() == email.ToLower() && (!idIgnorado.HasValue || c.Id != idIgnorado.Value));

    public bool ExisteTelefone(string telefone, Guid? idIgnorado = null) =>
        registros.Any(c => c.Telefone == telefone && (!idIgnorado.HasValue || c.Id != idIgnorado.Value));

    public bool PossuiCompromissos(Guid contatoId) =>
        dbContext.Compromissos.Any(c => c.ContatoId == contatoId);

    public override List<Contato> SelecionarTodos() => registros.OrderBy(c => c.Nome).ToList();
}
