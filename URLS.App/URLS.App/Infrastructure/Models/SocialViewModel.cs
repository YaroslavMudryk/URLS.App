namespace URLS.App.Infrastructure.Models
{
    public class SocialViewModel
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public DateTime LinkedAt { get; set; }
        public DateTime? LastSigIn { get; set; }
    }
}