using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObjects.Seguridad
{
    public class AuthenticationApiData
    {
        public string VerificarTokenEndpoint { get; set; }
        public string RefreshTokenEndpoint { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
