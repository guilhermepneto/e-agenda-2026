using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Categorias;

public record CadastrarCategoriaViewModel(
    [ValidateNever]
    string Titulo
);

public record EditarCategoriaViewModel(
    Guid Id,
    [ValidateNever]
    string Titulo
);

public record ExcluirCategoriaViewModel(
    Guid Id,
    string Titulo
);
