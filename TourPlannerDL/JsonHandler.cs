using Newtonsoft.Json.Linq;

namespace TourPlannerDL
{
    public class JsonHandler
    {
        DBInput dbIn = new();
        public void DeserializeJSON(string json)
        {
            var jsonData = JObject.Parse(json);
            var distance = jsonData["route"]["distance"].ToString();
            var totalTime = jsonData["route"]["formattedTime"].ToString();
            var highway = jsonData["route"]["hasHighway"].ToString();
            var access = jsonData["route"]["hasAccessRestriction"].ToString();
            dbIn.InsertTourDescription(distance, totalTime, highway, access);
        }
    }
}