using EUROFIRE_SHOP.DAO;
using EUROFIRE_SHOP.Enuns;
using EUROFIRE_SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EUROFIRE_SHOP.Controllers
{
    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        public UsuarioController()
        {
            DAO = new UsuarioDAO();
            NomeControllerRetorno = "Home";
        }

        protected override void Validacao(UsuarioViewModel model, string operacao)
        {      
            var dao = new UsuarioDAO();

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "O nome é obrigatório!");

            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "O e-mail é obrigatório!");
            else
            {
                if (!Regex.Match(model.Email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").Success)
                    ModelState.AddModelError("Email", "O e-mail é inválido!");
                else
                    if (operacao == "I" && dao.ConsultaEmail(model.Email) != null)
                    ModelState.AddModelError("Email", "Este E-mail já está em uso!");
            }

            if (string.IsNullOrEmpty(model.Senha))
                ModelState.AddModelError("Senha", "A senha é obrigatória!");
            else
            {
                if (model.Senha.Length <= 5)
                    ModelState.AddModelError("Senha", "A senha deve conter ao menos 6 caracteres!");
            }
            
            if (model.Sexo == 0)
                ModelState.AddModelError("Sexo", "A escolha do Sexo é obrigatória!");

            if ((string.IsNullOrEmpty(Convert.ToString(model.DataDeNascimento))) || (Convert.ToString(model.DataDeNascimento) == Convert.ToString(DateTime.Now)))
                ModelState.AddModelError("DataDeNascimento", "Preenchimento obrigatório com a data de Nascimento!");

            if (string.IsNullOrEmpty(model.Telefone) || (model.Telefone.Length < 11) || (model.Telefone.Length <= 0))
                ModelState.AddModelError("Telefone", "O Telefone é obrigatório!");

            if ((string.IsNullOrEmpty(model.Cep)) || (model.Cep.Length < 8))
                ModelState.AddModelError("Cep", "O Cep é obrigatório!");

            if (model.Numero <= 0)
                ModelState.AddModelError("Numero", "O Numero é obrigatório!");

            ValidaCPF(model.Cpf);

            ModelState.Remove("Complemento");

        }


        private void PreparaListaUsuariosParaCombo()
        {
            List<SelectListItem> listaUsuarios = new List<SelectListItem>();
            listaUsuarios.Add(new SelectListItem("Administrador", "1"));
            listaUsuarios.Add(new SelectListItem("Cliente", "2"));

            ViewBag.Usuarios = listaUsuarios;
        }

        public IActionResult CriarUsuario(int tipoUsuario)
        {
            ViewBag.TipoUsuario = (EnumTipoUsuario)tipoUsuario;
            return base.Criar();
        }

        protected override void PreencheDadosParaView(string Operacao, UsuarioViewModel model)
        {
            if (Operacao == "I")
            {
                model.Tipo = ViewBag.TipoUsuario;
                model.DataDeNascimento = DateTime.Now;
            }

            base.PreencheDadosParaView(Operacao, model);
        }

        /*
        public IActionResult SalvarUsuario(UsuarioViewModel model, string Operacao, string tipoUsuario)
        {
            ViewBag.TipoUsuario = tipoUsuario;
            return base.Salvar(model, Operacao);
        }
        */

        public void ValidaCPF(string ValidarCpf)
        {
            string CPF;
            CPF = ValidarCpf;
            CPF = CPF.Replace(".", "").Replace("-", "");

            if (CPF.Length != 11)            {
                ModelState.AddModelError("Cpf", "O CPF, sem ponto e traço não tem 11 dígitos.");
                return; // sai do programa
            }

            int soma = 0;
            int num = 10;

            for (int pos = 0; pos <= 8; pos++)
            {
                soma += Convert.ToInt32(CPF[pos].ToString()) * num;
                num--;
            }

            int resultado = soma % 11;
            int digito1;

            if (resultado == 0 || resultado == 1)
                digito1 = 0;
            else
                digito1 = 11 - resultado;

            soma = 0;
            num = 11;

            for (int pos = 0; pos <= 8; pos++)
                soma += Convert.ToInt32(CPF[pos].ToString()) * num--;

            soma += digito1 * 2;

            resultado = soma % 11;
            int digito2;

            if (resultado == 0 || resultado == 1)
                digito2 = 0;
            else
                digito2 = 11 - resultado;

            if (CPF[9].ToString() != digito1.ToString() &&
                CPF[10].ToString() != digito2.ToString())
                ModelState.AddModelError("Cpf", $"CPF incorreto. Deu: {digito1}{digito2}"); 
        }





        /*
         * 
         * 
         public IActionResult gridUsers()
        {
            var lista = DAO.Listagem();
            return View(lista);
        }


         [HttpPost]
        public JsonResult AlteraTipoUsuario(int idUsuario)
        {
            try
            {
                UsuarioDAO da = new UsuarioDAO();
                var userviewModel = da.Consulta(idUsuario);

                if (userviewModel.Tipo == Enuns.EnumTipoUsuario.Administador)
                {
                    //testar se tem outro adm, senão não pode mudar para colaborador!
                    string sql = "select id from usuarios where tipo = " + (int)Enuns.EnumTipoUsuario.Administador +
                                 " and ativo = 1 and id <> " + idUsuario;
                    DataTable resultado = HelperDAO.ExecutaSelect(sql, null);
                    if (resultado.Rows.Count == 0) // não há outros administadores
                    {
                        return Json(new { sucesso = false });
                    }
                    else
                    {
                        userviewModel.Tipo = Enuns.EnumTipoUsuario.Colaborador;
                        da.Update(userviewModel);
                        return Json(new { sucesso = true });
                    }
                }
                else
                {
                    userviewModel.Tipo = Enuns.EnumTipoUsuario.Administador;
                    da.Update(userviewModel);
                    return Json(new { sucesso = true });
                }
            }
            catch (Exception erro)
            {
                GeraLogErro(erro, idUsuario);
                return Json(new { sucesso = false });
            }
        }
         * 
         */
    }
}
