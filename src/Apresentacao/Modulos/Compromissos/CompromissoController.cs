using eAgenda.Aplicacao.Modulos.Compromissos;
using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAgenda.WebApp.Modulos.Compromissos;

public class CompromissoController(ServicoCompromisso servico, ServicoContato servicoContato) : Controller
{
    [HttpGet]
    public ActionResult Listar() => View(servico.SelecionarTodos());

    [HttpGet]
    public ActionResult Cadastrar()
    {
        PrepararContatos();
        return View(new CadastrarCompromissoViewModel("", DateOnly.FromDateTime(DateTime.Today), new TimeOnly(9, 0), new TimeOnly(10, 0), TipoCompromisso.Presencial, "", null));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarCompromissoViewModel vm)
    {
        Result<Guid> result = servico.Cadastrar(new(vm.Assunto, vm.DataOcorrencia, vm.HoraInicio, vm.HoraTermino, vm.Tipo, vm.LinkOuLocal, vm.ContatoId));
        if (result.IsFailed) { AdicionarErros(result); PrepararContatos(); return View(vm); }
        TempData["MensagemSucesso"] = "Compromisso cadastrado com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<CompromissoDto> result = servico.SelecionarPorId(id);
        if (result.IsFailed) { TempData["MensagemErro"] = result.Errors[0].Message; return RedirectToAction(nameof(Listar)); }
        CompromissoDto c = result.Value;
        PrepararContatos();
        return View(new EditarCompromissoViewModel(c.Id, c.Assunto, c.DataOcorrencia, c.HoraInicio, c.HoraTermino, c.Tipo, c.LinkOuLocal, c.ContatoId));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(EditarCompromissoViewModel vm)
    {
        Result result = servico.Editar(new(vm.Id, vm.Assunto, vm.DataOcorrencia, vm.HoraInicio, vm.HoraTermino, vm.Tipo, vm.LinkOuLocal, vm.ContatoId));
        if (result.IsFailed) { AdicionarErros(result); PrepararContatos(); return View(vm); }
        TempData["MensagemSucesso"] = "Compromisso atualizado com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<CompromissoDto> result = servico.SelecionarPorId(id);
        if (result.IsFailed) { TempData["MensagemErro"] = result.Errors[0].Message; return RedirectToAction(nameof(Listar)); }
        CompromissoDto c = result.Value;
        return View(new ExcluirCompromissoViewModel(c.Id, c.Assunto, c.DataOcorrencia, c.HoraInicio, c.HoraTermino));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Excluir(ExcluirCompromissoViewModel vm)
    {
        Result result = servico.Excluir(vm.Id);
        if (result.IsFailed) TempData["MensagemErro"] = result.Errors[0].Message;
        else TempData["MensagemSucesso"] = "Compromisso excluído com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    private void PrepararContatos()
    {
        ViewBag.Contatos = new SelectList(servicoContato.SelecionarTodos(), "Id", "Nome");
    }

    private void AdicionarErros(ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            string campo = erro.HasMetadataKey("Campo") ? erro.Metadata["Campo"]?.ToString() ?? string.Empty : string.Empty;
            if (string.IsNullOrEmpty(campo)) TempData["MensagemErro"] = erro.Message;
            else ModelState.AddModelError(campo, erro.Message);
        }
    }
}
