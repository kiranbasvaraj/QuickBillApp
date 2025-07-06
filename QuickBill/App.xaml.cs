
namespace QuickBill;

public partial class App : Application
{
	public App()
	{
		if (Current != null)
		{
			Current.UserAppTheme = AppTheme.Light;
		}
		InitializeComponent();

		//MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}

