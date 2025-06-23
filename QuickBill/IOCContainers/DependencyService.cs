using System;
using QuickBill.Interfaces;
using QuickBill.ViewModels;
using QuickBill.Views;

namespace QuickBill.IOCContainers;

public static class DependencyService
{

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        // mauiAppBuilder.Services.AddTransient<ILoggingService, LoggingService>();
        mauiAppBuilder.Services.AddTransient<INavigationService, NavigationService>();

        // More services registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<LoginViewModel>();
         mauiAppBuilder.Services.AddTransient<ReceiptHistoryViewModel>();
        mauiAppBuilder.Services.AddTransient<HomePageViewModel>();

        // More view-models registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<HomePage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<ReceiptHistoryPage>();
        // More views registered here.

        return mauiAppBuilder;
    }

}
