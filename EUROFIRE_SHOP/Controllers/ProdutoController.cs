using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class ProdutoController : PadraoController<ProdutoViewModel>
    {
        public ProdutoController()
        {
            DAO = new ProdutoDAO();
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        protected override void Validacao(ProdutoViewModel model, string operacao)
        {
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "O nome do produto é obrigatório!");
            //if (string.IsNullOrEmpty(model.Descricao))
                //ModelState.AddModelError("Descricao", "A descrição do produto é obrigatória!");
            if (model.IdCategoria <= 0)
                ModelState.AddModelError("IdCategoria", "Categoria inexistente!");
            if (model.Estoque < 0)
                ModelState.AddModelError("Estoque", "A quantidade em estoque é obrigatória!");
            if (model.Preco < 0)
                ModelState.AddModelError("Preco", "O Preco é obrigatório!");
            if (model.Imagem1 == null && operacao == "I")
                ModelState.AddModelError("Imagem1", "Escolha uma imagem!");
            if (model.IdFornecedor <= 0)
                ModelState.AddModelError("IdFornecedor", "Fornecedor inexistente!");
            if (model.IdMarca <= 0)
                ModelState.AddModelError("IdMarca", "Marca inexistente!");
            if (model.IdFornecedor <= 0)
                ModelState.AddModelError("IdFornecedor", "Fornecedor inexistente!");
            if (model.IdMarca <= 0)
                ModelState.AddModelError("IdMarca", "Marca inexistente!");

            ModelState.Remove("Descricao");

            //Quando algo pode aceitar nulo:
            // ModelState.Remove("DataCriacao");

            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem1 == null)
                {
                    ProdutoViewModel p = DAO.Consulta(model.Id);
                    model.ImagemEmByte1 = p.ImagemEmByte1;
                    model.ImagemEmByte2 = p.ImagemEmByte2;
                }
                else
                {
                    model.ImagemEmByte1 = ConvertImageToByte(model.Imagem1);
                    model.ImagemEmByte2 = ConvertImageToByte(model.Imagem2);
                }
            }
        }

        protected override void PreencheDadosParaView(string Operacao, ProdutoViewModel model)
        {
            PrepListaCategoriaParaCombo();
            PrepListaFornecedorParaCombo();
            PrepListaMarcaParaCombo();
        }

        private void PrepListaCategoriaParaCombo()
        {
            CategoriaDAO cat = new CategoriaDAO();
            var categorias = cat.Listar();
            List<SelectListItem> listaCategorias = new List<SelectListItem>();
            listaCategorias.Add(new SelectListItem("Selecione uma categoria...", "0"));
            foreach (var cate in categorias)
            {
                SelectListItem item = new SelectListItem(cate.Nome, cate.Id.ToString());
                listaCategorias.Add(item);
            }
            ViewBag.Categorias = listaCategorias;
        }

        private void PrepListaFornecedorParaCombo()
        {
            FornecedorDAO forn = new FornecedorDAO();
            var fornecedores = forn.Listar();
            List<SelectListItem> listaFornecedores = new List<SelectListItem>();
            listaFornecedores.Add(new SelectListItem("Selecione um fornecedor...", "0"));
            foreach (var forne in fornecedores)
            {
                SelectListItem item = new SelectListItem(forne.Nome, forne.Id.ToString());
                listaFornecedores.Add(item);
            }
            ViewBag.Fornecedor = listaFornecedores;
        }

        private void PrepListaMarcaParaCombo()
        {
            MarcaDAO mar = new MarcaDAO();
            var marcas = mar.Listar();
            List<SelectListItem> listaMarcas = new List<SelectListItem>();
            listaMarcas.Add(new SelectListItem("Selecione uma marca...", "0"));
            foreach (var marc in marcas)
            {
                SelectListItem item = new SelectListItem(marc.Nome, marc.Id.ToString());
                listaMarcas.Add(item);
            }
            ViewBag.Marca = listaMarcas;
        }        
    }
}

