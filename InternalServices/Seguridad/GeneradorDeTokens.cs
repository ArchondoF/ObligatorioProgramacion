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
        private readonly string _claveSecreta;
        private readonly string _audiencia;
        private readonly string _emisor;
        private readonly string _tiempoDeVidaToken;
        private readonly string _tiempoDeVidaRefreshToken;

        public GeneradorDeTokens()
        {
            this._claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            this._audiencia = ConfigurationManager.AppSettings["JWT_AUDENCE_TOKEN"];
            this._emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            this._tiempoDeVidaToken = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];
            this._tiempoDeVidaRefreshToken = ConfigurationManager.AppSettings["JWT_REFRESH-EXPIRE_DIAS"];
        }
        public string GenerarToken(string Mail)
        {
            string token = string.Empty;
            

            var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(this._claveSecreta));

            var credencialesDeFirmaDigital = new SigningCredentials(claveDeSeguridad, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsDeIdentidad = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Mail) });

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: this._audiencia,
                issuer: this._emisor,
                subject: claimsDeIdentidad,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(this._tiempoDeVidaToken)),
                signingCredentials: credencialesDeFirmaDigital

                );

            token = tokenHandler.WriteToken(jwtSecurityToken);


            return token;
        }

        public string GenerarRefreshToken()
        {
            string refreshToken = string.Empty;

            var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(this._claveSecreta));

            var credencialesDeFirmaDigital = new SigningCredentials(claveDeSeguridad, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: this._audiencia,
                issuer: this._emisor,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(this._tiempoDeVidaRefreshToken)),
                signingCredentials: credencialesDeFirmaDigital

                );

            return refreshToken;
        }
    }
}