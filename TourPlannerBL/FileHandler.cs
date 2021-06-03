using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class FileHandler
    {
        public static string ExportPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\ExportedTours\";

        public ObservableCollection<Tour> FillImportableTours()
        {
            ObservableCollection<Tour> ImportableTours = new ();
            var files = from file in Directory.EnumerateFiles(ExportPath) select file;
            foreach (var file in files)
            {
                ImportableTours.Add(new Tour()
                {
                    Name = file
                });
            }
            if (ImportableTours == null)
            {
                ImportableTours.Add(new Tour()
                {
                    Name = "No Tour Exported yet"
                });
            }
            return ImportableTours;
        }
    }
}
