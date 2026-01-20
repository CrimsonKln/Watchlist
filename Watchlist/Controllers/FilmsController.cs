// <copyright file="FilmsController.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Watchlist.Data;
    using Watchlist.Models;

    [Authorize]
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Utilisateur> gestionnaire;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmsController"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="gestionnaire">User manager.</param>
        public FilmsController(ApplicationDbContext context, UserManager<Utilisateur> gestionnaire)
        {
            this.context = context;
            this.gestionnaire = gestionnaire;
        }

        private Task<Utilisateur> GetCurrentUserAsync() => this.gestionnaire.GetUserAsync(this.HttpContext.User);

        [HttpGet]
        public async Task<string?> RecupererIdUtilisateurCourant()
        {
            Utilisateur utilisateur = await this.GetCurrentUserAsync();
            return utilisateur?.Id;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var idUtilisateur = await this.RecupererIdUtilisateurCourant();
            var vm = await this.context.Films.Select(x => new FilmViewModel
            {
                IdFilm = x.Id,
                Titre = x.Titre,
                AnneeDeSortie = x.AnneeDeSortie,
            }).ToListAsync();

            foreach (var item in vm)
            {
                var m = await this.context.FilmsUtilisateur.FirstOrDefaultAsync(x =>
                           x.UtilisateurId == idUtilisateur && x.FilmId == item.IdFilm);
                if (m != null)
                {
                    item.PresentDansListe = true;
                    item.Note = m.Note;
                    item.Vu = m.Vu;
                }
            }

            return this.View(vm);
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var film = await this.context.Films
                .Include(f => f.Realisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return this.NotFound();
            }

            var vm = new FilmDetailsViewModel
            {
                Id = film.Id,
                Titre = film.Titre,
                AnneeDeSortie = film.AnneeDeSortie,
            };

            if (film.RealisateurId is not null)
            {
                vm.RealisateurNom = film.Realisateur?.Nom;
            }

            return this.View(vm);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,AnneeDeSortie,RealisateurId")] Film film)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(film);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var film = await this.context.Films
                .Include(f => f.Realisateur)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (film == null)
            {
                return this.NotFound();
            }

            var vm = new FilmEditViewModel
            {
                Id = film.Id,
                Titre = film.Titre,
                AnneeDeSortie = film.AnneeDeSortie,
                RealisateurId = film.RealisateurId,
                RealisateurNom = film.Realisateur?.Nom,
            };

            return this.View(vm);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Watchlist.Models.FilmEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return this.NotFound();
            }

            var film = await this.context.Films.FindAsync(id);
            if (film == null)
            {
                return this.NotFound();
            }

            film.Titre = vm.Titre;
            film.AnneeDeSortie = vm.AnneeDeSortie;
            film.RealisateurId = vm.RealisateurId;

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(film);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.FilmExists(film.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var film = await this.context.Films
                .Include(f => f.Realisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return this.NotFound();
            }

            var vm = new FilmDeleteViewModel
            {
                Id = film.Id,
                Titre = film.Titre,
                AnneeDeSortie = film.AnneeDeSortie,
            };

            if (film.RealisateurId is not null)
            {
                vm.RealisateurNom = film.Realisateur?.Nom;
            }

            return this.View(vm);
        }

        // POST: Films/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await this.context.Films.FindAsync(id);
            if (film != null)
            {
                this.context.Films.Remove(film);
            }

            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult SearchRealisateurs(string term)
        {
            var realisateurs = this.context.Realisateurs
                .Where(r => r.Nom.Contains(term))
                .Select(r => new
                {
                    id = r.Id,
                    text = r.Nom,
                })
                .Take(20)
                .ToList();

            return this.Json(realisateurs);
        }

        [HttpGet]
        public async Task<IActionResult> AjouterSupprimer(int idFilm, int val)
        {
            int answer = -1;
            var idUtilisateur = await this.RecupererIdUtilisateurCourant();
            if (val == 0)
            {
                this.context.FilmsUtilisateur.Add(new FilmUtilisateur
                {
                    FilmId = idFilm,
                    UtilisateurId = idUtilisateur,
                    Vu = false,
                    Note = 0,
                });
                answer = 1;
            }
            else
            {
                var filmUtilisateur = await this.context.FilmsUtilisateur.FindAsync(idUtilisateur, idFilm);
                if (filmUtilisateur != null)
                {
                    this.context.FilmsUtilisateur.Remove(filmUtilisateur);
                    answer = 0;
                }
            }

            await this.context.SaveChangesAsync();
            return this.Json(answer);
        }

        private bool FilmExists(int id)
        {
            return this.context.Films.Any(e => e.Id == id);
        }
    }
}
