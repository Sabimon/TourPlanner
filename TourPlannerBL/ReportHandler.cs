using System.Collections.ObjectModel;
using TourPlannerDL;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class ReportHandler
    {
        private PDFOutput Printer = new();

        public void PrintOneTour(Tour SingleTour)
        {
            Printer.PrintOneReport(SingleTour);
        }
        public void PrintTourSummary(ObservableCollection<Tour> Tours)
        {
            Printer.PrintSummaryReport(Tours);
        }
    }
}
