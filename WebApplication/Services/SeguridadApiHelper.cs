using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TransferObjects.Seguridad;

namespace WebApplication.Services
{
    public class SeguridadApiHelper
    {
        public static AuthenticationApiData ObtenerDatosAutenticacionApiInterno()
        {
            var checkTokenEndPoint = ConfigurationManager.AppSettings["WEB_API_CHECK_TOKEN_ENDPOINT"];
            var refreshTokenEndPoint = ConfigurationManager.AppSettings["WEB_API_REFRESH_TOKEN"];
            var sesionUsuario = SeguridadService.ObtenerSesionUsuario();

            AuthenticationApiData datosAutenticacion = new AuthenticationApiData()
            {
                Token = sesionUsuario.Token,
                RefreshToken = sesionUsuario.RefreshToken,
                RefreshTokenEndpoint = refreshTokenEndPoint,
                VerificarTokenEndpoint = checkTokenEndPoint,
            };

            return datosAutenticacion;
        }
    }
}