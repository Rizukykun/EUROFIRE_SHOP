using EUROFIRE_SHOP.Enuns;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel model)
        {

            SqlParameter[] parametros = new SqlParameter[13];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("Nome", model.Nome);
            parametros[2] = new SqlParameter("Email", model.Email);
            parametros[3] = new SqlParameter("Senha", model.Senha);
            parametros[4] = new SqlParameter("tipoUsuario", model.Tipo);
            parametros[5] = new SqlParameter("Cpf", model.Cpf);
            parametros[6] = new SqlParameter("Sexo", model.Sexo);
            parametros[7] = new SqlParameter("DataDeNascimento", model.DataDeNascimento);
            parametros[8] = new SqlParameter("Telefone", model.Telefone);
            parametros[9] = new SqlParameter("Cep", model.Cep);
            parametros[10] = new SqlParameter("Numero", model.Numero);
            parametros[11] = new SqlParameter("Complemento", model.Complemento);
            parametros[12] = new SqlParameter("Usuario", model.IdUsuarioLogado);

            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel f = new UsuarioViewModel();
            f.Id = Convert.ToInt32(registro["Id"]);
            f.Nome = registro["Nome"].ToString();
            f.Email = registro["Email"].ToString();
            f.Senha = registro["Senha"].ToString().Trim();
            if (registro.Table.Columns.Contains("tipoUsuario"))
                f.Tipo = (EnumTipoUsuario)registro["tipoUsuario"];
            f.Cpf = registro["Cpf"].ToString();
            if (registro.Table.Columns.Contains("Sexo"))
                f.Sexo = (EnumSexo)registro["Sexo"];
            f.DataDeNascimento = Convert.ToDateTime(registro["DataDeNascimento"]);
            f.Telefone = registro["Telefone"].ToString();
            f.Cep = registro["Cep"].ToString();
            f.Numero = Convert.ToInt32(registro["Numero"]);
            if (registro.Table.Columns.Contains("Complemento"))
                f.Complemento = registro["Complemento"].ToString();


            //if (registro.Table.Columns.Contains("Descricao"))
            //f.Descricao = registro["Descricao"].ToString();

            return f;
        }

        protected override void SetTabela()
        {
            Tabela = "Usuario";
        }

        public UsuarioViewModel ConsultaEmail(string email)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("email", email.Trim()),
                new SqlParameter("tabela", Tabela)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spConsultaEmail", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
    }
}
