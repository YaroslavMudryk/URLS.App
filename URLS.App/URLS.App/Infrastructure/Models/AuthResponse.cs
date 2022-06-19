using System.Security.Claims;

namespace URLS.App.Infrastructure.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string TokenId { get; set; }
        public DateTime ExpiredAt { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}