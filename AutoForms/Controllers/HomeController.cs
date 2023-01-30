using AutoForms.BLL;
using AutoForms.DAL;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace AutoForms.Controllers
{
    [ValidarSessao]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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


        #region Login 

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int usuario, string senha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = UsuarioDAO.RetornarUsuarioPorLoginESenha(usuario, senha);

                    if (login != null)
                    {
                        login.NOME = login.NOME.Titleize();
                        ConfigurarSessao(login);
                        TempData["Sucesso"] = $"Bem vindo, {login.NOME}!";
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["Erro"] = "Usuário e/ou senha inválido(s), Por favor, tente novamente.";
                        return RedirectToAction("Login");
                    }
                }
                else
                    TempData["Erro"] = "Usuário e/ou senha inválido(s), Por favor, tente novamente.";
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Erro = $"Ocorreu o seguinte erro: '{ex.Message}'. Contate o departamento de TI.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = String.Format("Ocorreu o seguinte erro: {0}. " +
                    "Contate o departamento de TI" + ex.Message);

                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult AlterarSenha(string txtNovaSenha, string txtRepetirSenha)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtNovaSenha) || !string.IsNullOrEmpty(txtRepetirSenha))
                {
                    if (txtNovaSenha == txtRepetirSenha)
                    {
                        UsuarioDAO.AlterarSenha(UsuarioLogado.ID, txtNovaSenha);

                        TempData["Sucesso"] = "Alteração feita com sucesso, entre novamente!";

                        return RedirectToAction("Logout");
                    }
                    else
                    {
                        TempData["Erro"] = "As senhas não coincidem!";

                        return RedirectToAction("Index");
                    }

                }
                TempData["Erro"] = "As senhas estão em branco!";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult VerificarAlterarSenha(int idUsuario)
        {
            try
            {
                var usuario = UsuarioDAO.RetornarUsuarioPorId(idUsuario);
                if (usuario.MUDAR_SENHA == false)
                {
                    return Json(false);
                }

                return Json(true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public IActionResult Dashboard()
        {
            try
            {
                var model = new DashboardViewModel();

                if (DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID) != null)
                    model.DocumentoPendente = DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID);

                model.DocumentosFinalizados = DocumentoDAO.RetornarDocumentoFinalizadoIdUsuario(UsuarioLogado.ID);
                model.UsuarioLogado = UsuarioLogado;

                return View(model);

            }
            catch (Exception)
            {

                throw;
            }

        }

        #region Action Formulário

        [Route("Home/Formulario/{idFormulario}")]
        public IActionResult Formulario(int idFormulario)
        {

            var totalPerguntasPorCategoria =
               FormularioCategoriaDAO.RetornarTotalPerguntasCategoriaPorIdFormulario(idFormulario);

            if (totalPerguntasPorCategoria.Where(w => w.TOTAL_PERGUNTAS == 0).Count() > 0)
            {
                TempData["Erro"] = "Existem categorias sem perguntas cadastradas!";

                return RedirectToAction("FormularioVisaoGerencial");
            }

            if (DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID) != null)
            {
                if (DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID).ID_FORMULARIO != idFormulario)
                {
                    TempData["Erro"] = "Existem documentos pendentes!";
                    return RedirectToAction("FormularioVisaoGerencial");
                }
            }


            var model = new FormularioCategoriaViewModel();
            model.Documento = RegistroRespostaBLL.ValidarCriacaoDeFormulario(idFormulario, UsuarioLogado.ID);
            var medias = MediaCategoriaDAO.RetornarMediaCategoriaPorIdDocumento(model.Documento.ID);
            model.Formulario = FormularioDAO.RetornarFormularioPorId(idFormulario);
            model.ListaFormularioCategoria = FormularioCategoriaDAO.RetornarCategoriaPorIdFormulario(idFormulario);
            model.UsuarioLogado = UsuarioLogado;
            model.ListaObservacao = ObservacaoDAO.RetornarObservacaoIdFormulario(idFormulario);

            model.ListaFormularioCategoria.ForEach(f =>
            {
                f.MediaCategoria =
                medias.Where(w => f.ID_CATEGORIA == w.ID_CATEGORIA).FirstOrDefault();

            });

            return View(model);

        }

        [HttpPost]
        public IActionResult RetornarFormularioPorNome(string nomeFormulario)
        {
            try
            {
                var listaFormularios = FormularioDAO
                    .RetornarFormulario();

                var formulario =
                    listaFormularios.Where(w => w.NOME.Contains($"{nomeFormulario.ToUpper().Trim()}")).FirstOrDefault();

                return Json(new { redirectToUrl = Url.Action("Formulario", "Home", new { idFormulario = $"{formulario.ID}" }) });
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public JsonResult RetornarListaDeRespostaIdDocumento(string idDocumento)
        {
            try
            {
                var dicionarioRegistroResposta = new Dictionary<string, string>();

                var listaRegistroResposta = RegistroRespostaDAO
                    .RetornarListaDeRespostaIdDocumento(Convert.ToInt32(idDocumento));


                if (listaRegistroResposta.Count > 0)
                    foreach (var resposta in listaRegistroResposta)
                    {
                        dicionarioRegistroResposta.Add(resposta.ID_PERGUNTA.ToString(), resposta.RESPOSTA.ToString());
                    }
                else
                    dicionarioRegistroResposta = null;


                return Json(dicionarioRegistroResposta);
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public JsonResult VerificarMediaResposta([FromBody] RegistroRespostaViewModel registroResposta)
        {
            try
            {
                var dicionarioRegistroResposta = MediaCategoriaBLL.CalculoMediaPorCategoria(registroResposta.RegistroResposta);
                RegistroRespostaDAO.SalvarRespostaAutomaticamente
                    (registroResposta.ID_PERGUNTA, registroResposta.ID_DOCUMENTO, (int)registroResposta.RESPOSTA);

                return Json(dicionarioRegistroResposta);
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public JsonResult VerificarListaDeRespostaIdDocumento(string idDocumento)
        {
            try
            {
                var dicionarioRegistroResposta = new Dictionary<string, string>();

                var listaRegistroResposta = RegistroRespostaDAO
                    .RetornarListaDeRespostaIdDocumento(Convert.ToInt32(idDocumento));

                var verificarRegistroDeResposta = listaRegistroResposta.Where(w => w.RESPOSTA.ToString() == null);


                if (listaRegistroResposta.Count > 0)
                {
                    foreach (var resposta in listaRegistroResposta)
                    {
                        if (resposta.RESPOSTA == null)
                        {
                            dicionarioRegistroResposta.Add(resposta.ID_PERGUNTA.ToString(), "null");
                        }
                    }
                }

                if (dicionarioRegistroResposta.Count > 0)
                {

                    return Json("null");
                }


                return Json(dicionarioRegistroResposta);
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public JsonResult RetornarListaDeInformacoesResposta(string idDocumento)
        {
            try
            {
                var dicionarioRegistroInformacoes = new DocumentoViewModel();

                var listaRegistroResposta = DocumentoDAO
                    .RetornarDocumentoPorId(Convert.ToInt32(idDocumento));

                if (listaRegistroResposta != null)
                    dicionarioRegistroInformacoes = listaRegistroResposta;
                else
                    dicionarioRegistroInformacoes = null;

                return Json(dicionarioRegistroInformacoes);
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        public JsonResult RetornarListaDeInformacoesObservacao(string idDocumento)
        {
            try
            {
                var dicionarioRegistroObservacao = new Dictionary<int, string>();

                var listaRegistroObservacao = RegistroObservacaoDAO
                    .RetornarListaDeRespostaObservacaoIdDocumento(Convert.ToInt32(idDocumento));

                if (listaRegistroObservacao.Count > 0)
                    foreach (var resposta in listaRegistroObservacao)
                        dicionarioRegistroObservacao.Add(resposta.ID_OBSERVACAO, resposta.TEXTO_OBSERVACAO);

                else
                    dicionarioRegistroObservacao = null;

                return Json(dicionarioRegistroObservacao);
            }
            catch (Exception ex)
            {
                var viewDeOrigem = Regex.Match(Request.Headers["Referer"].ToString(), "([^/]+$)").ToString();

                TempData["Erro"] = $"Ocorreu o seguinte erro: '{ex.Message}'. " +
                    $"Contate o departamento de TI.";

                return new JsonResult(new { redirectToUrl = Url.Action(viewDeOrigem, "Home") })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [Route("Home/ExcluirFormularioGerado/{idDocumento}")]
        public IActionResult ExcluirFormularioGerado(int idDocumento)
        {
            try
            {
                DocumentoBLL.ExcluirDocumento(idDocumento);
                TempData["Sucesso"] = $"Documento N°{idDocumento} excluido!";
                return RedirectToAction("FormularioVisaoGerencial");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult FormularioVisaoGerencial()
        {
            var model = new FormularioViewModel();
            if (DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID) == null)
                model.PENDENTE = false;
            else
            {
                model.Documento = DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(UsuarioLogado.ID);
                model.PENDENTE = true;
            }
            model.UsuarioLogado = UsuarioLogado;
            model.ListaFormulario = FormularioDAO.RetornarFormulario();

            return View(model);
        }


        #region Action RegistroRespostas

        public void SalvarMediaGeral(int idDocumento)
        {
            //RegistroRespostaDAO.SalvarRespostaAutomaticamente((int)registroResposta.ID_PERGUNTA, registroResposta.ID_DOCUMENTO, (int)registroResposta.RESPOSTA);
            MediaCategoriaBLL.SalvarMediaGeral(idDocumento);
        }

        public JsonResult SalvarInformacoes([FromBody] DocumentoViewModel documento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Erro"] = "Existem campos em branco!";
                    return Json(new { Success = false });
                }

                DocumentoDAO.SalvarInformacoes(documento);

                return Json(new { Success = true });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult SalvarObservacao([FromBody] List<RegistroObservacaoViewModel> registroObservacao)
        {
            try
            {
                var idDocumento = registroObservacao[0].ID_DOCUMENTO;
                RegistroObservacaoBLL.SalvarRegistroObservacao(registroObservacao, UsuarioLogado.ID);
                MediaCategoriaBLL.SalvarMediaGeral(idDocumento);
                DocumentoDAO.FinalizarDocumento(idDocumento);

                TempData["Sucesso"] = $"Documento Nº{idDocumento} salvo com sucesso! ";
                return Json(new { redirectToUrl = Url.Action("FormularioVisaoGerencial", "Home") });

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #endregion


        #region Action Comunicação Interna

        public IActionResult ComunicacaoInternaVisaoGerencial()
        {
            return View();
        }


        #endregion


        #region Action Autorização Saida

        public IActionResult AutorizacaoSaidaVisaoGerencial()
        {
            return View();
        }


        #endregion


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
