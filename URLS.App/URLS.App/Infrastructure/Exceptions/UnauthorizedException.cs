namespace URLS.App.Infrastructure.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string error) : base(error) { }
    }
}
