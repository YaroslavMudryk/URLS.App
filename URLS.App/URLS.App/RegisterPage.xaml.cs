using URLS.App.ViewModels;

namespace URLS.App;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerView)
	{
		InitializeComponent();
		BindingContext = registerView;
	}
}