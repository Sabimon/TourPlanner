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
    public class route
    {
        public float distance { get; set; }
        public DateTime formattedTime { get; set; }
    }
    public class Info
    {
        //public List<infos> route { get; set; }
    }

    public class JsonHandler
    {
        public void DeserializeJSON()
        {
            string filename = @$"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\RouteResponses\Floridsdorf-Kagran.json";
            var json = File.ReadAllText(filename);
            var jsonData = JObject.Parse(json);
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(@$"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\RouteResponses\test.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, jsonData["route"]["distance"]);
            }
        }
    }
}