using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class HR_SignTransferKong : System.Web.UI.Page
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
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp2, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList1, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList2, null, null, null);
            WX.HR.TransferKong.MODEL tfk = WX.HR.TransferKong.NewDataModel(Request["TFID"]);
            if (tfk != null)
            {
                ui_dempop.Text = "";
                ui_demp.SelectedValue = tfk.BackDempID.ToString();
                ui_demp2.SelectedValue = tfk.NowDempID.ToString();
                bindjob1();
                bindjob();
                ui_duty.SelectedValue = tfk.BackDutyID.ToString();
                ui_duty2.SelectedValue = tfk.NowDutyID.ToString();
                DropDownList1.SelectedValue = tfk.BackGrade.ToString();
                DropDownList2.SelectedValue = tfk.NowGrade.ToString();
                li_addtime.Text = ((DateTime)tfk.Addtime.value).ToString("yyyy-MM-dd");
                li_center.Text = tfk.type.ToString() == "1" ? "调动" : "提升";
                li_dept.Text = tfk.dempOpinion.ToString();
                Text_dept.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.dempManager.ToString());
                li_hr.Text = tfk.hrOpinion.ToString();
                Text_hr.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.hrManager.ToString());
                li_boss.Text = tfk.bossOpinion.ToString();
                Text_boss.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.bossManager.ToString());
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + tfk.UserID.ToString() + "'");
                try
                {
                    li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                }
                catch
                {
                }
                Employee.MODEL employee = WX.Model.Employee.GetModelToID(tfk.UserID.ToString());
                WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(tfk.UserID.ToString());
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
                    if (tfk == null)
                    {
                        ui_demp.SelectedValue = ui_demp2.SelectedValue = usermodel.DepartmentID.ToString();
                        bindjob1();
                        bindjob();
                        ui_duty.SelectedValue = ui_duty2.SelectedValue = usermodel.DutyId.ToString();
                        DropDownList1.SelectedValue = DropDownList2.SelectedValue = usermodel.Grade.ToString();
                    }
                    li_Prof.Text = employee.Prof.ToString();
                    li_fl.Text = employee.ForeignL.ToString();
                }
                if (tfk.bossManager.value == null)
                {
                    bool flag = false;
                    if (tfk.dempManager.ToString() == "")
                    {
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()));
                        if (WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID) //部门主管
                            flag = true;
                        Literal1.Text = "部门";
                    }
                    else if (tfk.hrManager.ToString() == "")//人力资源部
                    {
                        tr_dept.Visible = true;
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetHRUserID);
                        if (WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
                            ui_demp2.Enabled = ui_duty2.Enabled = DropDownList2.Enabled = flag = true;
                        Literal1.Text = "人资（行政）部";
                    }
                    else if (tfk.bossManager.ToString() == "")//中心领导
                    {
                        tr_dept.Visible = tr_HR.Visible = true;
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetBossUserID);
                        if (WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID)
                            flag = true;
                        Literal1.Text = "中心领导";
                    }
                    Button1.Enabled = flag;
                }
                else
                {
                    tr_dept.Visible = tr_HR.Visible =tr_boss.Visible = true;
                    tr_sub.Visible = tr_sub2.Visible = false;
                }
                if (Request["mes"] != null)
                    WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%HR_SignTransferKong.aspx?TFID={1}%'", WX.Main.CurUser.UserID, Request["TFID"]));

            }

        }
        public void bindjob1()
        {
            DataTable dt = WX.Model.DutyDetail.GetTableDepartent(ui_demp.SelectedValue);
            this.ui_duty.DataSource = dt;
            this.ui_duty.DataTextField = "Name";
            this.ui_duty.DataValueField = "ID";
            this.ui_duty.DataBind();
        }
        public void bindjob()
        {
            DataTable dt2 = WX.Model.DutyDetail.GetTableDepartent(ui_demp2.SelectedValue);
            this.ui_duty2.DataSource = dt2;
            this.ui_duty2.DataTextField = "Name";
            this.ui_duty2.DataValueField = "ID";
            this.ui_duty2.DataBind();
            if (this.ui_duty2.Items.Count == 0)
                Button1.Enabled = false;
            else
                Button1.Enabled = true;
        }
        protected void ui_demp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            WX.HR.TransferKong.MODEL tfk = WX.HR.TransferKong.NewDataModel(Request["TFID"]);
            WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(tfk.UserID.ToString());
            ui_dempop.Text = ui_dempop.Text + "(" + DateTime.Now + ")";
            if (WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID) //部门主管
            {
                tfk.dempOpinion.value = ui_dempop.Text;
                tfk.dempManager.value = WX.Main.CurUser.UserID;
                WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请，请人力资源部审批——" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 10, 0);

            }
            else if (WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
            {
                tfk.hrOpinion.value = ui_dempop.Text;
                tfk.hrManager.value = WX.Main.CurUser.UserID;
                tfk.NowGrade.value = DropDownList2.SelectedValue;
                WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请，请中心领导审批——" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetBossUserID, WX.Main.CurUser.UserID, 10, 0);

            }
            else if (WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID)//中心领导
            {
                tfk.bossOpinion.value = ui_dempop.Text;
                tfk.bossManager.value = WX.Main.CurUser.UserID;
                usermodel.DepartmentID.value = ui_demp2.SelectedValue;
                usermodel.DutyId.value = ui_duty2.SelectedValue;
                usermodel.Grade.value = DropDownList2.SelectedValue;
                usermodel.Update();
                WX.Model.DutyDetail.MODEL dutydetailback = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_duty.SelectedValue));
                dutydetailback.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetailback.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
                dutydetailback.Update();
                WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_duty2.SelectedValue));
                dutydetail.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
                dutydetail.Update();
                //日志
                log.Content.value = "员工" + (tfk.type.ToString() == "1" ? "调岗" : "升职");
                log.UserID.value = usermodel.UserID.ToString();
                log.BackDutyID.value = ui_duty.SelectedValue;
                log.BackDempID.value = ui_demp.SelectedValue;
                log.NowDutyID.value = ui_duty2.SelectedValue;
                log.NowDempID.value = ui_demp2.SelectedValue;
                log.Backtableid.value = tfk.type.ToString() == "1" ? 2 : 3;
                log.Backcolumid.value = tfk.ID.ToString();
                log.Starttime.value = DateTime.Now;
                log.GradeID.value = usermodel.Grade.value;
                WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + usermodel.UserID.ToString() + "' order by Starttime desc");
                if (backlog != null)
                {
                    backlog.stoptime.value = DateTime.Now;
                    backlog.Nowtableid.value = tfk.type.ToString() == "1" ? 2 : 3;
                    backlog.Nowcolumid.value = tfk.ID.ToString();
                    backlog.Update();
                }
                log.Insert();
                WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>您提交的" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请已审批完成——系统已自动" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 10, 0);
                WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请已审批完成——系统已自动" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", WX.Main.CurUser.UserModel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 10, 0);
                WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请已审批完成——系统已自动" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 10, 0);
                WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfk.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请已审批完成——系统已自动" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetBossUserID, WX.Main.CurUser.UserID, 10, 0);
            }
            tfk.Update();
            pageinit();
        }
    }
}