using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Controllers.Filtros;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [SesionRequerida]
    public class UsuariosController : Controller
    {

        public ActionResult Perfil()
        {
            return View();
        }


        public ActionResult Bardos()
        {
            List<PeleadorModel> modelo = new List<PeleadorModel>();


            //Creamos una instancia del apiclient recibiendo una instancia unica para todo el sistema del HttpClient a taves del HttpClientAccessor
            IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.HttpClient);
            var sesion = SeguridadService.ObtenerSesionUsuario();
            //Creamos parametros para la query de la url
            var parameters = new Dictionary<string, string>()
            {
                ["idPeleador"] = sesion.Id.ToString(),
            };

            //Obtenemos url base desde el web.config de la aplicacion web
            var baseUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

            //Formamos url para el metodo de cosnsulta de autores
            Uri consultaAutorUri = new Uri($"{baseUrl}Peleador/GetBardos");

            //Ejecutamos llamda GET
            var response = apiClient.Get(consultaAutorUri, parameters);

            if (response.IsSuccessStatusCode)
            {
                var peleadores = JsonConvert.DeserializeObject<List<PeleadorModel>>(response.Content.ReadAsStringAsync().Result);

                modelo = peleadores;
            }

            return View(modelo);
        }



        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Perfil/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
