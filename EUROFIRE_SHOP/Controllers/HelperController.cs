using EUROFIRE_SHOP.Enuns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class HelperController : Controller
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

        public static void SetUserLogado(int usuarioId, EnumTipoUsuario tipoUsuario, ISession sessao)
        {
            sessao.SetString("Logado", usuarioId.ToString());
            sessao.SetInt32("TipoUserLogado", (int)tipoUsuario);
        }

        public static EnumTipoUsuario GetTipoUsuarioLogado(ISession sessao)
        {
            int? tipouserLogado = sessao.GetInt32("TipoUserLogado");
            if (tipouserLogado == null)
                return EnumTipoUsuario.Cliente;
            else
                return (EnumTipoUsuario)(int)tipouserLogado;
        }

    }
}
