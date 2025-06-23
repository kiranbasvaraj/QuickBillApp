using System;
using QuickBill.Interfaces;

namespace QuickBill;

public class NavigationService : INavigationService
{
    public Task NavigateAsync(string pageName)
    {
        return Shell.Current.GoToAsync(pageName);
    }
    public Task GobackAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    public static Page GetMainPage()
    {
        var page = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (page == null)
            throw new InvalidOperationException("page not found.");
        return page;
    }
}
