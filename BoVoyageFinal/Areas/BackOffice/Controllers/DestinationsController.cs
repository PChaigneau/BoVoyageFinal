﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyageFinal.Models;
using Microsoft.AspNetCore.Authorization;



namespace BoVoyageFinal.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    //[AllowAnonymous]
    [Area("BackOffice")]
    public class DestinationsController : Controller
    {
        private readonly BoVoyageContext _context;

        public DestinationsController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Destinations
        public async Task<IActionResult> Index()
        {
            var boVoyageContext = await _context.Destination.ToListAsync();
            return View(boVoyageContext);
        }

        // GET: BackOffice/Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination
                .Include(d => d.IdParenteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // GET: BackOffice/Destinations/Create
        public IActionResult Create()
        {
            //ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom");
            //ViewData["IdParente"] = new SelectList(_context.Destination.Where(d => d.Niveau < 3).OrderBy(d => d.Nom), "Id", "Nom");

            // Essai
            var destinationParente = _context.Destination.Where(d => d.Niveau < 3).OrderBy(d => d.Nom).AsNoTracking().ToList();
            ViewData["IdParente"] = new SelectList(destinationParente, "Id", "Nom");

            return View();
        }

        // POST: BackOffice/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdParente,Nom,Niveau,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom", destination.IdParente);
            // L'objectif de la requête ci-dessous serait de n'afficher que les IdParente de destinations de niveau inférieur à 3, donc pas les régions
            //ViewData["IdParente"] = new SelectList(_context.Destination.Where(d => d.Niveau < 3).OrderBy(d => d.Nom), "Id", "Nom", destination.IdParente);

            // Essai
            var destinationParente = await _context.Destination.Where(d => d.Niveau < 3).OrderBy(d => d.Nom).AsNoTracking().ToListAsync();
            ViewData["IdParente"] = new SelectList(destinationParente, "Id", "Nom", destination.IdParente);

            return View(destination);
        }

        // GET: BackOffice/Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            ViewData["IdParente"] = new SelectList(_context.Destination, "Id", "Nom", destination.IdParente);
            return View(destination);
        }

        // POST: BackOffice/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdParente,Nom,Niveau,Description")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdParente"] = new SelectList(_context.Destination.AsNoTracking(), "Id", "Nom", destination.IdParente);
            return View(destination);
        }

        // GET: BackOffice/Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destination
                .Include(d => d.IdParenteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: BackOffice/Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destination = await _context.Destination.FindAsync(id);
            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.Id == id);
        }
    }
}
