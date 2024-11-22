using Microsoft.Maui;

namespace PeakForm;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private void OnSearchButtonPressed(object sender, EventArgs e)
    {
        string query = UidSearchBar.Text;

        if (query == "092345")
        {
            UserNameLabel.Text = "John Kristoffer Miranda";
            RankLabel.Text = "ELITE";
            MutualFriendsLabel.Text = "2 Mutual friends";

            ResultCard.IsVisible = true;
        }
        else
        {
            ResultCard.IsVisible = false;
        }

    }
}