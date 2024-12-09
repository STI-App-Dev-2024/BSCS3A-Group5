using PeakForm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakForm.newWindow
{
        public class MyWindow : Window
        {

            private HomePageViewModel homePageViewModel;

            public MyWindow() : base()
            {
            }

            public MyWindow(Page page) : base(page)
            {
            }

            public MyWindow(HomePageViewModel homePageViewModel)
            {
            this.homePageViewModel = homePageViewModel;
            }

            protected override void OnCreated()
            {
                base.OnCreated();
              
             }
        
    
        }
    
}
