using System;
using System.Security.Cryptography.X509Certificates;

namespace QuickBill.AppConstants;

public static class Settings
{

    private const string CompanyNameKey = "CompanyNameKey";
    private const string CompanyAddressKey = "CompanyAddressKey";
    private const string PhoneNumberKey = "PhoneNumberKey";
    private const string EmailKey = "EmailKey";
    private const string IsLoginSuccessKey = "IsLoginSuccessKey";

    private static string CompanyNameKeyDefaultValue => string.Empty;
    private static string CompanyAddressKeyDefaultValue => string.Empty;
    private static string PhoneNumberKeyDefaultValue => string.Empty;
    private static string EmailKeyDefaultValue => string.Empty;
    private static bool IsLoginSuccessDefault => false;

    public static string CompanyName
    {
        get { return Preferences.Get(CompanyNameKey, CompanyNameKeyDefaultValue); }
        set { Preferences.Set(CompanyNameKey, value); }
    }

    public static string CompanyAddress
    {
        get { return Preferences.Get(CompanyAddressKey, CompanyAddressKeyDefaultValue); }
        set { Preferences.Set(CompanyAddressKey, value); }
    }

    public static string PhoneNumber
    {
        get { return Preferences.Get(PhoneNumberKey, PhoneNumberKeyDefaultValue); }
        set { Preferences.Set(PhoneNumberKey, value); }
    }

    public static string Email
    {
        get { return Preferences.Get(EmailKey, EmailKeyDefaultValue); }
        set { Preferences.Set(EmailKey, value); }
    }
    
       public static bool IsLoginSuccess
    {
        get { return Preferences.Get(IsLoginSuccessKey, IsLoginSuccessDefault); }
        set { Preferences.Set(IsLoginSuccessKey, value); }
    }



}
