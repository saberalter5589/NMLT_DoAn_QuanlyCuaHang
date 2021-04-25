using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Commons
{
    public class Error
    {
        private Dictionary<string, string> ErrorList = new Dictionary<string, string>
        {
            { "ERROR_001", ""},
            { "ERROR_002", ""},
            { "ERROR_003", ""},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getErrorListBasedOnKey(string key) 
        {
            if (!ErrorList.ContainsKey(key)) 
            {
                return ErrorList[key];
            }

            return "Not found any error content matched key";
        }
    }
}
