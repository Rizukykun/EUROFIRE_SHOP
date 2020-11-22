using EUROFIRE_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class CategoriaDAO : PadraoDAO<CategoriaViewModel>
    {

        protected override SqlParameter[] CriaParametros(CategoriaViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("Nome", model.Nome);
            parametros[2] = new SqlParameter("Usuario", model.IdUsuarioLogado);

            return parametros;
        }

        protected override CategoriaViewModel MontaModel(DataRow registro)
        {
            CategoriaViewModel n = new CategoriaViewModel();
            n.Id = Convert.ToInt32(registro["Id"]);
            n.Nome = registro["Nome"].ToString();

            return n;
        }

        protected override void SetTabela()
        {
            Tabela = "Categoria";
        }
    }
}
