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
            modelBuilder.Entity<FilmUtilisateur>()
            .HasKey(t => new { t.IdUtilisateur, t.IdFilm });
            modelBuilder.Entity<FilmViewModel>().ToTable("FilmViewModel", t => t.ExcludeFromMigrations());
        }

        public DbSet<FilmViewModel> FilmViewModel { get; set; }

        public DbSet<Realisateur> Realisateurs { get; set; }
    }
}
