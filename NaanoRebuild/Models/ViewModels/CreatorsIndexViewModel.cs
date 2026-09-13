namespace NaanoMVP.Models.ViewModels
{
    public class CreatorsIndexViewModel
    {
        public List<Creator> Creators { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> Platforms { get; set; } = new();

        public string? Search { get; set; }
        public string? Category { get; set; }
        public string? Platform { get; set; }
        public string? SortBy { get; set; }
    }
}
