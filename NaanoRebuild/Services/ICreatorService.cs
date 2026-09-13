using NaanoMVP.Models;

namespace NaanoMVP.Services
{
    public interface ICreatorService
    {
        List<Creator> GetCreators(string? search, string? category, string? platform, string? sortBy);
        Creator? GetById(int id);
        List<string> GetCategories();
        List<string> GetPlatforms();
        int GetTotalCreatorCount();
    }
}
