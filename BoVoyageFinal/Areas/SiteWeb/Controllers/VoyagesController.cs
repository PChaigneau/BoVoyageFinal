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
    public class VoyagesController : Controller
    {
        private readonly BoVoyageContext _context;

        public VoyagesController(BoVoyageContext context)
        {
            _context = context;
        }

        
        // GET: SiteWeb/Voyages
        public async Task<IActionResult> Index()
        {
            var boVoyageContext = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo);
            return View(await boVoyageContext.ToListAsync());
        }
        
        /*
        // GET: SiteWeb/Voyages
        public async Task<IActionResult> Index(string sortOrder)
        {
            IQueryable<Voyage> reqVoyages = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo);

            // si la chaîne de tri reçue en paramère est vide,
            // on définit un critère de tri par défaut
            if (string.IsNullOrEmpty(sortOrder)) sortOrder = "Destination";

            // On applique le tri à la requête
            switch (sortOrder)
            {
                case "Destination":
                    reqVoyages = reqVoyages.OrderBy(v => v.IdDestinationNavigation);
                    break;
                case "Destination_desc":
                    reqVoyages = reqVoyages.OrderByDescending(v => v.IdDestinationNavigation);
                    break;
                case "Date_depart":
                    reqVoyages = reqVoyages.OrderByDescending(v => v.DateDepart);
                    break;
                case "Date_depart_desc":
                    reqVoyages = reqVoyages.OrderByDescending(v => v.DateDepart);
                    break;
                case "Prix":
                    reqVoyages = reqVoyages.OrderBy(v => v.PrixHt);
                    break;
                case "Prix_desc":
                    reqVoyages = reqVoyages.OrderByDescending(v => v.PrixHt);
                    break;
            }

            // Pour chaque critère, on envoie le sens du tri inverse
            // à la vue pour le prochain appel de la méthode
            ViewData["TriParDestination"] = sortOrder == "Destination" ? "Destination_desc" : "Destination";
            ViewData["TriParDate_depart"] = sortOrder == "Date_depart" ? "Date_depart_desc" : "Date_depart";
            ViewData["TriParPrix"] = sortOrder == "Prix" ? "Prix_desc" : "Prix";

            // On récupère les données triées
            var voyages = await reqVoyages.AsNoTracking().ToListAsync();

            return View(voyages);
        }
        */

        // GET: SiteWeb/Voyages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage
                .Include(v => v.IdDestinationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }
    }
}
