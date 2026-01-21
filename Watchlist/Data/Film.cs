// <copyright file="Film.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Film
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Film"/> class.
        /// </summary>
        public Film()
        {
            this.Utilisateurs = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Titre { get; set; }

        [Display(Name = "Release year")]
        public int AnneeDeSortie { get; set; }

        [Display(Name = "Director")]
        public int? RealisateurId { get; set; }

        public Realisateur? Realisateur { get; set; }

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
