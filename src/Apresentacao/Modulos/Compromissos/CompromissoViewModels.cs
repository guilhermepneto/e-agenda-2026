using eAgenda.Dominio.Modulos.Compromissos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Compromissos;

[ValidateNever]
public record CadastrarCompromissoViewModel(string Assunto, DateOnly DataOcorrencia, TimeOnly HoraInicio,
    TimeOnly HoraTermino, TipoCompromisso Tipo, string LinkOuLocal, Guid? ContatoId);
[ValidateNever]
public record EditarCompromissoViewModel(Guid Id, string Assunto, DateOnly DataOcorrencia, TimeOnly HoraInicio,
    TimeOnly HoraTermino, TipoCompromisso Tipo, string LinkOuLocal, Guid? ContatoId);
[ValidateNever]
public record ExcluirCompromissoViewModel(Guid Id, string Assunto, DateOnly DataOcorrencia,
    TimeOnly HoraInicio, TimeOnly HoraTermino);
