using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models
{
    public class FotoModel
    {
        [Required]
        public string Ruta { get; set; }
        [Required]
        public long IdPeleador { get; set; }
        
    }
}