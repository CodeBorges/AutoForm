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
    public class CadastroController : Controller
    {

        #region VerificaSessão

        private Usuario UsuarioLogado
        {
            get
            {
                return JsonConvert
                    .DeserializeObject<Usuario>(HttpContext.Session.GetString("Usuario"));
            }
        }

        private void ConfigurarSessao(Usuario usuario)
        {
            try
            {
                HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Action Localizacao

        public IActionResult CadastroLocalizacao()
        {
            var model = LocalizacaoDAO.RetornarLocalizacao();
            return View(model);
        }

        [HttpPost]
        public IActionResult CadastrarLocalizacao([FromBody] LocalizacaoViewModel localizacao)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(localizacao.NOME))
                {
                    var usuario = LocalizacaoBLL.VerificarSeExisteLocalizacaoPorNome(localizacao.NOME.Trim());

                    if (!usuario)
                    {
                        LocalizacaoDAO.CadastrarLocalizacao(localizacao, UsuarioLogado.ID);
                        TempData["Sucesso"] = "Localização cadastrado com sucesso!";
                    }
                    else
                    {
                        TempData["Erro"] = "Localização já cadastrado!";
                        return Json(new { redirectToUrl = Url.Action("CadastroLocalizacao", "Cadastro") });
                    }
                }
                else
                    TempData["Erro"] = "Digite a localização!";
                return Json(new { redirectToUrl = Url.Action("CadastroLocalizacao", "Cadastro") });
            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }
        }

        public IActionResult EditarLocalizacao([FromBody] LocalizacaoViewModel localizacao)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(localizacao.NOME))
                {
                    var usuario = LocalizacaoBLL.VerificarSeExisteLocalizacaoPorNome(localizacao.NOME.Trim());

                    if (!usuario)
                    {
                        LocalizacaoDAO.EditarLocalizacao(localizacao, UsuarioLogado.ID);
                        TempData["Sucesso"] = "Localização cadastrada com sucesso!";
                    }
                    else
                    {
                        TempData["Erro"] = $"A Localização {localizacao.NOME.Trim().Titleize()} já cadastrada!";
                        return Json(new { redirectToUrl = Url.Action("CadastroLocalizacao", "Cadastro") });
                    }
                }
                else
                    TempData["Erro"] = "Digite a localização!";
                return Json(new { redirectToUrl = Url.Action("CadastroLocalizacao", "Cadastro") });
            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }
        }

        [Route("Cadastro/ExcluirLocalizacao/{idLocalizacao}")]
        public IActionResult ExcluirLocalizacao(int idLocalizacao)
        {
            try
            {
                LocalizacaoDAO.ExcluirLocalizacao(idLocalizacao, UsuarioLogado.ID);

                TempData["Sucesso"] = "Localização excluída com sucesso!";

                return RedirectToAction("CadastroLocalizacao");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Action Categoria

        public IActionResult CadastroCategoria()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CarregarFormularioCategoriaPorIdFormulario(int idFormulario)
        {
            try
            {
                var model = FormularioCategoriaDAO.RetornarFormularioCategoriaPorIdFormulario(idFormulario);
                if (model.Count > 0)
                    return PartialView("_TabelaViewFormularioCategoriaBodyPartial", model);
                else
                    ViewBag.Erro = "Não existe nenhuma categoria vinculada a esse formulário!";
                return PartialView("_TabelaViewFormularioCategoriaBodyPartial", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CategoriaViewModel categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                }

                var documentoFinalizadoPorIdFormulario =
                    DocumentoDAO.RetornarDocumentoFinalizadoPorIdFormulario(categoria.ID_FORMULARIO);

                if (documentoFinalizadoPorIdFormulario != null)
                {
                    if (documentoFinalizadoPorIdFormulario.Count > 0)
                    {
                        var nomes = String.Join(", ", documentoFinalizadoPorIdFormulario.Select(s => s.NOME_USUARIO.Titleize()).ToArray());

                        TempData["Erro"] = $"Existe um documento pendente a ser finalizado pelo usuário " +
                        $"{nomes}";


                        return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                    }

                    if (!CategoriaBLL.VerificarSeExisteCategoriaPorNome(categoria.NOME))
                    {
                        TempData["Erro"] = $"A categoria {categoria.NOME.Titleize()} já esta cadastrada!";

                        return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                    }

                }

                var idCategoria = CategoriaDAO.CadastrarCategoria(categoria, UsuarioLogado.ID);
                FormularioCategoriaBLL.VincularFormularioCategoria(idCategoria, UsuarioLogado.ID, categoria);

                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar a " +
                    $"Categoria: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
            }
        }

        public IActionResult EditarCategoria([FromBody] CategoriaViewModel categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                }

                if (!CategoriaBLL.VerificarSeExisteCategoriaParaEditar(categoria))
                {
                    TempData["Erro"] = $"A categoria {categoria.NOME.Titleize()} já esta cadastrada!";

                    return base.Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                }

                CategoriaDAO.EditarCategoria(categoria, UsuarioLogado.ID);
                FormularioCategoriaDAO.EditarFormularioCategoria(categoria, UsuarioLogado.ID);
                TempData["Sucesso"] = "Editado com sucesso!";

                return base.Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
            }

            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"Categoria: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
            }
        }

        [Route("Cadastro/ExcluirCategoria/{idCategoria}")]
        public IActionResult ExcluirCategoria(int idCategoria)
        {
            try
            {
                var idUsuario = UsuarioLogado.ID;

                CategoriaDAO.ExcluirCategoria(idCategoria, idUsuario);
                FormularioCategoriaDAO.ExcluirFormularioCategoria(idCategoria, idUsuario);

                TempData["Erro"] = "Categoria excluida com sucesso!";

                return RedirectToAction("CadastroCategoria");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Action Formulario

        public IActionResult CadastroFormulario()
        {
            var model = FormularioDAO.RetornarFormulario();
            return View("CadastroFormulario", model);
        }

        [HttpPost]
        public IActionResult CadastrarFormulario([FromBody] FormularioViewModel formulario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
                }

                if (FormularioBLL.VerificarSeExisteFormularioPorNome(formulario.NOME))
                {
                    TempData["Erro"] = $"O Usuário {formulario.NOME.Titleize()} já esta cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
                }

                var idUsuario = UsuarioLogado.ID;
                FormularioDAO.CadastrarFormulario(formulario, idUsuario);
                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });

            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"Formulário: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
            }

        }

        public IActionResult EditarFormulario([FromBody] FormularioViewModel formulario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
                }

                if (FormularioBLL.VerificarSeExisteFormularioParaEditar(formulario))
                {
                    TempData["Erro"] = $"O formulário {formulario.NOME.Titleize()} já esta cadastrado!";

                    return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
                }

                FormularioDAO.EditarFormulario(formulario, UsuarioLogado.ID);
                TempData["Sucesso"] = "Editado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
            }

            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"Formulário: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroFormulario", "Cadastro") });
            }
        }

        [Route("Cadastro/ExcluirFormulario/{idFormulario}")]
        public IActionResult ExcluirFormulario(int idFormulario, int idUsuario)
        {
            try
            {
                FormularioDAO.ExcluirFormulario(idFormulario, UsuarioLogado.ID);

                TempData["Erro"] = "Formulário excluido com sucesso!";

                return RedirectToAction("CadastroFormulario");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Action Pergunta

        public IActionResult CadastroPerguntas()
        {
            var model = PerguntaDAO.RetornarPergunta();
            return View(model);
        }

        [HttpPost]
        public IActionResult CadastrarPergunta([FromBody] PerguntaViewModel pergunta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });
                }

                var documentoFinalizadoPorIdFormulario =
                    DocumentoDAO.RetornarDocumentoFinalizadoPorIdFormulario(pergunta.ID_FORMULARIO);

                if (documentoFinalizadoPorIdFormulario != null)
                {
                    if (documentoFinalizadoPorIdFormulario.Count > 0)
                    {
                        var nomes = String.Join(", ", documentoFinalizadoPorIdFormulario.Select(s => s.NOME_USUARIO.Titleize()).ToArray());

                        TempData["Erro"] = $"Existe um documento pendente a ser finalizado pelo usuário " +
                        $"{nomes}";

                        return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                    }
                }

                PerguntaDAO.CadastrarPergunta(pergunta, UsuarioLogado.ID);
                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });

            }

            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"Pergunta: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });
            }
        }

        public IActionResult EditarPergunta([FromBody] PerguntaViewModel pergunta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });
                }

                PerguntaDAO.EditarPergunta(pergunta, UsuarioLogado.ID);
                TempData["Sucesso"] = "Editado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"FoPerguntarmulário: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroPerguntas", "Cadastro") });
            }
        }

        [Route("Cadastro/ExcluirPergunta/{idPergunta}")]
        public IActionResult ExcluirPergunta(int idPergunta)
        {
            try
            {
                PerguntaDAO.ExcluirPergunta(idPergunta, UsuarioLogado.ID);

                TempData["Erro"] = "Formulário excluido com sucesso!";

                return RedirectToAction("CadastroPerguntas");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult RetornarListaCategoriaPorIdFormulario(string idFormulario)
        {
            try
            {
                var categoria = PerguntaBLL.ConfigurarListaCategoriaPorIdFormulario(Convert.ToInt32(idFormulario));

                return Json(categoria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult CarregarPerguntaPorIdCategoria(int idCategoria)
        {
            try
            {
                var model = PerguntaDAO.RetornarPerguntaPorIdCategoria(idCategoria);
                if (model.Count > 0)
                    return PartialView("_TabelaViewPerguntaBodyPartial", model);
                else
                    ViewBag.Erro = "Não existe nenhuma categoria vinculada a esse formulário!";
                return PartialView("_TabelaViewPerguntaBodyPartial", model);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region Action Observação
        public IActionResult CadastroObservacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CarregarObservacaoPorIdFormulario(int idFormulario)
        {
            try
            {
                var model = ObservacaoDAO.RetornarObservacaoPorIdFormulario(idFormulario);
                if (model.Count > 0)
                    return PartialView("_TabelaViewObservacaoBodyPartial", model);
                else
                    ViewBag.Erro = "Não existe nenhuma categoria vinculada a esse formulário!";
                return PartialView("_TabelaViewObservacaoBodyPartial", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult CadastrarObservacao([FromBody] ObservacaoViewModel observacao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";

                    return Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });
                }

                var documentoFinalizadoPorIdFormulario =
                    DocumentoDAO.RetornarDocumentoFinalizadoPorIdFormulario(observacao.ID_FORMULARIO);

                if (documentoFinalizadoPorIdFormulario != null)
                {
                    if (documentoFinalizadoPorIdFormulario.Count > 0)
                    {
                        var nomes = String.Join(", ", documentoFinalizadoPorIdFormulario.Select(s => s.NOME_USUARIO.Titleize()).ToArray());

                        TempData["Erro"] = $"Existe um documento pendente a ser finalizado pelo usuário " +
                        $"{nomes}";

                        return Json(new { redirectToUrl = Url.Action("CadastroCategoria", "Cadastro") });
                    }
                }

                ObservacaoDAO.CadastrarObservacao(observacao, UsuarioLogado.ID);
                TempData["Sucesso"] = "Cadastrado com sucesso!";

                return Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });

            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar a " +
                    $"Observação: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });
            }

        }

        public IActionResult EditarObservacao([FromBody] ObservacaoViewModel observacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ObservacaoDAO.EditarObservacao(observacao, UsuarioLogado.ID);
                    TempData["Sucesso"] = "Editado com sucesso!";
                    return base.Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });
                }
                TempData["Erro"] = "Existem campos em branco!";
                return Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu o seguinte erro ao tentar editar o " +
                    $"Observação: '{ex.Message}'. Contate o departamento de TI.";
                return Json(new { redirectToUrl = Url.Action("CadastroObservacao", "Cadastro") });
            }
        }

        [Route("Cadastro/ExcluirObservacao/{idObservacao}")]
        public IActionResult ExcluirObservacao(int idObservacao)
        {
            try
            {

                ObservacaoDAO.ExcluirObservacao(idObservacao, UsuarioLogado.ID);

                TempData["Erro"] = "Observação excluida com sucesso!";

                return RedirectToAction("CadastroObservacao");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        public IActionResult Index()
        {
            return View();
        }

    }
}
