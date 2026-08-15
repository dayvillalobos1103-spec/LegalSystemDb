
using LegalSystem.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LegalSystem.Application.Interfaces.Dbcontex;
using LegalSystem.Domain.Entities;
namespace LegalSystem.Infraestructura.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>, IApplicationDbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        // Definición de las tablas (DbSets)
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Clientes> Clientes { get; set; } = null!;
        public DbSet<CasoJuridico> CasosJuridicos { get; set; } = null!;
        public DbSet<Cita> Citas { get; set; } = null!;
        public DbSet<Documento> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // --- CLIENTES ---
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Clienteid);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Cedula).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Telefono).HasMaxLength(15);
                entity.Property(e => e.Domicilio).HasMaxLength(150);
                entity.HasKey(e => e.Clienteid);
                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioId");

                // Configuramos la relación explícita
                entity.HasOne(d => d.Usuario)
                      .WithMany(p => p.Clientes) // Ahora coincide con el nombre en la clase Usuario
                      .HasForeignKey(d => d.UsuarioId);
            });

            // --- CASOS JURÍDICOS ---
            modelBuilder.Entity<CasoJuridico>(entity =>
            {
                entity.HasKey(e => e.Casoid);
                entity.Property(e => e.TituloCaso).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(300);
                entity.Property(e => e.FechaInicio).IsRequired();

                entity.Property(e => e.UsuarioId).IsRequired().HasColumnName("UsuarioId");

                entity.HasOne(d => d.Usuario)
                      .WithMany()
                      .HasForeignKey(d => d.UsuarioId)
                      .IsRequired();
            });

            // --- CITAS ---
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.Citaid);
                entity.Property(e => e.Motivo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Lugar).HasMaxLength(150);
                entity.Property(e => e.FechaHora).IsRequired();
                entity.Property(e => e.Estado).HasMaxLength(200).HasDefaultValue("programada");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioId");

                // Relación con Usuario
                entity.HasOne(d => d.Usuario)
                      .WithMany()
                      .HasForeignKey(d => d.UsuarioId)
                      .HasPrincipalKey("Id");

                // Relación con Cliente
                entity.HasOne(ci => ci.Cliente)
                      .WithMany(c => c.Citas)
                      .HasForeignKey(ci => ci.ClienteId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación con Caso Jurídico (CORREGIDO: Usaba ClienteId por error)
                entity.HasOne(ci => ci.CasosJuridico)
                      .WithMany(c => c.Citas)
                      .HasForeignKey(ci => ci.Casoid)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }



    }
}



