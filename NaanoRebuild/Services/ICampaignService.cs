using NaanoMVP.Models;

namespace NaanoMVP.Services
{
    public interface ICampaignService
    {
        List<Campaign> GetAllCampaigns();
        Campaign? GetById(int id);
        Campaign CreateCampaign(Campaign campaign, List<int> creatorIds);
        void AddCreatorToCampaign(int campaignId, int creatorId);
        bool IsCreatorInCampaign(int campaignId, int creatorId);
        List<Creator> GetCreatorsForCampaign(int campaignId);
    }
}
