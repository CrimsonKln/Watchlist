// <copyright file="FilmsController.cs" company="CrimsonKln">
// Copyright (c) CrimsonKln. All rights reserved.
// </copyright>

namespace Watchlist.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Watchlist.Data;

    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext context;

        public FilmsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var film = await this.context.Films
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return this.NotFound();
            }

            return this.View(film);
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
        public async Task<IActionResult> Create([Bind("Id,Titre,AnneeDeSortie")] Film film)
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

            var film = await this.context.Films.FindAsync(id);
            if (film == null)
            {
                return this.NotFound();
            }

            return this.View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,AnneeDeSortie")] Film film)
        {
            if (id != film.Id)
            {
                return this.NotFound();
            }

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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return this.NotFound();
            }

            return this.View(film);
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

        private bool FilmExists(int id)
        {
            return this.context.Films.Any(e => e.Id == id);
        }
    }
}
