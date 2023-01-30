using AutoForms.DAL;
using AutoForms.Models;
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
    public class RelatorioController : Controller
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

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RelatorioUsuario()
        {
            var model = DocumentoDAO.RetornarDocumentoFinalizadoPorIdUsuario(UsuarioLogado.ID);

            return View(model);
        }
    }
}
