
using Firebase.Auth;
using PeakForm.Model;
using PeakForm.Services;
using System.ComponentModel;

namespace PeakForm.ViewModel;

public class SignUpViewModel : INotifyPropertyChanged{
   
    private string email, password,confirmpassword, firstname, lastname,username;
    private DateOnly birthdate;
    private float height, weight;
    private readonly INavigation _navigationService;
    public event PropertyChangedEventHandler? PropertyChanged;


    public string FirstName {
        get => firstname;
        set {
            firstname = value;
            RaisePropertyChanged(nameof(FirstName));
        }
    }
    public string LastName {
        get => lastname;
        set { 
            lastname = value;
            RaisePropertyChanged(nameof(LastName));
        }
    }
    public string UserName {
        get => username;
        set {
            username = value;   
            RaisePropertyChanged(nameof(UserName));
        }
    }
    public DateOnly Birthdate { 
        get => birthdate;
        set { 
            birthdate = value;
            RaisePropertyChanged(nameof(Birthdate));
        }
    }

    public string Email {
        get=>email;
        set {
            email = value;
            RaisePropertyChanged(nameof(Email));
        }
    }
    public string Password
    {
        get => password;
        set
        {
            password = value;
            RaisePropertyChanged(nameof(Password));
        }
    }
    public string ConfirmPassword {
        get => confirmpassword;
        set {
            confirmpassword = value;
            RaisePropertyChanged(nameof(ConfirmPassword));
        }
    }
    public float Height {
        get => height;
        set {
            height = value; 
            RaisePropertyChanged(nameof(Height));
        }
    }
    public float Weight
    {
        get => weight;
        set
        {
            weight = value;
            RaisePropertyChanged(nameof(Weight));
        }
    }


    public Command SignUp { get; }
    public SignUpViewModel(INavigation navigationService) {
        _navigationService = navigationService;
        SignUp = new Command(RegisterUserTappedAsysnc);
    }

    private async void RegisterUserTappedAsysnc(object obj)
    {
        AuthServices _authServices = new AuthServices(_navigationService);
        bool Check = Password.Equals(ConfirmPassword);
        string message = "Password not match";
        string nullMessage = "The following fields are null: ";
        if (Check)
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                   string.IsNullOrWhiteSpace(LastName) ||
                   string.IsNullOrWhiteSpace(UserName) ||
                   Birthdate == null || 
                   string.IsNullOrWhiteSpace(Email) ||
                   string.IsNullOrWhiteSpace(Password) ||
                   Height <= 0 || 
                   Weight <= 0) 
            {
             
                if (string.IsNullOrWhiteSpace(FirstName)) nullMessage += "First Name, ";
                if (string.IsNullOrWhiteSpace(LastName)) nullMessage += "Last Name, ";
                if (string.IsNullOrWhiteSpace(UserName)) nullMessage += "Username, ";
                if (Birthdate == null) nullMessage += "Birthdate, ";
                if (string.IsNullOrWhiteSpace(Email)) nullMessage += "Email, ";
                if (string.IsNullOrWhiteSpace(Password)) nullMessage += "Password, ";
                if (Height <= 0) nullMessage += "Height, ";
                if (Weight <= 0) nullMessage += "Weight, ";

               
                nullMessage = nullMessage.TrimEnd(',', ' ');

             
                await Shell.Current.DisplayAlert("Alert!", nullMessage, "Ok");
            }
            else
            {
                await _authServices.Register(FirstName, LastName, UserName, Birthdate, Email, Password, Height, Weight);
            }
        }
        else {
           await Shell.Current.DisplayAlert("Alert!", message, "Ok");
        }
        
    }
    
    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

}
