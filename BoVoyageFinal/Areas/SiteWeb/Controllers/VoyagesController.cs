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
        public async Task<IActionResult> Index(string destination, DateTime depart, DateTime retour, double PrixMin, double PrixMax)
        {
            //Chargement des voyages avec destinations et photos associées
            var VoyagesReq = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).AsNoTracking();

            //Si le filtre par destination est vide, on affecte une chaîne vide à la variable destination
            if (destination==null){ destination = String.Empty; }
            //Par défaut, le premier champ de date est à la date du jour
            if (depart == DateTime.MinValue)
            { depart = DateTime.Today; }
            //Si pas de retour, retour = dateMax
            if (retour == DateTime.MinValue)
            { retour = DateTime.MaxValue; }

            //On récuprère les voyages...
            VoyagesReq = VoyagesReq.
            //...dont le nom de destination contient la chaîne cherchée (éventuellement vide)
            Where(v => v.IdDestinationNavigation.Nom.Contains(destination));
            //...

            //On affecte un ViewBag avec les valeurs à afficher dans le formulaire
            ViewBag.Destination = destination;
            ViewBag.Depart = depart.ToString("yyyy-MM-dd");

            List<Voyage> Voyages = await VoyagesReq.ToListAsync();

            return View(Voyages);
        }


        // GET: SiteWeb/Voyages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }
    }
}
