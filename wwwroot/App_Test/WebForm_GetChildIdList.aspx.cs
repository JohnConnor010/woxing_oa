using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot
{
    public partial class WebForm_GetChildIdList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string sSql = String.Format("exec sp_get_tree_table 'Ass_CateGory','Id','Name','ParentId','Id',{0},1,4", 2);
            //string s = "2," + ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList();
            //sSql = "Select * from Ass_WareHouse where Id in (" + s + ")";
            //Response.Write(sSql);
            //ULCode.QDA.XSql.GetXDataTable("").ToColValueList();
        }
    }
}