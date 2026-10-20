using eAgenda.Dominio.Modulos.Tarefas;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Tarefas;

[ValidateNever]
public record CadastrarTarefaViewModel(
    string Titulo,
    PrioridadeTarefa Prioridade
);

[ValidateNever]
public record EditarTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa Prioridade
);

[ValidateNever]
public record ExcluirTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa Prioridade,
    DateOnly DataCriacao
);

[ValidateNever]
public record GerenciarItensTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa Prioridade,
    DateOnly DataCriacao,
    DateOnly? DataConclusao,
    int PercentualConcluido,
    bool Concluida,
    List<ItemTarefaViewModel> Itens
);

[ValidateNever]
public record AdicionarItemTarefaViewModel(
    Guid TarefaId,
    string Titulo
);

[ValidateNever]
public record ItemTarefaViewModel(
    Guid Id,
    string Titulo,
    bool Concluido
);
