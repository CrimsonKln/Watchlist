using Microsoft.EntityFrameworkCore;
using Watchlist.Models;
// <copyright file="ApplicationDbContext.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Film> Films { get; set; }

        public DbSet<FilmUtilisateur> FilmsUtilisateur { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.ListeFilms)
                .WithMany(e => e.Utilisateurs)
                .UsingEntity<FilmUtilisateur>(
                    j => j
                        .HasOne(pt => pt.Film)
                        .WithMany()
                        .HasForeignKey(pt => pt.FilmId),
                    j => j
                        .HasOne(pt => pt.Utilisateur)
                        .WithMany()
                        .HasForeignKey(pt => pt.UtilisateurId),
                    j =>
                    {
                        j.HasKey(t => new { t.UtilisateurId, t.FilmId });
                    });

            modelBuilder.Entity<FilmViewModel>().ToTable("FilmViewModel", t => t.ExcludeFromMigrations());
        }

        public DbSet<FilmViewModel> FilmViewModel { get; set; }

        public DbSet<Realisateur> Realisateurs { get; set; }
    }
}
