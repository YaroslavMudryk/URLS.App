namespace URLS.App.Infrastructure.Models
{
    public class SecureUserModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int Index { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
    }
}
