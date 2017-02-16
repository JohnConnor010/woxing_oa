using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WX.Data;
namespace wwwroot.Manage.Finance
{
    public partial class AddDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList2.DataSource = ULCode.QDA.XSql.GetDataTable("select ID,Name from TE_Departments  where ParentID=0");
                DropDownList2.DataTextField = "Name";
                DropDownList2.DataValueField = "ID";
                DropDownList2.DataBind();
                drop3Bind();
                if (Request["ID"] != null && Request["ID"] != "")
                {
                    WX.Model.Sell.MODEL sell = WX.Model.Sell.NewDataModel(Request["ID"]);
                    DropDownList2.SelectedValue = sell.DivisionID.ToString();
                    DropDownList3.SelectedValue = sell.UserID.ToString();
                    Fee.Text = sell.Fee.ToString();
                    DropDownList1.SelectedValue = sell.TypeID.ToString();
                    TextBox5.Text = sell.PayingTime.ToDateTime().ToString("yyyy-MM-dd");
                }
            }
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count>0)
            {
                Norm.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT Name,Cost FROM [Count_TypeNorm] where TypeID=" + DropDownList1.SelectedValue + " order by Cost asc");
                Norm.DataTextField = "Name";
                Norm.DataValueField = "Cost";
                Norm.DataBind();
                Literal1.Text = ULCode.QDA.XSql.GetDataTable("Select ColumStr3 from Count_Type where ID=" + DropDownList1.SelectedValue ).Rows[0][0].ToString().Replace("<td>", "<td><div style='width:100px;'>").Replace("</td>", "</div></td>");
                Repeater2.DataSource = WX.Model.Sell.GetList(DropDownList1.SelectedValue, "", "");
                Repeater2.DataBind();
            }
            Norm.Visible = Norm.Items.Count > 0;
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ULCode.QDA.XSql.Execute("exec Count_DeleteSell " + e.CommandArgument); PageInit();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.Sell.MODEL sell;
            if (Request["ID"] != null && Request["ID"] != "")
                sell = WX.Model.Sell.NewDataModel(Request["ID"]);
            else
                sell = WX.Model.Sell.NewDataModel();
            sell.DivisionID.value = DropDownList2.SelectedValue;
            sell.UserID.value = DropDownList3.SelectedValue;
            sell.Fee.value = Fee.Text;
            sell.TypeID.value = DropDownList1.SelectedValue;
            sell.PayingTime.value = TextBox5.Text;
            sell.AddUserID.value = WX.Main.CurUser.UserID;

            int id = 0;
            if (Request["ID"] != null && Request["ID"] != "")
            {
                WX.Main.ExecuteDelete("Count_TypeColum_Value", "SellID", sell.ID.ToString());
                sell.Update();

                WX.Main.AddLog(WX.LogType.Default, "财务管理新增数据！", String.Format("{0}", DropDownList3.SelectedItem.Text +DropDownList1.SelectedItem.Text+ "：" + Fee.Text));
                id = sell.ID.ToInt32();
            }
            else
            {
                id = sell.Insert(true);
                sell.ID.value = id;
            }
            string valuestr1, valuestr2, valuestr3;
            valuestr1= "<td>" + DropDownList1.SelectedItem.Text + "</td><td>" + sell.Fee.ToString() + "</td>";
             valuestr2 = "<td>" + WX.CommonUtils.GetRealNameListByUserIdList(DropDownList3.SelectedValue) + "</td><td>" + DropDownList1.SelectedItem.Text + "</td><td>" + sell.Fee.ToString() + "</td>";
             valuestr3 = "<td>" + DropDownList2.SelectedItem.Text + "</td><td>" + WX.CommonUtils.GetRealNameListByUserIdList(DropDownList3.SelectedValue) + "</td><td>" + DropDownList1.SelectedItem.Text + "</td><td>" + sell.Fee.ToString() + "</td>";
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_TypeColum] where TypeID=" + sell.TypeID.ToString() + " order by OrderNo asc");
            System.Data.DataTable dataone = new System.Data.DataTable();
            object[] objs = new Object[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataone.Columns.Add(dt.Rows[i]["Mark"].ToString());
                objs[i] = "";
            }
            dataone.Rows.Add(objs);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] =Convert.ToDouble(dataone.Compute(WX.Model.TypeColum.GetFormula(this, dt.Rows[i]["Formula"].ToString(), dataone), "")).ToString("0.00");
                if(dt.Rows[i]["Visible1"].ToString()=="1")
                valuestr1 += "<td>" + dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] + "</td>";
                if (dt.Rows[i]["Visible2"].ToString() == "1")
                    valuestr2 += "<td>" + dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] + "</td>";
                if (dt.Rows[i]["Visible3"].ToString() == "1")
                    valuestr3 += "<td>" + dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] + "</td>";
                ULCode.QDA.XSql.Execute(String.Format("insert into Count_TypeColum_Value values({0},{1},'{2}',{3})", dt.Rows[i]["ID"].ToString(), id.ToString(), dt.Rows[i]["Formula"].ToString(), dataone.Rows[0][dt.Rows[i]["Mark"].ToString()]));
            }
            sell = WX.Model.Sell.NewDataModel(id);
            sell.Valuestr1.value = valuestr1;
            sell.Valuestr2.value = valuestr2;
            sell.Valuestr3.value = valuestr3;
            sell.Update(); 
            for (int i = 0; i < this.Master.FindControl("ContentPlaceHolder").Controls.Count; i++) 
             {
                 if (this.Master.FindControl("ContentPlaceHolder").Controls[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                     ((TextBox)(this.Master.FindControl("ContentPlaceHolder").Controls[i])).Text = "";
             } 
            PageInit();
        }


        private void drop3Bind()
        {
            DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_Type] where DivisionID="+DropDownList2.SelectedValue+" order by ID asc");
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
            string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + DropDownList2.SelectedValue).ToColValueList(",", 0);
            DropDownList3.Items.Clear();
            DropDownList3.DataSource = ULCode.QDA.XSql.GetDataTable("Select * from view_DeptUsersAll where  DepartmentID in(" + DropDownList2.SelectedValue + (ids != "" ? "," + ids : "") + ")");
            DropDownList3.DataTextField = "RealName";
            DropDownList3.DataValueField = "UserID";
            DropDownList3.DataBind();
            PageInit();
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drop3Bind();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }
    }
}