using Microsoft.AspNetCore.Mvc;
using NaanoMVP.Services;

namespace NaanoMVP.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var model = _dashboardService.GetDashboardData();
            return View(model);
        }
    }
}
