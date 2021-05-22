using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool StringValidation(string Input)
        {
            Regex regex = new Regex("[^a-z]+[^A-Z]+");
            //code snacked from https://abundantcode.com/how-to-allow-only-numeric-input-in-a-textbox-in-wpf/
            if (regex.IsMatch(Input))
            {
                return true;
            }
            return false;
        }
    }
}
