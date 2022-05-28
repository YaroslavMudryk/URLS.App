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
            var callBack = new Uri("myapp://");

            var res = await WebAuthenticator.AuthenticateAsync(new WebAuthenticatorOptions
            {
                Url = new Uri(""),
                CallbackUrl = callBack,
                PrefersEphemeralWebBrowserSession = true
            });


            IsBusy = true;
            await Task.Delay(1500);
            IsBusy = false;
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
