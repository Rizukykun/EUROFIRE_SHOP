using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Models
{
    public class CarrinhoViewModel : PadraoViewModel
    {
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public string Nome { get; set; }
        public string ImagemEmBase64 { get; set; }
        public double ValorTotalDoPedido { get; set; }
        public string Descricao { get; set; }
    }
}
