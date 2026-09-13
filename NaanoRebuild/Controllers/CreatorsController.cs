using Microsoft.AspNetCore.Mvc;
using NaanoMVP.Models.ViewModels;
using NaanoMVP.Services;

namespace NaanoMVP.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly ICreatorService _creatorService;
        private readonly ICampaignService _campaignService;

        public CreatorsController(ICreatorService creatorService, ICampaignService campaignService)
        {
            _creatorService = creatorService;
            _campaignService = campaignService;
        }

        public IActionResult Index(string? search, string? category, string? platform, string? sortBy)
        {
            var vm = new CreatorsIndexViewModel
            {
                Creators = _creatorService.GetCreators(search, category, platform, sortBy),
                Categories = _creatorService.GetCategories(),
                Platforms = _creatorService.GetPlatforms(),
                Search = search,
                Category = category,
                Platform = platform,
                SortBy = sortBy
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var creator = _creatorService.GetById(id);
            if (creator == null)
            {
                return NotFound();
            }

            return View(creator);
        }
    }
}
