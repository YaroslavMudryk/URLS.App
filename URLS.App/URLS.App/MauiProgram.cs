using URLS.App.Infrastructure.Services.Implementations;
using URLS.App.Infrastructure.Services.Interfaces;
using URLS.App.ViewModels;

namespace URLS.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<HttpClient>(_ =>
        new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 3,
            AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
            CheckCertificateRevocationList = false,
            ClientCertificateOptions = ClientCertificateOption.Manual,
            CookieContainer = new System.Net.CookieContainer(),
            Credentials = null,
            DefaultProxyCredentials = null,
            PreAuthenticate = false,
            SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11,
            UseCookies = true,
            UseProxy = false,
            UseDefaultCredentials = false,
            Proxy = null,
#if ANDROID
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost") || cert.Issuer.Equals("CN=Keyoti Conveyor Root Certificate Authority 2 - For development testing only!"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            }
#endif
        })
        {
            BaseAddress = new Uri("https://192.168.0.2:45455/"),
            //BaseAddress = new Uri("https://urls.com.ua/"),
            Timeout = TimeSpan.FromMinutes(1),
        });

        return builder.Build();
    }
}
