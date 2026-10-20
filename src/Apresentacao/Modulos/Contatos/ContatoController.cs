using eAgenda.Aplicacao.Modulos.Contatos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Contatos;

public class ContatoController(ServicoContato servico) : Controller
{
    [HttpGet]
    public ActionResult Listar() => View(servico.SelecionarTodos());

    [HttpGet]
    public ActionResult Cadastrar() => View(new CadastrarContatoViewModel("", "", "", "", ""));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarContatoViewModel vm)
    {
        Result<Guid> result = servico.Cadastrar(new(vm.Nome, vm.Email, vm.Telefone, vm.Cargo, vm.Empresa));
        if (result.IsFailed) { AdicionarErros(result); return View(vm); }
        TempData["MensagemSucesso"] = "Contato cadastrado com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ContatoDto> result = servico.SelecionarPorId(id);
        if (result.IsFailed) { TempData["MensagemErro"] = result.Errors[0].Message; return RedirectToAction(nameof(Listar)); }
        ContatoDto c = result.Value;
        return View(new EditarContatoViewModel(c.Id, c.Nome, c.Email, c.Telefone, c.Cargo, c.Empresa));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(EditarContatoViewModel vm)
    {
        Result result = servico.Editar(new(vm.Id, vm.Nome, vm.Email, vm.Telefone, vm.Cargo, vm.Empresa));
        if (result.IsFailed) { AdicionarErros(result); return View(vm); }
        TempData["MensagemSucesso"] = "Contato atualizado com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ContatoDto> result = servico.SelecionarPorId(id);
        if (result.IsFailed) { TempData["MensagemErro"] = result.Errors[0].Message; return RedirectToAction(nameof(Listar)); }
        ContatoDto c = result.Value;
        return View(new ExcluirContatoViewModel(c.Id, c.Nome, c.Email, c.Telefone, c.Cargo, c.Empresa));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Excluir(ExcluirContatoViewModel vm)
    {
        Result result = servico.Excluir(vm.Id);
        if (result.IsFailed) TempData["MensagemErro"] = result.Errors[0].Message;
        else TempData["MensagemSucesso"] = "Contato excluído com sucesso.";
        return RedirectToAction(nameof(Listar));
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
