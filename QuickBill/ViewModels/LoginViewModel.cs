using System;
using System.Windows.Input;
using QuickBill.Interfaces;

namespace QuickBill.ViewModels;

public class LoginViewModel : ILoginViewModel
{
    INavigationService _navigationService { get; }
    public ICommand ContinueCommand { get; }
    private string? _companyName { get; set; }
    public string? CompanyName
    {
        get => _companyName;
        set
        {
            if (_companyName != value)
            {
                _companyName = value;
                // Notify property changed if using INotifyPropertyChanged
            }
        }
    }

    public LoginViewModel(INavigationService navigationService)
    {
        ContinueCommand = new Command(async (obj) => await OnContinue());
        _navigationService = navigationService;
    }

    private async Task OnContinue()
    {
        // Handle the continue button click event
        if (!string.IsNullOrWhiteSpace(CompanyName))
            await _navigationService.NavigateAsync("//MainTabs");
        else
            await NavigationService.GetMainPage().DisplayAlert("Opps!", "Please enter a company name.", "OK");


    }
}
