using Newtonsoft.Json.Linq;
using TourPlannerModels;
using TourPlannerDL;

namespace TourPlannerBL
{
    public class JsonHandler
    {
        DBBusiness db = new();
        JsonOutput jsonOutput = new();
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
        public void ExportTour(Tour SingleTour)
        {
            jsonOutput.ExportTour(SingleTour);
        }
        public void ImportTour()
        {

        }
    }
}