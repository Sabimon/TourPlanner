using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class JsonOutput
    {
        public static string ExportPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\ExportedTours\";

        public void ExportTour(Tour SingleTour)
        {
            string output = JsonConvert.SerializeObject(SingleTour);
            Task filetask = File.WriteAllTextAsync($"{ExportPath}{SingleTour.Name}.json", output);
        }

        public Tour ImportTour(Tour SingleTour)
        {
            string FilePath = $"{ExportPath}{SingleTour.Name}.json";
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                SingleTour = JsonConvert.DeserializeObject<Tour>(json);
            }
            return SingleTour;
        }
    }
}
