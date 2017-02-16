using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Finance
{
    public partial class MyDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_Type] where DivisionID=" + WX.Main.GetParentDeptID().ToString() + " order by ID asc");
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
                if (DropDownList1.Items.Count > 0)
                {
                PageInit();
                   
                }
                else
                {
                    DropDownList1.Visible = Repeater2.Visible  = false;
                    Literal2.Text = "<br><br><center>事业部无查询内容！</center>";
                }
            }
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count > 0)
            {
                Literal1.Text = ULCode.QDA.XSql.GetDataTable("Select ColumStr1 from Count_Type where ID=" + DropDownList1.SelectedValue).Rows[0][0].ToString().Replace("<td>", "<td><div style='width:100px;'>").Replace("</td>", "</div></td>");
                Repeater2.DataSource = WX.Model.Sell.GetList(DropDownList1.SelectedValue, "", WX.Main.CurUser.UserID);
                Repeater2.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }
    }
}