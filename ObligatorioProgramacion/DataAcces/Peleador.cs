namespace ObligatorioProgramacion.DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Peleador")]
    public partial class Peleador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Peleador()
        {
            Fotos = new HashSet<Fotos>();
        }

        [Key]
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

        public string RefreshToken { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fotos> Fotos { get; set; }
    }
}
