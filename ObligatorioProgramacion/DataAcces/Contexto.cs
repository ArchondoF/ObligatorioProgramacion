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
        public virtual DbSet<Estados> Estados { get; set; }


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
               .Property(e => e.Ganador)
               .IsFixedLength();

            modelBuilder.Entity<Bardos>()
                .Property(e => e.Estado)
                .IsFixedLength();

            modelBuilder.Entity<Bardos>()
                .HasOptional(e => e.Bardos1)
                .WithRequired(e => e.Bardos2);

            modelBuilder.Entity<Estados>()
                .Property(e => e.Id)
                .IsFixedLength();

            modelBuilder.Entity<Estados>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Estados>()
                .HasMany(e => e.Bardos)
                .WithRequired(e => e.Estados)
                .HasForeignKey(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peleador>()
                .HasMany(e => e.Fotos)
                .WithRequired(e => e.Peleador)
                .HasForeignKey(e => e.IdPeleador)
                .WillCascadeOnDelete(false);
        }
    }
}
