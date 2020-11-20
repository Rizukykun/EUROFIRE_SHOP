using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class CategoriaController : PadraoController<CategoriaViewModel>
    {
        public CategoriaController()
        {
            DAO = new CategoriaDAO();
        }

        protected override void Validacao(CategoriaViewModel model, string operacao)
        {
            if (string.IsNullOrEmpty(model.Nome) && ((operacao =="I") || operacao == "A") )
                ModelState.AddModelError("Nome", "O nome da categoria é obrigatória!");
        }

        /*
        protected override void SetDefaults(DisciplinaViewModel model)
        {
            model.Ativo = true;
            base.SetDefaults(model);
        }

        */
    }
}
