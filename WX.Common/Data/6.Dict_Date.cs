using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Text;

namespace WX.Data
{
    public partial class Dict
    {
        #region //1.年
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">可以为年份(2000)，也可为偏差值（-2）</param>
        /// <param name="end">可以为年份(2000)，也可为偏差值（-2）</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ListItem[] GetListItems_Year(int start, int end)
        {
            List<ListItem> lli = new List<ListItem>();
            if (start < 0) start = DateTime.Now.AddYears(start).Year;
            if (end < 50) end = DateTime.Now.AddYears(end).Year;
            for (int y = start; y <= end; y++)
            {
                lli.Add(new ListItem(String.Format("{0}", y), String.Format("{0}", y)));
            }
            return lli.ToArray();
        }
        public static void BindListCtrl_Year(object listCtrl, string textFormat, string defaultValue, string SelectedValue,int start,int end)
        {
            BindListCtrl(GetListItems_Year(start,end), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //2.月
        public static ListItem[] GetListItems_Month()
        {
            List<ListItem> lli = new List<ListItem>();
            for (int m = 1; m <= 12; m++)
            {
                lli.Add(new ListItem(String.Format("{0}", m), String.Format("{0}", m)));
            }
            return lli.ToArray();
        }
        public static void BindListCtrl_Month(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Month(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //1.日
        public static ListItem[] GetListItems_Day(int year, int month)
        {
            List<ListItem> lli = new List<ListItem>();
            if (year != 0)
            {
                int start = 1;
                //get end
                int end = 0;
                DateTime dTemp = DateTime.Parse(String.Format("{0}-{1}-{2}", year, month, 1));
                dTemp = dTemp.AddMonths(1);
                dTemp = DateTime.Parse(String.Format("{0}-{1}-{2}", dTemp.Year, dTemp.Month, 1));
                dTemp = dTemp.AddDays(-1);
                end = dTemp.Day;
                for (int y = start; y <= end; y++)
                {
                    lli.Add(new ListItem(String.Format("{0}", y), String.Format("{0}", y)));
                }
            }
            return lli.ToArray();
        }
        public static void BindListCtrl_Day(object listCtrl, string textFormat, string defaultValue, string SelectedValue, int year, int month)
        {
            BindListCtrl(GetListItems_Day(year, month), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
    }
}
