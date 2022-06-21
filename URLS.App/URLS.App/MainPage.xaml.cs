using URLS.App.Infrastructure.Helpers;
using URLS.App.ViewModels;

namespace URLS.App;

public partial class MainPage : ContentPage
{
    public MainPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (await SecureStorage.Default.IsAuthorizeAsync())
            await Shell.Current.GoToAsync(nameof(Dashboard));
    }
}