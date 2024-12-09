using Firebase.Auth;
using Newtonsoft.Json;
using PeakForm.Model;


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

            await _navigationService.PushAsync(new HomePage(Email));

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert!", ex.Message, "OK");
        }
    }
    public async Task Register(string firstname, string lastName,string userName,DateOnly birthdate ,string email, string password, float height, float weight) {
        try
        {
            var _firebaseStoreServices = new FireStoreServices(_navigationService);
            var generate = new GenerateExercises(_navigationService);
            var bmi = new BMI();
            var authprovider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            var auth = await authprovider.CreateUserWithEmailAndPasswordAsync(email, password);
            string token = auth.FirebaseToken;
            string uid = auth.User.LocalId;
            if (token != null)
            {
                var userAccount = new UserAccount{
                    ID = uid,
                    FirstName = firstname,
                    LastName = lastName,
                    UserName = userName,
                 // Birthdate = birthdate,
                    Email = email,
                    Height = height,
                    Weight = weight,
                    CreateAt = DateTime.Now

                };
                var user = new Users
                {
                    ID = uid,
                    UserName=userName,
                    BMI = bmi.CalculateBMI(weight, height),
                    BodyType = bmi.InterpretBMI(bmi.CalculateBMI(weight, height)),
                    CreateAt =  DateTime.Now
                };
                

                Quests quests = generate.GenerateExercise(bmi.InterpretBMI(bmi.CalculateBMI(weight, height)));
                await _firebaseStoreServices.CreateAccount(userAccount);
                await _firebaseStoreServices.CreateUserAndExercise(user, quests);
                await Shell.Current.DisplayAlert("Alert!", "User Registerd Succesfully", "OK");
                await _navigationService.PushAsync(new LoginPage());
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alert", ex.Message, "OK");
        }
    }
    
    



}
