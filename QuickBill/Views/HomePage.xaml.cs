using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using QuickBill.PdfGeneratorHelper;
using System.Net;

namespace QuickBill.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
	private async void OnGenerateInvoiceClicked(object sender, EventArgs e)
	{
		var pdfFile =await PdfHelper.OnGenerateInvoiceClicked();
		 //var stream = File.OpenRead(pdfFile);


		//pdfViewer.DocumentSource = stream;


		#if ANDROID
		            pdfview.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(pdfFile)}";
		#else
					pdfview.Source = pdfFile;
		#endif

	}

}
