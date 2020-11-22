using EUROFIRE_SHOP.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Models
{
    public class PadraoViewModel
    {
        public int Id { get; set; }
        public int IdUsuarioLogado { get; set; }
        public EnumTipoUsuario Tipo { get; set; }
    }
}
