using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace InternalServices.Seguridad
{
    public class ValidadorDeRefreshTokens
    {
        public bool Validar(string refreshToken)
        {
            bool tokenEsValido = false;

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                var claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audiencia = ConfigurationManager.AppSettings["JWT_AUDENCE_TOKEN"];
                var emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];

                var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audiencia,
                    ValidIssuer = emisor,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = claveDeSeguridad,

                };

                SecurityToken securityToken;
                tokenHandler.ValidateToken(refreshToken, validationParameters, out securityToken);
                tokenEsValido = true;
            }
            catch (Exception ex)
            {

                tokenEsValido = false;

            }
            return tokenEsValido;
        }
    }
}