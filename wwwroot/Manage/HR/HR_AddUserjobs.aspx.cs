using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace wwwroot.Manage.HR
{
    public partial class HR_AddUserjobs : System.Web.UI.Page
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
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string userId = WX.Request.rUserId;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            Label1.Text = "当前员工姓名：" + usermodel.RealName.ToString();
            WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
            bindjob();
        }
        public void bindjob()
        {
            DataTable dt;

            WX.Model.User.MODEL user = WX.Request.rUser;
            dt = WX.Model.DutyDetail.GetTablenullDepartent(this.ddlDepartment.SelectedValue, user.RealName.ToString());
            this.ui_jobname.DataSource = dt;
            this.ui_jobname.DataTextField = "Name";
            this.ui_jobname.DataValueField = "ID";
            this.ui_jobname.DataBind();
            if (this.ui_jobname.Items.Count == 0)
                Button1.Enabled = false;
            else
                Button1.Enabled = true;
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string userId = WX.Request.rUserId;
            string dutydetailid = ui_jobname.SelectedValue;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = ULCode.QDA.XSql.Execute("insert into TU_User_X_DutyDetail(UserID,DutyDetailID) values('" + userId + "'," + dutydetailid + ")");
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Model.DutyDetail.MODEL dutydetail = WX.Model.DutyDetail.GetModel(Convert.ToInt32(dutydetailid));
                WX.Model.User.MODEL usermodel = WX.Request.rUser;
                WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
                log.UserID.value = userId;
                log.NowDutyID.value = dutydetailid;
                log.NowDempID.value = ddlDepartment.SelectedValue;
                log.Backtableid.value = 5;
                log.Backcolumid.value = 0;
                log.Starttime.value = DateTime.Now;
                log.GradeID.value = dutydetail.GradeID.value;
                log.Content.value = "员工职务添加";
                log.Insert();
                dutydetail.UsersName.value = dutydetail.UsersName.ToString() + usermodel.RealName.ToString() + ",";
                dutydetail.Update();
                ULCode.Debug.Alert("员工职务添加成功！", "HR_Userjobs.aspx?UserID="+userId);
            }
            
        }
    }
}