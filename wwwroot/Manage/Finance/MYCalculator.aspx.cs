using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Finance
{
    public partial class MYCalculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (WX.Main.IsFinanceRole())
                {
                    DropDownList2.DataSource = ULCode.QDA.XSql.GetDataTable("select ID,Name from TE_Departments  where ParentID=0");
                    DropDownList2.DataTextField = "Name";
                    DropDownList2.DataValueField = "ID";
                    DropDownList2.DataBind();
                    DropDownList2.Visible = true;
                }
                else
                    DropDownList2.Visible = false;
                BindDrop1();
            }
        }
        private void BindDrop1()
        {
            DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [Count_Type] where DivisionID=" + (DropDownList2.Visible ? DropDownList2.SelectedValue : WX.Main.GetParentDeptID().ToString()) + " order by ID asc");
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
            Literal3.Text = "";
            if (DropDownList1.Items.Count > 0)
                PageInit();
            Norm.Visible = Norm.Items.Count > 0;
            if (DropDownList2.Visible == false && DropDownList1.Items.Count == 0)
                Literal3.Text = "<br><br><center>事业部无查询内容！</center>";
        }
        private void PageInit()
        {
            if (DropDownList1.Items.Count > 0)
            {
                Norm.DataSource = ULCode.QDA.XSql.GetDataTable("SELECT Name,Cost FROM [Count_TypeNorm] where TypeID=" + DropDownList1.SelectedValue + " order by Cost asc");
                Norm.DataTextField = "Name";
                Norm.DataValueField = "Cost";
                Norm.DataBind();
            }
            Literal1.Text = Literal2.Text = "";
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDrop1();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageInit();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (Convert.ToInt32(Fee.Text.Trim()) > 0)
                {
                    WX.Model.Sell.MODEL sell;
                    sell = WX.Model.Sell.NewDataModel();
                    sell.Fee.value = Fee.Text;
                    sell.TypeID.value = DropDownList1.SelectedValue;
                    string valuestr1;
                    valuestr1 = "<td>" + DropDownList1.SelectedItem.Text + "</td><td>" + sell.Fee.ToString() + "</td>";
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
                        try
                        {
                            dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] = Convert.ToDouble(dataone.Compute(WX.Model.TypeColum.GetFormula(this, dt.Rows[i]["Formula"].ToString(), dataone), "")).ToString("0.00");
                        }
                        catch { Response.Write(dt.Rows[i]["Formula"].ToString()+"----"); }
                        if (dt.Rows[i]["Visible1"].ToString() == "1")
                            valuestr1 += "<td>" + dataone.Rows[0][dt.Rows[i]["Mark"].ToString()] + "</td>";
                    }
                    Literal1.Text = ULCode.QDA.XSql.GetDataTable("Select ColumStr1 from Count_Type where ID=" + DropDownList1.SelectedValue).Rows[0][0].ToString().Replace("<td>", "<td><div style='width:100px;'>").Replace("</td>", "</div></td>");
                    Literal2.Text = valuestr1;

                    WX.Main.AddLog(WX.LogType.Default, "计算业务提成！", String.Format("{0}", DropDownList1.SelectedItem.Text+"："+ Fee.Text ));
                }
            //}
            //catch { }
        }
    }
}