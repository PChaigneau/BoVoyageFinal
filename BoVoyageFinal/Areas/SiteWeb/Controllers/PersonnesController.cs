using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyageFinal.Models;

namespace BoVoyageFinal.Areas.SiteWeb.Controllers
{
    [Area("SiteWeb")]
    public class PersonnesController : Controller
    {
        private readonly BoVoyageContext _context;

        public PersonnesController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: SiteWeb/Personnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }


        // GET: SiteWeb/Personnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personne.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }
            return View(personne);
        }

        // GET: SiteWeb/Personnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteWeb/Personnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypePers,Civilite,Nom,Prenom,Email,Telephone,Datenaissance")] Personne personne)
        {
            personne.TypePers = 1; // Pour definir que c'est un client : TypePers =1.
            if (ModelState.IsValid)
            {
                _context.Add(personne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }


        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.Id == id);
        }
    }
}
