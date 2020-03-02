using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoVoyageFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

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
        [AllowAnonymous]
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.Top5Prix = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).OrderBy(v => v.PrixHt).Take(5).ToList();
            vm.Top5DateDepart = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).OrderBy(v => v.DateDepart).Take(5).ToList();
            //vm.Top5Destination = _context.Destination.Include(d => d.Photo).OrderBy(v => v.Nom).Take(5).ToList();

            vm.Top5Destination = _context.Destination.FromSqlRaw(@"select distinct top(5) d.Id,d.Nom, count(*) as IdParente, cast (0 as tinyint) as Niveau, '' as Description from Voyage v
                                                                   inner join Destination d on d.Id = v.IdDestination
                                                                   group by d.Id, d.Nom
                                                                   order by Niveau desc").ToList(); 

            return View(vm);  
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {

            return View();
        }
        [AllowAnonymous]
        public IActionResult About()
        {


            return View();
        }
        public IActionResult Addrole()
        {


            return View();
        }
        public IActionResult ListRoles()
        {


            return View();
        }
    }
}
