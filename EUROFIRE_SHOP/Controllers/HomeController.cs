using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using EUROFIRE_SHOP.Enuns;
using Microsoft.AspNetCore.Http;
using EUROFIRE_SHOP.DAO;

namespace EUROFIRE_SHOP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var lista = dao.Listar();
            
            return View(lista);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HelperController.VerificaUserLogado(HttpContext.Session))
            {
                ViewBag.Logado = true;
                ViewBag.TipoUsuario = (EnumTipoUsuario)Convert.ToInt32(HttpContext.Session.GetString("TipoUsuario"));
                base.OnActionExecuting(context);
            }
            else
            {
                ViewBag.Logado = false;
                base.OnActionExecuting(context);
            }
        }


        /*public IActionResult Buscar(string filtroBusca)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var lista = dao.Listar();
            lista = lista.FindAll(p => p.Descricao.Contains(filtroBusca));
            ViewBag.FiltroBusca = filtroBusca;
            return View("index", lista);
        }*/

        public IActionResult buscar(string filtroBusca)
        {
            ProdutoDAO PDAO = new ProdutoDAO();
            ViewBag.FiltroBusca = filtroBusca;
            var lista = PDAO.ListarBusca(filtroBusca, 1);
            return View("index", lista);
        }

    }
}
