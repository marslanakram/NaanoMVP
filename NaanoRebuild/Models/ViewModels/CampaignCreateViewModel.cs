using System.ComponentModel.DataAnnotations;

namespace NaanoMVP.Models.ViewModels
{
    public class CampaignCreateViewModel
    {
        [Required(ErrorMessage = "Please enter a campaign name.")]
        [StringLength(150)]
        [Display(Name = "Campaign Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select an objective.")]
        [StringLength(100)]
        public string Objective { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please describe your target audience.")]
        [StringLength(150)]
        [Display(Name = "Target Audience")]
        public string Audience { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a campaign message.")]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a budget.")]
        [Range(1, 10000000, ErrorMessage = "Budget must be greater than 0.")]
        public decimal Budget { get; set; }

        // Pre-selected creator to attach once the campaign is created (from "Add to Campaign" flow)
        public int? PreselectedCreatorId { get; set; }

        public List<int> SelectedCreatorIds { get; set; } = new();

        public List<Creator> AvailableCreators { get; set; } = new();
    }
}
