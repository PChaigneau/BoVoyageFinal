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
        public async Task<IActionResult> Index(string destination)
        {
            //Déclaration des cookies
            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5)   // Le cookie expirera dans 5 minutes 
            };

            //Chargement des voyages avec destinations et photos associées
            var VoyagesReq = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).AsNoTracking();

            //Au premier chargement de la page, ou si le chps de recherche par destiantion est vide
            if (String.IsNullOrEmpty(destination))
            { 
                //Si une destination est mémorisée en cookie, on l'utilise
                if (Request.Cookies.TryGetValue("destination", out string savedDest)) 
                    { destination = savedDest; }
                //Sinon on mémorise une chaîne vide en cookie pour permettre
                //le filtrage des voyages selon d'autres critères
                else
                {Response.Cookies.Append("destination", "", options); }

            }
            //Si l'utilisateur filtre par le contenu de la description
            else
            {
                //on récuprère les voyages dont le nom de destination contient la chaîne cherchée
                VoyagesReq = VoyagesReq.Where(v => v.IdDestinationNavigation.Nom.Contains(destination));
                //on mémorise la chaîne de filtre dans  un cookie
                Response.Cookies.Append("destination", destination, options);
            }

            //On instancie les ViewBags qui servent à conserver dans la vue les valeurs entrées par l'utilisateur ou les valeurs par défaut
            ViewBag.Destination = destination;

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
