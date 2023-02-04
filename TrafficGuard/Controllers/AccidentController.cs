using Microsoft.AspNetCore.Mvc;
using TrafficGuard.Data;
using TrafficGuard.Models;
using TrafficGuard.Services;

namespace TrafficGuard.Controllers
{
    public class AccidentController : Controller
    {
        private readonly TrafficManagerAccidentDBContext _dbContext;

        public AccidentController(TrafficManagerAccidentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int pg = 1)
        {
            PagerManager.ControllerType = "Accident";

            const int pageSize = 3;
            if (pg < 1) pg = 1;

            int recsCount = _dbContext.Accidents.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            List<Accident> cities = _dbContext.Accidents.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(cities);
        }
    }
}
