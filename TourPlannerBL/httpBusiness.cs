﻿using System;
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

        public void FindRoute(string FromDest, string ToDest)
        {
            http.FindRoute(FromDest, ToDest);
        }
    }
}
