using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrafficGuard.Data;
using TrafficGuard.Models;
using TrafficGuard.Services;

namespace TrafficGuard.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public LocationController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            Pager.ControllerType = "Location";

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Locations.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            List<Location> locations = _dbContext.Locations.Skip(recSkip).Take(pager.PageSize).ToList();
            locations.ForEach(e => e.District = _dbContext.Districts.Find(e.DistrictId));

            this.ViewBag.Pager = pager;

            return View(locations);
        }

        public IActionResult Details(int id)
        {
            Location location = _dbContext.Locations.Find(id);
            location.District = _dbContext.Districts.Find(location.DistrictId);
            return View(location);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            this.ViewBag.DistrictName = new SelectList(_dbContext.Districts, "Name", "Name");
            Location location = _dbContext.Locations.Find(id);
            location.District = _dbContext.Districts.Find(location.DistrictId);
            return View(location);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            location.District = _dbContext.Districts.Where(e => e.Name == location.District.Name).FirstOrDefault();
            location.DistrictId = location.District.Id;

            _dbContext.Attach(location);
            _dbContext.Entry(location).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Location location = _dbContext.Locations.Find(id);
            location.District = _dbContext.Districts.Find(location.DistrictId);
            return View(location);
        }

        [HttpPost]
        public IActionResult Delete(Location location)
        {
            _dbContext.Attach(location);
            _dbContext.Entry(location).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Error = null;
            this.ViewBag.DistrictName = new SelectList(_dbContext.Districts, "Name", "Name");
            Location location = new Location();
            location.District = new District();
            return View(location);
        }

        [HttpPost]
        public IActionResult Create(Location location)
        {
            ViewBag.Error = null;
            location.District = _dbContext.Districts.Where(e => e.Name == location.District.Name).FirstOrDefault();
            try
            {
                ValidateModelService.CheckModel(location);
                location.DistrictId = location.District.Id;

                _dbContext.Attach(location);
                _dbContext.Entry(location).State = EntityState.Added;
                _dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                this.ViewBag.DistrictName = new SelectList(_dbContext.Districts, "Name", "Name");
                return View(location);
            }
        }
    }
}
