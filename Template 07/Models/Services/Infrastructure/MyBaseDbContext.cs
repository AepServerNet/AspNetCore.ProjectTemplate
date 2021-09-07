using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Template_SQLite_EfCore.Models.Entities;

namespace Template_SQLite_EfCore.Models.Services.Infrastructure
{
    public partial class MyBaseDbContext : DbContext
    {
        public MyBaseDbContext(DbContextOptions<MyBaseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Utente> Utenti { get; set; }
        public virtual DbSet<Profilo> Profili { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utente>(entity =>
            {
                entity.ToTable("Utenti");
                entity.HasKey(utente => utente.Id);
                
                //Mapping per le relazioni
                entity.HasMany(utente => utente.Profili)
                      .WithOne(profilo => profilo.Utente)
                      .HasForeignKey(profilo => profilo.UtenteId);

                //Il mapping generato automaticamente dal tool di reverse engineering
            });

            modelBuilder.Entity<Profilo>(entity =>
            {
                //Il mapping generato automaticamente dal tool di reverse engineering
            });
        }
    }
}
