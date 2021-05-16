using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class PDFOutput
    {
        public static string ReportPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\Reports\";

        public void PrintOneReport(Tour SingleTour)
        {
            System.IO.Directory.CreateDirectory(ReportPath);
            string FileName = $"{SingleTour.Name}.pdf";
            string FileLocation = $@"{ReportPath}\{FileName}";
            var writer = new PdfWriter(FileLocation);
            var pdf = new PdfDocument(writer);
            var File = new Document(pdf, PageSize.A4, false);
            Image Map = new Image(ImageDataFactory.Create(SingleTour.ImagePath));
            // Erstelle Tabelle mit 10 Spalten
            Table table = new Table(UnitValue.CreatePercentArray(10)).UseAllAvailableWidth();

            // Table header
            table.AddCell("Date");
            table.AddCell("Duration in Minutes");
            table.AddCell("Distance in KM");
            table.AddCell("Report");
            table.AddCell("Rating");
            table.AddCell("Total Cost in Euro");
            table.AddCell("Access Limitation");
            table.AddCell("Weather");
            table.AddCell("Animal Friendly");
            table.AddCell("Has Highways");
            /*foreach (var logItem in tourItem.Log)
            {
                table.AddCell($"Neue Tabellen Zelle");
            }*/

            File.Add(new Paragraph($"Tour Report").SetFontSize(25));
            File.Add(new Paragraph($"Description").SetFontSize(20));
            //File.Add(new Paragraph($"Tour Shit"));
            File.Add(new Paragraph($"Map").SetFontSize(20));
            File.Add(Map);
            File.Add(new Paragraph($"Logs").SetFontSize(20));
            File.Add(table);

            // Erstellt das Dokument 
            File.Close();
        }
    }
}
