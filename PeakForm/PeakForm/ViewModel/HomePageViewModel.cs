using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using PeakForm.Services;
using PeakForm.Model;
using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace PeakForm.ViewModel;

public partial class HomePageViewModel : ObservableObject
{
    private readonly INavigation _navigation;
    private readonly FireStoreServices _fireStoreServices;

    // ObservableCollections for data
    public ObservableCollection<Penalty> PenaltyData { get; set; } =[];
    public ObservableCollection<Quests> QuestData { get; set; } = [];

    private readonly string _email; // Store the user's email
    private string _userName; // Fetched username from Firestore

    // Constructor
    public HomePageViewModel(INavigation navigation, string email)
    {
        _navigation = navigation;
        _fireStoreServices = new FireStoreServices(_navigation);
        _email = email;

        // Initialize the ViewModel
        _ = InitializeAsync();
    }

    // Initialization: Fetch username and quests
    private async Task InitializeAsync()
    {
        try
        {
            // Step 1: Fetch the username using the email
            _userName = await FetchUserNameAsync(_email);

            if (!string.IsNullOrEmpty(_userName))
            {
                Console.WriteLine($"Fetched UserName: {_userName}");

                // Step 2: Use the username to fetch quests
                await LoadQuestsAsync(_userName);
            }
            else
            {
                Console.WriteLine("Failed to fetch username. Cannot load quests.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during initialization: {ex.Message}");
        }
    }

    // Fetch the username based on the email
    public async Task<string> FetchUserNameAsync(string email)
    {
        try
        {
            string userName = await _fireStoreServices.GetUserNameAsync(email);
            if (!string.IsNullOrEmpty(userName))
            {
                return userName;
            }
            else
            {
                Console.WriteLine("UserName not found for the given email.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching username: {ex.Message}");
            return null;
        }
    }

    // Load quests for the given username
    private async Task LoadQuestsAsync(string username)
    {
        try
        {
            var quests = await _fireStoreServices.GetQuestsAsync(username);

            // Update the ObservableCollection
            QuestData.Clear();
            foreach (var quest in quests)
            {
                string title = quest.Title;
                QuestData.Add(quest);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading quests: {ex.Message}");
        }
    }
  
}
