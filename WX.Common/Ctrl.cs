using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
namespace WX
{
    public class Ctrl
    {
        /// <summary>
        /// 使用方法：
        /// this.GridView1.RowDataBound += new GridViewRowEventHandler(WX.Ctrl.GridView_DynamicColor_RowDataBound);
        /// </summary>
        public static void GridView_DynamicColor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string defaultColor = String.Empty;
                e.Row.Attributes.Add("onmouseover", "style.backgroundColor='lightyellow';style.cursor='hand';");
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='" + defaultColor + "';style.cursor='';");
            }
        }
    }
}
