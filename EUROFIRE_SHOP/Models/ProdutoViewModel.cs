using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int IdCategoria { get; set; }
        public int Estoque { get; set; }
        public decimal Preco { get; set; }
        public IFormFile Imagem1 { get; set; }
        public IFormFile Imagem2 { get; set; }

        public byte[] ImagemEmByte1 { get; set; }
        public byte[] ImagemEmByte2 { get; set; }

        public string ImagemEm64_1
        {
            get
            {
                if (ImagemEmByte1 != null)
                    return Convert.ToBase64String(ImagemEmByte1);
                else
                    return "";
            }
        }

        public string ImagemEm64_2
        {
            get
            {
                if (ImagemEmByte2 != null)
                    return Convert.ToBase64String(ImagemEmByte2);
                else
                    return "";
            }
        }
    }
}
