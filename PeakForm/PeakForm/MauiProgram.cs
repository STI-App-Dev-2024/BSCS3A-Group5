using Google.Api.Gax;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;

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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
        
       
    }
}
