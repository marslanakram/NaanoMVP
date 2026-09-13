namespace NaanoMVP.Models.ViewModels
{
    public class AddToCampaignViewModel
    {
        public Creator Creator { get; set; } = null!;
        public List<Campaign> ExistingCampaigns { get; set; } = new();
    }
}
