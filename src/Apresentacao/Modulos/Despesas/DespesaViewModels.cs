
using eAgenda.Aplicacao.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Despesas;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

[ValidateNever]
public record CadastrarDespesaViewModel(
    string Descricao,
    DateOnly DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    List<Guid> CategoriasIds
)
{
    public List<CategoriaDespesaDto> CategoriasDisponiveis { get; set; } = [];
};

[ValidateNever]
public record EditarDespesaViewModel(
    Guid Id,
    string Descricao,
    DateOnly DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    List<Guid> CategoriasIds
)
{
    public List<CategoriaDespesaDto> CategoriasDisponiveis { get; set; } = [];
};

[ValidateNever]
public record ExcluirDespesaViewModel(
    Guid Id,
    string Descricao,
    DateOnly DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento
);
