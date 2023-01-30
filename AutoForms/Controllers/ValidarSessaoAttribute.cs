using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AutoForms.Controllers
{
    public class ValidarSessaoAttribute : ActionFilterAttribute
    {
        public ValidarSessaoAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionDescriptor.RouteValues["action"].Equals("Login"))
            {
                if (!context.HttpContext.Session.Keys.Contains("Usuario"))
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "Login", controller = "Home" }));
                }
            } 
        }
    }
}