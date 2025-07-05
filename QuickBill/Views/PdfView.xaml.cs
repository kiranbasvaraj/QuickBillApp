using QuickBill.ViewModels;

namespace QuickBill.Views;

public partial class PdfView : ContentPage
{
	HomePageViewModel _homePageViewModel;
	public PdfView(HomePageViewModel homePageViewModel)
	{
		InitializeComponent();
		this.BindingContext =_homePageViewModel= homePageViewModel;
		pdfWebview.Source = homePageViewModel.PdfSource;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		pdfWebview.Source = _homePageViewModel.PdfSource;
		
    }

}