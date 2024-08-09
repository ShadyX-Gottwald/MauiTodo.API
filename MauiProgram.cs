using MauiTodo.Client1.Services;
using Microsoft.Extensions.Logging;
using MauiTodo.Client1.Views;
namespace MauiTodo.Client1; 

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IRestDataService, RestDataService>(); 

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ManageTodos>();





#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
