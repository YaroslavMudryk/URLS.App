using Extensions.DeviceDetector.Models;
using System.ComponentModel;
using URLS.App.Infrastructure.Helpers;
using URLS.App.Infrastructure.Models;
using URLS.App.Infrastructure.Services.Interfaces;

namespace URLS.App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly Page _page;
        private readonly IAuthService _authService;
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

        public LoginViewModel(IAuthService authService)
        {
            _page = App.Current.MainPage;
            RegisterBtn = new Command(RegisterBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
            _authService = authService;
        }

        private async void LoginBtnTappedAsync(object obj)
        {
            if (await SecureStorage.Default.IsAuthorizeAsync())
            {
                await Shell.Current.GoToAsync(nameof(Dashboard));
            }
            else
            {
                try
                {
                    IsBusy = true;

                    var device = DeviceInfo.Current;

                    var loginModel = new LoginCreateModel
                    {
                        App = AppCreds.GetApp(),
                        Client = new ClientInfo
                        {
                            Browser = null,
                            OS = new OS
                            {
                                Name = device.Platform.ToString(),
                                Version = device.VersionString
                            },
                            Device = new Extensions.DeviceDetector.Models.Device
                            {
                                Brand = device.Manufacturer,
                                Model = device.Model,
                                Type = device.Idiom.ToString()
                            }
                        },
                        Login = UserName,
                        Password = UserPassword
                    };

                    var response = await _authService.LoginAsync(loginModel);

                    if (!response.IsSuccess())
                    {
                        IsBusy = false;
                        await _page.DisplayAlert("Помилка", response.GetError(), "OK");
                        return;
                    }

                    _authService.HttpClient.SetBearerToken(response.Data.Token);

                    var currentUserResult = await _authService.GetMeAsync();

                    await SecureStorage.Default.AuthorizeAsync(currentUserResult.Data.MapToSecure(response.Data));

                    IsBusy = false;
                    await Shell.Current.GoToAsync(nameof(Dashboard));
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await _page.DisplayAlert("Помилка", ex.Message, "OK");
                }
            }
        }

        private async void RegisterBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
