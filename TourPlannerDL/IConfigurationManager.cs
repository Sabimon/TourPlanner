using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerDL
{
    public interface IConfigurationManager
    {
        string GetAppSetting(string key);
        ConnectionStringSettings GetConnectionString(string key);
    }
}
