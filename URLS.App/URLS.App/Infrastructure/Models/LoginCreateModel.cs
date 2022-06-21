using Extensions.DeviceDetector.Models;

namespace URLS.App.Infrastructure.Models
{
    public class LoginCreateModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ClientInfo Client { get; set; }
        public AppLoginCreateModel App { get; set; }
    }
}