using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Models
{
    public class PedidoItemViewModel : PadraoViewModel
    {
        public int PedidoId { get; set; }
        public int CidadeId { get; set; }
        public int Quantidade { get; set; }        
    }
}
