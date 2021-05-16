using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerDL;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class ReportHandler
    {
        private PDFOutput Printer = new();

        public void PrintOneReport(Tour SingleTour)
        {
            Printer.PrintOneReport(SingleTour);
        }
    }
}
