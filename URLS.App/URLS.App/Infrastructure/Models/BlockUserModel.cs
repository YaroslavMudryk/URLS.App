namespace URLS.App.Infrastructure.Models
{
    public class BlockUserModel
    {
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public int UserId { get; set; }
    }
}
