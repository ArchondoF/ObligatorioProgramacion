namespace ObligatorioProgramacion.prueba
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fotos
    {
        [Key]
        [StringLength(255)]
        public string Ruta { get; set; }

        public long IdPeleador { get; set; }

        public virtual Peleador Peleador { get; set; }
    }
}
