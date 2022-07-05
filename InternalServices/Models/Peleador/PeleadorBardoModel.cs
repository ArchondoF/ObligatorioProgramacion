using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models
{
    public class PeleadorBardoModel
    {
        public long IdPeliador { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(20)]
        public string Ciudad { get; set; }
    }
}