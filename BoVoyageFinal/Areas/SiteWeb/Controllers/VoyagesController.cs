using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyageFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BoVoyageFinal.Areas.SiteWeb.Controllers
{
    [Area("SiteWeb")]
    public class VoyagesController : Controller
    {
        private readonly BoVoyageContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VoyagesController(BoVoyageContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            if (destination == null) { destination = String.Empty; }
            //Par défaut, le premier champ de date est mis à la date du jour
            if (depart == DateTime.MinValue)
            {
                depart = DateTime.Today;
            }

            //Affectation par propriétés dynamiques des valeurs modifiées par l'action à retransmettre à la vue
            ViewBag.Destination = destination;
            ViewBag.Depart = depart.ToString("yyyy-MM-dd");


            //Si retour<depart (comprend le cas où retour n'est pas renseigné), retour prend la plus haute valeur possible
            if (retour < depart)
            { retour = DateTime.MaxValue; }
            //Si pas de PrixMax renseigné, PrixMax prend la plus haute valeur possible
            if (prixMax == 0) { prixMax = decimal.MaxValue; }


            //On récuprère les voyages...
            voyagesReq = voyagesReq.
            //...dont le nom de destination contient la chaîne cherchée (éventuellement vide),
            Where(v => v.IdDestinationNavigation.Nom.Contains(destination)
            //...dont la date de départ se situe entre les dates de depart et retour renseignées ou définies par défaut,
            && ((v.DateDepart >= depart) && (v.DateDepart <= retour))
            //...et dont le prix se situe dans la fourchette de prix renseignée ou définie par défaut
            && ((v.PrixHt >= prixMin) && (v.PrixHt <= prixMax))
            );

            List<Voyage> voyages = await voyagesReq.ToListAsync();

            return View(voyages);
        }

        public async Task<IActionResult> BookIndex(int id)
        {
            //Chargement de la formule selectionnée
            Voyage voyage = await _context.Voyage.Include(v => v.IdDestinationNavigation).SingleOrDefaultAsync(v => v.Id == id);
            //Récupération de l'email de l'utilisateur loggé
            var userMail = _userManager.GetUserName(HttpContext.User);
            //tester si le mail est présent dans une ligne de Personne et si oui  : récupérer l'id correspondant, créer un Voyageur
            var user = await _context.Personne.AsNoTracking().SingleOrDefaultAsync(p=>p.Email==userMail);
            int userId;
            int userClientId;
            int userVoyageurId;
            if(user != null)
            {
                userId = user.Id;
                userClientId = user.Client.Id;
                //créer un Voyageur
            }

            //initialiser une liset de participants commençant par le client voyageur. Requiert d'éditer le ResaViewModel pour qu'il 
            // corresponde au type Voyage + liste de voyageurs.

            if (voyage == null)
            {
                return NotFound();
            }

            ResaViewModel resa = new ResaViewModel();
            resa.Voyage = voyage;
            //resa.Voyageur =;



            return View(resa);
        }


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
