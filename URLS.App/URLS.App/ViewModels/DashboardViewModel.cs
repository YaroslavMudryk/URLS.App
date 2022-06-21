using System.ComponentModel;
using URLS.App.Infrastructure.Helpers;
using URLS.App.Infrastructure.Services.Interfaces;

namespace URLS.App.ViewModels
{
    public class DashboardViewModel
    {
        private readonly IAuthService _authService;
        private readonly Page _page;
        private bool isBusy;
        private string title;
        private string fullName;
        public readonly IConnectivity _connectivity;

        public DashboardViewModel(IAuthService authService, IConnectivity connectivity)
        {
            _authService = authService;
            _page = App.Current.MainPage;
            _connectivity = connectivity;
            _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
            Title = GetStatusInternet(connectivity.NetworkAccess);
        }

        private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Title = GetStatusInternet(e.NetworkAccess);
        }

        private string GetStatusInternet(NetworkAccess network)
        {
            switch (network)
            {
                case NetworkAccess.Unknown:
                case NetworkAccess.Local:
                    return "З'єднання";
                case NetworkAccess.None:
                    return "Інтернет відсутній";
                case NetworkAccess.ConstrainedInternet:
                    return "Інтернет обмежений";
                case NetworkAccess.Internet:
                    return "URLS";
                default:
                    return "URLS";
            }
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                RaisePropertyChanged("FullName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private async void LogoutUserTappedAsync(object obj)
        {
            if (await SecureStorage.Default.IsAuthorizeAsync())
            {
                SecureStorage.Default.ClearAuthorize();
                await _page.Navigation.PopAsync(true);
            }
        }
    }
}
