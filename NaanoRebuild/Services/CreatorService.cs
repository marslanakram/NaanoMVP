using NaanoMVP.Data;
using NaanoMVP.Models;

namespace NaanoMVP.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ApplicationDbContext _context;

        public CreatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Creator> GetCreators(string? search, string? category, string? platform, string? sortBy)
        {
            var query = _context.Creators.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(term) ||
                    c.Category.ToLower().Contains(term) ||
                    c.Username.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "All")
            {
                query = query.Where(c => c.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(platform) && platform != "All")
            {
                query = query.Where(c => c.Platform == platform);
            }

            query = sortBy switch
            {
                "followers_desc" => query.OrderByDescending(c => c.Followers),
                "followers_asc" => query.OrderBy(c => c.Followers),
                "engagement_desc" => query.OrderByDescending(c => c.EngagementRate),
                "engagement_asc" => query.OrderBy(c => c.EngagementRate),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "price_asc" => query.OrderBy(c => c.Price),
                _ => query.OrderByDescending(c => c.Followers)
            };

            return query.ToList();
        }

        public Creator? GetById(int id)
        {
            return _context.Creators.FirstOrDefault(c => c.Id == id);
        }

        public List<string> GetCategories()
        {
            return _context.Creators.Select(c => c.Category).Distinct().OrderBy(c => c).ToList();
        }

        public List<string> GetPlatforms()
        {
            return _context.Creators.Select(c => c.Platform).Distinct().OrderBy(c => c).ToList();
        }

        public int GetTotalCreatorCount()
        {
            return _context.Creators.Count();
        }
    }
}
