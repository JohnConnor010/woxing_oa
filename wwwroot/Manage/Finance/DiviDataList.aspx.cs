using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Finance
{
    public partial class DiviDataList : System.Web.UI.Page
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
                    DropDownList3.DataSource = WX.Main.GetDeptUsersAll();
                    DropDownList3.DataTextField = "RealName";
                    DropDownList3.DataValueField = "UserID";
                    DropDownList3.DataBind();
                    DropDownList3.Items.Add(new ListItem("全部", ""));
                    DropDownList3.SelectedValue = "";
                    PageInit();
                }
                else {
                    DropDownList1.Visible = DropDownList3.Visible = Repeater2.Visible = false;
                    Label1.Text = "<br><br><center>事业部无查询内容！</center>";
                }
            }
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count > 0)
            {
                Literal1.Text = ULCode.QDA.XSql.GetDataTable("Select ColumStr2 from Count_Type where ID=" + DropDownList1.SelectedValue).Rows[0][0].ToString().Replace("<td>", "<td><div style='width:100px;'>").Replace("</td>", "</div></td>");
                Repeater2.DataSource = WX.Model.Sell.GetListDivi(DropDownList1.SelectedValue, WX.Main.GetParentDeptID().ToString(), DropDownList3.SelectedValue);
                Repeater2.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }
    }
}