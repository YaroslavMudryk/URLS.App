using URLS.App.ViewModels;

namespace URLS.App;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}