using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerBL
{
    public class StringHandler
    {
        public List<String> StringSplitter(string Input)
        {
            string[] subs = Input.Split('-');
            List<String> Result = new List<string>(subs);
            /*foreach (var sub in subs)
            {
                Result.Add(sub);
            }*/
            return Result;
        }
    }
}
