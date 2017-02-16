using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX
{
    public class Soft
    {
        public static string Name
        {
            get 
            {
                return Convert.ToString(ConfigurationManager.AppSettings["SoftName"]);
            }
        }
    }
}
