using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaDev.Models
{
    public partial class CursosCTX : DbContext
    {
        public CursosCTX()
        {
        }

        public CursosCTX(DbContextOptions<CursosCTX> options)
            : base(options)
        {
        }

        public virtual DbSet<TbPersonasFisicas> TbPersonasFisicas { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer("Server=DESKTOP-AA2MRFT\\SQLEXPRESS;Database=DbPruebaDev;Trusted_Connection=True;");
        //     }
        // }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbPersonasFisicas>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.ApellidoMaterno).IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno).IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Rfc).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
