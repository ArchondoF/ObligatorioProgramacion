namespace ObligatorioProgramacion.DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bardos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdBardo { get; set; }

        public long IdPeleadorUno { get; set; }

        public long IdPeleadorDos { get; set; }

        [StringLength(20)]
        public string Ganador { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        public virtual Bardos Bardos1 { get; set; }

        public virtual Bardos Bardos2 { get; set; }

        public virtual Estados Estados { get; set; }
    }
}
