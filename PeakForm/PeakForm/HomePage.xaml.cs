using PeakForm.Model;
using PeakForm.ViewModel;
using System.Collections.ObjectModel;

namespace PeakForm;

public partial class HomePage : ContentPage
{
 
    public HomePage()
    {
        InitializeComponent();
        DisplayWelcomeMessage();
        BindingContext = new HomePageViewModel(Navigation);
        
    }
    private void DisplayWelcomeMessage()
    {
        //Get first and last name bruh
        /*
        string firstName = Preferences.Get("FirstName", "Guest");
        string lastName = Preferences.Get("LastName", "");
        string fullName = $"{firstName} {lastName}".Trim();
        
        //Full name bruh
        FullName.Text = $"{fullName}";
        */
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