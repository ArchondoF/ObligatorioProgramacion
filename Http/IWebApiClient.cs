using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Seguridad;

namespace Http
{
    public interface IWebApiClient
    {
        //Interface con los metodos para realizar las llamadas
        HttpResponseMessage Get(Uri adress, Dictionary<string , string> parameters);
        HttpResponseMessage Post<T>(Uri adress,T transferObject);
        HttpResponseMessage Delete<T>(Uri adress, T transferObject);
        HttpResponseMessage Put<T>(Uri adress, T transferObject);
        HttpResponseMessage Patch<T>(Uri adress, T transferObject);

        //Metodos con seguridad 

        HttpResponseMessage Get(Uri adress, Dictionary<string, string> parameters, AuthenticationApiData authData);
        HttpResponseMessage Post<T>(Uri adress, T transferObject, AuthenticationApiData authData);
        HttpResponseMessage Delete<T>(Uri adress, T transferObject, AuthenticationApiData authData);
        HttpResponseMessage Put<T>(Uri adress, T transferObject, AuthenticationApiData authData);
        HttpResponseMessage Patch<T>(Uri adress, T transferObject, AuthenticationApiData authData);

    }
}


