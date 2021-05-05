using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerDL;
using TourPlannerModels;

namespace TourPlannerBL
{
    public class DBBusiness
    {
        private DBInput dbIn = new();
        private DBOutput dbOut = new();

        public void InsertNewRoute(string FromDest, string ToDest)
        {
            dbIn.InsertNewRoute(FromDest, ToDest);
        }
        public void InsertLog(ObservableCollection<Logs> AddLogs, string Name)
        {
            int ID = dbOut.GetRouteID(Name);
            dbIn.InsertTourLogs(AddLogs, ID);
        }
        public void DeleteRoute(string Name)
        {
            dbIn.DeleteRoute(Name);
        }
        public ObservableCollection<Logs> GetLogs(string Name)
        {
            int ID = dbOut.GetRouteID(Name);
            return dbOut.GetLogs(ID);
        }
        public void InsertTourDescription(string distance, string totalTime, string highway, string access, string routeName)
        {
            int ID = dbOut.GetRouteID(routeName);
            dbIn.InsertTourDescription(distance, totalTime, highway, access, ID);
        }
        public void ChangeLog(ObservableCollection<Logs> ChangeLogs, string ChangeID)
        {
            int ID = Convert.ToInt32(ChangeID);
            dbIn.ChangeLog(ChangeLogs, ID);
        }
        public void DeleteLog(string DeleteID)
        {
            int ID = Convert.ToInt32(DeleteID);
            dbIn.DeleteLog(ID);
        }
    }
}
