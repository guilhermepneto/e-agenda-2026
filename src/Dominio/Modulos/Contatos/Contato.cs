using System.Text.RegularExpressions;
using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public sealed class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? Empresa { get; set; }

    public Contato() { }

    public Contato(string nome, string email, string telefone, string? cargo = null, string? empresa = null)
    {
        Nome = nome?.Trim() ?? string.Empty;
        Email = email?.Trim() ?? string.Empty;
        Telefone = telefone?.Trim() ?? string.Empty;
        Cargo = string.IsNullOrWhiteSpace(cargo) ? null : cargo.Trim();
        Empresa = string.IsNullOrWhiteSpace(empresa) ? null : empresa.Trim();
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];
        if (string.IsNullOrWhiteSpace(Nome) || Nome.Trim().Length < 2 || Nome.Trim().Length > 100)
            erros.Add(new() { Campo = nameof(Nome), Mensagem = "O nome deve conter entre 2 e 100 caracteres." });

        if (string.IsNullOrWhiteSpace(Email) || Email.Length > 254 ||
            !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            erros.Add(new() { Campo = nameof(Email), Mensagem = "Informe um e-mail válido." });

        if (string.IsNullOrWhiteSpace(Telefone) ||
            !Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add(new() { Campo = nameof(Telefone), Mensagem = "O telefone deve seguir o formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX." });

        return erros;
    }

    public override void Atualizar(Contato atualizado)
    {
        Nome = atualizado.Nome;
        Email = atualizado.Email;
        Telefone = atualizado.Telefone;
        Cargo = atualizado.Cargo;
        Empresa = atualizado.Empresa;
    }
}
