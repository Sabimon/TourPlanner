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
            //code snacked from https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp/1181426
            if (Regex.IsMatch(Input, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            return false;
        }
    }
}
