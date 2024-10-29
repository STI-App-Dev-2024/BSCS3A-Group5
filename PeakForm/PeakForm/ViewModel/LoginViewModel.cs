using System.ComponentModel;
using PeakForm.Services;

namespace PeakForm.ViewModel;

public class LoginViewModel : INotifyPropertyChanged
{
    
    private string email;
    private string password;    
    private readonly INavigation _navigationService;
    
    public string Email
    {
        get => email;
        set
        {
            email = value;
            RaisePropertyChanged(nameof(Email));
        }
    }
    public string Password {
        get => password;
        set {
            password = value;
            RaisePropertyChanged(nameof(Password));
        }
    }
    public Command Login { get; }
    public LoginViewModel(INavigation navigation)
    {
        _navigationService = navigation;
        Login = new Command(LoginUserTappedAsysnc);
    }

    private async void LoginUserTappedAsysnc(object obj)
    {
        AuthServices _authServices = new AuthServices(_navigationService);
        await _authServices.Login(Email, Password);
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

}
