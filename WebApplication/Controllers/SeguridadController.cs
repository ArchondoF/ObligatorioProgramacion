using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransferObjects.Seguridad;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class SeguridadController : Controller
    {
        // GET: Seguridad
        public ActionResult InicioSesion()
        {
            InicioSesionModel inicioSesionModel = new InicioSesionModel();
            var existeSesion = SeguridadService.ExisteSesion();

            if (existeSesion)
            {
                return RedirectToAction("InicioSesion", "Seguridad");
            }
            else
            {
                

                HttpCookie authCokie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCokie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCokie.Value);
                    inicioSesionModel.Correo = ticket.Name;
                    inicioSesionModel.Remember = true;
                }
            }
            return View(inicioSesionModel);
        }

        public ActionResult CerrarSesion()
        {
            SeguridadService.AbandonarSesionUsuario();
            return RedirectToAction("InicioSesion", "Seguridad");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(InicioSesionModel inicioSesionModel)
        {
            if (ModelState.IsValid)
            {
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.HttpClient);

                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

                Uri autenticarUsuarioUrl = new Uri(webApiUrl + "Login/Autenticar");

                var response = apiClient.Post(autenticarUsuarioUrl, inicioSesionModel);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Error autenticacion" , "Usuario o contraseña invalidos");
                }
                else
                {
                    SeguridadService seguridadService = new SeguridadService();

                    var usuario = JsonConvert.DeserializeObject<SesionUsuario>(response.Content.ReadAsStringAsync().Result);

                    seguridadService.GuardarSesionUsuario(usuario);

                    if (inicioSesionModel.Remember)
                    {
                        FormsAuthentication.SetAuthCookie(inicioSesionModel.Correo , inicioSesionModel.Remember);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(inicioSesionModel);
        }


    }
}