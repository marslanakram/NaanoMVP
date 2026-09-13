using NaanoMVP.Models;

namespace NaanoMVP.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Creators.Any())
            {
                return; // Already seeded
            }

            var creators = new List<Creator>
            {
                new Creator { Name = "Amara Ndlovu", Username = "@amara.creates", Bio = "Lifestyle and wellness storyteller helping brands connect with a health-conscious audience.", Category = "Lifestyle", Location = "Cape Town, ZA", Followers = 245000, EngagementRate = 4.8m, Price = 1200m, Platform = "Instagram" },
                new Creator { Name = "Leo Marchetti", Username = "@leomarchetti", Bio = "Travel filmmaker documenting hidden gems across Europe with a cinematic touch.", Category = "Travel", Location = "Milan, IT", Followers = 512000, EngagementRate = 3.9m, Price = 2500m, Platform = "YouTube" },
                new Creator { Name = "Priya Chandran", Username = "@priya.eats", Bio = "Home-cooking creator turning everyday recipes into shareable video moments.", Category = "Food", Location = "Chennai, IN", Followers = 189000, EngagementRate = 6.2m, Price = 950m, Platform = "Instagram" },
                new Creator { Name = "Jordan Blake", Username = "@jblakefit", Bio = "Certified trainer sharing approachable workouts and nutrition tips for busy professionals.", Category = "Fitness", Location = "Austin, US", Followers = 340000, EngagementRate = 5.1m, Price = 1600m, Platform = "TikTok" },
                new Creator { Name = "Sofia Reyes", Username = "@sofiastyled", Bio = "Fashion and thrifting creator focused on sustainable, budget-friendly outfits.", Category = "Fashion", Location = "Mexico City, MX", Followers = 278000, EngagementRate = 5.6m, Price = 1350m, Platform = "TikTok" },
                new Creator { Name = "Ethan Cole", Username = "@ethanbuilds", Bio = "Tech reviewer breaking down gadgets and productivity tools for everyday creators.", Category = "Technology", Location = "Toronto, CA", Followers = 421000, EngagementRate = 3.4m, Price = 2100m, Platform = "YouTube" },
                new Creator { Name = "Nina Kowalski", Username = "@ninadraws", Bio = "Digital artist and illustrator collaborating with brands on original visual campaigns.", Category = "Art & Design", Location = "Warsaw, PL", Followers = 98000, EngagementRate = 7.4m, Price = 700m, Platform = "Instagram" },
                new Creator { Name = "Marcus Webb", Username = "@marcusgames", Bio = "Full-time streamer and gaming content creator with a highly engaged nightly audience.", Category = "Gaming", Location = "London, UK", Followers = 610000, EngagementRate = 4.2m, Price = 2800m, Platform = "Twitch" },
                new Creator { Name = "Hana Suzuki", Username = "@hana.beauty", Bio = "Beauty and skincare creator known for honest, in-depth product breakdowns.", Category = "Beauty", Location = "Osaka, JP", Followers = 356000, EngagementRate = 5.9m, Price = 1750m, Platform = "Instagram" },
                new Creator { Name = "Diego Fernandez", Username = "@diegomoney", Bio = "Personal finance educator simplifying investing and budgeting for young professionals.", Category = "Finance", Location = "Madrid, ES", Followers = 152000, EngagementRate = 4.5m, Price = 1100m, Platform = "YouTube" },
                new Creator { Name = "Zoe Bennett", Username = "@zoebparenting", Bio = "Parenting creator sharing candid stories and practical tips for new families.", Category = "Family", Location = "Sydney, AU", Followers = 134000, EngagementRate = 6.8m, Price = 900m, Platform = "Instagram" },
                new Creator { Name = "Kwame Asante", Username = "@kwamemoves", Bio = "Dance choreographer creating viral short-form content and brand challenges.", Category = "Entertainment", Location = "Accra, GH", Followers = 723000, EngagementRate = 8.1m, Price = 3200m, Platform = "TikTok" },
                new Creator { Name = "Isabella Rossi", Username = "@bellahome", Bio = "Interior design creator sharing budget makeovers and cozy home inspiration.", Category = "Home & Living", Location = "Florence, IT", Followers = 176000, EngagementRate = 5.3m, Price = 1050m, Platform = "Instagram" },
                new Creator { Name = "Tyler Osei", Username = "@tylerplays", Bio = "Mobile gaming creator with fast-growing shorts and tutorial content.", Category = "Gaming", Location = "Lagos, NG", Followers = 289000, EngagementRate = 6.0m, Price = 1300m, Platform = "YouTube" },
                new Creator { Name = "Chloe Martin", Username = "@chloewrites", Bio = "Book and productivity creator building a cozy community around reading habits.", Category = "Lifestyle", Location = "Paris, FR", Followers = 87000, EngagementRate = 7.9m, Price = 650m, Platform = "Instagram" },
                new Creator { Name = "Ravi Malhotra", Username = "@ravitravels", Bio = "Budget travel creator specializing in Southeast Asia itineraries and hacks.", Category = "Travel", Location = "Delhi, IN", Followers = 402000, EngagementRate = 4.7m, Price = 1900m, Platform = "YouTube" },
                new Creator { Name = "Grace Muthoni", Username = "@gracefit", Bio = "Yoga instructor and wellness creator promoting mindful, low-impact fitness.", Category = "Fitness", Location = "Nairobi, KE", Followers = 121000, EngagementRate = 6.4m, Price = 850m, Platform = "Instagram" },
                new Creator { Name = "Oliver Hughes", Username = "@oliverreviews", Bio = "Consumer tech and gadget unboxing creator trusted for balanced reviews.", Category = "Technology", Location = "Manchester, UK", Followers = 265000, EngagementRate = 3.7m, Price = 1450m, Platform = "YouTube" },
            };

            context.Creators.AddRange(creators);
            context.SaveChanges();

            var campaigns = new List<Campaign>
            {
                new Campaign
                {
                    Name = "Summer Wellness Launch",
                    Objective = "Brand Awareness",
                    Audience = "Health-conscious adults 25-40",
                    Message = "Introduce our new plant-based supplement line with authentic, relatable stories.",
                    Budget = 15000m,
                    Status = "Active",
                    CreatedAt = DateTime.Now.AddDays(-21)
                },
                new Campaign
                {
                    Name = "Back to Campus Tech Bundle",
                    Objective = "Sales Conversion",
                    Audience = "Students and young professionals 18-28",
                    Message = "Showcase our student laptop bundle with real productivity use cases.",
                    Budget = 22000m,
                    Status = "Active",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new Campaign
                {
                    Name = "Festive Fashion Edit",
                    Objective = "Engagement",
                    Audience = "Fashion-forward shoppers 18-35",
                    Message = "Style our holiday collection into everyday, shareable looks.",
                    Budget = 9000m,
                    Status = "Completed",
                    CreatedAt = DateTime.Now.AddDays(-45)
                }
            };

            context.Campaigns.AddRange(campaigns);
            context.SaveChanges();

            var byUsername = creators.ToDictionary(c => c.Username, c => c.Id);

            var links = new List<CampaignCreator>
            {
                new CampaignCreator { CampaignId = campaigns[0].Id, CreatorId = byUsername["@amara.creates"] },
                new CampaignCreator { CampaignId = campaigns[0].Id, CreatorId = byUsername["@gracefit"] },
                new CampaignCreator { CampaignId = campaigns[0].Id, CreatorId = byUsername["@jblakefit"] },
                new CampaignCreator { CampaignId = campaigns[1].Id, CreatorId = byUsername["@ethanbuilds"] },
                new CampaignCreator { CampaignId = campaigns[1].Id, CreatorId = byUsername["@oliverreviews"] },
                new CampaignCreator { CampaignId = campaigns[2].Id, CreatorId = byUsername["@sofiastyled"] },
                new CampaignCreator { CampaignId = campaigns[2].Id, CreatorId = byUsername["@bellahome"] },
                new CampaignCreator { CampaignId = campaigns[2].Id, CreatorId = byUsername["@hana.beauty"] },
            };

            context.CampaignCreators.AddRange(links);
            context.SaveChanges();

            var metrics = new List<CampaignMetric>
            {
                new CampaignMetric { CampaignId = campaigns[0].Id, Impressions = 482000, Clicks = 19600, Leads = 1240, Spend = 9800m },
                new CampaignMetric { CampaignId = campaigns[1].Id, Impressions = 715000, Clicks = 31200, Leads = 2680, Spend = 14300m },
                new CampaignMetric { CampaignId = campaigns[2].Id, Impressions = 256000, Clicks = 12100, Leads = 860, Spend = 9000m },
            };

            context.CampaignMetrics.AddRange(metrics);
            context.SaveChanges();
        }
    }
}
