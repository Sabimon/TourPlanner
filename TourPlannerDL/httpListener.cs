using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace TourPlannerDL
{
    public class httpListener
    {
        private static httpListener instance = null;
        private static HttpClient httpClient = null;
        private static string key = "V5j8RGvth4UydnpUgMg2RYyVNpE12fJy";
        private static string path = "C:\\Users\\Lenovo\\source\\repos\\TourPlanner\\TourPlannerDL\\";
        private static string fileName = "";

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
                var response = httpClient.GetStringAsync("http://www.mapquestapi.com/directions/v2/route?key=" + key + "&from=Clarendon Blvd,Arlington,VA" + "&to=2400+S+Glebe+Rd,+Arlington,+VA");
                string respBody = response.Result;

                Debug.WriteLine("OUTPUT FROM WEBSITE_\n" + respBody);

                Task filetask = File.WriteAllTextAsync(path + fileName + ".txt", response.Result.ToString() + "\n" + respBody);
                return respBody;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("Exception Caught!!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return e.Message;
            }
        }
    }
}
