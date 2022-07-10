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
                    return RedirectToAction("IndexUser", "Home");
                }
            }
            return View(inicioSesionModel);
        }

        public ActionResult Registrar()
        {
            
            return View();
        }
        public ActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PeleadorModel peleadorModel)
        {
            //Ojo hay que agregar que si es modelo es valido
            if (ModelState.IsValid)
            {
                //Instanciar apiclient para realizar llamdas HTTP a apis RESTful
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.HttpClient);

                //Obtener url base desde web.config para la web api
                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

                //Crear url para metodo de creacion de autores
                Uri crearAutorUri = new Uri(webApiUrl + "Peleador/AddPeleador");

                //Obtener datos de autenticacion de api interno

                if (peleadorModel.Fotos != null)
                {
                    peleadorModel.Fotos = new List<string>();

                    System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(peleadorModel.Foto.InputStream);
                    Byte[] bytes = binaryReader.ReadBytes((int)peleadorModel.Foto.InputStream.Length);

                    peleadorModel.Fotos.Add(Convert.ToBase64String(bytes, 0, bytes.Length));
                    peleadorModel.Fotos = null;
                }

                //Ejecutar llamda http post y obtener respuesta no mandamos el auth data porque al estar registradonse no
                //va a tener informacion del usuario
                var response = apiClient.Post(crearAutorUri, peleadorModel);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Error de API", "Ocurrio un error al intentar crear el peleador");
                }
                else
                {
                    return RedirectToAction("InicioSesion", "Seguridad");
                }
            }

            return View(peleadorModel);
        }

    }
}