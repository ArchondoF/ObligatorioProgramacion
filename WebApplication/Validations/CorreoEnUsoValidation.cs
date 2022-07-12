using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication.Validations
{
    public class CorreoEnUsoValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.HttpClient);

                var parameters = new Dictionary<string, string>()
                {
                    ["Correo"] = value.ToString()
                };

                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

                Uri consultaExisteMail = new Uri(webApiUrl + "Peleador/ExisteMail");

                var response = apiClient.Get(consultaExisteMail, parameters);

                if (response.IsSuccessStatusCode)
                {
                    var existeMail = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);

                    if (existeMail)
                        return new ValidationResult("El correo ya esta en uso");
                }
                else
                {
                    return new ValidationResult("No es posible el correo");
                }
            }

            return ValidationResult.Success;
        }
    }
}