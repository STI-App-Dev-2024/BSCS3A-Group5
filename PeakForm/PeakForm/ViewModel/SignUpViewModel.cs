
using Firebase.Auth;
using PeakForm.Model;
using System.ComponentModel;

namespace PeakForm.ViewModel;

public class SignUpViewModel : INotifyPropertyChanged{
    public string WebAPIKey = "AIzaSyCexbVR5gHKyMCms0xryoFjTCeQB9UHg8Q";
    private INavigation _navigation;
    private string email;
    private string password;    
    public string Email {
        get=>email;
        set {
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

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

    public Command SignUp { get; }
    public SignUpViewModel(INavigation navigation) {
        this._navigation = navigation;
        SignUp = new Command(RegisterUserTappedAsysnc);
    }

    private async void RegisterUserTappedAsysnc(object obj)
    {
        try {
            
            var authprovider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            var auth = await authprovider.CreateUserWithEmailAndPasswordAsync(email, password);
            string token = auth.FirebaseToken;
            if (token != null) {
                await App.Current.MainPage.DisplayAlert("Alert!", "User Registerd Succesfully", "OK");
                await this._navigation.PushAsync(new LoginPage());
            }
            

        }
        catch(Exception ex) {
            await App.Current.MainPage.DisplayAlert("Alert", ex.Message,"OK");
            throw;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertChanged(string Value) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Value));
    }
        

}
