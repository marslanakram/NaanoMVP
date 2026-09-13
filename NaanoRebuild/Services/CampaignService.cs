using NaanoMVP.Data;
using NaanoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace NaanoMVP.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ApplicationDbContext _context;

        public CampaignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Campaign> GetAllCampaigns()
        {
            return _context.Campaigns
                .Include(c => c.CampaignCreators)
                .Include(c => c.CampaignMetrics)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public Campaign? GetById(int id)
        {
            return _context.Campaigns
                .Include(c => c.CampaignCreators)
                    .ThenInclude(cc => cc.Creator)
                .Include(c => c.CampaignMetrics)
                .FirstOrDefault(c => c.Id == id);
        }

        public Campaign CreateCampaign(Campaign campaign, List<int> creatorIds)
        {
            campaign.CreatedAt = DateTime.Now;
            if (string.IsNullOrWhiteSpace(campaign.Status))
            {
                campaign.Status = "Active";
            }

            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            foreach (var creatorId in creatorIds.Distinct())
            {
                _context.CampaignCreators.Add(new CampaignCreator
                {
                    CampaignId = campaign.Id,
                    CreatorId = creatorId
                });
            }

            // Seed a starter metrics row so the dashboard has something to show immediately.
            _context.CampaignMetrics.Add(new CampaignMetric
            {
                CampaignId = campaign.Id,
                Impressions = 0,
                Clicks = 0,
                Leads = 0,
                Spend = 0
            });

            _context.SaveChanges();

            return campaign;
        }

        public void AddCreatorToCampaign(int campaignId, int creatorId)
        {
            if (!IsCreatorInCampaign(campaignId, creatorId))
            {
                _context.CampaignCreators.Add(new CampaignCreator
                {
                    CampaignId = campaignId,
                    CreatorId = creatorId
                });
                _context.SaveChanges();
            }
        }

        public bool IsCreatorInCampaign(int campaignId, int creatorId)
        {
            return _context.CampaignCreators.Any(cc => cc.CampaignId == campaignId && cc.CreatorId == creatorId);
        }

        public List<Creator> GetCreatorsForCampaign(int campaignId)
        {
            return _context.CampaignCreators
                .Where(cc => cc.CampaignId == campaignId)
                .Select(cc => cc.Creator!)
                .ToList();
        }
    }
}
