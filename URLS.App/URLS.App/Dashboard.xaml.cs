using URLS.App.Infrastructure.Helpers;
using URLS.App.ViewModels;

namespace URLS.App;

public partial class Dashboard : ContentPage
{
    private readonly DashboardViewModel _viewModel;
    public Dashboard(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        if (await SecureStorage.Default.IsAuthorizeAsync())
        {
            var currentUser = await SecureStorage.Default.GetAuthorizeAsync();
            _viewModel.FullName = currentUser.FullName;
        }
    }
}