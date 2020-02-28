using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoVoyageFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BoVoyageFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BoVoyageContext _context;

        public HomeController(ILogger<HomeController> logger, BoVoyageContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.Top5Prix = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).OrderBy(v => v.PrixHt).Take(5).ToList();
            vm.Top5DateDepart = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).OrderBy(v => v.DateDepart).Take(5).ToList();
            vm.Top5Destination = _context.Destination.OrderBy(v => v.Nom).Take(5).ToList();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact(int id, string nom)
        {

            return View();
        }
        public IActionResult About()
        {


            return View();
        }
    }
}
