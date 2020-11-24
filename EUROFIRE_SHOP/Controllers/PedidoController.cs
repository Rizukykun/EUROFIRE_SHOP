using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class PedidoController  : PadraoController<PedidoViewModel>
    {
        public IActionResult EfetuarPedido()
        {//VERIFICAR TODA ESSA JOÇA
            
            using (var transacao = new System.Transactions.TransactionScope())
            {

                PedidoViewModel pedido = new PedidoViewModel();
                pedido.Data = DateTime.Now;
                PedidoDAO pedidoDAO = new PedidoDAO();
                int idPedido = pedidoDAO.Inserir(pedido);
                PedidoItemDAO itemDAO = new PedidoItemDAO();
                var carrinho = ViewBag.CarrinhoNaSession;
                foreach (var elemento in carrinho)
                {
                    PedidoItemViewModel item = new PedidoItemViewModel();
                    item.PedidoId = idPedido;
                    item.CidadeId = elemento.Id;
                    item.Quantidade = elemento.Quantidade;
                    itemDAO.Inserir(item);
                }
                transacao.Complete();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
