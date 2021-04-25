using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerDL;

namespace TourPlannerBL
{
    public class DBBusiness
    {
        private DBInput dbIn = new();

        public void InsertNewRoute(string FromDest, string ToDest)
        {
            dbIn.InsertNewRoute(FromDest, ToDest);
        }

        public void DeleteRoute(string name)
        {
            dbIn.DeleteRoute(name);
        }
    }
}
