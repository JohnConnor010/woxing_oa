using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.HR
{
    public partial class HR_Leavejobs21 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            WX.HR.LeaveJob.MODEL LeaveJob = null;
            string userId = WX.Request.rUserId;
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            LeaveJob = WX.HR.LeaveJob.GetModel("select top 1 * from HR_LeaveJobs where UserID='" + userId + "' order by Addtime desc");
            if (LeaveJob != null)
            {
                ui_reason.Text = LeaveJob.reason.ToString();
                ui_days.Text = LeaveJob.days.ToString();
                ui_lasttime.Text = Convert.ToDateTime(LeaveJob.lasttime.ToString()).ToString("yyyy-MM-dd");
                ui_dempop.Text = LeaveJob.dempOpinion.ToString();
                ui_dempuser.Value = LeaveJob.dempManager.ToString();
                li_dempname.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.dempManager.ToString());
                ui_adminop.Text = LeaveJob.financialOpinion.ToString();
                ui_financialHandleManager.Value = LeaveJob.financialHandleManager.ToString();
                li_financialHandleManagername.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.financialHandleManager.ToString());
                ui_adminuser.Value = LeaveJob.financialManager.ToString();
                li_adminname.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.financialManager.ToString());
                ui_hrop.Text = LeaveJob.hrOpinion.ToString();
                ui_hruser.Value = LeaveJob.hrManager.ToString();
                li_hrname.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.hrManager.ToString());
                ui_bossop.Text = LeaveJob.bossOpinion.ToString();
                ui_bossuser.Value = LeaveJob.bossManager.ToString();
                li_bossname.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.bossManager.ToString());
                li_addtime.Text = ((DateTime)LeaveJob.Addtime.value).ToString("yyyy-MM-dd");

            }
            else
                li_addtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Employee.MODEL employee = WX.Request.rEmpolyee;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            MenuBar1.Param2 = employee.LoadSucceed || true ? usermodel.State.ToString() : "0";
            if (employee.LoadSucceed || true)
            {
                li_name.Text = usermodel.RealName.ToString();
                li_sex.Text = ((bool)employee.Sex.value ? "男" : "女");
                li_age.Text = ((DateTime)employee.Birthday.value).ToString("yyyy-MM-dd");
                li_edu.Text = employee.Edu.ToString();
                li_Mobile.Text = employee.Mobile.ToString();
                ui_demp.SelectedValue = usermodel.DepartmentID.ToString();

                bindjob();
                ui_duty.SelectedValue = usermodel.DutyId.ToString();
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + userId + "'");
                if (intojob == null)
                    Response.Redirect("HR_AddIntojobs.aspx?UserID=" + WX.Request.rUserId);
                li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_sqrname.Text = usermodel.RealName.ToString();
                if (usermodel.State.ToString() == "40" && LeaveJob != null)
                    Button1.Enabled = false;
            }
        }
        public void bindjob()
        {
            System.Data.DataTable dt = WX.Model.DutyDetail.GetTableDepartent(ui_demp.SelectedValue);
            this.ui_duty.DataSource = dt;
            this.ui_duty.DataTextField = "Name";
            this.ui_duty.DataValueField = "ID";
            this.ui_duty.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            string userId = WX.Request.rUserId;
            WX.HR.LeaveJob.MODEL LeaveJob= WX.HR.LeaveJob.GetModel("select top 1 * from HR_LeaveJobs where UserID='" + userId + "' order by Addtime desc");
            bool flag = true;
            int ofid =0;
            if (LeaveJob == null)
            {
                flag = false;
                LeaveJob = WX.HR.LeaveJob.NewDataModel();
                LeaveJob.Addtime.value = DateTime.Now;
                LeaveJob.UserID.value = userId;
                LeaveJob.reason.value = radio_reason.SelectedValue + "。" + ui_reason.Text;
                LeaveJob.days.value = ui_days.Text;
                LeaveJob.lasttime.value = ui_lasttime.Text;
            }else{
                ofid=LeaveJob.ID.ToInt32();
            }
            LeaveJob.dempOpinion.value = ui_dempop.Text;
            LeaveJob.dempManager.value = ui_dempuser.Value;
            LeaveJob.financialOpinion.value = ui_adminop.Text;
            LeaveJob.financialManager.value = ui_adminuser.Value;
            LeaveJob.financialHandleManager.value = ui_financialHandleManager.Value;
            LeaveJob.hrOpinion.value = ui_hrop.Text;
            LeaveJob.hrManager.value = ui_hruser.Value;
            LeaveJob.bossOpinion.value = ui_bossop.Text;
            LeaveJob.bossManager.value = ui_bossuser.Value;
            if (flag)
                LeaveJob.Update();
            else
            ofid = LeaveJob.Insert(true);

            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            usermodel.State.value = 40;
            usermodel.Update();
            WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=1", "UserID='" + userId + "'");
            //日志
            log.UserID.value = userId;
            log.Backtableid.value = 4;
            log.Backcolumid.value = ofid;
            log.Starttime.value = DateTime.Now;
            log.BackDutyID.value = usermodel.DutyId.value;
            log.BackDempID.value = usermodel.DepartmentID.value;
            log.Content.value = "员工离职";
            WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + userId + "' order by Starttime desc");
            if (backlog != null)
            {
                backlog.stoptime.value = DateTime.Now;
                backlog.Nowtableid.value = 4;
                backlog.Nowcolumid.value = ofid;
                backlog.Update();
            }
            log.Insert();
            //从职务中删除去
            WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(usermodel.DutyId.ToInt32());
            dutydetail.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
            dutydetail.Update();
            //foreach (DutyDetail.MODEL du in DutyDetail.Caches)
            //{
            //    if (!du.UsersName.isEmpty)
            //    {
            //        string users = du.UsersName.f(",{0},");
            //        string userThis = usermodel.RealName.f(",{0},");
            //        if (users.Contains(userThis))
            //        {
            //            users = users.Replace(String.Format(",{0}", usermodel.RealName), "");
            //            string[] arrUsers = users.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //            users = String.Join(",", arrUsers);
            //            users = users + ",";
            //            du.UsersName.set(users);
            //            du.Update();
            //        }
            //    }
            //}
            Response.Redirect("HR_Intojobs.aspx?state=40");
        }
    }
}