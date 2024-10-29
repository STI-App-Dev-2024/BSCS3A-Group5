using PeakForm.Services;
using PeakForm.ViewModel;

namespace PeakForm;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPage : ContentPage
{

	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}