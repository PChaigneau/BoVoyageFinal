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
        const string SessionKeyModel = "_model";

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

        public async Task<IActionResult> BookIndex(int id, ResaViewModel currentResa)
        {

            //Récupération du voyage avec sa destiantion à partir de l'id :
            Voyage voyage = await _context.Voyage.Include(v => v.IdDestinationNavigation).SingleOrDefaultAsync(v => v.Id == id);

            //On instancie un modèle pour vue avec l'objet éventuellement mémorisé pour cette session
            ResaViewModel resa = HttpContext.Session.Get<ResaViewModel>("_model");

            //Si aucun objet n'était mémorisé pour cette session, on initialise le modèle pour vue en lui affectant la formule selectionnée par l'utilisateur
            if (resa == null)
            {
                resa = new ResaViewModel();
                resa.VoyageId = id;
                resa.NomDestination = voyage.IdDestinationNavigation.Nom;
            }


            //On affecte un éventuel participant enregistré via la vue modale à la liste des participants
            if (currentResa.CurrentPerson != null)
                resa.Participants.Add(currentResa.CurrentPerson);

            //Mémorisation du modèle pour la session en cours
            HttpContext.Session.Set("_model", resa);

            //Calcul du prix total en fonction du nombre de participants et du montant du taux de réduction
            int nbParticipants = resa.Participants.Count();
            decimal prixTotalHTr = 0M;
            decimal prixTotalHT = nbParticipants * voyage.PrixHt;
            if (voyage.Reduction!=0)
            {
             prixTotalHTr = nbParticipants * voyage.PrixHt * (1 - voyage.Reduction);
            }
            else
            {
                prixTotalHTr = nbParticipants * voyage.PrixHt;
            }
            decimal prixTTC = prixTotalHTr*1.2M;
            ViewBag.Nbr = nbParticipants;
            ViewBag.PrixTHT = Math.Round(prixTotalHT,2);
            ViewBag.Reduction = (int)(voyage.Reduction*100);
            ViewBag.PrixTHTr = Math.Round(prixTotalHTr,2) ;
            ViewBag.TVA = Math.Round(prixTTC - prixTotalHTr, 2);
            ViewBag.PrixTTC = Math.Round(prixTTC, 2);

            return View(resa);
        }

        public IActionResult Paiement()
        {

            return View();
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
