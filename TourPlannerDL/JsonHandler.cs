using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TourPlannerDL
{
    public class JsonHandler
    {
        DBInput dbIn = new();
        public void DeserializeJSON(string json)
        {
            var jsonData = JObject.Parse(json);
            JsonSerializer serializer = new JsonSerializer();
            var distance = jsonData["route"]["distance"].ToString();
            var totalTime = jsonData["route"]["formattedTime"].ToString();
            var highway = jsonData["route"]["hasHighway"].ToString();
            var access = jsonData["route"]["hasAccessRestriction"].ToString();
            dbIn.InsertTourLogs(distance, totalTime, highway, access);
        }
    }
}