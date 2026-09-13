using NaanoMVP.Data;
using NaanoMVP.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace NaanoMVP.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DashboardViewModel GetDashboardData()
        {
            var campaigns = _context.Campaigns
                .Include(c => c.CampaignCreators)
                .Include(c => c.CampaignMetrics)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            var model = new DashboardViewModel
            {
                TotalCampaigns = campaigns.Count,
                TotalCreators = _context.Creators.Count(),
                TotalImpressions = campaigns.SelectMany(c => c.CampaignMetrics).Sum(m => (int?)m.Impressions) ?? 0,
                TotalClicks = campaigns.SelectMany(c => c.CampaignMetrics).Sum(m => (int?)m.Clicks) ?? 0,
                TotalLeads = campaigns.SelectMany(c => c.CampaignMetrics).Sum(m => (int?)m.Leads) ?? 0,
                TotalSpend = campaigns.SelectMany(c => c.CampaignMetrics).Sum(m => (decimal?)m.Spend) ?? 0m,
            };

            model.Campaigns = campaigns.Select(c => new DashboardCampaignRow
            {
                CampaignId = c.Id,
                Name = c.Name,
                Status = c.Status,
                Budget = c.Budget,
                Spend = c.CampaignMetrics.Sum(m => (decimal?)m.Spend) ?? 0m,
                CreatorCount = c.CampaignCreators.Count,
                Impressions = c.CampaignMetrics.Sum(m => (int?)m.Impressions) ?? 0,
                Clicks = c.CampaignMetrics.Sum(m => (int?)m.Clicks) ?? 0,
                Leads = c.CampaignMetrics.Sum(m => (int?)m.Leads) ?? 0
            }).ToList();

            return model;
        }
    }
}
