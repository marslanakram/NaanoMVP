namespace NaanoMVP.Models
{
    public class CampaignCreator
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public int CreatorId { get; set; }
        public Creator? Creator { get; set; }
    }
}
