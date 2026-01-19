// <copyright file="FilmDetailsViewModel.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for displaying details of a film.
    /// </summary>
    public class FilmDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the film identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the film title.
        /// </summary>
        [Display(Name = "Title")]
        public string Titre { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the film's release year.
        /// </summary>
        [Display(Name = "Release year")]
        public int AnneeDeSortie { get; set; }

        /// <summary>
        /// Gets or sets the director's name.
        /// </summary>
        [Display(Name = "Director")]
        public string? RealisateurNom { get; set; }
    }
}
