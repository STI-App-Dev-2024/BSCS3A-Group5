using Google.Api.Gax;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using PeakForm.newWindow;
using PeakForm.ViewModel;

namespace PeakForm
{
    public class MauiProgram
    {


        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    
                });
            
            builder.Services.AddSingleton<SignUpPage>();
            builder.Services.AddSingleton<SignUpViewModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomePageViewModel>();



            return builder.Build();

        }
        

    }
}
