
using PeakForm.Model;
using PeakForm.Services;
using PeakForm.ViewModel;
using System.Collections.ObjectModel;

namespace PeakForm;

public partial class HomePage : ContentPage
{
    FireStoreServices fireStoreServices;

    
    public HomePage(string email)
    {
        InitializeComponent();
        BindingContext = new HomePageViewModel(Navigation,email);

    }

    


    private async void OnFullNameTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnProfImageTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnViewAllQuestTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        await Navigation.PushAsync(new QuestPage());
        
    }
    private async void OnViewAllPenaltyTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        await Navigation.PushAsync(new PenaltyPage());
    }

    private async void OnItemSelect(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Quests; // Cast to the appropriate type

        if (selectedItem != null)
        {
            await Navigation.PushAsync(new QuestInfoPage(selectedItem));
        }

    }

}