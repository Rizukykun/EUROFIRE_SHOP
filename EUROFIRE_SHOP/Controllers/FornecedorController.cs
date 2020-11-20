using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class FornecedorController : PadraoController<FornecedorViewModel>
    {
        public FornecedorController()
        {
            DAO = new FornecedorDAO();
        }

        protected override void Validacao(FornecedorViewModel model, string operacao)
        {
            if (string.IsNullOrEmpty(model.Nome) && ((operacao == "I") || operacao == "A"))
                ModelState.AddModelError("Nome", "O nome do fornecedor é obrigatório!");
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