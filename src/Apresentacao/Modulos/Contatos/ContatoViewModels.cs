using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Contatos;

[ValidateNever]
public record CadastrarContatoViewModel(string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
[ValidateNever]
public record EditarContatoViewModel(Guid Id, string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
[ValidateNever]
public record ExcluirContatoViewModel(Guid Id, string Nome, string Email, string Telefone, string? Cargo, string? Empresa);
