using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_DutyCtrl : System.Web.UI.Page
    {
        DataTable DT_Duties = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.FillData();
            }
        }
        private void FillData()
        {
            this.DT_Duties = ULCode.QDA.XSql.GetDataTable("select ID,Name,DutyCatagoryID from te_duties ");
            //添加职务分类行
            DataTable dtC = WX.Data.Dict.GetDataTable_DutyCatagory();
            foreach (DataRow dr in dtC.Rows)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = Convert.ToString(dr["Name"]);
                bf.DataField = "DC" + Convert.ToString(dr["ID"]);
                Gv_duty.Columns.Add(bf);
            }
            //添加部门列
            System.Data.DataTable dtR = WX.Model.DutyDetail.GetDeptList();
            Gv_duty.DataSource = dtR;
            Gv_duty.DataBind();
            Gv_duty.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_duty.HeaderStyle.Height = Unit.Pixel(40);
        }
        protected void Gv_duty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    //创建DropDownList
                    DataTable dt = WX.Model.DutyDetail.GetTable(e.Row.Cells[0].Text, e.Row.Cells[i].Text);
                        Label li = new Label();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string color = "";
                            int persons=Convert.ToInt32(dt.Rows[j]["Persons"].ToString());
                            int users=dt.Rows[j]["UsersName"].ToString() == ""?0:dt.Rows[j]["UsersName"].ToString().Split(',').Length-1;
                            if (persons == 0 && users == 0)
                                color = " style='color:#aaaaaa;'";
                            else if (persons > users)
                                color = " style='color:red;'";
                            string userName = Convert.ToString(dt.Rows[j]["UsersName"]);
                            if (userName.EndsWith(",")) userName = userName.Substring(0, userName.Length - 1);
                            li.Text += "<a" + color + " title='职务全称：" + dt.Rows[j]["Name"] 
                                + "\n当前人员：" + userName 
                                + "\n职务级别:" + dt.Rows[j]["GradeID"].ToString() + "级（" + dt.Rows[j]["GradeName"].ToString() 
                                + "）\n限制人数：" + dt.Rows[j]["Persons"] + "' href=\"javascript:PopupIFrame('Duty_DutyDetailEdit.aspx?DutyDetailID=" + dt.Rows[j]["ID"].ToString() + "&type=1','编辑具体职务','sdf','sdf',468,240)\">" + (dt.Rows[j]["Name"].ToString().Length > 4 ? dt.Rows[j]["Name"].ToString().Substring(0, 4) : dt.Rows[j]["Name"].ToString()) + "（" 
                                + (userName.Length ==0 ? 0 : userName.Split(new String[]{"，",","}, StringSplitOptions.RemoveEmptyEntries).Length) + "/" + dt.Rows[j]["Persons"].ToString() + "）</a>&nbsp;&nbsp;";
                            if (j > 0 && (j + 1) % 5 == 0)
                            {
                                li.Text += "<br/>";
                            }
                        }
                        li.Visible = true;
                        li.ID = "li_" + e.Row.Cells[0].Text + "_" + e.Row.Cells[i].Text;
                        e.Row.Cells[i].Controls.Add(li);
                }
            }
        }
    }
}