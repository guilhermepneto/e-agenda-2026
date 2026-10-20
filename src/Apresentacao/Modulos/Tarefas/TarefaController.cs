using eAgenda.Aplicacao.Modulos.Tarefas;
using eAgenda.Dominio.Modulos.Tarefas;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Tarefas;

public class TarefaController(ServicoTarefa servicoTarefa) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<TarefaDto> dtos = servicoTarefa.SelecionarTodas();

        return View(dtos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTarefaViewModel viewModel = new(
            string.Empty,
            PrioridadeTarefa.Normal
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTarefaViewModel viewModel)
    {
        CadastrarTarefaDto dto = new(
            viewModel.Titulo,
            viewModel.Prioridade
        );

        Result<Guid> resultado = servicoTarefa.Cadastrar(dto);

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
        Result<TarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        TarefaDto dto = resultado.Value;

        EditarTarefaViewModel viewModel = new(
            id,
            dto.Titulo,
            dto.Prioridade
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarTarefaViewModel viewModel)
    {
        EditarTarefaDto dto = new(
            viewModel.Id,
            viewModel.Titulo,
            viewModel.Prioridade
        );

        Result resultado = servicoTarefa.Editar(dto);

        if (resultado.IsFailed)
        {
            if (!resultado.Errors.Any(e => e.HasMetadataKey("Campo")))
            {
                TempData["MensagemErro"] = resultado.Errors.First().Message;

                return RedirectToAction(nameof(Listar));
            }

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
        Result<TarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        TarefaDto dto = resultado.Value;

        ExcluirTarefaViewModel viewModel = new(
            id,
            dto.Titulo,
            dto.Prioridade,
            dto.DataCriacao
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTarefaViewModel viewModel)
    {
        Result resultado = servicoTarefa.Excluir(viewModel.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult GerenciarItens(Guid tarefaId)
    {
        Result<TarefaDto> resultado = servicoTarefa.SelecionarPorId(tarefaId);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        TarefaDto dto = resultado.Value;

        GerenciarItensTarefaViewModel viewModel = new(
            dto.Id,
            dto.Titulo,
            dto.Prioridade,
            dto.DataCriacao,
            dto.DataConclusao,
            dto.PercentualConcluido,
            dto.Concluida,
            dto.Itens.Select(i => new ItemTarefaViewModel(
                i.Id,
                i.Titulo,
                i.Concluido
            )).ToList()
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult AdicionarItem(AdicionarItemTarefaViewModel viewModel)
    {
        AdicionarItemTarefaDto dto = new(viewModel.TarefaId, viewModel.Titulo);

        Result resultado = servicoTarefa.AdicionarItem(dto);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(GerenciarItens), new { tarefaId = viewModel.TarefaId });
    }

    [HttpPost]
    public ActionResult AlterarConclusaoItem(Guid tarefaId, Guid itemId, bool concluido)
    {
        AlterarConclusaoItemTarefaDto dto = new(
            tarefaId,
            itemId,
            concluido
        );

        Result resultado = servicoTarefa.AlterarConclusaoItem(dto);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(GerenciarItens), new { tarefaId });
    }

    [HttpPost]
    public ActionResult RemoverItem(Guid tarefaId, Guid itemId)
    {
        RemoverItemTarefaDto dto = new(tarefaId, itemId);

        Result resultado = servicoTarefa.RemoverItem(dto);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(GerenciarItens), new { tarefaId });
    }
}
