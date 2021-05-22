using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerDL;

namespace TourPlannerBL
{
    public class httpBusiness
    {
        private httpListener http = httpListener.Instance();
        private StringHandler strHander = new();

        public void FindRoute(string TourName)
        {
            List<String> Destination = strHander.StringSplitter(TourName);
            string respBody= http.FindRoute(Destination[0], Destination[1]);
            //http.FindRoute(FromDest, ToDest);
            JsonHandler json = new();
            json.DeserializeAPIResponse(respBody, TourName);
        }

        public async Task GetAndSaveImage(string TourName)
        {
            List<String> Destination = strHander.StringSplitter(TourName);
            await http.GetAndSaveImage(Destination[0], Destination[1]);
        }
    }
}
