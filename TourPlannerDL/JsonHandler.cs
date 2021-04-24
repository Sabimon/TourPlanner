using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Data;
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