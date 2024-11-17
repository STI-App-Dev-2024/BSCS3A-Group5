
using PeakForm.Model;
using PeakForm.Services;
using PeakForm.ViewModel;
using System.Collections.ObjectModel;

namespace PeakForm;

public partial class HomePage : ContentPage
{
    FireStoreServices fireStoreServices;

    
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new HomePageViewModel(Navigation);

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Access the ViewModel if it's set as the BindingContext
        var viewModel = BindingContext as HomePageViewModel;

        if (viewModel != null)
        {
            // Call the ViewModel's LoadItemsAsync method
          //  await viewModel.LoadItemsAsync();
        }
    }


    private async void OnFullNameTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnProfImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnViewAllQuestTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuestPage());
    }
    private async void OnViewAllPenaltyTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PenaltyPage());
    }
}