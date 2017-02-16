using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.Manage.Work
{
    public partial class Work_ApplyLeavejobs : System.Web.UI.Page
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
            WX.Model.Employee.MODEL employee;
            WX.Model.User.MODEL usermodel;
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            bool flag = true;
            if (Request["LJID"] != null)
                LeaveJob = WX.HR.LeaveJob.NewDataModel(Request["LJID"]);
            else
            {
                flag = false;
                LeaveJob = WX.HR.LeaveJob.GetModel("select top 1 * from HR_LeaveJobs where UserID='" + WX.Main.CurUser.UserID + "' order by Addtime desc");
            }
            if ((flag && LeaveJob != null) || (LeaveJob != null && flag == false && LeaveJob.EndTime.value == null))
            {
                li_dept.Text = LeaveJob.dempOpinion.ToString();
                Text_dept.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.dempManager.ToString());
                tr_dept.Visible = LeaveJob.dempManager.value != null;
                li_hr.Text = LeaveJob.hrOpinion.ToString();
                Text_hr.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.hrManager.ToString());
                tr_HR.Visible = LeaveJob.hrManager.value != null;
                li_boss.Text = LeaveJob.bossOpinion.ToString();
                Text_boss.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.bossManager.ToString());
                tr_boss.Visible = LeaveJob.bossManager.value != null;
                li_sqrname.Text = WX.CommonUtils.GetRealNameListByUserIdList(LeaveJob.UserID.ToString());
                ui_reason.Text = LeaveJob.reason.ToString();
                ui_days.Text = LeaveJob.days.ToString();
                try
                {
                    ui_lasttime.Text = LeaveJob.lasttime.ToDateTime().ToString("yyyy-MM-dd");
                }
                catch { }
                if (LeaveJob.EndTime.value == null)
                {
                    ui_reason.Text = LeaveJob.reason.ToString();
                    ui_days.Text = LeaveJob.days.ToString();
                    li_addtime.Text = LeaveJob.Addtime.ToDateTime().ToString("yyyy-MM-dd");
                    try
                    {
                        ui_lasttime.Text = LeaveJob.lasttime.ToDateTime().ToString("yyyy-MM-dd");
                    }
                    catch { }
                    if (LeaveJob.bossManager.value == null)
                        divstr.InnerHtml = "您提交的申请正在审核中，请耐心等待结果！";
                    else
                    {
                        divstr.InnerHtml = "正在交接中，请协助各部门办理离职交接事宜！";
                        //tr_rece.Visible = true;
                        li_rece.Text = Text_RECE.Text = LeaveJob.ReceiveContent.ToString();
                        li_rece.Text = li_rece.Text.Replace("\r\n", "<br/>");
                        try
                        {
                            Literal1.Text = li_annex.Text = "<a style='color:#666;' href='" + LeaveJob.ReceiveAnnex.ToString().Split('|')[1] + "' target='_blank'>点击下载：" + LeaveJob.ReceiveAnnex.ToString().Split('|')[0] + "</a>";
                        }
                        catch { }
                        tr_receshow.Visible = LeaveJob.ReceiveContent.value != null;
                    }
                }
                else
                {
                    divstr.InnerHtml = "您提交的申请被驳回！";
                }
                tr_sub.Visible = radio_reason.Visible = false;
                BindRECE(LeaveJob.UserID.ToString());
                //div_rece.Visible = Gv_Receive.Rows.Count > 0;
                employee = WX.Model.Employee.NewDataModel(LeaveJob.UserID.ToString());
                usermodel = WX.Model.User.NewDataModel(LeaveJob.UserID.ToString());
                HiddenID.Value = LeaveJob.ID.ToString();
            }
            else
            {
                li_addtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                WX.Main.CurUser.LoadEmployeeUser();
                WX.Main.CurUser.LoadUserModel(true);
                employee = WX.Main.CurUser.EmployeeUser;
                usermodel = WX.Main.CurUser.UserModel;
            }
            li_name.Text = usermodel.RealName.ToString();
            li_sex.Text = ((bool)employee.Sex.value ? "男" : "女");
            li_age.Text = ((DateTime)employee.Birthday.value).ToString("yyyy-MM-dd");
            li_edu.Text = employee.Edu.ToString();
            li_Mobile.Text = employee.Mobile.ToString();
            ui_demp.SelectedValue = usermodel.DepartmentID.ToString();
            bindjob();
            ui_duty.SelectedValue = usermodel.DutyId.ToString();
            WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + WX.Main.CurUser.UserID + "'");
            if (intojob == null)
                Response.Redirect("HR_AddIntojobs.aspx?UserID=" + WX.Request.rUserId);
            li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
            li_sqrname.Text = usermodel.RealName.ToString();
            if (usermodel.State.ToString() == "40" && LeaveJob != null)
                Button1.Enabled = false;
            if (Request["mes"] != null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Work_ApplyLeavejobs.aspx%'", WX.Main.CurUser.UserID, Request["LJID"]));

        }
        private void BindRECE(string userid)
        {
            WX.Main.CurUser.LoadUserModel(true);
            System.Data.DataTable dt = WX.HR.Receive.GetList("UserID='" + userid + "'");
            this.Gv_Receive.DataSource = dt;
            this.Gv_Receive.DataBind();
        }
        protected void Gv_Receive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arr = e.CommandArgument.ToString().Split('|');
            WX.HR.Receive.MODEL receive = WX.HR.Receive.NewDataModel(arr[0]);
            if (e.CommandName == "linkup")
            {
                TextBox1.Text = receive.Answer.ToString();
                li_Question.Text = receive.Question.ToString();
                hidden_receid.Value = receive.ID.ToString();
                div_rece.Visible = true;
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.HR.LeaveJob.MODEL LeaveJob = WX.HR.LeaveJob.NewDataModel();
            LeaveJob = WX.HR.LeaveJob.NewDataModel();
            LeaveJob.Addtime.value = DateTime.Now;
            LeaveJob.UserID.value = WX.Main.CurUser.UserID;
            LeaveJob.reason.value = radio_reason.SelectedValue + "。" + ui_reason.Text;
            LeaveJob.days.value = ui_days.Text;
            LeaveJob.lasttime.value = ui_lasttime.Text;
            int ofid = LeaveJob.Insert(true);

            WX.Model.User.MODEL usermodel = WX.Main.CurUser.UserModel;
            //1、向部门发送通知
            WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + ofid + "&mes=1>" + usermodel.RealName.ToString() + "申请离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 11, 0);
            //2、向人资发送通知
            //WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + ofid + "&mes=1>" + usermodel.RealName.ToString() + "申请离职——离职通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 11, 0);

            Response.Redirect("Work_Apply.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            WX.HR.LeaveJob.MODEL LeaveJob = WX.HR.LeaveJob.NewDataModel(HiddenID.Value);
            LeaveJob.ReceiveContent.value = Text_RECE.Text;
            if (FileUpload1.HasFile)
            {
                string filepath = "/UploadFiles/Receive/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(filepath));
                LeaveJob.ReceiveAnnex.value = Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "|" + filepath;
                li_annex.Text = Literal1.Text = "<a href='" + filepath + "' target='_blank'>点击下载：" + Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "</a>";
               
            } 
            li_rece.Text = Text_RECE.Text;
                tr_receshow.Visible = true;
                tr_rece.Visible = false;
            LeaveJob.Update();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() != "")
            {
                WX.HR.LeaveJob.MODEL LeaveJob =Request["LJID"] != null?WX.HR.LeaveJob.NewDataModel(Request["LJID"]):WX.HR.LeaveJob.GetModel("select top 1 * from HR_LeaveJobs where UserID='" + WX.Main.CurUser.UserID + "' order by Addtime desc");
        
                WX.HR.Receive.MODEL receive = WX.HR.Receive.NewDataModel(hidden_receid.Value);
                receive.Answer.value = TextBox1.Text;
                receive.AnswerTime.value = DateTime.Now;
                receive.State.value = 1;
                receive.Update();
                li_Question.Text = "";
                TextBox1.Text = "";
                hidden_receid.Value = "";
                div_rece.Visible = false;
                WX.Main.MessageSend("<a href=/Manage/HR/HR_Leavejobs.aspx?LJID=" + LeaveJob.ID.ToString() + "&mes=1>" +WX.Main.CurUser.UserModel.RealName.ToString() + "已回复您的交接信息——离职通知</a>", "/Manage/Main/messagelist.aspx", receive.AddUserID.ToString(), WX.Main.CurUser.UserID, 11, 0);
                BindRECE(WX.Main.CurUser.UserID);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            tr_rece.Visible = true;
        }
    }
}