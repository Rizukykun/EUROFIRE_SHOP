using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class HelperDAO
    {
        public static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    conexao.Close();
                    return tabela;
                }
            }
        }

        public static int ExecutaProc(string nomeProc, SqlParameter[] parametros, bool consultaUltimoIdentity = false)
        {
            using (SqlConnection conexao = ConexaoDB.GetConnection())
            {
                using (SqlCommand comando = new SqlCommand(nomeProc, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();

                    if (consultaUltimoIdentity)
                    {
                        string sql = "select isnull(@@IDENTITY,0)";
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = sql;
                        int pedidoId = Convert.ToInt32(comando.ExecuteScalar());
                        conexao.Close();
                        return pedidoId;
                    }
                    else
                        return 0;
                }
                //conexao.Close();
            }
        }

        public static bool ExecutaProcLogin(string usuario, string senha)
        {
            using (SqlConnection conexao = ConexaoDB.GetConnection())
            {
                using (SqlCommand comando = new SqlCommand("spConsulta_Usuario", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("Email", usuario));
                    comando.Parameters.Add(new SqlParameter("Senha", senha));

                    int retorno = Convert.ToInt32(comando.ExecuteScalar());
                    conexao.Close();
                    if (retorno == 0)
                        return false;
                    else if (retorno == 1)
                        return true;
                    else
                        throw new Exception("O valor de login estava duplicado no banco");
                }
            }
        }
    }
}
