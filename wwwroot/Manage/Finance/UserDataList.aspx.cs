using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Finance
{
    public partial class UserDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList2.DataSource = ULCode.QDA.XSql.GetDataTable("select ID,Name from TE_Departments  where ParentID=0");
                DropDownList2.DataTextField = "Name";
                DropDownList2.DataValueField = "ID";
                DropDownList2.DataBind();
               
                InitDrop3();
                PageInit();
                if (Request["type"] == "1")
                {
                    MenuBar1.CurIndex = 3;
                    HiddenField1.Value = "2";
                }
            }
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count > 0)
            {
                Literal1.Text = ULCode.QDA.XSql.GetDataTable("Select ColumStr" + Request["type"] + " from Count_Type where ID=" + DropDownList1.SelectedValue).Rows[0][0].ToString().Replace("<td>", "<td><div style='width:100px;'>").Replace("</td>", "</div></td>");
                if (Request["type"] == "1")
                    Repeater2.DataSource = WX.Model.Sell.GetListUser(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue);
                else
                    Repeater2.DataSource = WX.Model.Sell.GetListDivi(DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue);
                Repeater2.DataBind();
                DropDownList1.Visible = DropDownList3.Visible = true;
                Label1.Text = "";
            }
            else {
                DropDownList1.Visible = DropDownList3.Visible = false;
                Label1.Text = "<br><br><center>事业部无查询内容！</center>";
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }
        private void InitDrop3()
        {
            DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_Type] where DivisionID=" + DropDownList2.SelectedValue + " order by ID asc");
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();

            if (DropDownList1.Items.Count > 0)
            {
                if (DropDownList2.SelectedValue != "")
                {
                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + DropDownList2.SelectedValue).ToColValueList(",", 0);
                    DropDownList3.DataSource = ULCode.QDA.XSql.GetDataTable("Select * from view_DeptUsersAll where  DepartmentID in(" + DropDownList2.SelectedValue + (ids != "" ? "," + ids : "") + ")");
                }
                else
                    DropDownList3.DataSource = ULCode.QDA.XSql.GetDataTable("Select * from view_DeptUsers");

                DropDownList3.DataTextField = "RealName";
                DropDownList3.DataValueField = "UserID";
                DropDownList3.DataBind();
                DropDownList3.Items.Add(new ListItem("全部", ""));
                DropDownList3.SelectedValue = "";
            }
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDrop3();
            PageInit();
        }
    }
}
