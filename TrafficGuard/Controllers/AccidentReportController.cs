using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TrafficGuard.Data;
using TrafficGuard.Models;
using TrafficGuard.Services;

namespace TrafficGuard.Controllers
{
    public class AccidentReportController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public AccidentReportController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Error = null;
            AccidentReport report = new AccidentReport();
            return View(report);
        }

        [HttpPost]
        public IActionResult Create(AccidentReport report)
        {
            ViewBag.Error = null;
            try
            {
                ValidateModelService.CheckModel(report);

                report.Location = new Models.Location();
                report.Location.Latitude = report.Latitude;
                report.Location.Longitude = report.Longitude;
                report.Location.DistrictId = 1;

                Accident accident = new Accident();
                accident.Location = report.Location;
                accident.DateTime = report.DateTime;
                accident.NumVehicles = report.NumVehicles;
                accident.Description = report.Description;
                accident.PathToFiles = report.PathToFiles;
                accident.TrustWorthyRating = report.TrustWorthyRating;

                _dbContext.Attach(accident);
                _dbContext.Entry(accident).State = EntityState.Added;
                _dbContext.SaveChanges();

                TempData["msg"] = "Accident reported!";
                return View(new AccidentReport());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(report);
            }
        }
    }
}
