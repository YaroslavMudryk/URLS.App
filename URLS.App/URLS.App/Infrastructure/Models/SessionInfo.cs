namespace URLS.App.Infrastructure.Models
{
    public class SessionInfo
    {
        public List<SessionViewModel> Sessions { get; set; }
        public int TotalSessions { get; set; }
        public int ActiveSessions { get; set; }
    }
}
