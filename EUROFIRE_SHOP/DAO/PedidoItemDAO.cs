using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class PedidoItemDAO : PadraoDAO<PedidoItemViewModel>
    {

        protected override SqlParameter[] CriaParametros(PedidoItemViewModel model)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("PedidoId", model.PedidoId),
                new SqlParameter("CidadeId", model.CidadeId),
                new SqlParameter("Quantidade", model.Quantidade)
            };
            return parametros;
        }

        protected override PedidoItemViewModel MontaModel(DataRow registro)
        {
            PedidoItemViewModel c = new PedidoItemViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                CidadeId = Convert.ToInt32(registro["Cidadeid"]),
                PedidoId = Convert.ToInt32(registro["PedidoId"]),
                Quantidade = Convert.ToInt32(registro["id"]),
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "ItemPedido";
        }
    }
}
