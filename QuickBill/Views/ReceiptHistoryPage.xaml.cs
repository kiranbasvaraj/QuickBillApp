using QuickBill.ViewModels;

namespace QuickBill.Views;

public partial class ReceiptHistoryPage : ContentPage
{
	public ReceiptHistoryPage(ReceiptHistoryViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}