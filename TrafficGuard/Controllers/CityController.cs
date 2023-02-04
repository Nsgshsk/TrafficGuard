using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrafficGuard.Data;
using TrafficGuard.Models;
using TrafficGuard.Services;

namespace TrafficGuard.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public CityController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            PagerManager.ControllerType = "City";

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Cities.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize; 

            List<City> cities = _dbContext.Cities.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(cities);
        }

        public IActionResult Details(int id)
        {
            City? city = _dbContext.Cities.Find(id);
            return View(city);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            City? city = _dbContext.Cities.Find(id);
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(City city)
        {
            _dbContext.Attach(city);
            _dbContext.Entry(city).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            City? city = _dbContext.Cities.Find(id);
            return View(city);
        }

        [HttpPost]
        public IActionResult Delete(City city)
        {
            _dbContext.Attach(city);
            _dbContext.Entry(city).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            City city = new City();
            return View(city);
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            //city.Id = _dbContext.Cities.Max(x => x.Id) + 1;

            _dbContext.Attach(city);
            _dbContext.Entry(city).State = EntityState.Added;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
