using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace WX
{
    public class PlanSet
    {
        public static int Plan_WeekFirst
        {
            get
            {
                if (ConfigurationManager.AppSettings["Plan-WeekFirst"] != null)
                    return Convert.ToInt32(ConfigurationManager.AppSettings["Plan-WeekFirst"]);
                else
                    return 1;
            }
        }
        public static int Plan_WeekEnd
        {
            get
            {
                if (ConfigurationManager.AppSettings["Plan-WeekEnd"] != null)
                    return Convert.ToInt32(ConfigurationManager.AppSettings["Plan-WeekEnd"]);
                else
                    return 7;
            }
        }
        public static bool Plan_NewDayPlanIfNotWorkDay
        {
            get
            {
                if (ConfigurationManager.AppSettings["Plan-DayPlanIfNotWorkDay"] != null)
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["Plan-DayPlanIfNotWorkDay"]);
                else
                    return false;
            }
        }
    }
}
