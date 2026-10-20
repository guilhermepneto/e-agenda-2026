using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;

namespace eAgenda.Dominio.Modulos.Compromissos;

public sealed class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; } = string.Empty;
    public DateOnly DataOcorrencia { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string LinkOuLocal { get; set; } = string.Empty;
    public Guid? ContatoId { get; set; }
    public Contato? Contato { get; set; }

    public Compromisso() { }

    public Compromisso(string assunto, DateOnly dataOcorrencia, TimeOnly horaInicio,
        TimeOnly horaTermino, TipoCompromisso tipo, string linkOuLocal, Guid? contatoId = null)
    {
        Assunto = assunto?.Trim() ?? string.Empty;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        Tipo = tipo;
        LinkOuLocal = linkOuLocal?.Trim() ?? string.Empty;
        ContatoId = contatoId;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];
        if (string.IsNullOrWhiteSpace(Assunto) || Assunto.Trim().Length < 2 || Assunto.Trim().Length > 100)
            erros.Add(new() { Campo = nameof(Assunto), Mensagem = "O assunto deve conter entre 2 e 100 caracteres." });

        if (DataOcorrencia == default)
            erros.Add(new() { Campo = nameof(DataOcorrencia), Mensagem = "Informe a data do compromisso." });

        if (HoraTermino <= HoraInicio)
            erros.Add(new() { Campo = nameof(HoraTermino), Mensagem = "A hora de término deve ser posterior à hora de início." });

        if (!Enum.IsDefined(Tipo))
            erros.Add(new() { Campo = nameof(Tipo), Mensagem = "Selecione Remoto ou Presencial." });

        if (string.IsNullOrWhiteSpace(LinkOuLocal))
            erros.Add(new() { Campo = nameof(LinkOuLocal), Mensagem = "Informe o link da reunião ou o local do compromisso." });
        else if (Tipo == TipoCompromisso.Remoto &&
                 (!Uri.TryCreate(LinkOuLocal, UriKind.Absolute, out Uri? uri) ||
                  (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)))
            erros.Add(new() { Campo = nameof(LinkOuLocal), Mensagem = "Para compromissos remotos, informe um link válido iniciado por http:// ou https://." });

        return erros;
    }

    public override void Atualizar(Compromisso atualizado)
    {
        Assunto = atualizado.Assunto;
        DataOcorrencia = atualizado.DataOcorrencia;
        HoraInicio = atualizado.HoraInicio;
        HoraTermino = atualizado.HoraTermino;
        Tipo = atualizado.Tipo;
        LinkOuLocal = atualizado.LinkOuLocal;
        ContatoId = atualizado.ContatoId;
    }
}
