namespace NaanoMVP.Models
{
    public class CampaignMetric
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public int Leads { get; set; }
        public decimal Spend { get; set; }
    }
}
