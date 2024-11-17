
using System.Collections.ObjectModel;
using PeakForm.Services;
using PeakForm.Model;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace PeakForm.ViewModel;

public partial class HomePageViewModel : ObservableObject {
    private readonly INavigation _navigation;

    private readonly FireStoreServices fireStoreServices;
    public ObservableCollection<Penalty> PenaltyData { get; set; } = [];

    public ObservableCollection<string> QuestData { get; set; } = [];

    public HomePageViewModel(INavigation navigation) { 
        _navigation = navigation;
        FireStoreServices _firebaseStoreServices = new FireStoreServices(_navigation);
       // _ = _firebaseStoreServices.StartListeningToCollection("Quests", OnDataUpdate);
       

    }
    
    private async Task LoadItemsAsync(string username)
    {

        List<string> exerciseTitles = await fireStoreServices.GetStringsAsync(username);
        QuestData.Clear();
        if (exerciseTitles.Any())
        { 
            Console.WriteLine($"Exercise titles for user '{username}':");
            foreach (var title in exerciseTitles)
            {
                QuestData.Add(title);
            }
        }
        else
        {
            Console.WriteLine($"No exercises found for user '{username}'.");
        }
    }
   
}
