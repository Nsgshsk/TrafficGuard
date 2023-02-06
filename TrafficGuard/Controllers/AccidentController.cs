using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TrafficGuard.Data;
using TrafficGuard.Models;

namespace TrafficGuard.Controllers
{
    [Authorize]
    public class AccidentController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public AccidentController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            Pager.ControllerType = "Accident";

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Accidents.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            List<Accident> accidents = _dbContext.Accidents.Skip(recSkip).Take(pager.PageSize).ToList();
            accidents.ForEach(e => e.Location = _dbContext.Locations.Find(e.LocationId));

            this.ViewBag.Pager = pager;

            return View(accidents);
        }

        public IActionResult Details(int id)
        {
            Accident accident = _dbContext.Accidents.Find(id);
            accident.Location = _dbContext.Locations.Find(accident.LocationId);
            return View(accident);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            this.ViewBag.LocationId = new SelectList(_dbContext.Locations, "Id", "Id");
            Accident accident = _dbContext.Accidents.Find(id);
            accident.Location = _dbContext.Locations.Find(accident.LocationId);
            return View(accident);
        }

        [HttpPost]
        public IActionResult Edit(Accident accident)
        {
            _dbContext.Attach(accident);
            _dbContext.Entry(accident).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Accident accident = _dbContext.Accidents.Find(id);
            accident.Location = _dbContext.Locations.Find(accident.LocationId);
            return View(accident);
        }

        [HttpPost]
        public IActionResult Delete(Accident accident)
        {
            _dbContext.Attach(accident);
            _dbContext.Entry(accident).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.ViewBag.LocationId = new SelectList(_dbContext.Locations, "Id", "Id");
            Accident accident = new Accident();
            return View(accident);
        }

        [HttpPost]
        public IActionResult Create(Accident accident)
        {
            _dbContext.Attach(accident);
            _dbContext.Entry(accident).State = EntityState.Added;
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
