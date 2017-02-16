using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_Userjobs : System.Web.UI.Page
    {
        public string userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            userId = WX.Request.rUserId;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            Label1.Text = "当前员工姓名："+usermodel.RealName.ToString();
                pageinit();
            }
        }
        private void pageinit()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select *,tedd.Name,ted.Name DeptName,tedc.Name DutyCatagoryName from TU_User_X_DutyDetail tuxd left join TE_DutyDetail tedd on tuxd.DutyDetailID=tedd.ID left join TE_Departments ted on tedd.DepartentID=ted.ID left join TE_DutyCatagory tedc on tedd.DutyCatagoryID=tedc.ID where UserID='"+Request["UserID"]+"'");
            Gv_tfk.DataSource = dt;
            Gv_tfk.DataBind();
            if (Gv_tfk.Rows.Count > 0)
            {
                Gv_tfk.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_tfk.HeaderStyle.Height = Unit.Pixel(40);
            }
        }

        protected void Gv_tfk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from TU_User_X_DutyDetail where ID=" + e.CommandArgument);
            int row = ULCode.QDA.XSql.Execute("delete from TU_User_X_DutyDetail where ID=" +e.CommandArgument);
            //if (row > 0)
            //{
                WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(dt.Rows[0]["DutyDetailID"]));
                WX.Model.User.MODEL usermodel = WX.Request.rUser;
                dutydetail.UsersName.value = dutydetail.UsersName.ToString().Replace(usermodel.RealName.ToString() + ",", "");
                dutydetail.Update();
                // ULCode.Debug.Alert("员工职务添加成功！", "Userjobs.aspx");
                WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + usermodel.UserID.ToString() + "' and Backtableid=5 order by Starttime desc ");
                if (backlog != null)
                {
                    backlog.stoptime.value = DateTime.Now;
                    backlog.Nowtableid.value = 6;
                    backlog.Nowcolumid.value = e.CommandArgument;
                    backlog.Update();
                }
                pageinit();
            //}
                userId = WX.Request.rUserId;
        }

    }
}