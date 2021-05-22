using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class JsonHandler
    {
        DBBusiness db = new();
        public void DeserializeAPIResponse(string json, string routeName)
        {
            var jsonData = JObject.Parse(json);
            var distance = jsonData["route"]["distance"].ToString();
            var totalTime = jsonData["route"]["formattedTime"].ToString();
            var highway = jsonData["route"]["hasHighway"].ToString();
            var access = jsonData["route"]["hasAccessRestriction"].ToString();
            var narratives = jsonData["route"]["legs"]["origNarrative"].ToString();
            db.InsertTourDescription(distance, totalTime, highway, access, routeName);
        }
        public void ImportTour(Tour SingleTour, ObservableCollection<Logs> Logs, ObservableCollection<Description> Description)
        {

        }
        public void ExportTour()
        {

        }
    }
}