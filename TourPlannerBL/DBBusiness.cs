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

        public void DeleteRoute(string Name)
        {
            dbIn.DeleteRoute(Name);
        }
        public ObservableCollection<Logs> GetLogs(string Name)
        {
            int ID;
            ID =dbOut.GetRouteID(Name);
            return dbOut.GetLogs(ID);
        }
    }
}
