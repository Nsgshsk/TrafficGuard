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
    public class DistrictController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public DistrictController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            Pager.ControllerType = "District";

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Districts.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            List<District> districts = _dbContext.Districts.Skip(recSkip).Take(pager.PageSize).ToList();
            districts.ForEach(e => e.City = _dbContext.Cities.Find(e.CityId)!);

            this.ViewBag.Pager = pager;

            return View(districts);
        }

        public IActionResult Details(int id)
        {
            District district = _dbContext.Districts.Find(id)!;
            district.City = _dbContext.Cities.Find(district.CityId)!;
            return View(district);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            this.ViewBag.CityName = new SelectList(_dbContext.Cities, "Name", "Name");
            District district = _dbContext.Districts.Find(id)!;
            district.City = _dbContext.Cities.Find(district.CityId)!;
            return View(district);
        }

        [HttpPost]
        public IActionResult Edit(District district)
        {
            district.City = _dbContext.Cities.Where(e => e.Name == district.City.Name).FirstOrDefault()!;
            district.CityId = district.City.Id;

            _dbContext.Attach(district);
            _dbContext.Entry(district).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            District district = _dbContext.Districts.Find(id)!;
            district.City = _dbContext.Cities.Find(district.CityId)!;
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
            ViewBag.Error = null;
            ViewBag.CityName = new SelectList(_dbContext.Cities, "Name", "Name");
            District district = new District();
            district.City = new City();
            return View(district);
        }

        [HttpPost]
        public IActionResult Create(District district)
        {
            ViewBag.Error = null;
            district.City = _dbContext.Cities.Where(e => e.Name == district.City.Name).FirstOrDefault()!;

            try
            {
                ValidateModelService.CheckModel(district);

                district.CityId = district.City.Id;

                _dbContext.Attach(district);
                _dbContext.Entry(district).State = EntityState.Added;
                _dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                this.ViewBag.CityName = new SelectList(_dbContext.Cities, "Name", "Name");
                return View(district);
            }
        }
    }
}
