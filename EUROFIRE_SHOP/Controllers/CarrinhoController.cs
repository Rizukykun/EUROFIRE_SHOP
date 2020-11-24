using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult Index()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var listaDeProdutos = dao.Listar();

            var carrinho = ObtemCarrinhoNaSession();
            @ViewBag.TotalCarrinho = carrinho.Sum(c => c.Quantidade);

            return View(listaDeProdutos);
        }

        private List<CarrinhoViewModel> ObtemCarrinhoNaSession()
        {
            List<CarrinhoViewModel> carrinho = new List<CarrinhoViewModel>();
            string carrinhoJson = HttpContext.Session.GetString("carrinho");
            if (carrinhoJson != null)
                carrinho = JsonConvert.DeserializeObject<List<CarrinhoViewModel>>(carrinhoJson);
            return carrinho;
        }

        public IActionResult AdicionarCarrinho(int id, int Quantidade)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();
            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.Id == id);
            if (carrinhoModel != null && Quantidade == 0)
            {
                //tira do carrinho
                carrinho.Remove(carrinhoModel);
            }
            else if (carrinhoModel == null && Quantidade > 0)
            {
                //não havia no carrinho, vamos adicionar
                ProdutoDAO prodDao = new ProdutoDAO();
                var modelCidade = prodDao.Consulta(id);
                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.Id = id;
                carrinhoModel.Nome = modelCidade.Nome;
                carrinho.Add(carrinhoModel);
            }
            if (carrinhoModel != null)
                carrinhoModel.Quantidade = Quantidade;
            string carrinhoJson = JsonConvert.SerializeObject(carrinho);
            HttpContext.Session.SetString("carrinho", carrinhoJson);
            return RedirectToAction("Index");
        }

        public IActionResult Visualizar()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var carrinho = ObtemCarrinhoNaSession();
            foreach (var item in carrinho)
            {
                var produ = dao.Consulta(item.Id);
                item.ImagemEmBase64 = produ.ImagemEm64_1;
            }
            return View(carrinho);
        }

        public IActionResult Detalhes(int idProduto)
        {
            List<CarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();
            ProdutoDAO cidDao = new ProdutoDAO();
            var modelProdutos = cidDao.Consulta(idProduto);
            CarrinhoViewModel carrinhoModel = carrinho.Find(c => c.Id == idProduto);
            if (carrinhoModel == null)
            {
                carrinhoModel = new CarrinhoViewModel();
                carrinhoModel.Id = idProduto;
                carrinhoModel.Nome = modelProdutos.Nome;
                carrinhoModel.Preco = Convert.ToDouble(modelProdutos.Preco);
                carrinhoModel.Descricao = modelProdutos.Descricao;
                carrinhoModel.Quantidade = 0;
            }

            double soma = 0;
            foreach (var item in carrinho)            
                soma += item.Quantidade * item.Preco;

            ViewBag.TotalDoPedido = soma;   //mostrar na tela o viewba        

            // preenche a imagem
            carrinhoModel.ImagemEmBase64 = modelProdutos.ImagemEm64_1;
            return View(carrinhoModel);
        }
                      

    }

}
