// <copyright file="FilmEditViewModel.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Models
{
    public class FilmEditViewModel
    {
        public int Id { get; set; }

        public string Titre { get; set; } = string.Empty;

        public int AnneeDeSortie { get; set; }

        public int? RealisateurId { get; set; }

        public string? RealisateurNom { get; set; }
    }
}