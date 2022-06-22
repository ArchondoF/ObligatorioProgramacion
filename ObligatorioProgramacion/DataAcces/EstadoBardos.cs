namespace ObligatorioProgramacion.DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EstadoBardos
    {
        public long IdBardo { get; set; }

        [Key]
        public long IdEstado { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        public virtual Bardos Bardos { get; set; }
    }
}
