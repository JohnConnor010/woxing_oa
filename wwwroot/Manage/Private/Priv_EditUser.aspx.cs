using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Data;
using WX.Model;

namespace wwwroot.Manage.Private
{
    public partial class Priv_EditUser : System.Web.UI.Page
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
                this.FillDataCtrl();
            }
        }
        private void FillDataCtrl()
        {
            WX.Main.CurUser.LoadEmployeeUser(true);
            WX.Main.CurUser.LoadUserModel(false);
            this.tableInit.Visible = WX.Main.CurUser.UserModel.State.ToInt32() == 5;
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser;
            try
            {
                if (Request["mes"] != null)
                    WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Priv_EditUser.aspx%'", WX.Main.CurUser.UserID));
            }
            catch
            {
            }
            if (!WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean())
            {
                Response.Redirect("Priv_UserInfo.aspx");
                ULCode.Debug.we("你没有权限访问此功能！");
                return;
            }
            if (true)
            {
                for (int i = 0; i < Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                //this.ddlCompany.SelectedItem.Value = employee.CompanyID.ToString();
                //deptId = employee.DepartmentID.ToString();
                this.txtRealName.Text = WX.Main.CurUser.UserModel.RealName.ToString();
                this.txtIDCard.Text = employee.IDCard.ToString();
                //this.ddlPosition.SelectedItem.Value = employee.DutyId.ToString();
                this.txtBirthday.Text = employee.Birthday.f("{0:yyyy-MM-dd}");
                this.txtMobile.Text = employee.Mobile.ToString();
                this.rblSex.SelectedValue = employee.Sex.isEmpty || employee.Sex.ToBoolean() ? "1" : "0";
                this.txtQQNumber.Text = employee.QQ.ToString();
                this.txtEmail.Text = employee.Email.ToString();
                this.txtTelephone.Text = employee.Tel.ToString();
                this.ui_Titles.Text = employee.Titles.ToString();
                this.ui_Ethnic.Text = employee.Ethnic.ToString();
                this.ui_edu.SelectedValue = employee.Edu.ToString();
                this.ui_Prof.Text = employee.Prof.ToString();
                this.ui_ForeignL.Text = employee.ForeignL.ToString();
                this.ui_Rating.Text = employee.Rating.ToString();
                this.ui_Marital.Text = employee.Marital.ToString();
                this.ui_Health.Text = employee.Health.ToString();
                string[] addrarry = employee.Address.ToString().Split('|');
                if (addrarry.Length > 1)
                {
                    this.ui_jg.Text = addrarry[1].Split(':')[0];
                    this.ui_hkd.Text = addrarry[2].Split(':')[0];
                    this.txtAddress.Text = addrarry[0].Split(':')[0];
                    try
                    {
                        this.txtaddresscode.Text = addrarry[0].Split(':')[1];
                    }
                    catch { }
                    this.txtaddress2.Text = addrarry[1].Split(':')[0];
                    try
                    {
                        this.txtaddress2code.Text = addrarry[1].Split(':')[1];
                    }
                    catch { }
                }
                if (employee.UserFace.isEmpty)
                {
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"/Images/NoPhoto.gif\" alt=\"\" style=\"width: 99%; height: 99%; \" />";
                }
                else
                {
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 99%; height: 99%; \" />";
                }
                this.txtSort.Text = employee.Sort.ToString();
                this.txtContent.Text = employee.Introduction.ToString();
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select top 1 * from [TU_Employees_Credentials] where Name='身份证扫描件' and UserId='" + WX.Main.CurUser.UserID + "'");
                if (model != null)
                    Literal1.Text = "<a href=\"javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=" + model.Id.ToString() + "','查看详细','','',1000,800)\">查看扫描件</a>";
                WX.Model.EmployeeCredential.MODEL model2 = WX.Model.EmployeeCredential.GetModel("Select top 1 * from [TU_Employees_Credentials] where Name='" + ui_edu.SelectedItem.Text + "毕业证扫描件' and UserId='" + WX.Main.CurUser.UserID + "'");
                if (model2 != null)
                    Literal2.Text = "<a href=\"javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=" + model2.Id.ToString() + "','查看详细','','',1000,800)\">查看扫描件</a>";
                WX.Model.EmployeeCredential.MODEL model3 = WX.Model.EmployeeCredential.GetModel("Select top 1 * from [TU_Employees_Credentials] where Name='" + "健康证扫描件' and UserId='" + WX.Main.CurUser.UserID + "'");
                if (model3 != null)
                    Literal3.Text = "<a href=\"javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=" + model2.Id.ToString() + "','查看详细','','',1000,800)\">查看扫描件</a>";

                //WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany,null, null, employee.CompanyID.ToString());


                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, employee.DepartmentID.ToString());
                bindjob();
                ui_jobname.SelectedValue = employee.DutyId.ToString();
                ui_salary.Text = employee.Salary.ToString();
                WX.Main.CurUser.LoadUserModel(false);
                btnSubmit.Enabled = WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean();
            }
        }
        public void bindjob()
        {
            DataTable dt;
            WX.HR.IntoJob.MODEL intojob = WX.HR.IntoJob.GetModel("select * from HR_Intojobs where UserID='" + WX.Main.CurUser.UserID + "'");
            if (intojob != null)
            {
                dt = WX.Model.DutyDetail.GetTableDepartent(ddlDepartment.SelectedValue);
            }
            else
            {
                User.MODEL user = WX.Request.rUser;
                dt = WX.Model.DutyDetail.GetTablenullDepartent(ddlDepartment.SelectedValue, String.Empty);
            }
            this.ui_jobname.DataSource = dt;
            this.ui_jobname.DataTextField = "Name";
            this.ui_jobname.DataValueField = "ID";
            this.ui_jobname.DataBind();
            if (this.ui_jobname.Items.Count == 0)
                Button1.Enabled = false;
            else
                Button1.Enabled = true;
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
            //string companyId = this.ddlCompany.SelectedItem.Value;
            //string departmentId = this.departmentId.Value;
            string realName = this.txtRealName.Text.Trim();
            string idCard = this.txtIDCard.Text.Trim();
            //string position = this.ddlPosition.SelectedItem.Value;
            string birthday = Request.Form["ctl00$ContentPlaceHolder$txtBirthday"];// this.txtBirthday.Text;
            if (!ULCode.Validation.IsDateTime(birthday))
            {
                ULCode.Debug.Alert(this, "日期格式不正确，请正确使用日期控件！");
                return;
            }
            string mobile = this.txtMobile.Text.Trim();
            bool sex = rblSex.SelectedValue == "1" ? true : false;
            string qq = this.txtQQNumber.Text.Trim();
            string email = Request.Form["ctl00$ContentPlaceHolder$txtEmail"];//this.txtEmail.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim();
            string address = this.txtAddress.Text.Trim() + ":" + this.txtaddresscode.Text.Trim() + "|" + this.txtaddress2.Text.Trim() + ":" + this.txtaddress2code.Text.Trim();
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
            if (FileUpload2.HasFile)
            {
                WX.Main.ExecuteDelete("TU_Employees_Credentials", "UserId='" + WX.Main.CurUser.UserID + "' and Name", "身份证扫描件");
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.NewDataModel();
                model.Name.value = "身份证扫描件";
                model.Unit.value = "公安局";
                model.Ctime.value = (Convert.ToInt32(idCard.Substring(6, 4)) + 18) + "-01-01";
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + System.IO.Path.GetExtension(FileUpload2.FileName);
                FileUpload2.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
                model.UserId.value = WX.Main.CurUser.UserID;
                model.Save();
            }
            if (FileUpload3.HasFile)
            {
                WX.Main.ExecuteDelete("TU_Employees_Credentials", "UserId='" + WX.Main.CurUser.UserID + "' and Name", ui_edu.SelectedItem.Text + "毕业证扫描件");

                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.NewDataModel();
                model.Name.value = ui_edu.SelectedItem.Text + "毕业证扫描件";
                model.Unit.value = "教育部";
                model.Ctime.value = (Convert.ToInt32(idCard.Substring(6, 4)) + 22) + "-01-01";
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + System.IO.Path.GetExtension(FileUpload3.FileName);
                FileUpload3.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
                model.UserId.value = WX.Main.CurUser.UserID;
                model.Save();
            } 
            if (FileUpload4.HasFile)
            {
                WX.Main.ExecuteDelete("TU_Employees_Credentials", "UserId='" + WX.Main.CurUser.UserID + "' and Name", "健康证扫描件");

                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.NewDataModel();
                model.Name.value = "健康证扫描件";
                model.Unit.value = "卫生部";
                model.Ctime.value = (Convert.ToInt32(idCard.Substring(6, 4)) + 20) + "-01-01";
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + System.IO.Path.GetExtension(FileUpload4.FileName);
                FileUpload4.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
                model.UserId.value = WX.Main.CurUser.UserID;
                model.Save();
            }
            WX.Main.CurUser.LoadEmployeeUser(false);
            WX.Main.CurUser.LoadUserModel(false);
            WX.Model.User.MODEL usermodel = WX.Main.CurUser.UserModel;
            usermodel.RealName.value = realName;
            usermodel.Update();
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser;
            #region string filePath=..
            string filePath = null;
            if (FileUpload1.HasFile)
            {
                if (employee.UserFace.isEmpty)
                {
                    string fileDir = "/UploadFiles/UserPhotos/";
                    string fileName = usermodel.RealName.ToString(); //DateTime.Now.ToString("yyyyMMddHHmmss");
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
            #endregion
            //employee.CompanyID.value = companyId;
            //employee.DepartmentID.value = departmentId;
            //employee.DutyId.value = position;
            employee.IDCard.value = idCard;
            employee.Sex.value = sex;
            employee.Birthday.value = birthday;
            employee.Mobile.value = mobile;
            employee.QQ.value = qq;
            employee.Email.value = email;
            employee.Tel.value = telephone;
            if (!String.IsNullOrEmpty(filePath))
            {
                employee.UserFace.value = filePath;
            }
            employee.Sort.value = sort;
            employee.Introduction.value = content;
            employee.Titles.value = this.ui_Titles.Text;
            employee.Ethnic.value = this.ui_Ethnic.Text;
            employee.Edu.value = ui_edu.SelectedValue;
            employee.Prof.value = ui_Prof.Text;
            employee.ForeignL.value = ui_ForeignL.Text;
            employee.Rating.value = ui_Rating.Text;
            employee.Marital.value = ui_Marital.SelectedValue;
            employee.Health.value = ui_Health.Text;
            employee.Address.value = address + "|" + ui_jg.Text + "|" + ui_hkd.Text;
            //教育经历
            string[] items = System.Configuration.ConfigurationManager.AppSettings["Priv-Edu"].ToString().Split('|');
            ULCode.KeyXmlString kxs0 = new ULCode.KeyXmlString();
            if (employee.DFields["Education"].ToString().IndexOf("<KeyXmlString>") > -1)
            {
                kxs0.LoadData(employee.Education.ToString().Replace("&nbsp;", ""));
            }
            if (employee.Education.ToString().IndexOf("<学历>" + ui_edu.SelectedItem.Text + "</学历>") == -1)
            {
                ULCode.KeyXmlString kxs = new ULCode.KeyXmlString();
                kxs.SetItemValue("时间", "");
                kxs.SetItemValue("学历", ui_edu.SelectedItem.Text);
                kxs.SetItemValue("专业", employee.Prof.ToString());
                kxs.SetItemValue("学校", "");
                kxs0.AddItem("Node", kxs.GetSavedData());
                employee.Education.value = kxs0.GetSavedData();
            }
            employee.DepartmentID.value = ddlDepartment.SelectedValue;
            employee.DutyId.value = ui_jobname.SelectedValue;
            employee.Salary.value = ui_salary.Text;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            //6.登记日志
            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "修改个人信息成功！", "");
                ULCode.Debug.Alert(Page, "个人信息修改成功！请完善下一步“个人技能”", "Priv_Skill.aspx");
            }
            else
            {
                usermodel.RestoreInitial();
                ULCode.Debug.Alert(Page, "个人信息修改失败！");
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindjob();
        }
    }
}