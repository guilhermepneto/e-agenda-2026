namespace eAgenda.Aplicacao.Modulos.Contatos;

public record ContatoDto(Guid Id, string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
public record CadastrarContatoDto(string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
public record EditarContatoDto(Guid Id, string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
