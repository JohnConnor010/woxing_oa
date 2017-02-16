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
    public partial class HR_AddTransferKong : System.Web.UI.Page
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
            string userId = WX.Request.rUserId;
            WX.HR.TransferKong.MODEL tfk = null;
            Literal1.Text = Request["type"] == "1" ? "调岗" : "升职";
            li_center.Text = Request["type"] == "1" ? "调动" : "提升";
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp, null, null, null);
            WX.Data.Dict.BindListCtrl_DeptList(this.ui_demp2, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList1, null, null, null);
            WX.Data.Dict.BindListCtrl_GradeList(this.DropDownList2, null, null, null);
            if (Request["id"] != null && Request["id"] != "")
            {
                tfk = WX.HR.TransferKong.GetModel("select * from HR_TransferKong where id='" + Request["id"] + "'");
                if (tfk != null)
                {
                    ui_demp.SelectedValue = tfk.BackDempID.ToString();
                    ui_demp2.SelectedValue = tfk.NowDempID.ToString();
                    bindjob1();
                    bindjob();
                    ui_duty.SelectedValue = tfk.BackDutyID.ToString();
                    ui_duty2.SelectedValue = tfk.NowDutyID.ToString();
                    DropDownList1.SelectedValue = tfk.BackGrade.ToString();
                    DropDownList2.SelectedValue = tfk.NowGrade.ToString();
                    ui_dempop.Text = tfk.dempOpinion.ToString();
                    ui_dempuser.Value = tfk.dempManager.ToString();
                    li_dempname.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.dempManager.ToString());
                    ui_hrop.Text = tfk.hrOpinion.ToString();
                    ui_hruser.Value = tfk.hrManager.ToString();
                    li_hrname.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.hrManager.ToString());
                    ui_bossop.Text = tfk.bossOpinion.ToString();
                    ui_bossuser.Value = tfk.bossManager.ToString();
                    li_bossname.Text = WX.CommonUtils.GetRealNameListByUserIdList(tfk.bossManager.ToString());
                    li_addtime.Text = ((DateTime)tfk.Addtime.value).ToString("yyyy-MM-dd");
                    Button1.Enabled = false;
                    Literal1.Text = tfk.type.ToString() == "1" ? "调岗" : "升职";
                    li_center.Text = tfk.type.ToString() == "1" ? "调动" : "提升";
                }
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
                    ui_duty.SelectedValue = ui_duty2.SelectedValue =usermodel.DutyId.ToString();
                    DropDownList1.SelectedValue =DropDownList2.SelectedValue = usermodel.Grade.ToString();
                }
                WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + userId + "'");
                if (intojob == null)
                    Response.Redirect("HR_AddIntojobs.aspx?uid=" + Request["uid"]);
                li_intotime.Text = ((DateTime)intojob.Addtime.value).ToString("yyyy-MM-dd");
                li_Prof.Text = employee.Prof.ToString();
                li_fl.Text = employee.ForeignL.ToString();
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
            DataTable dt2;
            //if (Request["id"] != null && Request["id"] != "")
            //{
                dt2 = WX.Model.DutyDetail.GetTableDepartent(ui_demp2.SelectedValue);
            //}
            //else
            //{
            //    User.MODEL user = WX.Request.rUser;
            //    dt2 = WX.Model.DutyDetail.GetTablenullDepartent(ui_demp.SelectedValue, user.RealName.ToString());
            //}
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
            string userId =WX.Request.rUserId;
            WX.HR.TransferKong.MODEL tfk = WX.HR.TransferKong.NewDataModel();
            WX.Model.User.MODEL usermodel =WX.Request.rUser;
            tfk.Addtime.value = DateTime.Now;
            tfk.UserID.value = userId;
            tfk.BackDutyID.value = ui_duty.SelectedValue;
            tfk.BackDempID.value = ui_demp.SelectedValue;
            tfk.NowDutyID.value = ui_duty2.SelectedValue;
            tfk.NowDempID.value = ui_demp2.SelectedValue;
            tfk.BackGrade.value = DropDownList1.SelectedValue;
            tfk.NowGrade.value = DropDownList2.SelectedValue;
            tfk.dempOpinion.value = ui_dempop.Text;
            tfk.dempManager.value = ui_dempuser.Value;
            tfk.hrOpinion.value = ui_hrop.Text;
            tfk.hrManager.value = ui_hruser.Value;
            tfk.bossOpinion.value = ui_bossop.Text;
            tfk.bossManager.value = ui_bossuser.Value;
            tfk.type.value = Request["type"] == "1" ? 1 : 2;
            int tfkid = tfk.Insert(true);
            log.Content.value = "员工" + (tfk.type.ToString() == "1" ? "调岗" : "升职");
            WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_duty2.SelectedValue));
            usermodel.Grade.value = dutydetail.GradeID.value;
            usermodel.DepartmentID.value = ui_demp2.SelectedValue;
            usermodel.DutyId.value = ui_duty2.SelectedValue;
            usermodel.Grade.value = DropDownList2.SelectedValue;
            usermodel.Update();
            WX.Model.DutyDetail.MODEL dutydetailback = WX.Model.DutyDetail.GetModel(Convert.ToInt32(ui_duty.SelectedValue));
            ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetailback.ID.ToString() + " and State>6 and State<40");
            dutydetailback.UsersName.value = xdt.ToColValueList(",", 0);
            if (dutydetailback.UsersName.ToString() != "")
                dutydetailback.Update();
            xdt = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users  where DutyId=" + dutydetail.ID.ToString() + " and State>6 and State<40");
            dutydetail.UsersName.value = xdt.ToColValueList(",", 0);
            if (dutydetail.UsersName.ToString() != "")
            {
                ULCode.QDA.XSql.Execute("update TE_DutyDetail set UsersName=replace(UsersName,'" + usermodel.RealName.ToString() + ",','')");
                dutydetail.UsersName.value = dutydetail.UsersName.ToString() + ",";
                dutydetail.Update();
            }
            //日志
            log.UserID.value = userId;
            log.BackDutyID.value = ui_duty.SelectedValue;
            log.BackDempID.value = ui_demp.SelectedValue;
            log.NowDutyID.value = ui_duty2.SelectedValue;
            log.NowDempID.value = ui_demp2.SelectedValue;
            log.Backtableid.value = tfk.type.ToString() == "1" ? 2 : 3;
            log.Backcolumid.value = tfkid;
            log.Starttime.value = DateTime.Now;
            log.GradeID.value = usermodel.Grade.value;
            WX.HR.DutyLog.MODEL backlog = WX.HR.DutyLog.GetModel("select top 1 * from HR_DutyLogs where UserID='" + userId + "' order by Starttime desc");
            if (backlog != null)
            {
                backlog.stoptime.value = DateTime.Now;
                backlog.Nowtableid.value = tfk.type.ToString() == "1" ? 2 : 3;
                backlog.Nowcolumid.value = tfkid;
                backlog.Update();
            }
            log.Insert();
            Response.Redirect("HR_Intojobs.aspx?state=20");
        }
    }
}