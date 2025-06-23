using System;
using QuickBill.DataServices;
using QuickBill.Interfaces;
using QuickBill.Interfaces.LocalDbInterfaces;
using QuickBill.LocalDatabase;
using QuickBill.ViewModels;
using QuickBill.Views;

namespace QuickBill.IOCContainers;

public static class DependencyService
{

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        // mauiAppBuilder.Services.AddTransient<ILoggingService, LoggingService>();
        mauiAppBuilder.Services.AddTransient<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddTransient<IReceiptRepository, ReceiptRepository>();

        // More services registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModelsForBindingContext(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<ReceiptHistoryViewModel>();
        mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
        // More view-models registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModelsAsService(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<IReceiptHistoryViewModel, ReceiptHistoryViewModel>();
        mauiAppBuilder.Services.AddTransient<IHomePageViewModel, HomePageViewModel>();

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
    public static MauiAppBuilder InitialzeSqliteConnection(this MauiAppBuilder mauiAppBuilder)
    {
        SqliteDbHelper.InitializeDatabase();
        return mauiAppBuilder;

    }

}
