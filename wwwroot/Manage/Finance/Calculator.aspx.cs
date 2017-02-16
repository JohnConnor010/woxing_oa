using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace wwwroot.Manage.Finance
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList2.DataSource = ULCode.QDA.XSql.GetDataTable("select ID,Name from TE_Departments  where ParentID=0");
                DropDownList2.DataTextField = "Name";
                DropDownList2.DataValueField = "ID";
                DropDownList2.DataBind(); 
                if (Request["Type"] != null && Request["Type"] != "")
                {
                    DropDownList2.SelectedValue = ULCode.QDA.XSql.GetDataTable("SELECT DivisionID FROM [Count_Type] where ID=" + Request["Type"]).Rows[0][0].ToString();
                        drop1Bind();
                        DropDownList1.SelectedValue = Request["Type"];
                        DropDownList1.Enabled = false;
                   
                }
                else
                drop1Bind();
               PageInit();
            }
        }
        private void drop1Bind()
        {
            DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_Type] where DivisionID="+DropDownList2.SelectedValue+" order by ID asc");
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count > 0)
            {
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_TypeColum] where TypeID=" + DropDownList1.SelectedValue + " order by OrderNo asc");
                DataList2.DataSource = dt;
                DataList2.DataBind();
                DataList3.DataSource = dt;
                DataList3.DataBind();
            }

        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drop1Bind();
            PageInit();
        }
        protected void Button21_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text.Trim() == "" || TextBox4.Text.Trim() == "" || TextBox1.Text.Trim() == "")
            {
                ULCode.Debug.Alert("名称、列标识、公式均不可为空！");
                return;
            }
            WX.Model.TypeColum.MODEL tcount;
            string wherestr = "TypeID=" + DropDownList1.SelectedValue + " and Mark='" + TextBox4.Text.Trim()+"'";
            if (HiddenID.Value != "")
            {
                tcount = WX.Model.TypeColum.NewDataModel(HiddenID.Value);
                wherestr += " and ID !=" + HiddenID.Value;
            }
            else
                tcount = WX.Model.TypeColum.NewDataModel();
            tcount.TypeID.value = DropDownList1.SelectedValue;
            tcount.Name.value = TextBox2.Text;
            tcount.Mark.value = TextBox4.Text;
            if (tcount.IsNull(wherestr))
            {
                ULCode.Debug.Alert("列标识已存在！");
                return;
            }
            tcount.OrderNo.value = TextBox3.Text;
            tcount.FormulaShow.value = TextBox1.Text;
            tcount.Formula.value = HiddenField3.Value;
            tcount.Visible1.value = CheckBox1.Checked ? 1 : 0;
            tcount.Visible2.value = CheckBox2.Checked ? 1 : 0;
            tcount.Visible3.value = CheckBox3.Checked ? 1 : 0;
            if (HiddenID.Value != "")
                tcount.Update();
            else
            {
                tcount.Insert(true);
            }
            string columstr1="<td>销售类型</td></td><td>销售额</td>";
            string columstr2="<td>销售人<td>销售类型</td></td><td>销售额</td>";
            string columstr3="<td>事业部</td><td>销售人<td>销售类型</td></td><td>销售额</td>";
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_TypeColum] where TypeID="+tcount.TypeID.ToString()+" order by OrderNo asc");
            string cc = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cc= "<td>" + dt.Rows[i]["Mark"] + dt.Rows[i]["Name"] + "<br/>" + WX.Main.ShowFormula(dt.Rows[i]["FormulaShow"].ToString()) + "</td>";
                    columstr1 += dt.Rows[i]["Visible1"].ToString() == "1"?cc:"";
                    columstr2 += dt.Rows[i]["Visible2"].ToString() == "1" ? cc : "";
                    columstr3 += dt.Rows[i]["Visible3"].ToString() == "1" ? cc : "";
            }
            WX.Main.ExcuteUpdate("Count_Type", "ColumStr1='" + columstr1 + "',ColumStr2='" + columstr2 + "',ColumStr3='" + columstr3 + "'", "ID=" + tcount.TypeID.ToString());
            PageInit();
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox3.Text = "";
            TextBox1.Text = "";
            HiddenID.Value = "";
            HiddenField1.Value = "";
            HiddenField2.Value = "";
            HiddenField3.Value = "";
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
        }

        protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Up")
            {
                WX.Model.TypeColum.MODEL tcount = WX.Model.TypeColum.NewDataModel(e.CommandArgument);
                if (tcount != null)
                {
                    HiddenID.Value = e.CommandArgument.ToString();
                    TextBox2.Text = tcount.Name.ToString();
                    TextBox4.Text = tcount.Mark.ToString();
                    TextBox3.Text = tcount.OrderNo.ToString();
                    TextBox1.Text = tcount.FormulaShow.ToString();
                    HiddenField3.Value = tcount.Formula.ToString();
                    CheckBox1.Checked = tcount.Visible1.ToInt32() == 1;
                    CheckBox2.Checked = tcount.Visible2.ToInt32() == 1;
                    CheckBox3.Checked = tcount.Visible3.ToInt32() == 1;
                }
            }
            else if (e.CommandName == "Del")
            {
                WX.Model.TypeColum.MODEL tcount = WX.Model.TypeColum.NewDataModel(e.CommandArgument);
                if(tcount!=null)
                    tcount.Delete();
                PageInit();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }

    }
}
