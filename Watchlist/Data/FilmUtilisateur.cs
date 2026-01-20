// <copyright file="FilmUtilisateur.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Data
{
    public class FilmUtilisateur
    {
        public string UtilisateurId { get; set; }

        public int FilmId { get; set; }

        public bool Vu { get; set; }

        public int Note { get; set; }

        public virtual Utilisateur Utilisateur { get; set; } = null!;

        public virtual Film Film { get; set; } = null!;
    }
}
