using eAgenda.Aplicacao.Modulos.Categorias;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Categorias;

public sealed class CategoriaController(ServicoCategoria servicoCategoria) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<CategoriaDto> categorias = servicoCategoria.SelecionarTodos();

        return View(categorias);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel viewModel = new(string.Empty);

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel viewModel)
    {
        CadastrarCategoriaDto dto = new(viewModel.Titulo ?? string.Empty);

        Result<Guid> resultado = servicoCategoria.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<CategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        CategoriaDto dto = resultado.Value;

        EditarCategoriaViewModel viewModel = new(
            dto.Id,
            dto.Titulo
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel viewModel)
    {
        EditarCategoriaDto dto = new(
            viewModel.Id,
            viewModel.Titulo ?? string.Empty
        );

        Result resultado = servicoCategoria.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<CategoriaDto> resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        CategoriaDto dto = resultado.Value;

        ExcluirCategoriaViewModel viewModel = new(dto.Id, dto.Titulo);

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCategoriaViewModel viewModel)
    {
        servicoCategoria.Excluir(viewModel.Id);

        return RedirectToAction(nameof(Listar));
    }
}
