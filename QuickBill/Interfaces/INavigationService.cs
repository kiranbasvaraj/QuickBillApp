using System;

namespace QuickBill.Interfaces;

public interface INavigationService
{
    Task NavigateAsync(string pageName);
    Task GobackAsync();


}
