using System.ComponentModel;

namespace URLS.App.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private string email;
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

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
            get => password; set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public Command RegisterUser { get; }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public RegisterViewModel(INavigation navigation)
        {
            _navigation = navigation;

            RegisterUser = new Command(RegisterUserTappedAsync);
        }

        private async void RegisterUserTappedAsync(object obj)
        {

        }
    }
}
