using System.Collections.ObjectModel;
using TourPlannerDL;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class ReportHandler
    {
        private PDFOutput Printer = new();

        public void PrintOneReport(Tour SingleTour, ObservableCollection<Logs> Logs, ObservableCollection<Description> Description)
        {
            Printer.PrintOneReport(SingleTour, Logs, Description);
        }
        public void PrintSummaryReport(ObservableCollection<Tour> Tours, ObservableCollection<Logs> Logs, ObservableCollection<Description> Description)
        {
            Printer.PrintSummaryReport(Tours, Logs, Description);
        }
    }
}
