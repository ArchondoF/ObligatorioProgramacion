namespace ObligatorioProgramacion.DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bardos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bardos()
        {
            EstadoBardos = new HashSet<EstadoBardos>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdBardo { get; set; }

        public long IdPeleadorUno { get; set; }

        public long IdPeleadorDos { get; set; }

        [StringLength(20)]
        public string Ganador { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoBardos> EstadoBardos { get; set; }
    }
}
