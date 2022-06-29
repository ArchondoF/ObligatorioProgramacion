using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Services;

namespace WebApplication.Controllers.Filtros
{
    public class SesionRequerida : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SeguridadService.ExisteSesion())
            {
                filterContext.Result = new RedirectToRouteResult
                (
                    new RouteValueDictionary
                    {
                        { "controller" , "Seguridad" },
                        { "action" , "InicioSesion" }
                    }
                );
            }
        }
    }
}