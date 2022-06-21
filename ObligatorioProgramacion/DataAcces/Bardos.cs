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

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public virtual Peleador Peleador { get; set; }

        public virtual Peleador Peleador1 { get; set; }
    }
}
