using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Validations;

namespace WebApplication.Models
{
    public class PeleadorModel
    {
        public long IdPeliador { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string Apellido { get; set; }
        
        [Required]
        [CorreoEnUsoValidation]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo no es correcto.")]
        [Display(Name = "Correo")]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(256)]
        [ContraseniaValidation]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(256)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        public string PasswordSalt { get; set; }

        [Required]
        [StringLength(50)]
        public string Resumen { get; set; }

        [Required]
        [StringLength(20)]
        public string Pais { get; set; }

        [Required]
        [StringLength(20)]
        public string Ciudad { get; set; }


        public HttpPostedFileBase Foto { get; set; }
        public List<string> Fotos { get; set; }
    }
}