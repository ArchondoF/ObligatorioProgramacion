using BussniesLogic.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Seguridad;

namespace BussniesLogic.Services.Seguridad
{
    public class SecurityServices
    {
        public SesionUsuario ValidarCredencialesUsuario(string Correo, string password)
        {
            SesionUsuario usuarioValido = null;

            using (UnitOfWork uow = new UnitOfWork())
            {
                //Obtener usuario
                var usuario = uow.PeleadorRepository.GetPeleadorByCorreo(Correo);

                if (usuario != null)
                {
                    //Hashear password
                    var hashedPassword = GenerarHashSHA256(password.Trim(), usuario.PasswordSalt.Trim());

                    //Comparacion de password de usuario con con el hashedpasword del input
                    if (usuario.Password.Trim() == hashedPassword)
                    {
                        usuarioValido = new SesionUsuario();
                        usuarioValido.Id = usuario.IdPeliador;
                        usuarioValido.Nombre = usuario.Nombre.Trim();
                        usuarioValido.Appelido = usuario.Apellido.Trim();
                        usuarioValido.Correo = usuario.Correo.Trim();
                        usuarioValido.Resumen = usuario.Resumen.Trim();
                        usuarioValido.Pais = usuario.Pais.Trim();
                        usuarioValido.Ciudad = usuario.Ciudad.Trim();
                        usuarioValido.Fotos = uow.FotosRepository.GetRutasFotosById(usuario.IdPeliador);
                    }
                        
                }
            }

            return usuarioValido;
        }

        public string GenerarHashSHA256(string plainString, string salt)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoder = Encoding.UTF8;

                Byte[] bytes = hash.ComputeHash(encoder.GetBytes(plainString.Trim() + salt.Trim()));

                foreach (Byte bite in bytes)
                {
                    stringBuilder.Append(bite.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }

        public string GenerarSalt(int tamanoBuffer)
        {
            var rng = new RNGCryptoServiceProvider();

            var buffer = new byte[tamanoBuffer];

            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }
    }
}
