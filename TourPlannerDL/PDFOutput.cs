using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class PDFOutput
    {
        public static string ReportPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\Reports\";
        private DBOutput dbOut = new();
        private int headerSize=20;
        private int fontSize=12;

        public void PrintOneReport(Tour SingleTour, ObservableCollection<Logs> Logs, ObservableCollection<Description> Description)
        {
            System.IO.Directory.CreateDirectory(ReportPath);
            string FileName = $"{SingleTour.Name}.pdf";
            string FileLocation = $@"{ReportPath}\{FileName}";
            var writer = new PdfWriter(FileLocation);
            var pdf = new PdfDocument(writer);
            var File = new Document(pdf, PageSize.A4, false);
            Image Map = new Image(ImageDataFactory.Create(SingleTour.ImagePath));
            Map.ScaleToFit(PageSize.A5.GetWidth(), PageSize.A4.GetHeight());

            Table DescripitonTable = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            DescripitonTable.AddCell("Duration in Minutes");
            DescripitonTable.AddCell("Distance in KM");
            DescripitonTable.AddCell("Has Highways");
            DescripitonTable.AddCell("Access Limitation");
            foreach (Description description in Description)
            {
                DescripitonTable.AddCell(description.Time);
                DescripitonTable.AddCell(description.Distance);
                DescripitonTable.AddCell(description.Highway);
                DescripitonTable.AddCell(description.Access);
            }

            Table LogTable = new Table(UnitValue.CreatePercentArray(10)).UseAllAvailableWidth();
            LogTable.AddCell("Date");
            LogTable.AddCell("Duration in Minutes");
            LogTable.AddCell("Distance in KM");
            LogTable.AddCell("Report");
            LogTable.AddCell("Rating");
            LogTable.AddCell("Total Cost in Euro");
            LogTable.AddCell("Access Limitation");
            LogTable.AddCell("Weather");
            LogTable.AddCell("Animal Friendly");
            LogTable.AddCell("Has Highways");
            foreach (Logs log in Logs)
            {
                LogTable.AddCell(log.Date);
                LogTable.AddCell(log.Time);
                LogTable.AddCell(log.Distance);
                LogTable.AddCell(log.Report);
                LogTable.AddCell(log.Rating);
                LogTable.AddCell(log.Cost);
                LogTable.AddCell(log.Access);
                LogTable.AddCell(log.Weather);
                LogTable.AddCell(log.Animals);
                LogTable.AddCell(log.Highway);
            }

            File.Add(new Paragraph($"Tour Report For {SingleTour.Name}").SetFontSize(headerSize));
            File.Add(new Paragraph($"Description").SetFontSize(fontSize));
            File.Add(DescripitonTable);
            File.Add(new Paragraph($"Map").SetFontSize(fontSize));
            File.Add(Map);
            File.Add(new Paragraph($"Your Logs").SetFontSize(fontSize));
            File.Add(LogTable);

            File.Close();
        }
        public void PrintSummaryReport(ObservableCollection<Tour> Tours, ObservableCollection<Logs> Logs, ObservableCollection<Description> Description)
        {
            System.IO.Directory.CreateDirectory(ReportPath);
            string FileLocation = $@"{ReportPath}\SummaryReport.pdf";
            var writer = new PdfWriter(FileLocation);
            var pdf = new PdfDocument(writer);
            var File = new Document(pdf, PageSize.A4, false);

            File.Add(new Paragraph($"Summary Tour Report").SetFontSize(headerSize));
            File.Add(new Paragraph($"Tours: ").SetFontSize(fontSize));
            foreach (Tour tours in Tours)
            {
                File.Add(new Paragraph($"Tour Report For {tours.Name}").SetFontSize(fontSize));
                File.Add(new Paragraph($"Description").SetFontSize(fontSize));
                Table DescripitonTable = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                DescripitonTable.AddCell("Duration in Minutes");
                DescripitonTable.AddCell("Distance in KM");
                DescripitonTable.AddCell("Has Highways");
                DescripitonTable.AddCell("Access Limitation");

                foreach (Description description in Description)
                {
                    if (tours.TourID == description.TourID)
                    {
                        DescripitonTable.AddCell(description.Time);
                        DescripitonTable.AddCell(description.Distance);
                        DescripitonTable.AddCell(description.Highway);
                        DescripitonTable.AddCell(description.Access);
                    }
                }
                File.Add(DescripitonTable);

                File.Add(new Paragraph($"Your Logs").SetFontSize(fontSize));
                Table LogTable = new Table(UnitValue.CreatePercentArray(10)).UseAllAvailableWidth();
                LogTable.AddCell("Date");
                LogTable.AddCell("Duration in Minutes");
                LogTable.AddCell("Distance in KM");
                LogTable.AddCell("Report");
                LogTable.AddCell("Rating");
                LogTable.AddCell("Total Cost in Euro");
                LogTable.AddCell("Access Limitation");
                LogTable.AddCell("Weather");
                LogTable.AddCell("Animal Friendly");
                LogTable.AddCell("Has Highways");
                foreach (Logs log in Logs)
                {
                    if (tours.TourID == log.TourID)
                    {
                        LogTable.AddCell(log.Date);
                        LogTable.AddCell(log.Time);
                        LogTable.AddCell(log.Distance);
                        LogTable.AddCell(log.Report);
                        LogTable.AddCell(log.Rating);
                        LogTable.AddCell(log.Cost);
                        LogTable.AddCell(log.Access);
                        LogTable.AddCell(log.Weather);
                        LogTable.AddCell(log.Animals);
                        LogTable.AddCell(log.Highway);
                    }
                }
                File.Add(LogTable);
            }
            File.Close();
        }
    }
}
