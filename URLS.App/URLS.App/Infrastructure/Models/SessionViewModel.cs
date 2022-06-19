using Extensions.DeviceDetector.Models;

namespace URLS.App.Infrastructure.Models
{
    public class SessionViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppModel App { get; set; }
        public ClientInfo Client { get; set; }
        public Location Location { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
