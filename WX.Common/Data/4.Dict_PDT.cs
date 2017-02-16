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
    public partial class Dict:DictBase
    {
        #region //1.产品分类
        public static DataTable GetDataTable_ProductCatagory()
        {
            return GetDataTable("select ID,Name from PDT_ProductCategory");
        }
        public static ListItem[] GetListItems_ProductCatagory()
        {
            return GetListItems(GetDataTable_ProductCatagory());
        }
        public static void BindListCtrl_ProductCatagory(object listCtrl, string textFormat, string defaultValue, string SelectedValue)
        {
            BindListCtrl(GetListItems_ProductCatagory(), listCtrl, textFormat, defaultValue, SelectedValue);
        }
        #endregion
    }
}
