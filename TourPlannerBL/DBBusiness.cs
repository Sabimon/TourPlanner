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
        public void InsertLog(ObservableCollection<Logs> AddLogs, int ID)
        {
            dbIn.InsertTourLogs(AddLogs, ID);
        }
        public void DeleteRoute(string Name)
        {
            dbIn.DeleteRoute(Name);
        }
        public ObservableCollection<Logs> GetLogs(ObservableCollection<Logs> Logs, int ID)
        {
            foreach (Logs log in dbOut.GetLogs(ID))
            {
                Logs.Add(log);
            }
            return Logs;
        }
        public void InsertTourDescription(ObservableCollection<Description> Description, string routeName)
        {
            int ID = dbOut.GetRouteID(routeName);
            if (dbOut.CountRouteIDInDescription(ID) < 1) //no description added yet
            {
                dbIn.InsertTourDescription(Description, ID);
            }
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
        public int GetRouteID(string TourName)
        {
            return dbOut.GetRouteID(TourName);
        }
    }
}
