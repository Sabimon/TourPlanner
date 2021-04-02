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
        DBConn db = new DBConn();
        private static string key = "V5j8RGvth4UydnpUgMg2RYyVNpE12fJy";
        private static string path = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\";

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

                Task filetask = File.WriteAllTextAsync(path + "Test.json", response.Result.ToString() + "\n" + respBody);
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
                //db.InsertNewRoute(fileName);
                fileName = fileName + ".json";

                Task filetask = File.WriteAllTextAsync(path + fileName, response.Result.ToString() + "\n" + respBody);
                return respBody;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("Exception Caught!!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return e.Message;
            }
        }

        public string getMapImage()
        {
            try
            {
                var response = httpClient.GetStringAsync("https://www.mapquestapi.com/staticmap/v4/getmap?key=V5j8RGvth4UydnpUgMg2RYyVNpE12fJy&size=600,400&type=map&imagetype=png&declutter=false&shapeformat=cmp&shape=uajsFvh}qMlJsK??zKfQ??tk@urAbaEyiC??y]{|AaPsoDa~@wjEhUwaDaM{y@??t~@yY??DX&scenter=40.0337,-76.5047&ecenter=39.9978,-76.3545&traffic=4");
                string respBody = response.Result;
                Task filetask = File.WriteAllTextAsync(path + "Test.jpg", response.Result + "\n" + respBody);
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
