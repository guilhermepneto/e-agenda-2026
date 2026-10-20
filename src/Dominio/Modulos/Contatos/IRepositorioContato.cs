using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public interface IRepositorioContato : IRepositorio<Contato>
{
    bool ExisteEmail(string email, Guid? idIgnorado = null);
    bool ExisteTelefone(string telefone, Guid? idIgnorado = null);
    bool PossuiCompromissos(Guid contatoId);
}
