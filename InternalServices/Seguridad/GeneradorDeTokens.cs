using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace InternalServices.Seguridad
{
    public class GeneradorDeTokens
    {
        public static string GenerarToken(string Mail)
        {
            string token = string.Empty;
            var claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audencia = ConfigurationManager.AppSettings["JWT_AUDENCE_TOKEN"];
            var emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var tiempoDeVida = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

            var credencialesDeFirmaDigital = new SigningCredentials(claveDeSeguridad, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsDeIdentidad = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Mail) });

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audencia,
                issuer: emisor,
                subject: claimsDeIdentidad,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(tiempoDeVida)),
                signingCredentials: credencialesDeFirmaDigital

                );

            token = tokenHandler.WriteToken(jwtSecurityToken);


            return token;
        }
    }
}