using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Data;

namespace TourPlannerDL
{
    public class TourInfos
    {
        [JsonInclude]
        public IList<string> legs { get; set; }
        [JsonInclude]
        public IList<string> maneuvers { get; set; }
        [JsonInclude]
        public float distance { get; set; }
        [JsonInclude]
        public float formattedTime { get; set; }
    }

    public class JsonHandler
    {
        public void DeserializeJSON()
        {
            string filename = @$"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\RouteResponses\Floridsdorf-Kagran.json";
            //TourInfos tour = JsonConvert.DeserializeObject<TourInfos>(File.ReadAllText(filename));
            TourInfos tour;
            //var tour = System.Text.Json.JsonSerializer.Deserialize<TourInfos>(File.ReadAllText(filename));
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(filename))
            {
                tour = (TourInfos)serializer.Deserialize(file, typeof(TourInfos));
            }

            using (StreamWriter sw = new StreamWriter(@$"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\RouteResponses\test.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, tour.distance);
            }
        }
    }
}