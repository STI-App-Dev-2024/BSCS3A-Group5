using PeakForm.newWindow;
using PeakForm.ViewModel;

namespace PeakForm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
      
    }   
}
