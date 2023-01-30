using AutoForms.Helpers;
using AutoForms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Text;

namespace AutoForms.Controllers
{
    public class VerificarAdminAttribute : ActionFilterAttribute
    {
        public VerificarAdminAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionDescriptor.RouteValues["action"].Equals("Login"))
            {
                if (context.HttpContext.Session.TryGetValue("Usuario", out byte[] session))
                {
                    Usuario usuario = JsonConvert
                        .DeserializeObject<Usuario>(Encoding.UTF8.GetString(session));
                    
                    if (usuario.ID_TIPO_USUARIO != (int)Utils.TipoUsuario.ADMINISTRADOR)
                    {
                        context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                    }
                }
                else
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "Login", controller = "Home" }));
                }
            }
        }

    }
}