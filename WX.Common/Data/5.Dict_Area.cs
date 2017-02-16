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
        #region //1.省份
        public static DataTable GetDataTable_Province()
        {
            return GetDataTable("SELECT code,name FROM CRM_Province order by code");
        }
        public static ListItem[] GetListItems_Province()
        {
            return GetListItems(GetDataTable_Province());
        }
        public static void BindListCtrl_Province(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_Province(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //2.城市
        public static DataTable GetDataTable_City(string prov)
        {
            string sSql = String.Format("SELECT code,name FROM CRM_City Where ProvinceId='{0}' order by code", prov);
            return GetDataTable(sSql);
        }
        public static ListItem[] GetListItems_City(string prov)
        {
            return GetListItems(GetDataTable_City(prov));
        }
        public static void BindListCtrl_City(object listCtrl, string textFormat, string defaultValue, string SelectedValue, string prov)
        {
            BindListCtrl(GetListItems_City(prov), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
        #region //1.地县
        public static DataTable GetDataTable_Area(string city)
        {
            string sSql = String.Format("SELECT code,name FROM CRM_Area Where CityId='{0}' order by code", city);
            return GetDataTable(sSql);
        }
        public static ListItem[] GetListItems_Area(string city)
        {
            return GetListItems(GetDataTable_Area(city));
        }
        public static void BindListCtrl_Area(object listCtrl, string textFormat, string defaultValue, string SelectedValue, string city)
        {
            BindListCtrl(GetListItems_Area(city), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
    }
}
