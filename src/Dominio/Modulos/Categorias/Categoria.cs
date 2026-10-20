using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Despesas;

namespace eAgenda.Dominio.Modulos.Categorias;

public sealed class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; } = string.Empty;
    public List<Despesa> Despesas { get; set; } = [];

    public Categoria() { }

    public Categoria(string titulo) : this()
    {
        Titulo = titulo;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Titulo),
                Mensagem = "O campo \"Título\" deve ser preenchido"
            });
        }

        if (Titulo?.Length < 2 || Titulo?.Length > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Titulo),
                Mensagem = "O campo \"Título\" deve conter entre 2 e 100 caracteres"
            });
        }

        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
    }
}
