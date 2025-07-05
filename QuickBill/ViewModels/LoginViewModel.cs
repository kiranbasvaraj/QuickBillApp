using System;
using System.Windows.Input;
#if ANDROID
using Android.Provider;
#endif
using QuickBill.AppConstants;
using QuickBill.Interfaces;
using Settings = QuickBill.AppConstants.Settings;

namespace QuickBill.ViewModels;

public class LoginViewModel : BaseViewModel, ILoginViewModel
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
                Settings.CompanyName = _companyName = value;

                // RaisePropertyChanged(nameof(CompanyName)); // optional
            }
        }
    }

    private string? _companyAddress { get; set; }
    public string? CompanyAddress
    {
        get => _companyAddress;
        set
        {
            if (_companyAddress != value)
            {
                Settings.CompanyAddress = _companyAddress = value;
                // RaisePropertyChanged(nameof(CompanyAddress)); // optional
            }
        }
    }

    private string? _phoneNumber { get; set; }
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (_phoneNumber != value)
            {
                Settings.PhoneNumber = _phoneNumber = value;
                // RaisePropertyChanged(nameof(PhoneNumber)); // optional
            }
        }
    }

    private string? _email { get; set; }
    public string? Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                Settings.Email = _email = value;
                // RaisePropertyChanged(nameof(Email)); // optional
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
        if (!string.IsNullOrWhiteSpace(CompanyName) && !string.IsNullOrWhiteSpace(CompanyAddress) && !string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(Email))
        {
            Settings.IsLoginSuccess = true;
            await _navigationService.NavigateAsync("//MainTabs");
        }
        else
            await NavigationService.GetMainPage().DisplayAlert("Alert!", "Please fill all the details.", "OK");
    }

    //TODO:Remove this
    public async override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);
        await _navigationService.NavigateAsync("//MainTabs");
    }
}
