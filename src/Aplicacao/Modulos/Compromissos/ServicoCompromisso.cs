using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

public sealed class ServicoCompromisso(IRepositorioCompromisso repositorio, IRepositorioContato contatos)
{
    public Result<Guid> Cadastrar(CadastrarCompromissoDto dto)
    {
        Compromisso c = new(dto.Assunto, dto.DataOcorrencia, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.LinkOuLocal, dto.ContatoId);
        List<ErroValidacao> erros = c.Validar();
        ValidarContato(dto.ContatoId, erros);
        if (repositorio.ExisteConflito(c.DataOcorrencia, c.HoraInicio, c.HoraTermino))
            erros.Add(new() { Campo = nameof(dto.HoraInicio), Mensagem = "Já existe um compromisso nesse intervalo de horário." });
        if (erros.Count > 0) return Falhar<Guid>(erros);
        repositorio.Cadastrar(c);
        return Result.Ok(c.Id);
    }

    public Result Editar(EditarCompromissoDto dto)
    {
        if (repositorio.SelecionarPorId(dto.Id) is null) return Result.Fail("Compromisso não encontrado.");
        Compromisso c = new(dto.Assunto, dto.DataOcorrencia, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.LinkOuLocal, dto.ContatoId);
        List<ErroValidacao> erros = c.Validar();
        ValidarContato(dto.ContatoId, erros);
        if (repositorio.ExisteConflito(c.DataOcorrencia, c.HoraInicio, c.HoraTermino, dto.Id))
            erros.Add(new() { Campo = nameof(dto.HoraInicio), Mensagem = "Já existe um compromisso nesse intervalo de horário." });
        if (erros.Count > 0) return Falhar(erros);
        return repositorio.Editar(dto.Id, c) ? Result.Ok() : Result.Fail("Não foi possível editar o compromisso.");
    }

    public Result Excluir(Guid id) => repositorio.Excluir(id) ? Result.Ok() : Result.Fail("Compromisso não encontrado.");

    public Result<CompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? c = repositorio.SelecionarPorId(id);
        return c is null ? Result.Fail("Compromisso não encontrado.") : Result.Ok(Mapear(c));
    }

    public List<CompromissoDto> SelecionarTodos() => repositorio.SelecionarTodos()
        .OrderBy(c => c.DataOcorrencia).ThenBy(c => c.HoraInicio).Select(Mapear).ToList();

    private void ValidarContato(Guid? contatoId, List<ErroValidacao> erros)
    {
        if (contatoId.HasValue && contatos.SelecionarPorId(contatoId.Value) is null)
            erros.Add(new() { Campo = nameof(Compromisso.ContatoId), Mensagem = "O contato selecionado não existe." });
    }

    private static CompromissoDto Mapear(Compromisso c) =>
        new(c.Id, c.Assunto, c.DataOcorrencia, c.HoraInicio, c.HoraTermino, c.Tipo, c.LinkOuLocal, c.ContatoId, c.Contato?.Nome);

    private static Result Falhar(List<ErroValidacao> erros) =>
        Result.Fail(erros.Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList());

    private static Result<T> Falhar<T>(List<ErroValidacao> erros) =>
        Result.Fail<T>(erros.Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList());
}
