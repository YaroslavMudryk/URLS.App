using URLS.App.Infrastructure.Factories;
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

        builder.Services.AddSingleton<HttpClient>(_ => HttpClientFactory.CreateHttpClient());
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IConnectivity>(_ => Connectivity.Current);

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<RegisterViewModel>();

        builder.Services.AddSingleton<Dashboard>();
        builder.Services.AddSingleton<DashboardViewModel>();

        return builder.Build();
    }
}
