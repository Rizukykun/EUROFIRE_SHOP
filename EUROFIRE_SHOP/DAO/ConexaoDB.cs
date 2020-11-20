using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class ConexaoDB
    {
        public static SqlConnection GetConnection()
        {
            string strCon = "Data Source=LOCALHOST;Initial Catalog=EUROFIRESHOP; integrated security=true";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
