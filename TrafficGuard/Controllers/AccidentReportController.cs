using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Text.Json;
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
            TempData["error"] = String.Empty;
            TempData["success"] = false;

            AccidentReport report = new AccidentReport();

            return View(report);
        }

        [HttpPost]
        public IActionResult Create(AccidentReport report)
        {
            TempData["error"] = String.Empty;
            TempData["success"] = false;

            try
            {
                if (report.Json == default && (report.Latitude == default || report.Longitude == default)) throw new ArgumentException("Location was not selected!"); 

                var addressJson = JsonSerializer.Deserialize<Root>(report.Json!)!.address;

                string region = addressJson!.Region!;
                string subregion = addressJson.Subregion!;
                string city = addressJson.City!;
                string district = addressJson.District!;
                string neighborhood = addressJson.Neighborhood!;
                string addressString = addressJson.Address!;

                ValidateModelService.CheckModel(report);

                report.Location = new Models.Location();
                report.Location.Latitude = report.Latitude;
                report.Location.Longitude = report.Longitude;
                
                if (_dbContext.Districts.Any(e => e.Name == district) && !String.IsNullOrWhiteSpace(district))
                    report.Location.DistrictId = _dbContext.Districts.Where(e => e.Name == district).First().Id;

                else if (_dbContext.Districts.Any(e => e.Name == neighborhood) && !String.IsNullOrWhiteSpace(neighborhood))
                    report.Location.DistrictId = _dbContext.Districts.Where(e => e.Name == addressString).First().Id;

                else if (_dbContext.Districts.Any(e => e.Name == addressString) && !String.IsNullOrWhiteSpace(addressString))
                    report.Location.DistrictId = _dbContext.Districts.Where(e => e.Name == addressString).First().Id;

                else
                {
                    report.Location.District = new District();

                    if (!String.IsNullOrWhiteSpace(district)) report.Location.District.Name = district;
                    else if (!String.IsNullOrWhiteSpace(neighborhood)) report.Location.District.Name = neighborhood;
                    else if (!String.IsNullOrWhiteSpace(addressString)) report.Location.District.Name = addressString;
                    else report.Location.District.Name = String.Empty;

                    if (_dbContext.Cities.Any(e => e.Name == city) && !String.IsNullOrWhiteSpace(city))
                        report.Location.District.CityId = _dbContext.Cities.Where(e => e.Name == city).First().Id;

                    else if (_dbContext.Cities.Any(e => e.Name == subregion) && !String.IsNullOrWhiteSpace(subregion))
                        report.Location.District.CityId = _dbContext.Cities.Where(e => e.Name == subregion).First().Id;

                    else if (_dbContext.Cities.Any(e => e.Name == region) && !String.IsNullOrWhiteSpace(region))
                        report.Location.District.CityId = _dbContext.Cities.Where(e => e.Name == region).First().Id;

                    else
                    {
                        report.Location.District.City = new City();

                        if (!String.IsNullOrWhiteSpace(city)) report.Location.District.City.Name = city;
                        else if (!String.IsNullOrWhiteSpace(subregion)) report.Location.District.City.Name = subregion;
                        else if (!String.IsNullOrWhiteSpace(region)) report.Location.District.City.Name = region;
                        else report.Location.District.City.Name = String.Empty;
                    }
                }

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

                TempData["success"] = true;
                return View(new AccidentReport());
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Error with creating the report!";
                return View(report);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(report);
            }
        }
    }
}
