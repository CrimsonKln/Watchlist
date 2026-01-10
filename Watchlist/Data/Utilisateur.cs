// <copyright file="Utilisateur.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Data
{
    public class Utilisateur : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public Utilisateur()
            : base()
        {
            this.ListeFilms = new HashSet<FilmUtilisateur>();
        }

        public string Prenom { get; set; }

        public virtual ICollection<FilmUtilisateur> ListeFilms { get; set; }
    }
}
