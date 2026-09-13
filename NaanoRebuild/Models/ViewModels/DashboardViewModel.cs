namespace NaanoMVP.Models.ViewModels
{
    public class DashboardCampaignRow
    {
        public int CampaignId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public decimal Spend { get; set; }
        public int CreatorCount { get; set; }
        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public int Leads { get; set; }
    }

    public class DashboardViewModel
    {
        public int TotalCampaigns { get; set; }
        public int TotalCreators { get; set; }
        public int TotalImpressions { get; set; }
        public int TotalClicks { get; set; }
        public int TotalLeads { get; set; }
        public decimal TotalSpend { get; set; }

        public List<DashboardCampaignRow> Campaigns { get; set; } = new();
    }
}
