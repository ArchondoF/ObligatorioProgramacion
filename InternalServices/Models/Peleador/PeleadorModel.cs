using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models
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
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
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
        [Required]
        [StringLength(255)]
        public List<string> Fotos { get; set; }
    }
}