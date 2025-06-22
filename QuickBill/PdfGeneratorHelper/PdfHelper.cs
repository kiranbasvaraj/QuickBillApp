using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;


namespace QuickBill.PdfGeneratorHelper;

public class PdfHelper
{


    public static async Task<string> OnGenerateInvoiceClicked()
    {
        string fileName = "mauidotnet.pdf";

#if ANDROID
		var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
		var filePath = Path.Combine(docsDirectory.AbsoluteFile.Path, fileName);
#else
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif

        // using (PdfWriter writer = new PdfWriter(filePath))
        // {
        //     PdfDocument pdf = new PdfDocument(writer);
        //     Document document = new Document(pdf);

        //     // Title
        //     Paragraph header = new Paragraph("QuickBill Invoice")
        //         .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
        //         .SetFontSize(20);
        //     document.Add(header);

        //     // Subtitle
        //     Paragraph subheader = new Paragraph("Your Company Name")
        //         .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
        //         .SetFontSize(15);
        //     document.Add(subheader);

        //     // Line separator
        //     LineSeparator ls = new LineSeparator(new SolidLine());
        //     document.Add(ls);

        //     // Logo/Image
        //     var imgStream = await ConvertImageSourceToStreamAsync("dotnet_bot.png");
        //     var image = new iText.Layout.Element.Image(ImageDataFactory.Create(imgStream))
        //         .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
        //         .SetWidth(100);
        //     document.Add(image);

        //     // Spacer
        //     document.Add(new Paragraph("\n"));

        //     // Invoice Table Setup
        //     float[] columnWidths = { 1, 3, 2, 2, 2 };
        //     Table table = new Table(UnitValue.CreatePercentArray(columnWidths));
        //     table.SetWidth(UnitValue.CreatePercentValue(100));

        //     // Table Header
        //     table.AddHeaderCell("S.No");
        //     table.AddHeaderCell("Item");
        //     table.AddHeaderCell("Unit Price");
        //     table.AddHeaderCell("Quantity");
        //     table.AddHeaderCell("Total");

        //     // Sample Data Rows
        //     for (int i = 1; i <= 3; i++)
        //     {
        //         table.AddCell(i.ToString());
        //         table.AddCell("Item " + i);
        //         table.AddCell("₹100");
        //         table.AddCell("2");
        //         table.AddCell("₹200");
        //     }

        //     // Total Row
        //     iText.Layout.Element.Cell totalCell = new iText.Layout.Element.Cell(1, 4).Add(new Paragraph("Grand Total")).SetBold();
        //     totalCell.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
        //     table.AddCell(totalCell);
        //     table.AddCell(new Paragraph("₹600").SetBold());

        //     document.Add(table);

        //     // Footer
        //     document.Add(new Paragraph("\n"));
        //     Paragraph footer = new Paragraph("Thank you for choosing QuickBill!")
        //         .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
        //         .SetFontColor(iText.Kernel.Colors.ColorConstants.GRAY)
        //         .SetFontSize(12);
        //     document.Add(footer);

        //     // Finish
        //     document.Close();
        // }


        await GenerateReceiptPdfAsync(filePath);

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

    public static async Task GenerateReceiptPdfAsync(string filePath)
    {
        await Task.Run(() =>
        {
            using var writer = new PdfWriter(filePath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var regular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            // Company Header
            var companyTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
                .UseAllAvailableWidth();
            companyTable.AddCell(new Paragraph("Company Name\nAddress 1\nAddress 2")
                .SetFont(regular).SetFontSize(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            companyTable.AddCell(new Paragraph("TO:\nCustomer Name\nPhone: 123456789\nEmail: demo@example.com")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(regular).SetFontSize(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            document.Add(companyTable);

            document.Add(new Paragraph("\n"));

            // Receipt Info
            var receiptTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
                .UseAllAvailableWidth();
            receiptTable.AddCell(new Paragraph("Receipt # RT-356890")
                .SetFont(bold).SetFontSize(12).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            receiptTable.AddCell(new Paragraph("Feb 14, 2023")
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

            for (int i = 0; i < 3; i++)
            {
                items.AddCell(new Paragraph("Black Bag"));
                items.AddCell(new Paragraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit."));
                items.AddCell("1");
                items.AddCell("$120");
            }
            document.Add(items);

            // Totals
            var totals = new Table(UnitValue.CreatePercentArray(new float[] { 70, 30 }))
                .UseAllAvailableWidth().SetMarginTop(20);

            totals.AddCell(new Paragraph("Subtotal").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            totals.AddCell(new Paragraph("$360").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            totals.AddCell(new Paragraph("Shipping").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            totals.AddCell(new Paragraph("$10").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            totals.AddCell(new Paragraph("Tax").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            totals.AddCell(new Paragraph("$5").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            totals.AddCell(new Paragraph("Total").SetFont(bold).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            totals.AddCell(new Paragraph("$375").SetFont(bold).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            document.Add(totals);

            // Footer
            document.Add(new Paragraph("\nThank you for your business!")
                .SetFontSize(10).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontColor(ColorConstants.GRAY));

            document.Close();
        });
    }
}
