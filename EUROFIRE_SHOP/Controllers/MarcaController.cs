using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class MarcaController : PadraoController<MarcaViewModel>
    {
        public MarcaController()
        {
            DAO = new MarcaDAO();
        }

        protected override void Validacao(MarcaViewModel model, string operacao)
        {
            if (string.IsNullOrEmpty(model.Nome) && ((operacao == "I") || operacao == "A"))
                ModelState.AddModelError("Nome", "O nome da marca é obrigatória!");
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