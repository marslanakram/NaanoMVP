using System.ComponentModel.DataAnnotations;

namespace NaanoMVP.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Objective { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Audience { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Range(1, 10000000)]
        public decimal Budget { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(30)]
        public string Status { get; set; } = "Active";

        public ICollection<CampaignCreator> CampaignCreators { get; set; } = new List<CampaignCreator>();
        public ICollection<CampaignMetric> CampaignMetrics { get; set; } = new List<CampaignMetric>();
    }
}
