
using Firebase.Auth;
using PeakForm.Model;
using PeakForm.Services;
using System.ComponentModel;

namespace PeakForm.ViewModel;

public class SignUpViewModel : INotifyPropertyChanged{
   
    private string email;
    private string password;    
    private readonly INavigation _navigationService;
    public event PropertyChangedEventHandler? PropertyChanged;


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
    public Command SignUp { get; }
    public SignUpViewModel(INavigation navigationService) {
        _navigationService = navigationService;
        SignUp = new Command(RegisterUserTappedAsysnc);
    }

    private async void RegisterUserTappedAsysnc(object obj)
    {
        AuthServices _authServices = new AuthServices(_navigationService);
        await _authServices.Register(Email, Password);
    }
    
    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
    void OnPropertChanged(string Value) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Value));
    }
        

}
