using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Contatos;

public sealed class ServicoContato(IRepositorioContato repositorio)
{
    public Result<Guid> Cadastrar(CadastrarContatoDto dto)
    {
        Contato contato = new(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);
        List<ErroValidacao> erros = contato.Validar();
        if (repositorio.ExisteEmail(contato.Email))
            erros.Add(new() { Campo = nameof(dto.Email), Mensagem = "Já existe um contato com este e-mail." });
        if (repositorio.ExisteTelefone(contato.Telefone))
            erros.Add(new() { Campo = nameof(dto.Telefone), Mensagem = "Já existe um contato com este telefone." });
        if (erros.Count > 0) return Falhar<Guid>(erros);
        repositorio.Cadastrar(contato);
        return Result.Ok(contato.Id);
    }

    public Result Editar(EditarContatoDto dto)
    {
        if (repositorio.SelecionarPorId(dto.Id) is null) return Result.Fail("Contato não encontrado.");
        Contato contato = new(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);
        List<ErroValidacao> erros = contato.Validar();
        if (repositorio.ExisteEmail(contato.Email, dto.Id))
            erros.Add(new() { Campo = nameof(dto.Email), Mensagem = "Já existe um contato com este e-mail." });
        if (repositorio.ExisteTelefone(contato.Telefone, dto.Id))
            erros.Add(new() { Campo = nameof(dto.Telefone), Mensagem = "Já existe um contato com este telefone." });
        if (erros.Count > 0) return Falhar(erros);
        return repositorio.Editar(dto.Id, contato) ? Result.Ok() : Result.Fail("Não foi possível editar o contato.");
    }

    public Result Excluir(Guid id)
    {
        if (repositorio.PossuiCompromissos(id))
            return Result.Fail("Este contato não pode ser excluído porque possui compromissos vinculados.");
        return repositorio.Excluir(id) ? Result.Ok() : Result.Fail("Contato não encontrado.");
    }

    public Result<ContatoDto> SelecionarPorId(Guid id)
    {
        Contato? c = repositorio.SelecionarPorId(id);
        return c is null ? Result.Fail("Contato não encontrado.") :
            Result.Ok(new ContatoDto(c.Id, c.Nome, c.Email, c.Telefone, c.Cargo, c.Empresa));
    }

    public List<ContatoDto> SelecionarTodos() => repositorio.SelecionarTodos()
        .OrderBy(c => c.Nome)
        .Select(c => new ContatoDto(c.Id, c.Nome, c.Email, c.Telefone, c.Cargo, c.Empresa)).ToList();

    private static Result Falhar(List<ErroValidacao> erros) =>
        Result.Fail(erros.Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList());

    private static Result<T> Falhar<T>(List<ErroValidacao> erros) =>
        Result.Fail<T>(erros.Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList());
}
