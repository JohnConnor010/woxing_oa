using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;
using System.IO;

namespace wwwroot.Manage.HR
{
    public partial class HR_Official : System.Web.UI.Page
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
                if (Request["mes"] != null)
                    WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%HR_Official.aspx?UserID={1}%'", WX.Main.CurUser.UserID, WX.Request.rUserId));

            }
        }
        private void pageinit()
        {
            string userId = WX.Request.rUserId;

            bool flag = false;
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            Employee.MODEL employee = WX.Request.rEmpolyee;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            MenuBar1.Param2 = employee.LoadSucceed || true ? usermodel.State.ToString() : "0";
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
                ui_demp.SelectedValue = usermodel.DepartmentID.ToString();
                this.bindjob();
                ui_duty.SelectedValue = usermodel.DutyId.ToString();
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + userId + "'");
                if (intojob == null)
                    Response.Redirect("HR_AddIntojobs.aspx?UserID=" + userId);
                WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList1, null, null, intojob.PSalary.ToString());
                li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_sqrname.Text = usermodel.RealName.ToString();
                CheckBox1.Checked = employee.IsInsurance.ToInt32() == 1 ? true : false;
            }
            else { this.bindjob(); }

            WX.HR.Official.MODEL Official = WX.HR.Official.GetModel("select * from HR_Official where UserID='" + userId + "'");
            if (Official != null)
            {
                ui_imagine.Text = Official.imagine.ToString();
                try
                {
                    li_time.Text = ((DateTime)Official.Addtime.value).ToString("yyyy-MM-dd");
                }
                catch { }
                if (Official.EndTime.value == null && Convert.ToInt32(usermodel.State.value) < 20)
                {
                    if (Official.demptype.ToInt32() == 0)
                    {
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()));
                        if (WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID) //部门主管
                            flag = true;
                        Literal1.Text = "部门";
                    }
                    else if (Official.HRtype.ToInt32() == 0 && Official.demptype.ToInt32() > 0)//人力资源部
                    {
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetHRUserID);
                        if (WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
                            flag = true;
                        Literal1.Text = "人资";
                    }
                    else if (Official.admintype.ToInt32() == 0 && Official.HRtype.ToInt32() > 0)//综管
                    {
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetCAUserID);
                        if (WX.CommonUtils.GetCAUserID == WX.Main.CurUser.UserID)
                            flag = true;
                        Literal1.Text = "综管";
                    }
                    else if (Official.bosstype.ToInt32() == 0 && Official.admintype.ToInt32() > 0)//中心领导
                    {
                        li_sqname.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.CommonUtils.GetBossUserID);
                        if (WX.CommonUtils.GetBossUserID == WX.Main.CurUser.UserID)
                            flag = true;
                        Literal1.Text = "中心";
                    }
                }
                //ui_demptype.SelectedValue = Official.demptype.ToString();
                // ui_dempuser.Value = Official.dempUserID.ToString();
                DropDownList1.SelectedValue = Official.salary.ToString();

                li_dept.Text = Official.dempOpinion.ToString();
                Check_dept.SelectedValue = Official.demptype.ToString();
                Text_dept.Text = WX.CommonUtils.GetRealNameListByUserIdList(Official.dempUserID.ToString());
                li_hr.Text = Official.HROpinion.ToString();
                Check_hr.SelectedValue = Official.HRtype.ToString();
                Text_hr.Text = WX.CommonUtils.GetRealNameListByUserIdList(Official.HRUserID.ToString());
                li_ca.Text = Official.adminOpinion.ToString();
                Check_ca.SelectedValue = Official.admintype.ToString();
                Text_ca.Text = WX.CommonUtils.GetRealNameListByUserIdList(Official.adminUserID.ToString());
                li_boss.Text = Official.bossOpinion.ToString();
                Check_boss.SelectedValue = Official.bosstype.ToString();
                Text_boss.Text = WX.CommonUtils.GetRealNameListByUserIdList(Official.bossUserID.ToString());
                tr_dept.Visible = Official.dempUserID.value != null;
                tr_HR.Visible = Official.HRUserID.value != null;
                tr_CA.Visible = Official.adminUserID.value != null;
                tr_boss.Visible = Official.bossUserID.value != null;
                tr_sub.Visible = tr_sub2.Visible = Official.EndTime.value == null;
                ContractBind();

            } if (usermodel.State.ToInt32() == 20)
            {
                tr_ht.Visible = true;
                tr_sub.Visible = tr_sub2.Visible = flag;
                if (employee.IsInsurance.ToInt32() == 0 && WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
                    div_ht.Visible = GridView1.Columns[1].Visible = Button3.Visible = true;
            }
            Button1.Enabled = Button2.Enabled = flag;


        }
        public void bindjob()
        {
            DataTable dt;

            WX.HR.Official.MODEL Official = WX.HR.Official.GetModel("select * from HR_Official where UserID='" + WX.Request.rUserId + "'");
            if (Official != null)
            {
                dt = WX.Model.DutyDetail.GetTableDepartent(ui_demp.SelectedValue);
            }
            else
            {
                User.MODEL user = WX.Request.rUser;
                dt = WX.Model.DutyDetail.GetTablenullDepartent(ui_demp.SelectedValue, user.RealName.ToString());
            }
            this.ui_duty.DataSource = dt;
            this.ui_duty.DataTextField = "Name";
            this.ui_duty.DataValueField = "ID";
            this.ui_duty.DataBind();
            if (this.ui_duty.Items.Count == 0)
                Button1.Enabled = false;
            else
                Button1.Enabled = true;
        }
        protected void ui_demp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }
        private void UpdateUser(int ofid)
        {


            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            WX.Model.DutyDetail.MODEL dutydetailback = WX.Model.DutyDetail.GetModel(usermodel.DutyId.ToInt32());
            usermodel.State.value = 20;
            usermodel.DepartmentID.value = ui_demp.SelectedValue;
            usermodel.DutyId.value = ui_duty.SelectedValue;
            usermodel.Grade.value = DropDownList1.SelectedValue;
            usermodel.EditTime.value = DateTime.Now;
            usermodel.Update();
            dutydetailback.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetailback.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
            dutydetailback.Update();
            if (dutydetailback.ID.ToString() != ui_duty.SelectedValue)
            {
                WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_duty.SelectedValue));
                dutydetail.UsersName.value = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString() + " and State>6 and State<40").ToColValueList(",", 0);
                dutydetail.Update();
            }
            WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyOfficial.aspx?mes=1>您的转正申请通过审核，当前为正式员工——审批通知</a>", "/Manage/Main/messagelist.aspx", usermodel.UserID.ToString(), WX.Main.CurUser.UserID, 9, 0);
            WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请通过审核，当前为正式员工——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 9, 0);
            WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请通过审核，当前为正式员工——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 9, 0);
            WX.Main.MessageSend("<a href=/Manage/Finance/FD_NewUserList.aspx?mes=1>" + usermodel.RealName.ToString() + "的转正申请通过审核，当前为正式员工——转正通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetFDUserID, WX.Main.CurUser.UserID, 9, 0);

            if (usermodel.Grade.ToInt32() > Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Grade_Employee"]))
                WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请通过审核，当前为正式员工——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 9, 0);

            //日志
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            log.BackDutyID.value = usermodel.DutyId.value;
            log.BackDempID.value = usermodel.DepartmentID.value;
            log.UserID.value = WX.Request.rUserId;
            log.NowDutyID.value = ui_duty.SelectedValue;
            log.NowDempID.value = ui_demp.SelectedValue;
            log.Backtableid.value = 1;
            log.Backcolumid.value = ofid;
            log.Starttime.value = DateTime.Now;
            log.GradeID.value = usermodel.Grade.value;
            log.Content.value = "员工转正" + log.Content.ToString();
            WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + log.UserID.ToString() + "' order by Starttime desc");
            if (backlog != null)
            {
                backlog.stoptime.value = DateTime.Now;
                backlog.Nowtableid.value = 1;
                backlog.Nowcolumid.value = ofid;
                backlog.Update();
            }
            log.Insert();
        }
        private void SaveOfficial(int type)
        {
            WX.HR.Official.MODEL Official = WX.HR.Official.GetModel("select top 1 * from HR_Official where UserID='" + WX.Request.rUserId + "' order by Addtime desc");

            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            ui_dempop.Text = ui_dempop.Text + "(" + DateTime.Now + ")";
            if (WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()) == WX.Main.CurUser.UserID) //部门主管
            {
                Official.dempOpinion.value = ui_dempop.Text;
                Official.demptype.value = type;
                Official.dempUserID.value = WX.Main.CurUser.UserID;
                if (type == -1)
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyOfficial.aspx?mes=1>对不起，您的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", Official.UserID.ToString(), WX.Main.CurUser.UserID, 9, 0);
                else
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "申请转正，请人力资源部审核——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 9, 0);

            }
            else if (WX.CommonUtils.GetHRUserID == WX.Main.CurUser.UserID)
            {
                Official.DutyID.value = ui_duty.SelectedValue;
                Official.salary.value = DropDownList1.SelectedValue;
                Official.HROpinion.value = ui_dempop.Text;
                Official.HRtype.value = type;
                Official.HRUserID.value = WX.Main.CurUser.UserID;
                if (Official.salary.ToInt32() <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Grade_Employee"]))
                {
                    if (type > 0)
                    {
                        Official.EndTime.value = DateTime.Now;
                        UpdateUser(Official.ID.ToInt32());
                    }
                    else
                    {
                        WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyOfficial.aspx?mes=1>对不起，您的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", Official.UserID.ToString(), WX.Main.CurUser.UserID, 9, 0);
                        WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 9, 0);
                    }
                }
                else
                {
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "申请转正，请综合管理中心审核——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 9, 0);
                }
            }
            else if (Official.admintype.ToInt32() == 0 && Official.HRtype.ToInt32() > 0)//综管
            {
                Official.adminOpinion.value = ui_dempop.Text;
                Official.admintype.value = type;
                Official.adminUserID.value = WX.Main.CurUser.UserID;
                if (type == -1)
                {
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyOfficial.aspx?mes=1>对不起，您的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", Official.UserID.ToString(), WX.Main.CurUser.UserID, 9, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 9, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 9, 0);
                }
                else
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "申请转正，请中心中心领导审核——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetBossUserID, WX.Main.CurUser.UserID, 9, 0);
            }
            else if (Official.bosstype.ToInt32() == 0 && Official.admintype.ToInt32() > 0)//中心领导
            {
                Official.bossOpinion.value = ui_dempop.Text;
                Official.bosstype.value = type;
                Official.bossUserID.value = WX.Main.CurUser.UserID;
                if (type > 0)
                {
                    Official.EndTime.value = DateTime.Now;
                    UpdateUser(Official.ID.ToInt32());
                }
                else
                {
                    WX.Main.MessageSend("<a href=/Manage/Work/Work_ApplyOfficial.aspx?mes=1>对不起，您的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", Official.UserID.ToString(), WX.Main.CurUser.UserID, 9, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", usermodel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 9, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, WX.Main.CurUser.UserID, 9, 0);
                    WX.Main.MessageSend("<a href=/Manage/HR/HR_Official.aspx?UserID=" + usermodel.UserID.ToString() + "&mes=1>" + usermodel.RealName.ToString() + "的转正申请没有通过——审批通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetCAUserID, WX.Main.CurUser.UserID, 9, 0);
                }
            }
            if (type == -1)
                Official.EndTime.value = DateTime.Now;
            Official.Update();
        }
        private void ContractBind()
        {
            GridView1.DataSource = WX.Model.EmployeeContract.GetList(WX.Request.rUserId, 1);
            GridView1.DataBind();
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            WX.Model.EmployeeContract.MODEL model = WX.Model.EmployeeContract.NewDataModel();
            model.Name.value = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            model.UserId.value = WX.Request.rUserId;
            model.ManageUserID.value = WX.Main.CurUser.UserID;
            model.Type.value = 1;
            if (FileUpload1.HasFile)
            {
                string filepath = "/UploadFiles/Contract/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
            }
            model.Insert();
            ContractBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WX.Model.EmployeeContract.MODEL model = WX.Model.EmployeeContract.NewDataModel(e.CommandArgument);
            model.Delete();
            ContractBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveOfficial(Convert.ToInt32(ui_demptype.SelectedValue));
            Response.Redirect("HR_Intojobs.aspx?state=20");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SaveOfficial(-1);

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Employee.MODEL employee = WX.Request.rEmpolyee;
            employee.IsInsurance.value = CheckBox1.Checked ? 1 : 0;
            employee.Update();
            div_ht.Visible = GridView1.Columns[1].Visible = Button3.Visible = false;
        }
    }
}