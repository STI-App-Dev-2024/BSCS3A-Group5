using PeakForm.ViewModel;

namespace PeakForm;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel(Navigation);
	}

	private async void OnSignUpButtonClicked(object sender, EventArgs e) {
		await Navigation.PushAsync(new LoginPage());
	}
}