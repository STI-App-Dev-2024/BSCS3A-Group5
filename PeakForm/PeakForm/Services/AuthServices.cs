using Firebase.Auth;
using Newtonsoft.Json;

namespace PeakForm.Services;

public class AuthServices{
    private string WebAPIKey = "AIzaSyCexbVR5gHKyMCms0xryoFjTCeQB9UHg8Q";
    private readonly INavigation _navigationService;

    public AuthServices(INavigation navigationService)
    {
        _navigationService = navigationService;
    }
    public async Task Login(string Email, string Password) {
        var authprovider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
        try
        {
            var auth = await authprovider.SignInWithEmailAndPasswordAsync(Email, Password);
            var content =  await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("FreshFirebaseToken", serializedContent);
            await Shell.Current.DisplayAlert("Alert!", "User Login! Succesfully", "OK");
            await _navigationService.PushAsync(new HomePage());
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }
    }
    public async Task Register(string Email, string Password) {
        try
        {
            var authprovider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            var auth = await authprovider.CreateUserWithEmailAndPasswordAsync(Email, Password);
            string token = auth.FirebaseToken;
            if (token != null)
            {
                await  Shell.Current.DisplayAlert("Alert!", "User Registerd Succesfully", "OK");
                await _navigationService.PushAsync(new LoginPage());
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }
    }

}
