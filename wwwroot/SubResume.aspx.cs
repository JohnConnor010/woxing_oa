using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using WX.Model;
using System.Data;

namespace wwwUser
{
    public partial class SubResume : System.Web.UI.Page
    {
        public string skillstr = "";
        public string edustr = "";
        public string workstr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = WX.Soft.Name + " 简历提交页";
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
                bindjob();
                for (int i = 0; i < Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                //if(WX.Authentication.IsAuthenticated) {
                //    txtRealName.Text = WX.Main.CurUser.UserName;
                //    txtRealName.ReadOnly = true;
                //}
                //else {
                //    Response.Redirect("RegisterE.aspx");
                //}
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }

        public void bindjob()
        {
            DataTable dt;
            dt = WX.Model.DutyDetail.GetTableDepartent(ddlDepartment.SelectedValue);
            this.ui_jobname.DataSource = dt;
            this.ui_jobname.DataTextField = "Name";
            this.ui_jobname.DataValueField = "ID";
            this.ui_jobname.DataBind();
        }
        protected void RegisterUserother(object sender, EventArgs e)
        {

            if (!WX.Authentication.IsAuthenticated)
            {
                SubAdd();
                ULCode.Debug.Alert("保存成功！请继续完善个人技能、教育经历、工作经历！");
            }
            else
            {
                Session.Clear();
                WX.Authentication.LoginOut();
                ULCode.Debug.Alert(this, "提交成功!", "Login.aspx");//,"ceping.aspx"
            }
        }
        protected void RegisterUser(object sender, EventArgs e)
        {
            SubAdd();
            ULCode.Debug.Alert("保存成功！继续完善以下档案资料！");

        }
        private void SubAdd()
        {
            Employee.MODEL employee;
            string idCard = this.txtIDCard.Text.Trim();
            string name = this.txtRealName.Text;
            if (WX.Authentication.IsAuthenticated)
            {
                WX.Main.CurUser.LoadEmployeeUser();
                employee = WX.Main.CurUser.EmployeeUser;
            }
            else
            {
                //获取用户变量
                string pwd = idCard.Substring(idCard.Length - 6);
                string code = this.txtGetCode.Text;
                if (Membership.GetUser(name) != null)
                {
                    ULCode.Debug.Alert(this, "此用户已经存在!");
                    return;
                }
                //验证用户变量
                if (Membership.GetUserNameByEmail(txtEmail.Text.Trim()) != null)
                {
                    ULCode.Debug.Alert(this, "此邮箱已经存在请换一个!");
                    return;
                }
                if (HttpContext.Current.Session["CheckCode"] != null && Convert.ToString(HttpContext.Current.Session["CheckCode"]) != code)
                {
                    ULCode.Debug.Alert(this, "验证码不对!");
                    return;
                }
                ////注册员工默认信息

                //注册账户
                MembershipUser mu = Membership.CreateUser(name, pwd);
                //添加员工
                Roles.AddUserToRole(name, Convert.ToString(WX.RoleType.Employees));
                ////登录到浏览器
                WX.Authentication.LoginIn(name);
                //注册员工默认信息
                WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel();
                usermodel.UserID.set(mu.ProviderUserKey.ToString());
                usermodel.CompanyID.set(11);
                usermodel.DutyId.set(1000);
                usermodel.DepartmentID.set(0);
                usermodel.RealName.set(name);
                usermodel.ArchiveBySelf.set(true);
                usermodel.State.set(5);
                usermodel.Insert();
                usermodel.SaveIntoCaches();
                employee = Employee.NewDataModel();
                employee.UserID.set(usermodel.UserID.value);
            }

            //string position = this.ddlPosition.SelectedItem.Value;
            string birthday = txtBirthday.Text.Trim();// this.txtBirthday.Text;
            if (!ULCode.Validation.IsDateTime(birthday))
            {
                ULCode.Debug.Alert(this, "日期格式不正确，请正确使用日期控件！");
                return;
            }
            string mobile = this.txtMobile.Text.Trim();
            bool sex = rblSex.SelectedValue == "1" ? true : false;
            string email = txtEmail.Text.Trim();//this.txtEmail.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim();
            string address = this.txtAddress.Text.Trim() + ":|:";
            string content = this.txtContent.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            string fileExtension = null;
            if (this.FileUpload1.HasFile)
            {
                fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (!".gif.png.bmp.jpg".Contains(fileExtension))
                {
                    ULCode.Debug.Alert(this, "照片格式必须为.gif.png.bmp.jpg图片格式！");
                    return;
                }
            }
            string filePath = null;
            if (FileUpload1.HasFile)
            {
                if (employee.UserFace.isEmpty)
                {
                    string fileDir = "/UploadFiles/UserPhotos/";
                    string fileName = WX.Main.CurUser.UserName; //DateTime.Now.ToString("yyyyMMddHHmmss");
                    filePath = String.Format("{0}{1}{2}", fileDir, fileName, fileExtension);
                }
                else
                {   //如果原文件有则覆盖原文件
                    filePath = employee.UserFace.ToString();
                }
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
            //#endregion
            employee.IDCard.value = idCard;
            employee.Sex.value = sex;
            employee.Birthday.value = birthday;
            employee.Mobile.value = mobile;
            employee.Email.value = email;
            employee.Tel.value = telephone;
            if (!String.IsNullOrEmpty(filePath))
            {
                employee.UserFace.value = filePath;
            }
            employee.Introduction.value = content;
            employee.Titles.value = this.ui_Titles.Text;
            employee.Ethnic.value = this.ui_Ethnic.Text;
            employee.Edu.value = ui_edu.SelectedValue;
            employee.Prof.value = ui_Prof.Text;
            employee.ForeignL.value = ui_ForeignL.Text;
            employee.Rating.value = ui_Rating.Text;
            employee.Marital.value = ui_Marital.SelectedValue;
            employee.Health.value = ui_Health.Text;
            employee.Address.value = address + "||" + ui_hkd.Text;

            employee.DepartmentID.value = ddlDepartment.SelectedValue;
            employee.DutyId.value = ui_jobname.SelectedValue;
            employee.Salary.value = ui_salary.Text;

            int iR = employee.Insert();
            WX.Model.Audition.MODEL auditionmodel = WX.Model.Audition.NewDataModel();
            auditionmodel.UserID.value = employee.UserID.value;
            auditionmodel.Insert();
            WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=1", "UserID='" + employee.UserID.ToString() + "'");
            WX.Main.MessageSend("<a href=/Manage/HR/User_Resume.aspx?UserID=" + employee.UserID.ToString() + "&mes=1>" + name + "——面试通知</a>", "/Manage/Main/messagelist.aspx", WX.CommonUtils.GetHRUserID, employee.UserID.ToString(), 7, 0);
            skillstr = "/Manage/include/KeyXmlEdit.aspx?table=TU_Employees&column=Skill&appid=Priv-Skill&key=UserID&keyvalue=" + employee.UserID.ToString();
            edustr = "/Manage/include/KeyXmlEdit.aspx?table=TU_Employees&column=Education&appid=Priv-Edu&key=UserID&keyvalue=" + employee.UserID.ToString();
            workstr = "/Manage/include/KeyXmlEdit.aspx?table=TU_Employees&column=Work&appid=Priv-Workwew&key=UserID&keyvalue=" + employee.UserID.ToString();
            WX.HR.DutyLog.MODEL log = WX.HR.DutyLog.NewDataModel();
            log.UserID.value = employee.UserID.value;
            log.NowDutyID.value = employee.DutyId.value;
            log.NowDempID.value = employee.DepartmentID.value;
            log.Backtableid.value = 7;
            log.Backcolumid.value = 0;
            log.Starttime.value = DateTime.Now;
            log.Content.value = "员工简历登记";
            log.Insert();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Membership.GetUserNameByEmail(txtEmail.Text.Trim()) != null)
            {
                ULCode.Debug.Alert(this, "此邮箱已经存在请换一个!");
                return;
            }
            HttpContext.Current.Session["CheckCode"] = WX.Rand.Number(5);
            string bodystr = "欢迎使用我行信息有限公司OA办公管理系统，验证码为：<font color='red'>" + HttpContext.Current.Session["CheckCode"] + "</font><br><br>此邮件为系统发送，请勿回复！";
            if (WX.Main.SendEmail(txtEmail.Text.Trim(), "我行信息有限公司-邮箱验证！", bodystr))
                ULCode.Debug.Alert(Page, "验证码已发送到您的邮箱请登录邮箱查看！");
            else
                ULCode.Debug.Alert(Page, "验证码发送失败请重试！");
        }
    }
}