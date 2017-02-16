using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Data;
using WX.Model;

namespace wwwroot.Manage.Sys
{
    public partial class Priv_UserInfo : System.Web.UI.Page
    {
        protected string deptId;

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
                WX.Model.User.MODEL usermodel;
                if (WX.Request.rUserId != null)
                {
                    usermodel = WX.Request.rUser;
                    hlInit.Visible = false;
                }
                else
                {
                    WX.Main.CurUser.LoadUserModel(false);
                    usermodel = WX.Main.CurUser.UserModel;
                    hlInit.Visible = usermodel.ArchiveBySelf.ToBoolean();
                }

                this.FillDataCtrl();
            }
        }
        private void FillDataCtrl()
        {
            Employee.MODEL employee;
            WX.Model.User.MODEL usermodel;
            if (WX.Request.rUserId != null)
            {
                employee = WX.Request.rEmpolyee;
                usermodel = WX.Request.rUser;
            }
            else
            {
                WX.Main.CurUser.LoadUserModel(false);
                employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + WX.Main.CurUser.UserID + "'");
                usermodel = WX.Main.CurUser.UserModel;
            }
            if (employee.LoadSucceed || true)
            {
                Department.MODEL dept=Department.GetCache(usermodel.DepartmentID.ToInt32());
                if (dept!=null)
                {
                    lblDeptName.Text = dept.Name.ToString();
                }
                DutyDetail.MODEL duty=DutyDetail.GetCache(usermodel.DutyId.ToInt32());
                if (duty!=null)
                {
                    lblDutyName.Text = duty.Name.ToString();
                }
                Company.MODEL cmp = Company.GetCache(usermodel.CompanyID.ToInt32());
                if (cmp!=null)
                {
                    lblCompanyName.Text = cmp.Name.ToString();
                }
                //this.ddlCompany.SelectedItem.Value = employee.CompanyID.ToString();
                deptId = usermodel.DepartmentID.ToString();
                this.lblRealName.Text = usermodel.RealName.ToString();
                this.lblIdCard.Text = employee.IDCard.ToString();
                //this.ddlPosition.SelectedItem.Value = employee.DutyId.ToString();
                this.lblBirthday.Text = employee.Birthday.f("{0:yyyy年MM月dd日}");
                this.lblMoblie.Text = employee.Mobile.ToString();
                this.lblSex.Text = employee.Sex.ToBoolean() ? "男" : "女";
                this.lblQQ.Text = employee.QQ.ToString();
                this.lblEmail.Text = employee.Email.ToString();
                this.lblTelephone.Text = employee.Tel.ToString();
                string[] addrarry = employee.Address.ToString().Split('|');
                if (addrarry.Length > 1)
                {
                    this.lblAddress.Text = addrarry[0].Split(':')[0] + "&nbsp;&nbsp;" + addrarry[0].Split(':')[1];
                    this.lblAddress2.Text = addrarry[1].Split(':')[0] + "&nbsp;&nbsp;" + addrarry[1].Split(':')[1];
                }
                if (employee.UserFace.isEmpty)
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"/Images/NoPhoto.gif\" alt=\"\" style=\"width: 100%; height: 100%;\" />";
                else
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 100%; height: 100%; \" />";
                this.lblContent.Text = employee.Introduction.ToString();
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select top 1 * from [TU_Employees_Credentials] where Name='身份证扫描件'");
                if (model != null)
                    cardannex.Text = "<a href=\"javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=" + model.Id.ToString() + "','查看详细','','',1000,800)\">查看扫描件</a>";
            }
        }
    }
}