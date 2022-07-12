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

        public long? Estado { get; set; }
    }
}
