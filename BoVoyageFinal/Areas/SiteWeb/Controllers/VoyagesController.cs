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
