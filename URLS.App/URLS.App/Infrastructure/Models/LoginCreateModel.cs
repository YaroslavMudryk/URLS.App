using Extensions.DeviceDetector.Models;
using System.Security.Claims;

namespace URLS.App.Infrastructure.Models
{
    internal class LoginCreateModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ClientInfo Client { get; set; }
        public AppLoginCreateModel App { get; set; }
    }
}