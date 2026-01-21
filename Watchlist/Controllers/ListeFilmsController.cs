// <copyright file="ListeFilmsController.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Watchlist.Data;
    using Watchlist.Models;

    [Authorize]
    public class ListeFilmsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Utilisateur> gestionnaire;

        public ListeFilmsController(ApplicationDbContext context, UserManager<Utilisateur> gestionnaire)
        {
            this.context = context;
            this.gestionnaire = gestionnaire;
        }

        private Task<Utilisateur> GetCurrentUserAsync() => this.gestionnaire.GetUserAsync(this.HttpContext.User);

        [HttpGet]
        public async Task<string> RecupererIdUtilisateurCourant()
        {
            Utilisateur utilisateur = await this.GetCurrentUserAsync();
            return utilisateur?.Id;
        }

        public async Task<IActionResult> Index()
        {
            var id = await this.RecupererIdUtilisateurCourant();
            var filmsUtilisateur = this.context.FilmsUtilisateur.Where(x => x.UtilisateurId == id);
            var modele = filmsUtilisateur.Select(x => new FilmViewModel
            {
                IdFilm = x.FilmId,
                Titre = x.Film.Titre,
                AnneeDeSortie = x.Film.AnneeDeSortie,
                Vu = x.Vu,
                PresentDansListe = true,
                Note = x.Note,
            }).ToList();
            return this.View(modele);
        }
    }
}
