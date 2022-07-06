using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObjects.Seguridad
{
    public class SesionUsuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Appelido { get; set; }
        public string Correo { get; set; }
        public string Resumen { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public List<string> Fotos { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set;
        }
    }
}
