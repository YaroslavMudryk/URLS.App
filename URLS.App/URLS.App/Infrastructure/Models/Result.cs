namespace URLS.App.Infrastructure.Models
{
    public class Result<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public T Data { get; set; }
        public Meta Meta { get; set; }
    }
}
