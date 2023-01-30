using AutoForms.BLL;
using AutoForms.DAL;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Controllers
{
    [ValidarSessao]
    [VerificarAdmin]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region VerificaSessão

        private Usuario UsuarioLogado
        {
            get
            {
                return JsonConvert
                    .DeserializeObject<Usuario>(HttpContext.Session.GetString("Usuario"));
            }
        }

        #endregion


        #region Action Usuario

        public IActionResult CadastroUsuario()
        {
            var model = UsuarioDAO.RetornarUsuario();
            return View(model);
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                if (usuario == null)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
                }

                if (UsuarioBLL.VerificarSeExisteUsuarioParaCadastrar(usuario))
                {
                    TempData["Erro"] = "Usuário já cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
                }

                var idUsuario = UsuarioLogado.ID;
                UsuarioDAO.CadastrarUsuario(usuario, idUsuario);
                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });

            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar cadastrar o " +
                    $"usuário: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
            }
        }

        public IActionResult EditarUsuario([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                if (usuario == null)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
                }

                if (UsuarioBLL.VerificarSeExisteUsuarioParaEditar(usuario))
                {
                    TempData["Erro"] = $"O RE°:{usuario.USUARIO} já esta cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
                }

                var idUsuario = UsuarioLogado.ID;
                UsuarioDAO.EditarUsuario(usuario, idUsuario);
                TempData["Sucesso"] = "Editado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });

            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"usuário: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroUsuario", "Admin") });
            }

        }

        [Route("Admin/ExcluirUsuario/{Id}")]
        public IActionResult ExcluirUsuario(int Id)
        {
            var IdUsuario = UsuarioLogado.ID;

            UsuarioDAO.ExcluirUsuario(Id, IdUsuario);

            TempData["Sucesso"] = "Usuário excluido com sucesso!";

            return RedirectToAction("CadastroUsuario");
        }

        #endregion


        #region Action Departamento

        public IActionResult CadastroDepartamento()
        {
            var model = DepartamentoDAO.RetornarDepartamento();

            return View("CadastroDepartamento", model);
        }

        [HttpPost]
        public IActionResult CadastrarDepartamento([FromBody] DepartamentoViewModel departamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Digite o departamento!";

                    return Json(new { redirectToUrl = Url.Action("CadastroDepartamento", "Admin") });
                }

                if (DepartamentoBLL.VerificarSeExisteDepartamentoPorNome(departamento.NOME.Trim()))
                {
                    TempData["Erro"] = "Departamento já cadastrado!";


                }

                DepartamentoDAO.CadastrarDepartamento(departamento, UsuarioLogado.ID);
                TempData["Sucesso"] = "Departamento cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroDepartamento", "Admin") });

            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }
        }

        public IActionResult EditarDepartamento([FromBody] DepartamentoViewModel departamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Digite o departamento!";

                    return Json(new { redirectToUrl = Url.Action("CadastroDepartamento", "Admin") });
                }

                if (DepartamentoBLL.VerificarSeExisteDepartamentoPorNome(departamento.NOME.Trim()))
                {
                    TempData["Erro"] = "Departamento já cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroDepartamento", "Admin") });
                }

                DepartamentoDAO.EditarDepartamento(departamento, UsuarioLogado.ID);
                TempData["Sucesso"] = "Departamento editado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroDepartamento", "Admin") });

            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }

        }

        [Route("Admin/ExcluirDepartamento/{Id}")]
        public IActionResult ExcluirDepartamento(int Id)
        {

            DepartamentoDAO.ExcluirDepartamento(Id, UsuarioLogado.ID);

            TempData["Sucesso"] = "Excluido com sucesso!";

            return RedirectToAction("CadastroDepartamento");

        }

        #endregion


        #region Action TipoUsuario

        public IActionResult CadastroTipoUsuario()
        {
            var model = TipoUsuarioDAO.RetornarTipoUsuario();

            return View(model);
        }

        [HttpPost]
        public IActionResult CadastrarTipoUsuario([FromBody] TipoUsuarioViewModel tipoUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Digite o tipo de usuário!";

                    return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });
                }

                if (TipoUsuarioBLL.VerificarSeExisteTipoUsuarioPorNome(tipoUsuario.NOME.Trim()))
                {
                    TempData["Erro"] = "Tipo de usuário já cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });
                }

                TipoUsuarioDAO.CadastrarTipoUsuario(tipoUsuario, UsuarioLogado.ID);
                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });

            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }

        }

        public IActionResult EditarTipoUsuario([FromBody] TipoUsuarioViewModel tipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Digite o Tipo de Usuário!";

                return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });
            }

            if (TipoUsuarioBLL.VerificarSeExisteTipoUsuarioPorNome(tipoUsuario.NOME.Trim()))
            {
                TempData["Erro"] = "Tipo de Usuário já cadastrado!";

                return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });
            }

            TipoUsuarioDAO.EditarTipoUsuario(tipoUsuario, UsuarioLogado.ID);
            TempData["Sucesso"] = "Editado com sucesso!";

            return Json(new { redirectToUrl = Url.Action("CadastroTipoUsuario", "Admin") });

        }

        [Route("Admin/ExcluirTipoUsuario/{Id}")]
        public IActionResult ExcluirTipoUsuario(int Id)
        {

            TipoUsuarioDAO.ExcluirTipoUsuario(Id, UsuarioLogado.ID);

            TempData["Erro"] = "Tipo de usuário excluido com sucesso!";

            return View("CadastroTipoUsuario");
        }

        #endregion

    }
}
