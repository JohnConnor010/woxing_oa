using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Data;
using WX.Model;

namespace wwwroot.Manage.HR
{
    public partial class User_EditUser : System.Web.UI.Page
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
            
            string userId = WX.Request.rUserId;
            Employee.MODEL employee =WX.Request.rEmpolyee;
            WX.Model.User.MODEL usermodel =WX.Request.rUser;
            if(employee.LoadSucceed||true)
            {
                for (int i = 0; i < Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                    //this.ddlCompany.SelectedItem.Value = employee.CompanyID.ToString();
                deptId = usermodel.DepartmentID.ToString();
                this.txtRealName.Text = usermodel.RealName.ToString();
                this.txtIDCard.Text = employee.IDCard.ToString();
                //this.ddlPosition.SelectedItem.Value = employee.DutyId.ToString();
                this.txtBirthday.Text = employee.Birthday.f("{0:yyyy-MM-dd}");
                this.txtMobile.Text = employee.Mobile.ToString();
                if (Convert.ToBoolean(employee.Sex.ToString()))
                {
                this.rblSex.SelectedValue ="1";
                }
                else
                {
                    this.rblSex.SelectedValue = "0";
                }
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
                CheckBox1.Checked = employee.IsInsurance.ToString() == "1";
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
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"/Images/nophoto.gif\" alt=\"\" style=\"width: 99%; height: 99%;\" />";
                }
                else
                {
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 99%; height: 99%; \" />";
                }
                this.txtSort.Text = employee.Sort.ToString();
                this.txtContent.Text = employee.Introduction.ToString();
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select top 1 * from [TU_Employees_Credentials] where Name='身份证扫描件' and UserID='"+employee.UserID.ToString()+"'");
                if (model != null)
                    cardannex.Text = "<a href=\"javascript:PopupIFrame('/Manage/Private/Priv_CredentialsDetail.aspx?Id=" + model.Id.ToString() + "','查看详细','','',1000,800)\">查看扫描件</a>";
           
            }
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
            string realName = this.txtRealName.Text.Trim();
            string idCard = this.txtIDCard.Text.Trim();
            string birthday = this.txtBirthday.Text;
            string mobile = this.txtMobile.Text.Trim();
            bool sex = rblSex.SelectedValue=="1"? true:false;
            string qq = this.txtQQNumber.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim(); 
            string address = this.txtAddress.Text.Trim() + ":" + this.txtaddresscode.Text.Trim() + "|" + this.txtaddress2.Text.Trim() + ":" + this.txtaddress2code.Text.Trim();
            
            string sort = this.txtSort.Text.Trim();
            string content = this.txtContent.Text.Trim();
            string userId = WX.Request.rUserId;
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
                WX.Main.ExecuteDelete("TU_Employees_Credentials", "UserId='" + WX.Request.rUserId + "' and Name", "身份证扫描件");
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.NewDataModel();
                model.Name.value = "身份证扫描件";
                model.Unit.value = "公安局";
                model.Ctime.value = (Convert.ToInt32(idCard.Substring(6, 4)) + 18) + "-01-01";
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + System.IO.Path.GetExtension(FileUpload2.FileName);
                FileUpload2.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
                model.UserId.value = WX.Request.rUserId;
                model.Save();
            }
            Employee.MODEL employee = WX.Request.rEmpolyee; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            usermodel.RealName.value = realName;
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
            employee.Marital.value = ui_Marital.Text;
            employee.Health.value = ui_Health.Text;
            employee.Address.value = address+"|"+ui_jg.Text+"|"+ui_hkd.Text;
            employee.IsInsurance.value = CheckBox1.Checked ? 1 : 0;
            int iR = usermodel.Update();
                
            //5.（用户及业务对象）统计与状态
            if (iR != 0)
            {employee.Update();
                //if (employee.UserID.ToString() == WX.Main.CurUser.UserID.ToString())
                //{
                //    WX.Main.CurUser.LoadEmployeeUser(true);
                //}
            }
            else
            {
                employee.RestoreInitial();
            }
            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "添加用户信息成功！", "");
            }
            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                ULCode.Debug.Confirm(this, "员工信息修改成功！是否返回员工列表页？", "User_UserList.aspx?CompanyID=11", this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert(Page, "员工信息修改成功！");
            }
            //Response.Redirect("User_UserList.aspx?CompanyID=11");
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}