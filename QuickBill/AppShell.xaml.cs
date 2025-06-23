using QuickBill.PageRoutes;
using QuickBill.Views;

namespace QuickBill;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
		Routing.RegisterRoute(Routes.PdfView, typeof(PdfView));
		Routing.RegisterRoute(Routes.ReceiptHistoryPage, typeof(ReceiptHistoryPage));


	}
}

