using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ObligatorioProgramacion.DataAcces
{
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }


        public virtual DbSet<Fotos> Fotos { get; set; }
        public virtual DbSet<Peleador> Peleador { get; set; }
        public virtual DbSet<Bardos> Bardos { get; set; }
        public virtual DbSet<EstadoBardos> EstadoBardos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fotos>()
                .Property(e => e.Ruta)
                .IsUnicode(false);

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Nombre)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Apellido)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Correo)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.PasswordSalt)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Resumen)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Pais)
                .IsFixedLength();

            modelBuilder.Entity<Peleador>()
                .Property(e => e.Ciudad)
                .IsFixedLength();

            modelBuilder.Entity<Bardos>()
                .Property(e => e.Ganador)
                .IsFixedLength();

            modelBuilder.Entity<Bardos>()
                .HasMany(e => e.EstadoBardos)
                .WithRequired(e => e.Bardos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peleador>()
                .HasMany(e => e.Fotos)
                .WithRequired(e => e.Peleador)
                .HasForeignKey(e => e.IdPeleador)
                .WillCascadeOnDelete(false);
        }
    }
}
