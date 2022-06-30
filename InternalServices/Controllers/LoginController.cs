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
        [Route("api/Login/Autenticar")]
        [AllowAnonymous]
        public IHttpActionResult Autenticar([FromBody] LoginModel login)
        {
            try
            {
                SecurityServices securityService = new SecurityServices();
                var usuarioValido = securityService.ValidarCredencialesUsuario(login.Correo , login.Password); 
                if (usuarioValido != null)
                {
                    var token = GeneradorDeTokens.GenerarToken(login.Correo);
                    usuarioValido.Token = token;
                    return Ok(usuarioValido);
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
