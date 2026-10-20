namespace eAgenda.Aplicacao.Modulos.Categorias;

// DTO = Data Transfer Object

public record CategoriaDto(
    Guid Id,
    string Titulo
);

public record CadastrarCategoriaDto(
    string Titulo
);

public record EditarCategoriaDto(
    Guid Id,
    string Titulo
);
