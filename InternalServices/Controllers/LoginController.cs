using BussniesLogic.Services.Seguridad;
using InternalServices.Models;
using InternalServices.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Autenticar([FromBody] LoginModel login)
        {
            try
            {
                SecurityServices securityService = new SecurityServices();
                if (securityService.ValidarCredencialesUsuario(login.Mail, login.Password))
                {
                    var token = GeneradorDeTokens.GenerarToken(login.Mail);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
