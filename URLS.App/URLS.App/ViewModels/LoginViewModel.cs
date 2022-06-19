using System.ComponentModel;
using System.Text.Json;
using URLS.App.Infrastructure.Helpers;
using URLS.App.Infrastructure.Models;
using URLS.App.Infrastructure.Services.Implementations;

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

        public LoginViewModel()
        {
            _navigation = App.Current.MainPage.Navigation;
            RegisterBtn = new Command(RegisterBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
        }

        private async void LoginBtnTappedAsync(object obj)
        {
            if (SecureStorage.Default.TryGetUsers(out var data))
            {
                await _navigation.PushAsync(new Dashboard(data.OrderBy(s => s.Index).First().Token));
            }
            else
            {
                try
                {
                    IsBusy = true;
                    var authService = new AuthService("https://192.168.0.2:45455/");
                    var resposne = await authService.LoginAsync(UserName, UserPassword);
                    IsBusy = false;

                    var currentUser = await authService.GetMeAsync(resposne.Token);

                    SecureStorage.Default.TrySetNewUser(currentUser.MapToSecure(resposne), out var error);

                    await _navigation.PushAsync(new Dashboard(resposne.Token));
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
                }
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
