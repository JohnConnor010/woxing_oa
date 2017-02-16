using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;
using ULCode;
using ULCode.QDA;
using WX;
namespace wwwroot.Manage.HR
{
    public partial class User_AddUser : System.Web.UI.Page
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
                if (String.IsNullOrEmpty(Request.Url.Query))
                {
                    Response.Redirect(String.Format("{0}?CompanyId={1}", this.Request.Url, WX.Main.DefaultCompanyId), true);
                    return;
                }
                for (int i = 0; i < Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                BindCompany();
                BindDuties();
                this.txtBirthday.Text = "1980-01-01";//Convert.ToDateTime("1980-01-01").ToString();
            }
        }
        private void BindCompany()
        {
            //DataTable dataTable = XSql.GetDataTable("SELECT ID,Name FROM TE_Companys");
            //this.ddlCompany.DataSource = dataTable.DefaultView;
            //this.ddlCompany.DataTextField = "Name";
            //this.ddlCompany.DataValueField = "ID";
            //this.ddlCompany.DataBind();
            int companyId = Convert.ToInt32(Request.QueryString["CompanyId"]);
            WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany, null, null, Convert.ToString(companyId));
        }
        private void BindDuties()
        {
            //DataTable dataTable = XSql.GetDataTable("SELECT ID,Name FROM TE_Duties");
            //this.ddlPosition.DataSource = dataTable.DefaultView;
            //this.ddlPosition.DataTextField = "Name";
            //this.ddlPosition.DataValueField = "ID";
            //this.ddlPosition.DataBind();
            WX.Data.Dict.BindListCtrl_Duties(this.ddlPosition, null, null, null);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string userName = this.userName.Text;
            string pwd = this.password.Text;
            string email = this.txtEmail.Text.Trim();
            MembershipUser muNew = Membership.CreateUser(userName, pwd, email);
            if (muNew == null)
            {
                ULCode.Debug.Alert("添加用户失败！可能是重复添加！");
                return;
            }
            Roles.AddUserToRole(userName, Convert.ToString(WX.RoleType.Employees));

            string companyId = this.ddlCompany.SelectedItem.Value;
            string departmentId = this.departmentId.Value;
            string realName = this.txtRealName.Text.Trim();
            string idCard = this.txtIDCard.Text.Trim();
            string position = this.ddlPosition.SelectedItem.Value;
            string birthday = this.txtBirthday.Text;
            string mobile = this.txtMobile.Text.Trim();
            bool sex = rblSex.SelectedValue == "1" ? true : false;
            string qq = this.txtQQNumber.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim();
            string address = this.txtAddress.Text.Trim();
            string sort = this.txtSort.Text.Trim();
            string content = this.txtContent.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            string fileExtension = null;
            if (this.FileUpload1.HasFile)
            {
                fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (!".gif.png.bmp.jpg".Contains(fileExtension))
                {
                    ULCode.Debug.Alert(this, "照片格式必须为图片格式！");
                    return;
                }
            }
            //4.业务处理过程
            #region string filePath=..
            string filePath = null;
            if (FileUpload1.HasFile)
            {
                string fileDir = "/UploadFiles/UserPhotos/";
                string fileName = realName; //DateTime.Now.ToString("yyyyMMddHHmmss");
                filePath = String.Format("{0}{1}{2}", fileDir, fileName, fileExtension);
                try
                {
                    FileUpload1.SaveAs(Server.MapPath(filePath));
                }
                catch
                {
                    ULCode.Debug.Alert(this, "照片上传失败，可能是没有写的权限，请与管理员联系！");
                    filePath = null;
                }
            }
            #endregion
            WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel();
            usermodel.CompanyID.value = companyId;
            usermodel.DepartmentID.value = departmentId;
            usermodel.DutyId.value = position;
            usermodel.UserID.value = muNew.ProviderUserKey.ToString();
            usermodel.RealName.value = realName;
            usermodel.State.value = 5;
            usermodel.Grade.value = 0;
            int iR = usermodel.Insert();
            
            //5.（用户及业务对象）统计与状态
            //6.登记日志
            Employee.MODEL employee= Employee.NewDataModel();
            if (iR > 0)
            {
                employee.UserID.value = usermodel.UserID.value;
                employee.IDCard.value = idCard;
                employee.Sex.value = sex;
                employee.Birthday.value = birthday;
                employee.Mobile.value = mobile;
                employee.QQ.value = qq;
                employee.Email.value = email;
                employee.Tel.value = telephone;

                employee.Titles.value = this.ui_Titles.Text;
                employee.Ethnic.value = this.ui_Ethnic.Text;
                employee.Edu.value = ui_edu.SelectedValue;
                employee.Prof.value = ui_Prof.Text;
                employee.ForeignL.value = ui_ForeignL.Text;
                employee.Rating.value = ui_Rating.Text;
                employee.Marital.value = ui_Marital.Text;
                employee.Health.value = ui_Health.Text;
                employee.Address.value = address + "|" + ui_jg.Text + "|" + ui_hkd.Text;

                employee.UserFace.value = filePath;
                employee.Sort.value = sort;
                employee.Introduction.value = content;
                employee.Insert();
                usermodel.SaveIntoCaches();
                WX.Main.AddLog(LogType.Default, "添加用户信息成功！", "");
            }
            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                //ULCode.Debug.Confirm(this, "添加用户成功！是否继续添加？", this.Request.RawUrl, "User_UserList.aspx?CompanyID=11");
                Response.Redirect("/Manage/HR/HR_AddIntojobs.aspx?uid=" + employee.UserID.ToString());
            }
            else
            {
                ULCode.Debug.Alert(this,"添加用户失败！");
            }
        }
    }
}