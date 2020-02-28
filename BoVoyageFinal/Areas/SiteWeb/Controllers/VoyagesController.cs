using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyageFinal.Models;
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        // GET: SiteWeb/Voyages
        public async Task<IActionResult> Index(string destination, DateTime depart, DateTime retour, decimal prixMin, decimal prixMax)
        {
            //Affectation par propriétés dynamiques des valeurs à retransmettre inchangées à la vue                     
            ViewBag.Retour = retour.ToString("yyyy-MM-dd");
            ViewBag.PrixMin = prixMin;
            ViewBag.PrixMax = prixMax;

            //Chargement des voyages avec destinations et photos associées
            var voyagesReq = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).AsNoTracking();

            //Si le filtre par destination est vide, on affecte une chaîne vide à la variable destination
            if (destination==null){ destination = String.Empty; }
            //Par défaut, le premier champ de date est mis à la date du jour
            if (depart == DateTime.MinValue)
            { 
                depart = DateTime.Today; 
            }

            //Affectation par propriétés dynamiques des valeurs modifiées par l'action à retransmettre à la vue
            ViewBag.Destination = destination;
            ViewBag.Depart = depart.ToString("yyyy-MM-dd");


            //Si retour<depart (comprend le cas où retour n'est pas renseigné), retour prend la plus haute valeur possible
            if (retour < depart )
            { retour = DateTime.MaxValue; }
            //Si pas de PrixMax renseigné, PrixMax prend la plus haute valeur possible
            if(prixMax==0){prixMax=decimal.MaxValue;}
            

            //On récuprère les voyages...
            voyagesReq = voyagesReq.
            //...dont le nom de destination contient la chaîne cherchée (éventuellement vide),
            Where(v => v.IdDestinationNavigation.Nom.Contains(destination)
            //...dont la date de départ se situe entre les dates de depart et retour renseignées ou définies par défaut,
            && ((v.DateDepart>=depart) && (v.DateDepart<=retour))
            //...et dont le prix se situe dans la fourchette de prix renseignée ou définie par défaut
            && ((v.PrixHt>=prixMin) && (v.PrixHt<=prixMax))
            ); 

            List<Voyage> voyages = await voyagesReq.ToListAsync();

            return View(voyages);
        }

        //public async Task<IActionResult> Book(int? id)
        //{ 
            
        //}


        [AllowAnonymous]
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
