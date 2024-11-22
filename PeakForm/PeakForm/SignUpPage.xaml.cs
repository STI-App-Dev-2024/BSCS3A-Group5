using PeakForm.ViewModel;
using PeakForm.Services;
using Google.Api.Gax;
using Google.Cloud.Firestore;

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