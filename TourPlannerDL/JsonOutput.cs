using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(JsonOutput));
        public static string ExportPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\ExportedTours\";

        public void ExportTour(Tour SingleTour)
        {
            string output = JsonConvert.SerializeObject(SingleTour);
            try
            {
                Task filetask = File.WriteAllTextAsync($"{ExportPath}{SingleTour.Name}.json", output);
                log.Info("Export Tour success");
            }
            catch
            {
                log.Error("Export Tour not working");
            }
        }

        public Tour ImportTour(Tour SingleTour)
        {
            string FilePath = $"{ExportPath}{SingleTour.Name}.json";
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    SingleTour = JsonConvert.DeserializeObject<Tour>(json);
                }
                log.Info("Import Tour success");
                return SingleTour;
            }
            catch
            {
                log.Error("Import Tour not working");
                return null;
            }
            
        }
    }
}
