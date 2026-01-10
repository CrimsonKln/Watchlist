// <copyright file="FilmViewModel.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FilmViewModel
    {
        [Key]
        public int IdFilm { get; set; }

        public string Titre { get; set; }

        public int AnneeDeSortie { get; set; }

        public bool PresentDansListe { get; set; }

        public bool Vu { get; set; }

        public int? Note { get; set; }
    }
}
