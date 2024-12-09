namespace PeakForm;

public partial class QuestPage : ContentPage
{
	public QuestPage()
	{
		InitializeComponent();
	}

	private async void CloseButtonClicked(object sender, EventArgs e) 
	{
		await Navigation.PushAsync(new HomePage(null));
	}
}