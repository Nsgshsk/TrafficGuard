using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrafficGuard.Data;
using TrafficGuard.Models;
using TrafficGuard.Services;

namespace TrafficGuard.Controllers
{
    public class DistrictController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public DistrictController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            PagerManager.ControllerType = "District";

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Districts.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            List<District> cities = _dbContext.Districts.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(cities);
        }

        public IActionResult Details(int id)
        {
            District district = _dbContext.Districts.Find(id);
            return View(district);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            District district = _dbContext.Districts.Find(id);
            return View(district);
        }

        [HttpPost]
        public IActionResult Edit(District district)
        {
            _dbContext.Attach(district);
            _dbContext.Entry(district).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            District district = _dbContext.Districts.Find(id);
            return View(district);
        }

        [HttpPost]
        public IActionResult Delete(District district)
        {
            _dbContext.Attach(district);
            _dbContext.Entry(district).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.ViewBag.City = new SelectList(_dbContext.Cities, "Id", "Id");
            District district = new District();
            return View(district);
        }

        [HttpPost]
        public IActionResult Create(District district)
        {
            //district.CityId = _dbContext.Cities.Find(district.CityId).Id;

            _dbContext.Attach(district);
            _dbContext.Entry(district).State = EntityState.Added;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
