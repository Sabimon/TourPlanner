using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using log4net;
using System.Configuration;

namespace TourPlannerDL
{
    public class httpListener
    {
        private static httpListener instance = null;
        private static HttpClient httpClient = null;
        private static string key = ConfigurationManager.AppSettings["MapQuestKey"];
        public static string MapPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\";
        private static readonly ILog log = LogManager.GetLogger(typeof(httpListener));

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
                log.Info("MapQuest Connection success");
                return respBody;
            }
            catch (HttpRequestException e)
            {
                log.Error($"MapQuest Connection failed, Error Message: {e.Message}");
                return e.Message;
            }
        }
        public string FindRoute(string fromDestination, string toDestination)
        {
            try
            {
                var response = httpClient.GetStringAsync("http://www.mapquestapi.com/directions/v2/route?key=" + key + "&from=" + fromDestination + "&to=" + toDestination);
                log.Info($"MapQuest Route Response");
                return response.Result;
            }
            catch (HttpRequestException e)
            {
                log.Error($"MapQuest Response failed, Error Message: {e.Message}");
                return e.Message;
            }
        }

        public async Task GetAndSaveImage(string fromDestination, string toDestination)
        {
            try
            {
                System.IO.Directory.CreateDirectory(MapPath);
                string fileName = fromDestination + "-" + toDestination;
                string fileLocation = $@"{MapPath}\{fileName}.jpg";
                using WebClient client = new();
                await client.DownloadFileTaskAsync(new Uri($"https://www.mapquestapi.com/staticmap/v5/map?key=V5j8RGvth4UydnpUgMg2RYyVNpE12fJy&size=1920,1080&start=Wien&end=Graz&format=jpg"), fileLocation);
                log.Info($"MapQuest Map Image Response");
            }
            catch
            {
                log.Error($"MapQuest Map Image Response not working");
            }
        }
    }
}