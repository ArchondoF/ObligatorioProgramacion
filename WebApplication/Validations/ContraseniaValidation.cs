using Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Validations
{
    public class ContraseniaValidation :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string contraseña = value.ToString();
            if (contraseña.Length < 8 || contraseña.Length > 50)
                return new ValidationResult("La contraseña debe esta en un rango de 8 a 50");


            }

        return ValidationResult.Success;
    }
}
}