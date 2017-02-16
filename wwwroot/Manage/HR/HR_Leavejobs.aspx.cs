using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.HR
{
    public partial class HR_Leavejobs : System.Web.UI.Page
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
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            LeaveJob = WX.HR.LeaveJob.NewDataModel(Request["LJID"]);
            if (LeaveJob != null)
            {
                li_reason.Text = LeaveJob.reason.ToString();
                ui_days.Text = LeaveJob.days.ToString();
                    ui_lasttime.Text =LeaveJob.lasttime.ToDateTime().ToString("yyyy-MM-dd");
                li_addtime.Text = ((DateTime)LeaveJob.Addtime.value).ToString("yyyy-MM-dd");
                li_dept.Text = LeaveJob.dempOpinion.ToString();
                Text_dept.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.dempManager.ToString());

                li_hr.Text = LeaveJob.hrOpinion.ToString();
                Text_hr.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.hrManager.ToString());

                li_boss.Text = LeaveJob.bossOpinion.ToString();
                Text_boss.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.bossManager.ToString());

                Employee.MODEL employee = WX.Model.Employee.NewDataModel(LeaveJob.UserID.ToString());
                WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel(LeaveJob.UserID.ToString());
                li_name.Text = usermodel.RealName.ToString();
                li_sex.Text = ((bool)employee.Sex.value ? "男" : "女");
                li_age.Text = ((DateTime)employee.Birthday.value).ToString("yyyy-MM-dd");
                li_edu.Text = employee.Edu.ToString();
                li_Mobile.Text = employee.Mobile.ToString();
                ui_demp.SelectedValue = usermodel.DepartmentID.ToString();
                bindjob();
                ui_duty.SelectedValue = usermodel.DutyId.ToString();
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + usermodel.UserID.ToString() + "'");
                if (intojob != null)
                    li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_sqrname.Text = usermodel.RealName.ToString();
                if (LeaveJob.dempManager.value == null)
                {
                    tr_sub.Visible = tr_sub2.Visible = WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID;
                    Literal1.Text = "部门";
                    li_dempname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()));
                }
                else if (LeaveJob.hrManager.value == null)
                {
                    tr_sub.Visible = tr_sub2.Visible = WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID;
                    Literal1.Text = "人资";
                    li_dempname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetHRUserID);
                    tr_dept.Visible = true;
                }
                else if (LeaveJob.bossManager.value == null)
                {
                    tr_sub.Visible = tr_sub2.Visible = WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID;
                    Literal1.Text = "中心";
                    li_dempname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetBossUserID);
                    tr_dept.Visible = tr_HR.Visible = true;
                }
                else
                {
                    tr_dept.Visible = tr_HR.Visible = tr_boss.Visible = div_rece.Visible = true;
                    tr_sub.Visible = tr_sub2.Visible = false;
                    li_rece.Text = LeaveJob.ReceiveContent.ToString().Replace("\r\n", "<br/>");
                    try
                    {
                        li_annex.Text = "<a style='color:#666;' href='" + LeaveJob.ReceiveAnnex.ToString().Split('|')[1] + "' target='_blank'>点击下载：" + LeaveJob.ReceiveAnnex.ToString().Split('|')[0] + "</a>";
                    }
                    catch { }
                    tr_receshow.Visible = LeaveJob.ReceiveContent.value != null;
                    BindRECE(LeaveJob.UserID.ToString());
                    div_rece.Visible = usermodel.State.ToInt32() != 40;
                }

            }
            if (Request["mes"] != null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%HR_Leavejobs.aspx?LJID={1}%'", WX.Main.CurUser.UserID, Request["LJID"]));

        }
        private void BindRECE(string userid)
        {
            WX.Main.CurUser.LoadUserModel(true);
            System.Data.DataTable dt = WX.HR.Receive.GetList("UserID='" + userid + "' and DeptID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            this.Gv_Receive.DataSource = dt;
            this.Gv_Receive.DataBind();
        }
        protected void Gv_Receive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l1 = (LinkButton)e.Row.Cells[3].FindControl("LinkButton1");
                LinkButton l2 = (LinkButton)e.Row.Cells[3].FindControl("LinkButton2");
                string[] arr = l1.CommandArgument.Split('|');
                switch (arr[2])
                {
                    case "0": l1.Visible = l2.Visible = true; break;
                    case "1": l1.Visible = l2.Visible = true; l1.Text = "交接"; l1.CommandName = "State3"; l2.Text = "不合格";l2.CommandName="State2" ; break;
                    case "2": l1.Visible = l2.Visible = true; l1.Text = "交接"; l1.CommandName = "State3"; l2.Visible=false; break;
                    default: l1.Visible = l2.Visible = false; break;
                }
            }
        }
        protected void Gv_Receive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arr = e.CommandArgument.ToString().Split('|');
            WX.HR.Receive.MODEL receive = WX.HR.Receive.NewDataModel(arr[0]);
            if (e.CommandName == "linkup")
            {
                TextBox1.Text = receive.Question.ToString();
                hidden_receid.Value = receive.ID.ToString();
            }
            else if (e.CommandName == "linkdel")
                receive.Delete();
            else
            { 
                receive.State.value =e.CommandName == "State3"? 3:2;
                receive.ConfirmUserID.value = WX.Main.CurUser.UserID;
                receive.Update();
                if (receive.State.ToInt32() == 3)
                {
                   System.Data.DataTable dt = receive.GetDeptCount();
                   if (Convert.ToInt32(dt.Rows[0][0]) >= 4 && Convert.ToInt32(dt.Rows[0][1]) > Convert.ToInt32(dt.Rows[0][2]) && Convert.ToInt32(dt.Rows[0][2]) == 0)
                   {
                       WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel(receive.UserID.ToString());
                       usermodel.State.value = 40;
                       usermodel.Update();
                       WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=1", "UserID='" + usermodel.UserID.ToString() + "'");
                       WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(usermodel.DutyId.ToInt32());
                       dutydetail.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
                       dutydetail.Update();
                       //    //日志
                       WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
                       log.UserID.value = usermodel.UserID.ToString();
                       log.Backtableid.value = 4;
                       log.Backcolumid.value = Request["LJID"];
                       log.Starttime.value = DateTime.Now;
                       log.BackDutyID.value = usermodel.DutyId.value;
                       log.BackDempID.value = usermodel.DepartmentID.value;
                       log.Content.value = "员工离职";
                       WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + usermodel.UserID.ToString() + "' order by Starttime desc");
                       if (backlog != null)
                       {
                           backlog.stoptime.value = DateTime.Now;
                           backlog.Nowtableid.value = 4;
                           backlog.Nowcolumid.value = Request["LJID"];
                           backlog.Update();
                       }
                       log.Insert();
                       WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + Request["LJID"] + "&mes=1>" + usermodel.RealName.ToString() + "已离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 11, 0);
                       WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + Request["LJID"] + "&mes=1>" + usermodel.RealName.ToString() + "已离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 11, 0);
                       WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + Request["LJID"] + "&mes=1>" + usermodel.RealName.ToString() + "已离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetAdminUserID, WX.Main.CurUser.UserID, 11, 0);
                       WX.Main.MessageSend("<a href=/Manage/Finance/FD_NewUserList.aspx?mes=1>" + usermodel.RealName.ToString() + "已离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetFDUserID, WX.Main.CurUser.UserID, 11, 0);
                   }
                }
            }
            BindRECE(receive.UserID.ToString());
        }

        public void bindjob()
        {
            System.Data.DataTable dt = WX.Model.DutyDetail.GetTableDepartent(ui_demp.SelectedValue);
            this.ui_duty.DataSource = dt;
            this.ui_duty.DataTextField = "Name";
            this.ui_duty.DataValueField = "ID";
            this.ui_duty.DataBind();
        }
        private void SaveLeaveJob(int type)
        {

            WX.HR.LeaveJob.MODEL LeaveJob = WX.HR.LeaveJob.NewDataModel(Request["LJID"]);
            WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel(LeaveJob.UserID.ToString());
            ui_dempop.Text = ui_dempop.Text + "(" + DateTime.Now + ")";
            if (WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID)
            {
                LeaveJob.dempManager.value = WX.Main.CurUser.UserID;
                LeaveJob.dempOpinion.value = ui_dempop.Text;
                if (type == 1)
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交了离职申请，请人力资源部审批——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 11, 0);
            }
            else if (WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
            {
                LeaveJob.hrOpinion.value = ui_dempop.Text;
                LeaveJob.hrManager.value = WX.Main.CurUser.UserID;
                if (type == 1)
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交了离职申请，请中心领导审批——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetBossUserID, WX.Main.CurUser.UserID, 11, 0);
            }
            else if (WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID)
            {
                LeaveJob.bossManager.value = WX.Main.CurUser.UserID;
                LeaveJob.bossOpinion.value = ui_dempop.Text;
                if (type == 1)
                {
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyLeavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>您提交的离职申请已批准，请协助各部门办理交接——离职通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 11, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的离职申请已批准，请做好部门交接工作——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 11, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的离职申请已批准，请做好部门交接工作——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 11, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的离职申请已批准，请做好物品交接工作——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetAdminUserID, WX.Main.CurUser.UserID, 11, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的离职申请已批准，请做好部门交接工作——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetFDUserID, WX.Main.CurUser.UserID, 11, 0);
                    
                }
            }
            if (type == 2)
            {
                LeaveJob.EndTime.value = DateTime.Now;
                WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyLeavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>您提交的离职申请没通过，点击查看详情——离职通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 11, 0);
                WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交的离职申请被驳回，点击查看详情——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 11, 0);
                WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "提交了离职申请被驳回，点击查看详情——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 11, 0);
            }
            LeaveJob.Update();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveLeaveJob(1);
            Response.Redirect("HR_Leavejobs.aspx?LJID=" + Request["LJID"]);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SaveLeaveJob(2);
            Response.Redirect("HR_Leavejobs.aspx?LJID=" + Request["LJID"]);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() != "")
            {
                WX.HR.LeaveJob.MODEL LeaveJob = WX.HR.LeaveJob.NewDataModel(Request["LJID"]);
                WX.HR.Receive.MODEL receive;
                receive = hidden_receid.Value != "" ? WX.HR.Receive.NewDataModel(hidden_receid.Value) : WX.HR.Receive.NewDataModel();
                receive.Question.value = TextBox1.Text;
                receive.QuestionTime.value = DateTime.Now;
                if (hidden_receid.Value == "")
                {
                    receive.UserID.value = LeaveJob.UserID.value;
                    receive.DeptID.value = WX.Main.CurUser.UserModel.DepartmentID.value;
                    receive.NextUserID.value = WX.Main.CurUser.UserID;
                    receive.State.value = 0;
                    receive.AddUserID.value = WX.Main.CurUser.UserID;
                    receive.Addtime.value = DateTime.Now;
                    receive.Insert();
                    if(Gv_Receive.Rows.Count==0)
                        WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyLeavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>你收到"+WX.CommonUtils.GetDeptNameListByDeptIdList(WX.Main.CurUser.UserModel.DepartmentID.ToString())+"交接信息——离职通知</a>", "/Manage/Main/messagelist.aspx", LeaveJob.UserID.ToString(), WX.Main.CurUser.UserID, 11, 0);
                }
                else
                    receive.Update();
                TextBox1.Text = "";
                hidden_receid.Value = "";
                BindRECE(LeaveJob.UserID.ToString());
            }
        }
    }
}