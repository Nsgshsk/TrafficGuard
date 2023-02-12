using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrafficGuard.Data;
using TrafficGuard.Models;

namespace TrafficGuard.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(TrafficManagerAccidentDBContext dbContext, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var accidents = _dbContext.Accidents.Where(e => e.TrustWorthyRating > 0).ToList();
            accidents.ForEach(accident => { accident.Location = _dbContext.Locations.Find(accident.LocationId); });
            ViewBag.Accidents = accidents;
            return View();
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
    }
}