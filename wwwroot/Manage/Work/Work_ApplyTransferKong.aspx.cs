using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WX.Model;
namespace wwwroot.Manage.Work
{
    public partial class Work_ApplyTransferKong : System.Web.UI.Page
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
            WX.HR.TransferKong.MODEL tfk = null;
            li_center.Text = Request["type"] == "1" ? "调动" : "提升";
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp2, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList1, null, null, null);
            if (Request["TFID"] != null && Request["TFID"] != "")
            {

                tfk = WX.HR.TransferKong.NewDataModel(Request["TFID"]);
                if (tfk != null)
                {
                    ui_demp.SelectedValue = tfk.BackDempID.ToString();
                    ui_demp2.SelectedValue = tfk.NowDempID.ToString();
                    bindjob1();
                    bindjob();
                    ui_duty.SelectedValue = tfk.BackDutyID.ToString();
                    ui_duty2.SelectedValue = tfk.NowDutyID.ToString();
                    DropDownList1.SelectedValue = tfk.BackGrade.ToString();
                    li_addtime.Text = ((DateTime)tfk.Addtime.value).ToString("yyyy-MM-dd");
                  //  Button1.Enabled = false;
                    li_center.Text = tfk.type.ToString() == "1" ? "调动" : "提升";
                    li_dept.Text = tfk.dempOpinion.ToString();
                    Text_dept.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.dempManager.ToString());
                    li_hr.Text = tfk.hrOpinion.ToString();
                    Text_hr.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.hrManager.ToString());
                    li_boss.Text = tfk.bossOpinion.ToString();
                    Text_boss.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.bossManager.ToString());
                    tr_dept.Visible = tr_HR.Visible = tr_boss.Visible =true;
                    tr_sub.Visible = false;
                }
            }
            else
                li_addtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            WX.Main.CurUser.LoadEmployeeUser();
            WX.Main.CurUser.LoadUserModel(true);
            Employee.MODEL employee =WX.Main.CurUser.EmployeeUser;
            WX.Model.User.MODEL usermodel = WX.Main.CurUser.UserModel;
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
                if (tfk == null)
                {
                    ui_demp.SelectedValue = ui_demp2.SelectedValue = usermodel.DepartmentID.ToString();
                    bindjob1();
                    bindjob();
                    ui_duty.SelectedValue = ui_duty2.SelectedValue = usermodel.DutyId.ToString();
                    DropDownList1.SelectedValue = usermodel.Grade.ToString();
                }
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + WX.Main.CurUser.UserID + "'");
                if (intojob == null)
                    ULCode.Debug.Alert("您还没有入职，请选办理入职!", "Work_Apply.aspx");
                li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_Prof.Text = employee.Prof.ToString();
                li_fl.Text = employee.ForeignL.ToString();
            }
            if (Request["mes"] != null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Work_ApplyTransferKong.aspx?TFID={1}%'", WX.Main.CurUser.UserID, Request["TFID"]));
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
            this.ui_duty2.DataSource = WX.Model.DutyDetail.GetTableDepartent(ui_demp2.SelectedValue);
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
            WX.HR.TransferKong.MODEL tfk = WX.HR.TransferKong.NewDataModel();

            tfk.Addtime.value = DateTime.Now;
            tfk.UserID.value = WX.Main.CurUser.UserID;
            tfk.BackDutyID.value = ui_duty.SelectedValue;
            tfk.BackDempID.value = ui_demp.SelectedValue;
            tfk.NowDutyID.value = ui_duty2.SelectedValue;
            tfk.NowDempID.value = ui_demp2.SelectedValue;
            tfk.BackGrade.value = tfk.NowGrade.value = DropDownList1.SelectedValue;
            tfk.type.value = Request["type"] == "1" ? 1 : 2;
            int tfkid= tfk.Insert(true);
            //1、向部门发送通知
            WX.Main.MessageSend("<a href=/Manage/HR/HR_SignTransferKong.aspx?TFID=" + tfkid+ "&mes=1>" + WX.Main.CurUser.UserModel.RealName.ToString() + "提交" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "申请——" + (tfk.type.ToString() == "1" ? "调岗" : "升职") + "通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetDeptUserID(1, "[Host]", WX.Main.CurUser.UserModel.DepartmentID.ToInt32()), WX.Main.CurUser.UserID, 10, 0);

            ULCode.Debug.Alert("成功提交申请，请耐心等待结果!", "Work_Apply.aspx");
        }
    }
}