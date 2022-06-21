using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InternalServices.Seguridad.Habdelrs
{
    public class ManejadorDeValidacionDeTokens : DelegatingHandler
    {
        private static bool IntentarObtenerToken(HttpRequestMessage request, out string token)
        {

            token = null;
            bool tokenObtenido = false;

            IEnumerable<string> autHeaders;

            if (request.Headers.TryGetValues("Authorization", out autHeaders) && autHeaders.Count() == 1)
            {
                var barerToken = autHeaders.ElementAt(0);

                token = barerToken.StartsWith("Bearer") ? barerToken.Substring(7) : barerToken;

                tokenObtenido = true;

            }
            else
            {

            }

            return tokenObtenido;
        }





        public bool ValidarTiempoDeVidaToken(DateTime? notBefore, DateTime? expieres, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            bool tokenVivo = false;
            if (expieres != null && DateTime.UtcNow < expieres)
            {
                tokenVivo = true;
            }

            return tokenVivo;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            HttpStatusCode codigoEstado;
            string token = string.Empty;

            if (!IntentarObtenerToken(request, out token))
            {
                return base.SendAsync(request, cancellationToken);
            }
            try
            {

                var claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audencia = ConfigurationManager.AppSettings["JWT_AUDENCE_TOKEN"];
                var emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];

                var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

                SecurityToken securityToken;

                var tokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audencia,
                    ValidIssuer = emisor,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.ValidarTiempoDeVidaToken,
                    IssuerSigningKey = claveDeSeguridad
                };
                var tokenValido = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                Thread.CurrentPrincipal = tokenValido;
                HttpContext.Current.User = tokenValido;

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                codigoEstado = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                codigoEstado = HttpStatusCode.InternalServerError;
            }


            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(codigoEstado) { });

        }
    }
}