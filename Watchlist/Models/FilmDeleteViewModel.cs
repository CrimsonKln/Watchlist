// <copyright file="FilmDeleteViewModel.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for deleting a film.
    /// </summary>
    public class FilmDeleteViewModel
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