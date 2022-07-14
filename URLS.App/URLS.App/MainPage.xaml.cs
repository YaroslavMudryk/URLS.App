using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using URLS.App.Infrastructure.Helpers;
using URLS.App.ViewModels;

namespace URLS.App;

public partial class MainPage : ContentPage
{
    private readonly IFingerprint _fingerprint;
    public MainPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _fingerprint = CrossFingerprint.Current;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (await SecureStorage.Default.IsAuthorizeAsync())
        {
            if (await _fingerprint.IsAvailableAsync())
            {
                var request = new AuthenticationRequestConfiguration("Вхід у URLS", "Відскануйте, щоб увійти");
                var result = await _fingerprint.AuthenticateAsync(request);
                if (result.Authenticated)
                {
                    await Shell.Current.GoToAsync(nameof(Dashboard));
                }
            }
        }
    }
}