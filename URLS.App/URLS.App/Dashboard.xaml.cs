using URLS.App.Infrastructure.Services.Implementations;
using URLS.App.Infrastructure.Services.Interfaces;

namespace URLS.App;

public partial class Dashboard : ContentPage
{
    private readonly string _token;
    private readonly IAuthService _authService;
    public Dashboard(string token)
    {
        InitializeComponent();
        _token = token;
        _authService = new AuthService("https://192.168.0.2:45455/");
    }

    protected override async void OnAppearing()
    {
        var me = await _authService.GetMeAsync(_token);
        UserEmail.Text = me.UserName;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SecureStorage.Default.Remove("Users");
        App.Current.MainPage.Navigation.PopAsync(true);
    }
}