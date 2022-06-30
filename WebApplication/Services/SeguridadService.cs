using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransferObjects.Seguridad;

namespace WebApplication.Services
{
    public class SeguridadService
    {
        public void GuardarSesionUsuario(SesionUsuario usuario)
        {
            HttpContext.Current.Session["InformacionUsuario"] = usuario;

            
        }

        public static void AbandonarSesionUsuario()
        {
            HttpContext.Current.Session.Abandon();
        }
        public static bool ExisteSesion()
        {
            var sesion = ObtenerSesionUsuario();

            return (sesion != null);
        }

        public static SesionUsuario ObtenerSesionUsuario()
        {
            return (SesionUsuario)HttpContext.Current.Session["InformacionUsuario"];
        }
    }
}