// <copyright file="ListeFilmsController.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Watchlist.Data;
    using Watchlist.Models;

    public class ListeFilmsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Utilisateur> gestionnaire;

        public ListeFilmsController(ApplicationDbContext context, UserManager<Utilisateur> gestionnaire)
        {
            this.context = context;
            this.gestionnaire = gestionnaire;
        }

        private Task<Utilisateur> GetCurrentUserAsync() => this.gestionnaire.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<string> RecupererIdUtilisateurCourant()
        {
            Utilisateur utilisateur = await GetCurrentUserAsync();
            return utilisateur?.Id;
        }

        public async Task<IActionResult> Index()
        {
            var id = await RecupererIdUtilisateurCourant();
            var filmsUtilisateur = this.context.FilmsUtilisateur.Where(x => x.IdUtilisateur == id);
            var modele = filmsUtilisateur.Select(x => new FilmViewModel
            {
                IdFilm = x.IdFilm,
                Titre = x.Film.Titre,
                AnneeDeSortie = x.Film.AnneeDeSortie,
                Vu = x.Vu,
                PresentDansListe = true,
                Note = x.Note
            }).ToList();
            return this.View(modele);
        }
    }
}
