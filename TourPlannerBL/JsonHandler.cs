using Newtonsoft.Json.Linq;

namespace TourPlannerBL
{
    public class JsonHandler
    {
        DBBusiness db = new();
        public void DeserializeJSON(string json, string routeName)
        {
            var jsonData = JObject.Parse(json);
            var distance = jsonData["route"]["distance"].ToString();
            var totalTime = jsonData["route"]["formattedTime"].ToString();
            var highway = jsonData["route"]["hasHighway"].ToString();
            var access = jsonData["route"]["hasAccessRestriction"].ToString();
            var narratives = jsonData["route"]["legs"]["origNarrative"].ToString();
            db.InsertTourDescription(distance, totalTime, highway, access, routeName);
        }
    }
}