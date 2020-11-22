using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Enuns;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public abstract class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected string NomePaginaRetorno { get; set; } = "index";
        protected string NomeControllerRetorno { get; set; } = "";

        public PadraoController()
        {
            NomeControllerRetorno = this.GetType().Name.Substring(0, this.GetType().Name.Length - "controller".Length);
        }

        public IActionResult Index()
        {
            var lista = DAO.Listar();
            return View(lista);
        }

        public IActionResult Criar()
        {
            ViewBag.Operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T;

            PreencheDadosParaView("I", model);
            return View("Form", model);
        }

        public IActionResult Alterar(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                if (model == null)
                {

                    return RedirectToAction(NomePaginaRetorno, NomeControllerRetorno);
                }
                else
                {
                    PreencheDadosParaView("A", model);
                    return View("Form", model);
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction(NomePaginaRetorno, NomeControllerRetorno);
            }
        }

        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {

        }

        public IActionResult Excluir(int id)
        {
            try
            {
                DAO.Excluir(id);
                return RedirectToAction(NomePaginaRetorno, NomeControllerRetorno);
            }
            catch
            {
                return RedirectToAction(NomePaginaRetorno, NomeControllerRetorno);
            }
        }

        public IActionResult Salvar(T model, string Operacao)
        {
            try
            {
                Validacao(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View("Form", model);
                }
                else
                {

                    model.IdUsuarioLogado = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
                    
                    if (Operacao == "I")
                        DAO.Inserir(model);
                    else
                        DAO.Alterar(model);
                    return RedirectToAction(NomePaginaRetorno, NomeControllerRetorno);
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                ViewBag.Erro = erro.Message;
                PreencheDadosParaView(Operacao, model);
                return View("Form", model);
            }
        }

        protected virtual void Validacao(T model, string operacao)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (this.GetType().Name.ToUpper().Contains("USUARIOCONTROLLER"))
            {
                ViewBag.Logado = true;
                ViewBag.TipoUsuario = (EnumTipoUsuario)Convert.ToInt32(HttpContext.Session.GetString("TipoUsuario"));
                base.OnActionExecuting(context);
            }
            else
            {
                if (!HelperController.VerificaUserLogado(HttpContext.Session))
                    context.Result = RedirectToAction("Index", "Login");
                else
                {
                    ViewBag.Logado = true;
                    base.OnActionExecuting(context);
                }
            }
        }
    }
}
