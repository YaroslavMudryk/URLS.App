using URLS.App.ViewModels;

namespace URLS.App;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel(this);
	}
}