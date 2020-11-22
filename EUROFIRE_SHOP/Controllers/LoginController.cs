using EUROFIRE_SHOP.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FazLogin(string usuario, string senha)
        {
            if (HelperDAO.ExecutaProcLogin(usuario, senha))
            {
                UsuarioDAO userDAO = new UsuarioDAO();
                var userviewModel = userDAO.ConsultaEmail(usuario);

                HttpContext.Session.SetString("Logado", "true");
                HttpContext.Session.SetString("IdUsuario", userviewModel.Id.ToString());
                HttpContext.Session.SetString("NomeUsuario", userviewModel.Nome);
                HttpContext.Session.SetString("EmailUsuario", userviewModel.Email);
                HttpContext.Session.SetString("TipoUsuario", (((int)userviewModel.Tipo)).ToString());

                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Index");
            }
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            //return RedirectToAction("Index");
            return RedirectToAction("index", "Home");
        }


    }
}
