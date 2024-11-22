namespace PeakForm;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        DisplayWelcomeMessage();
    }
    private void DisplayWelcomeMessage()
    {
        //Get first and last name bruh
        string firstName = Preferences.Get("FirstName", "Guest");
        string lastName = Preferences.Get("LastName", "");
        string fullName = $"{firstName} {lastName}".Trim();

        //Full name bruh
        FullName.Text = $"{fullName}";
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
        await Navigation.PushAsync(new ProfilePage());
    }
    private async void OnViewAllPenaltyTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}