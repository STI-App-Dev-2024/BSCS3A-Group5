﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using PeakForm.Services;

namespace PeakForm.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    // Renamed the private fields to avoid conflict with property names
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;

    private readonly INavigation _navigationService;

    public LoginViewModel(INavigation navigation)
    {
        _navigationService = navigation;
    }

    [RelayCommand]
    private async void LoginUserTappedAsync(object obj)
    {
        // Created an instance of AuthServices to handle login
        AuthServices _authServices = new AuthServices(_navigationService);
        await _authServices.Login(email, password); // Use the field names as generated by ObservableProperty
    }
}
