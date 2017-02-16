using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_AssignDeptCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dtime = new DateTime(2013, 1, 1);
                DateTime nowtime = DateTime.Now;
                while (nowtime.ToString("yyyy-MM") != dtime.AddMonths(-1).ToString("yyyy-MM"))
                {
                    ListItem li = new ListItem(nowtime.ToString("yyyy年MM月"), nowtime.ToString("yyyy-MM"));
                    DropDownList1.Items.Add(li);
                    nowtime = nowtime.AddMonths(-1);
                }
                BindRepeat2();
            }
        }
        private void BindRepeat2()
        {
            WX.Main.CurUser.LoadUserModel(false);
            DateTime dtime = Convert.ToDateTime(DropDownList1.SelectedValue+"-1");
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select RealName,isnull((select sum(Count) from WorkOrder_Orders where State=8 and year(StopTime)=" + dtime.Year.ToString() + " and month(StopTime)=" + dtime.Month.ToString() + " and ExecUserID=TU_Users.UserID),0) WorkCount from TU_Users where DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DateTime dtime = Convert.ToDateTime(DropDownList1.SelectedValue+"-1");
            if (dtime.AddMonths(1) <= DateTime.Now)
            {
                dtime = dtime.AddMonths(1);
                DropDownList1.SelectedValue = dtime.ToString("yyyy-MM");
            }
            BindRepeat2();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DateTime dtime = Convert.ToDateTime(DropDownList1.SelectedValue + "-1");
            if (dtime.AddMonths(-1) >= new DateTime(2013, 1, 1))
            {
                dtime = dtime.AddMonths(-1);
                DropDownList1.SelectedValue = dtime.ToString("yyyy-MM");
            }
            BindRepeat2();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeat2();
        }
    }
}