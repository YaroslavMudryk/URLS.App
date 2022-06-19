namespace URLS.App.Infrastructure.Models
{
    public class ClaimViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}