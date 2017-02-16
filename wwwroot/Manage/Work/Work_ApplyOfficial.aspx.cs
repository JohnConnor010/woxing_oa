using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;
namespace wwwroot.Manage.Work
{
    public partial class Work_ApplyOfficial : System.Web.UI.Page
    {
        WX.HR.Official.MODEL Official = null;
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
            WX.Main.CurUser.LoadEmployeeUser(false);
            WX.Main.CurUser.LoadUserModel(false);
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser;
            WX.Model.User.MODEL usermodel = WX.Main.CurUser.UserModel;
            if (employee.LoadSucceed || true)
            {
                li_name.Text = usermodel.RealName.ToString();
                li_sex.Text = ((bool)employee.Sex.value ? "男" : "女");
                try
                {
                    li_age.Text = ((DateTime)employee.Birthday.value).ToString("yyyy-MM-dd");
                }
                catch { }
                li_edu.Text = employee.Edu.ToString();
                li_Mobile.Text = employee.Mobile.ToString();
                ui_demp.Text = WX.CommonUtils.GetDeptNameListByDeptIdList(usermodel.DepartmentID.ToString());
                ui_duty.Text = WX.CommonUtils.GetDutyNameListByDutyIdList(usermodel.DutyId.ToString());
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + WX.Main.CurUser.UserID + "'");
                if (intojob == null && usermodel.State.ToInt32() < 10)
                    Response.Redirect("Work_Apply.aspx");
                li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_sqrname.Text = usermodel.RealName.ToString();
            }

            li_endtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            WX.HR.Official.MODEL Official = WX.HR.Official.GetModel("select top 1 * from HR_Official where UserID='" + WX.Main.CurUser.UserID + "' order by Addtime desc");
            if (Official != null)
            {
                ui_imagine.Text = Official.imagine.ToString();
                li_dept.Text = Official.dempOpinion.ToString();
                li_HR.Text = Official.HROpinion.ToString();
                li_CA.Text = Official.adminOpinion.ToString();
                li_boss.Text = Official.bossOpinion.ToString();
                tr_dept.Visible = Official.dempUserID.value != null;
                tr_hr.Visible = Official.HRUserID.value != null;
                tr_ca.Visible = Official.adminUserID.value != null;
                tr_boss.Visible = Official.bossUserID.value != null;
            }
            divstr.InnerHtml = "";
            if (usermodel.State.ToInt32() < 10)
            {
                divstr.InnerHtml = "请先办理入职然后再申请转正！";
            }
            else if (usermodel.State.ToInt32() == 20)
            {
                divstr.InnerHtml = "您已转正请不要重复申请！";
            }
            else if (Official != null && DateTime.Now < Official.Addtime.ToDateTime().AddMonths(1))
            {
                divstr.InnerHtml = "请于" + Official.Addtime.ToDateTime().AddMonths(1).ToString("yyyy-MM-dd") + "后申请，1个月内请不要重复申请！";
            }
            if (Request["mes"] != null)
            {
                divstr.InnerHtml = "&nbsp;"; WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Work_ApplyOfficial.aspx%'", WX.Main.CurUser.UserID));
            }
            Button1.Visible = divstr.InnerHtml == "";

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            WX.Model.User.MODEL usermodel = WX.Main.CurUser.UserModel;
            WX.HR.Official.MODEL Official = WX.HR.Official.NewDataModel();
            Official = WX.HR.Official.GetModel("select top 1 * from HR_Official where UserID='" + usermodel.UserID.ToString() + "' order by Addtime desc");
            bool flag = true;
            if (Official == null)
            {
                Official = WX.HR.Official.NewDataModel();
                flag = false;
            }
            Official.UserID.value = usermodel.UserID.ToString();
            Official.DutyID.value = usermodel.DutyId.value;
            Official.salary.value = usermodel.Grade.value;
            Official.imagine.value = ui_imagine.Text;
            int ofid;
            if (flag)
            {
                ofid = Convert.ToInt32(Official.ID.ToString());
                Official.Update();
            }
            else
            {
                Official.Addtime.value = DateTime.Now;
                ofid = Official.Insert(true);
            }
            log.BackDutyID.value = usermodel.DutyId.value;
            log.BackDempID.value = usermodel.DepartmentID.value;
            //日志
            log.UserID.value = usermodel.UserID.ToString();
            log.Backtableid.value = 1;
            log.Backcolumid.value = ofid;
            log.Starttime.value = DateTime.Now;
            log.GradeID.value = usermodel.Grade.value;
            log.Content.value = "转正申请" + log.Content.ToString();
            log.Insert();

            //1、向部门发送通知
            WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "申请转正——转正通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 9, 0);
            //2、向人资发送通知
            WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "申请转正——转正通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 9, 0);

            Response.Redirect("Work_Apply.aspx");
        }
    }
}