using System.ComponentModel;

namespace URLS.App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private string userName;
        private string userPassword;
        private bool isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command RegisterBtn { get; }
        public Command LoginBtn { get; }

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                RaisePropertyChanged("UserPassword");
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

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            RegisterBtn = new Command(RegisterBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
        }

        private async void LoginBtnTappedAsync(object obj)
        {
            try
            {
                //var authUrl = new Uri("https://192.168.0.7:45455/api/v1/account/social/Google");
                var authUrl = new Uri("https://localhost:7234/api/v1/account/social/Google");
                var callbackUrl = new Uri("urlsapp://");

                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                string authToken = result.AccessToken;
                string refreshToken = result.RefreshToken;
            }
            catch(TaskCanceledException ex)
            {
                await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
            }
        }

        private async void RegisterBtnTappedAsync(object obj)
        {
            await _navigation.PushAsync(new RegisterPage());
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
