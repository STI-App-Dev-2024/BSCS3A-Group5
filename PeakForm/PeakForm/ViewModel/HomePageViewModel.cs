using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PeakForm.Services;
namespace PeakForm.ViewModel;

public partial class HomePageViewModel {
    private readonly INavigation _navigation;
    
    public HomePageViewModel(INavigation navigation) { 
        _navigation = navigation;
        
        FireStoreServices _firebaseStoreServices = new FireStoreServices(_navigation);
        _firebaseStoreServices.RetrieveQuestData();
        _firebaseStoreServices.RetrievePenaltiesData();
        
    }  


}
