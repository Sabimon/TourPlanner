using Newtonsoft.Json.Linq;
using TourPlannerModels;
using TourPlannerDL;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace TourPlannerBL
{
    public class JsonHandler
    {
        DBBusiness db = new();
        JsonOutput jsonOutput = new();
        StringHandler strHandler = new();
        public void DeserializeAPIResponse(string json, string routeName)
        {
            ObservableCollection<Description> Description= new();
            var jsonData = JObject.Parse(json);
            Description.Add(new Description()
            {
                Distance = jsonData["route"]["distance"].ToString(),
                Time = jsonData["route"]["formattedTime"].ToString(),
                Highway = jsonData["route"]["hasHighway"].ToString(),
                Access = jsonData["route"]["hasAccessRestriction"].ToString()
            });
            //var narratives = jsonData["route"]["legs"]["origNarrative"].ToString();
            db.InsertTourDescription(Description, routeName);
        }
        public void ExportTour(Tour SingleTour)
        {
            jsonOutput.ExportTour(SingleTour);
        }
        public Tour ImportTour(Tour SingleTour)
        {
            SingleTour =jsonOutput.ImportTour(SingleTour);
            if (SingleTour != null)
            {
                db.DeleteRoute(SingleTour.Name);
                List<String> Destination = strHandler.StringSplitter(SingleTour.Name);
                db.InsertNewRoute(Destination[0], Destination[1]);
                SingleTour.TourID= db.GetRouteID(SingleTour.Name);
                if (SingleTour.Logs != null)
                {
                    db.InsertLog(SingleTour.Logs, SingleTour.TourID);
                }
                if (SingleTour.Description.Count > 0)
                {
                    db.InsertTourDescription(SingleTour.Description, SingleTour.Name);
                }
            }
            return SingleTour;
        }
    }
}