using System.ComponentModel;
using URLS.App.Infrastructure.Models;
using URLS.App.Infrastructure.Services.Interfaces;

namespace URLS.App.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly Page _page;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string code;
        private bool isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string Code
        {
            get => code;
            set
            {
                code = value;
                RaisePropertyChanged("Code");
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

        public Command RegisterUser { get; }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public RegisterViewModel(IAuthService authService)
        {
            _page = App.Current.MainPage;

            RegisterUser = new Command(RegisterUserTappedAsync);

            _authService = authService;
        }

        private async void RegisterUserTappedAsync(object obj)
        {
            IsBusy = true;
            try
            {
                var result = await _authService.RegistrationAsync(new RegisterUserViewModel
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Login = Email,
                    Password = Password,
                    Code = Code
                });

                if (result.IsSuccess())
                {
                    await _page.DisplayAlert("Повідомлення", "Ви успішно зареєстровані!\nЗачекайте, поки вчитель схвалить ваш запит", "Чекаю)");

                    await _page.Navigation.PopAsync(true);
                }

            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Помилка", ex.Message, "OK");
            }
        }
    }
}
