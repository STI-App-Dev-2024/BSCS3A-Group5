using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PeakForm.Services;

namespace PeakForm.ViewModel;

public partial class LoginViewModel : ObservableObject 
{
    [ObservableProperty]
    string email;
    [ObservableProperty]
    string password;
    private readonly INavigation _navigationService;
    
   
  
    
    public LoginViewModel(INavigation navigation)
    {
        _navigationService = navigation;
        
    }
    [RelayCommand]
    private async void LoginUserTappedAsysnc(object obj)
    {
        AuthServices _authServices = new AuthServices(_navigationService);
        await _authServices.Login(Email, Password);
        
    }
    

}
