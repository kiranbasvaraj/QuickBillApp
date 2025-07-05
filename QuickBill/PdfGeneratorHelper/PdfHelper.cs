using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using QuickBill.Models;
using QuickBill.AppConstants;


namespace QuickBill.PdfGeneratorHelper;

public class PdfHelper
{


    public static async Task<string> OnGenerateInvoiceClicked(List<ReceiptItemModel> receiptItems, string custMobile, string custName, string CustEmail)
    {
        string fileName = "quickbill.pdf";

#if ANDROID
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        var filePath = Path.Combine(docsDirectory.AbsoluteFile.Path, fileName);
#else
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif

        await GenerateReceiptPdfAsync(filePath, receiptItems, custMobile, custName, CustEmail);

        return filePath;
    }


    private static async Task<byte[]> ConvertImageSourceToStreamAsync(string imageName)
    {
        using var ms = new MemoryStream();
        using (var stream = await FileSystem.OpenAppPackageFileAsync(imageName))
            await stream.CopyToAsync(ms);
        return ms.ToArray();
    }



    // Uncomment the following method if you need to convert an ImageSource to a byte array

    public static async Task GenerateReceiptPdfAsync(string filePath, List<ReceiptItemModel> receiptItems, string custMobile, string custName, string CustEmail)
    {
        await Task.Run(() =>
        {
            using var writer = new PdfWriter(filePath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var regular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            Paragraph header = new Paragraph("QuickBill Invoice")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);

            // Company Header
            var companyTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
                .UseAllAvailableWidth();
            companyTable.AddCell(new Paragraph($"Company Name:{Settings.CompanyName}\nPhone: {Settings.PhoneNumber}\nEmail: {Settings.Email}\nAddress 1{Settings.CompanyAddress}")
                .SetFont(regular).SetFontSize(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            companyTable.AddCell(new Paragraph($"TO:\n{custName}\nPhone: {custMobile}\nEmail: {CustEmail}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(regular).SetFontSize(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            document.Add(companyTable);

            document.Add(new Paragraph("\n"));

            // Receipt Info
            var receiptTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
                .UseAllAvailableWidth();
            receiptTable.AddCell(new Paragraph($"Receipt # RT-{Guid.NewGuid()}")
                .SetFont(bold).SetFontSize(12).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            receiptTable.AddCell(new Paragraph(DateTime.Now.ToString())
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(regular).SetFontSize(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            document.Add(receiptTable);

            // Horizontal line
            document.Add(new LineSeparator(new SolidLine(1f)));

            // Items Table
            var items = new Table(UnitValue.CreatePercentArray(new float[] { 40, 30, 15, 15 }))
                .UseAllAvailableWidth().SetMarginTop(10);
            items.AddHeaderCell("Item");
            items.AddHeaderCell("Description");
            items.AddHeaderCell("Qty");
            items.AddHeaderCell("Price");

            // for (int i = 0; i < 3; i++)
            // {
            //     items.AddCell(new Paragraph("Black Bag"));
            //     items.AddCell(new Paragraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit."));
            //     items.AddCell("1");
            //     items.AddCell("$120");
            // }

            foreach (var item in receiptItems)
            {
                items.AddCell(new Paragraph(item.ItemName));
                items.AddCell(new Paragraph("NA"));
                items.AddCell(item.Quantity?.ToString());
                items.AddCell(item.Price?.ToString());
            }
            document.Add(items);

            // Totals
            var totals = new Table(UnitValue.CreatePercentArray(new float[] { 70, 30 }))
                .UseAllAvailableWidth().SetMarginTop(20);

            //remove for now
            // totals.AddCell(new Paragraph("Subtotal").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            // totals.AddCell(new Paragraph("$360").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            // totals.AddCell(new Paragraph("Shipping").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            // totals.AddCell(new Paragraph("$10").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            // totals.AddCell(new Paragraph("Tax").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            // totals.AddCell(new Paragraph("$5").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            double total = receiptItems?.Sum(x => x.Price ?? 0) ?? 0;
            totals.AddCell(new Paragraph("Total").SetFont(bold).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            totals.AddCell(new Paragraph($"Rupees {total:N2} Only").SetFont(bold).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            document.Add(totals);

            // Footer
            document.Add(new Paragraph("\nThank you for your business!")
                .SetFontSize(10).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontColor(ColorConstants.GRAY));

            document.Close();
        });
    }
}
