namespace QuickBill.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	// public void OnLoginClicked(object sender, EventArgs e)
	// {
	// 	// Handle the login button click event here
	// 	// For example, you can validate user credentials and navigate to the main page
	// 	DisplayAlert("Login", "Login button clicked!", "OK");
	// }

	void Button_Clicked(System.Object sender, System.EventArgs e)
	{
		//DisplayAlert("login","ok","canel");
		Shell.Current.GoToAsync("//MainTabs");
    }
}