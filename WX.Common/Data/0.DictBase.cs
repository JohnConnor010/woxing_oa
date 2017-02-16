using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Data
{
    public class DictBase
    {
        #region //1.GetTable
        /// <summary>
        /// 从Sql语句获取DataTable
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sSql)
        {
            if (String.IsNullOrEmpty(sSql)) return null;
            return ULCode.QDA.XSql.GetDataTable(sSql);
        }
        #endregion

        #region //2.GetListItems
        /// <summary>
        /// 从Sql语句获取ListItems集合
        /// </summary>
        /// <param name="sSql">Sql语句</param>
        /// <returns></returns>
        public static ListItem[] GetListItems(string sSql)
        {
            return GetListItems(GetDataTable(sSql));
        }
        public static ListItem[] GetListItems(DataTable dt)
        {
            if (dt == null) return null;
            List<ListItem> li = new List<ListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                li.Add(new ListItem(Convert.ToString(dr[1]), Convert.ToString(dr[0])));
            }
            return li.ToArray();
        }
        #endregion

        #region //3.BindListCtrl
        /// <summary>
        /// 直接将Sql语句（格式：id+name，两个字段）
        /// </summary>
        /// <param name="sSql">Sql语句</param>
        /// <param name="listCtrl">列表控件</param>
        /// <param name="defaultValue">默认值(格式：值#文本)</param>
        /// <param name="SelectedValue">选择值</param>
        public static void BindListCtrl(String sSql, object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems(sSql), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        public static void BindListCtrl(ListItem[] listItems, object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            if (String.IsNullOrEmpty(textFormat))
            {
                textFormat = "{0}";
            }
            if (listCtrl is ListControl)
            {
                ListControl lc = (ListControl)listCtrl;
                lc.Items.Clear();
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    string[] arr_d = defaultValue.Split('#');
                    lc.Items.Add(new ListItem(arr_d[1], arr_d[0]));
                }
                foreach (ListItem li in listItems)
                {
                    li.Text = String.Format(textFormat, li.Text);
                }
                lc.Items.AddRange(listItems);
                if (!String.IsNullOrEmpty(SelectedValue))
                {
                    lc.SelectedValue = SelectedValue;
                }
            }
            else if (listCtrl is HtmlSelect)
            {
                HtmlSelect lc = (HtmlSelect)listCtrl;
                lc.Items.Clear();
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    string[] arr_d = defaultValue.Split('#');
                    lc.Items.Add(new ListItem(arr_d[1], arr_d[0]));
                }
                foreach (ListItem li in listItems)
                {
                    li.Text = String.Format(textFormat, li.Text);
                }
                lc.Items.AddRange(listItems);
                if (!String.IsNullOrEmpty(SelectedValue))
                {
                    lc.Value = SelectedValue;
                }
            }
        }
        #endregion
    }
}
