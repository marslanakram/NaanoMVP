using System.ComponentModel.DataAnnotations;

namespace NaanoMVP.Models
{
    public class Creator
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        // Not an external URL - used to derive a CSS avatar (initials + gradient) in the views.
        public string? ProfileImage { get; set; }

        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Location { get; set; } = string.Empty;

        public int Followers { get; set; }

        public decimal EngagementRate { get; set; }

        public decimal Price { get; set; }

        [Required, StringLength(30)]
        public string Platform { get; set; } = string.Empty;

        public ICollection<CampaignCreator> CampaignCreators { get; set; } = new List<CampaignCreator>();
    }
}
