using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TourPlannerDL
{
    public class httpListener
    {
        private static httpListener instance = null;
        private static HttpClient httpClient = null;
        private static string key = "V5j8RGvth4UydnpUgMg2RYyVNpE12fJy";
        public static string MapPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\";
        //private static string RoutePath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\RouteResponses\";

        public static httpListener Instance()
        {
            if (instance == null)
            {
                instance = new httpListener();
            }
            return instance;
        }


        public httpListener()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();

            }
        }

        public string TryConnection()
        {
            try
            {
                var response = httpClient.GetStringAsync("http://www.mapquestapi.com/directions/v2/route?key=" + key + "&from=Wien&to=Graz");
                string respBody = response.Result;
                return respBody;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("Exception Caught!!");
                Debug.WriteLine($"Message :{e.Message} ");
                return e.Message;
            }
        }
        public string FindRoute(string fromDestination, string toDestination)
        {
            try
            {
                var response = httpClient.GetStringAsync("http://www.mapquestapi.com/directions/v2/route?key=" + key + "&from=" + fromDestination + "&to=" + toDestination);
                string respBody = response.Result;
                string fileName = fromDestination + "-" + toDestination;
                //do JSON stuff
                JsonHandler json = new();
                json.DeserializeJSON(respBody);
                return respBody;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("Exception Caught!!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return e.Message;
            }
        }

        public async Task GetAndSaveImage(string fromDestination, string toDestination)
        {
            System.IO.Directory.CreateDirectory(MapPath);
            string fileName = fromDestination + "-" + toDestination;
            string fileLocation = $@"{MapPath}\{fileName}.jpg";
            using WebClient client = new();
            await client.DownloadFileTaskAsync(new Uri($"https://www.mapquestapi.com/staticmap/v5/map?key=V5j8RGvth4UydnpUgMg2RYyVNpE12fJy&size=1920,1080&start=Wien&end=Graz&format=jpg"), fileLocation);
        }
    }
}