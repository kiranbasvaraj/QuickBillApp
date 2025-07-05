using Android.Provider;
using QuickBill.ViewModels;
using Settings = QuickBill.AppConstants.Settings;
namespace QuickBill.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		this.BindingContext = loginViewModel;
	}
	override protected void OnAppearing()
	{
		base.OnAppearing();
		if(Settings.IsLoginSuccess)
		{
			// If the user is already logged in, navigate to the main page
			Shell.Current.GoToAsync("//MainTabs");
		}
		// If you need to perform any actions when the page appears, you can do it here
	}

	// public void OnLoginClicked(object sender, EventArgs e)
	// {
	// 	// Handle the login button click event here
	// 	// For example, you can validate user credentials and navigate to the main page
	// 	DisplayAlert("Login", "Login button clicked!", "OK");
	// }

	// void Button_Clicked(System.Object sender, System.EventArgs e)
	// {
	// 	//DisplayAlert("login","ok","canel");
	// 	Shell.Current.GoToAsync("//MainTabs");
	// }
}