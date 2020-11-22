using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EUROFIRE_SHOP.Models;

namespace EUROFIRE_SHOP.DAO
{
    public class PedidoDAO : PadraoDAO<PedidoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("data", model.Data);
            parametros[2] = new SqlParameter("Usuario", model.IdUsuarioLogado);

            return parametros;
        }
        protected override PedidoViewModel MontaModel(DataRow registro)
        {
            PedidoViewModel c = new PedidoViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Data = Convert.ToDateTime(registro["data"])
            };
            return c;
        }
        
        protected override void SetTabela()
        {
            Tabela = "Pedido";
        }
    }
}
