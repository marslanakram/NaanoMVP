using Microsoft.AspNetCore.Mvc;
using NaanoMVP.Services;

namespace NaanoMVP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreatorService _creatorService;

        public HomeController(ICreatorService creatorService)
        {
            _creatorService = creatorService;
        }

        public IActionResult Index()
        {
            var featured = _creatorService.GetCreators(null, null, null, "engagement_desc").Take(4).ToList();
            ViewBag.TotalCreators = _creatorService.GetTotalCreatorCount();
            return View(featured);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
