using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models
{
    public class LoginModel
    {
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}