using Firebase.Auth;
using Newtonsoft.Json;
using PeakForm.Model;
using System.ComponentModel;
using System.Net.Http.Json;

namespace PeakForm.ViewModel;

public class LoginViewModel : INotifyPropertyChanged
{
    public string WebAPIKey = "AIzaSyCexbVR5gHKyMCms0xryoFjTCeQB9UHg8Q";
    private string email;
    private string password;    
    UserAccount useraccount;
    private INavigation _navigation;
    public string Email
    {
        get => email;
        set
        {
            email = value;
            OnPropertChanged(nameof(Email));
        }
    }
    public string Password {
        get => password;
        set {
            password = value;
            OnPropertChanged(nameof(Password));
        }
    
    }

    public Command Login { get; }
    public LoginViewModel(INavigation navigation)
    {
        this._navigation = navigation;
        Login = new Command(LoginUserTappedAsysnc);
    }

    private async void LoginUserTappedAsysnc(object obj)
    {
        var authprovider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
        try { 
            var auth = await authprovider.SignInWithEmailAndPasswordAsync(email, password);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("FreshFirebaseToken", serializedContent);
            await App.Current.MainPage.DisplayAlert("Alert!", "User Registerd Succesfully", "OK");
            await this._navigation.PushAsync(new HomePage());
        } 
        catch (Exception ex) {
            await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }

    }

    

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertChanged(string v) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
}
