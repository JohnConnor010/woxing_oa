using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_DutyDetail : System.Web.UI.Page
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
                        li.Text += (dt.Rows[j]["GradeID"].ToString() == "0" ? "" : "<img alt='" + dt.Rows[j]["GradeID"].ToString() + "级（" + dt.Rows[j]["GradeName"].ToString() + "）' src='" + this.getGradeUrl(Convert.ToInt32(dt.Rows[j]["GradeID"])) + "'/>") + "<a title='职务全称：" + dt.Rows[j]["Name"] + "\n当前人员：" + dt.Rows[j]["UsersName"] + "\n职务级别:" + dt.Rows[j]["GradeID"].ToString() + "级（" + dt.Rows[j]["GradeName"].ToString() + "）\n限制人数：" + dt.Rows[j]["Persons"] + "' href=\"javascript:void(0)\">" + (dt.Rows[j]["Name"].ToString().Length > 4 ? dt.Rows[j]["Name"].ToString().Substring(0, 4) : dt.Rows[j]["Name"].ToString()) + "</a>&nbsp;&nbsp;";
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
        public string getGradeUrl(int grade)
        {
            if (grade <= 2)
                grade++;
            else
                grade = (grade / 3) + 3;
            return String.Format("/images/Grade/{0}.jpg",grade);
        }
    }
}