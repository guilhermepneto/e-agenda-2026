using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Categorias;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Categorias;

public sealed class ServicoCategoria(IRepositorioCategoria repositorioCategoria)
{
    public Result<Guid> Cadastrar(CadastrarCategoriaDto dto)
    {
        Categoria categoria = new(dto.Titulo);

        List<ErroValidacao> erros = categoria.Validar();

        if (repositorioCategoria.ExisteCategoriaPorTitulo(categoria.Titulo))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.Titulo),
                Mensagem = "Já existe uma categoria cadastrada com o título informado"
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCategoria.Cadastrar(categoria);

        return Result.Ok(categoria.Id);
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        Categoria categoriaAtualizada = new(dto.Titulo);

        List<ErroValidacao> erros = categoriaAtualizada.Validar();

        if (repositorioCategoria.ExisteCategoriaPorTitulo(categoriaAtualizada.Titulo, dto.Id))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(categoriaAtualizada.Titulo),
                Mensagem = "Já existe uma categoria cadastrada com o título informado"
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioCategoria.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível encontrar a categoria informada.");

        return Result.Ok();
    }

    public Result<CategoriaDto> SelecionarPorId(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new CategoriaDto(categoria.Id, categoria.Titulo));
    }

    public List<CategoriaDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias
            .Select(c => new CategoriaDto(c.Id, c.Titulo))
            .ToList();
    }
}
