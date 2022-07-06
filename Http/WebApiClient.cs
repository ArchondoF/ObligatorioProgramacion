using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Seguridad;

namespace Http
{
    public class WebApiClient : IWebApiClient
    {
        private readonly HttpClient _client;
        public WebApiClient(HttpClient _client)
        {
            this._client = _client;
        }
        public HttpResponseMessage Delete<T>(Uri adress, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }

        public HttpResponseMessage Get(Uri adress, Dictionary<string, string> parameters)
        {
            var queryParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                queryParameters.Add($"{parameter.Key}={parameter.Value}");
            }

            string query = $"?{string.Join("&" , queryParameters)}";

            var adressWithQuery = new Uri(adress, query);

            return this._client.GetAsync(adressWithQuery).Result;

        }

        public HttpResponseMessage Patch<T>(Uri adress, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }

        public HttpResponseMessage Post<T>(Uri adress, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");

            return this._client.SendAsync(request).Result;
        }

        public HttpResponseMessage Put<T>(Uri adress, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }



        //Con seguridad 


        public HttpResponseMessage Delete<T>(Uri adress, T transferObject, AuthenticationApiData authData)
        {
            this.VerifyAccesToken(authData);
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" , authData.Token);

            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }

        public HttpResponseMessage Get(Uri adress, Dictionary<string, string> parameters, AuthenticationApiData authData)
        {

            this.VerifyAccesToken(authData);
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.Token);

            var queryParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                queryParameters.Add($"{parameter.Key}={parameter.Value}");
            }

            string query = $"?{string.Join("&", queryParameters)}";

            var adressWithQuery = new Uri(adress, query);

            return this._client.GetAsync(adressWithQuery).Result;

        }

        public HttpResponseMessage Patch<T>(Uri adress, T transferObject, AuthenticationApiData authData)
        {
            this.VerifyAccesToken(authData);
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.Token);

            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }

        public HttpResponseMessage Post<T>(Uri adress, T transferObject, AuthenticationApiData authData)
        {
            this.VerifyAccesToken(authData);
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.Token);

            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");

            return this._client.SendAsync(request).Result;
        }

        public HttpResponseMessage Put<T>(Uri adress, T transferObject, AuthenticationApiData authData)
        {
            this.VerifyAccesToken(authData);
            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.Token);

            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, adress);

            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "aplication/json");

            var response = this._client.SendAsync(request).Result;

            return response;
        }


        private AuthenticationApiData VerifyAccesToken(AuthenticationApiData authdata)
        {
            var verifyAdress = new Uri(authdata.VerificarTokenEndpoint);

            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authdata.Token);

            var checkResponse = this._client.GetAsync(verifyAdress).Result;

            if (checkResponse.StatusCode == HttpStatusCode.Unauthorized || checkResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

                this._client.DefaultRequestHeaders.Authorization = null;

                var refreshAdress = new Uri(authdata.RefreshTokenEndpoint);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, refreshAdress);

                request.Content = new StringContent(JsonConvert.SerializeObject(authdata), Encoding.UTF8, "aplication/json");

                var refreshResponse = this._client.SendAsync(request).Result;

                if (refreshResponse.IsSuccessStatusCode)
                {
                    var accesToken = JsonConvert.DeserializeObject<string>(refreshResponse.Content.ReadAsStringAsync().Result);

                    authdata.Token = accesToken;
                }
            }

            return authdata;
        }



    }
}
