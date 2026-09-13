using Microsoft.AspNetCore.Mvc;
using NaanoMVP.Models;
using NaanoMVP.Models.ViewModels;
using NaanoMVP.Services;

namespace NaanoMVP.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ICampaignService _campaignService;
        private readonly ICreatorService _creatorService;

        public CampaignsController(ICampaignService campaignService, ICreatorService creatorService)
        {
            _campaignService = campaignService;
            _creatorService = creatorService;
        }

        // Step shown from a Creator Profile: choose an existing campaign or start a new one
        [HttpGet]
        public IActionResult AddToCampaign(int creatorId)
        {
            var creator = _creatorService.GetById(creatorId);
            if (creator == null)
            {
                return NotFound();
            }

            var vm = new AddToCampaignViewModel
            {
                Creator = creator,
                ExistingCampaigns = _campaignService.GetAllCampaigns()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachToExistingCampaign(int campaignId, int creatorId)
        {
            _campaignService.AddCreatorToCampaign(campaignId, creatorId);
            TempData["SuccessMessage"] = "Creator added to campaign successfully.";
            return RedirectToAction("Details", new { id = campaignId });
        }

        [HttpGet]
        public IActionResult Create(int? preselectedCreatorId)
        {
            var vm = new CampaignCreateViewModel
            {
                AvailableCreators = _creatorService.GetCreators(null, null, null, null),
                PreselectedCreatorId = preselectedCreatorId
            };

            if (preselectedCreatorId.HasValue)
            {
                vm.SelectedCreatorIds.Add(preselectedCreatorId.Value);
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CampaignCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AvailableCreators = _creatorService.GetCreators(null, null, null, null);
                return View(vm);
            }

            var campaign = new Campaign
            {
                Name = vm.Name,
                Objective = vm.Objective,
                Audience = vm.Audience,
                Message = vm.Message,
                Budget = vm.Budget
            };

            var created = _campaignService.CreateCampaign(campaign, vm.SelectedCreatorIds);

            TempData["SuccessMessage"] = $"Campaign \"{created.Name}\" created successfully.";
            return RedirectToAction("Details", new { id = created.Id });
        }

        public IActionResult Details(int id)
        {
            var campaign = _campaignService.GetById(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }
    }
}
