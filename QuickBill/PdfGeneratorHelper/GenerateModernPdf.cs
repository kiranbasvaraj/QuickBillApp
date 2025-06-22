using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Borders;
using iText.Kernel.Pdf.Canvas.Draw;

namespace QuickBill.PdfGeneratorHelper;

public class GenerateModernPdf
{


    public async Task GenerateReceiptPdfAsync(string filePath)
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
