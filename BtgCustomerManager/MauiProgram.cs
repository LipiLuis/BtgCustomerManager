using BtgCustomerManager.Core.Interfaces;
using BtgCustomerManager.Core.Services;
using BtgCustomerManager.ViewModels;
using Microsoft.Extensions.Logging;

namespace BtgCustomerManager;

public static class MauiProgram
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
        builder.Services
            .AddTransient<MainViewModel>()
            .AddTransient<CustomerViewModel>()
            .AddSingleton<ICustomerValidator, CustomerValidator>()
            .AddSingleton<ICustomerService, CustomerService>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}