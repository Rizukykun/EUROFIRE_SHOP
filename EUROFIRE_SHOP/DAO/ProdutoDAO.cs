using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class ProdutoDAO : PadraoDAO<ProdutoViewModel>
    {
        protected override SqlParameter[] CriaParametros(ProdutoViewModel model)
        {
            object imgByte1 = model.ImagemEmByte1;
            if (imgByte1 == null)
                imgByte1 = new byte[0];

            object imgByte2 = model.ImagemEmByte2;
            if (imgByte2 == null)
                imgByte2 = new byte[0];           

            SqlParameter[] parametros = new SqlParameter[11];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("Nome", model.Nome);
            parametros[2] = new SqlParameter("Descricao", model.Descricao);
            parametros[3] = new SqlParameter("IdCategoria", model.IdCategoria);
            parametros[4] = new SqlParameter("Estoque", model.Estoque);
            parametros[5] = new SqlParameter("Preco", model.Preco);
            parametros[6] = new SqlParameter("imagem1", imgByte1);
            parametros[7] = new SqlParameter("imagem2", imgByte2);
            parametros[8] = new SqlParameter("IdFornecedor", model.IdFornecedor);
            parametros[9] = new SqlParameter("IdMarca", model.IdMarca);
            parametros[10] = new SqlParameter("Usuario", model.IdUsuarioLogado);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel n = new ProdutoViewModel();
            n.Id = Convert.ToInt32(registro["Id"]);
            n.Nome = registro["Nome"].ToString();
            n.Descricao = registro["Descricao"].ToString();
            n.IdCategoria = Convert.ToInt32(registro["idCategoria"]);
            n.Estoque = Convert.ToInt32(registro["Estoque"]);
            n.Preco = Convert.ToDouble(registro["Preco"]);
            if (registro.Table.Columns.Contains("imagem1"))
                if (registro["imagem1"] != DBNull.Value)
                    n.ImagemEmByte1 = registro["imagem1"] as byte[];
            if (registro.Table.Columns.Contains("imagem2"))
                if (registro["imagem2"] != DBNull.Value)
                    n.ImagemEmByte2 = registro["imagem2"] as byte[];
            n.IdFornecedor = Convert.ToInt32(registro["idFornecedor"]);
            n.IdMarca = Convert.ToInt32(registro["IdMarca"]);

            return n;
        }

        protected override void SetTabela()
        {
            Tabela = "Produto";
        }

        /*public override List<ProdutoViewModel> Listar()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("Ordem", "1")
            };

            var tabela = HelperDAO.ExecutaProcSelect("spListagem_Produto", p);
            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }*/
    }
}
