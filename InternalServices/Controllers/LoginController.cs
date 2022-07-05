using BussniesLogic.DataModel;
using BussniesLogic.Services.Seguridad;
using InternalServices.Models;
using InternalServices.Models.Tokens;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Login/Autenticar")]
        public IHttpActionResult Autenticar([FromBody] LoginModel login)
        {
            try
            {
                SecurityServices securityService = new SecurityServices();
                var usuarioValido = securityService.ValidarCredencialesUsuario(login.Correo , login.Password); 
                if (usuarioValido != null)
                {
                    GeneradorDeTokens generadorDeTokens = new GeneradorDeTokens();

                    var token = generadorDeTokens.GenerarToken(login.Correo);
                    var refreshToken = generadorDeTokens.GenerarRefreshToken();

                    usuarioValido.Token = token;
                    usuarioValido.RefreshToken = refreshToken;

                    using (var uow = new UnitOfWork())
                    {
                        var usuario = uow.PeleadorRepository.GetPeleadorById(usuarioValido.Id);
                        usuario.RefreshToken = refreshToken;

                        uow.SaveChanges();
                    }

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

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Login/RefrescarTokenAcceso")]
        public IHttpActionResult RefrescarTokenAcceso([FromBody] RefreshTokenModel refresh)
        {
            try
            {
                ValidadorDeRefreshTokens validadorDeRefreshTokens = new ValidadorDeRefreshTokens();
                bool tokenEsValido = validadorDeRefreshTokens.Validar(refresh.RefreshToken);

                if (!tokenEsValido)
                    return BadRequest("Refresh token invalido");

                using (var uow = new UnitOfWork())
                {
                    var usuario = uow.PeleadorRepository.GetPeleadorByRefreshToken(refresh.RefreshToken);

                    if (usuario == null)
                        return NotFound();

                    GeneradorDeTokens generadorDeTokens = new GeneradorDeTokens();
                    var accesToken = generadorDeTokens.GenerarToken(usuario.Correo);

                    return Ok(accesToken);
                }
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/Login/VerificarAccesToken")]
        public IHttpActionResult VerificarAccesToken()
        {
            return Ok();
        }




    }
}
