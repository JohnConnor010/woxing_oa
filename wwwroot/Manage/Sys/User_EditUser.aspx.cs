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

            string userId = Convert.ToString(Request.QueryString["id"]);
            //Employee.MODEL employee = Employee.NewDataModel(userId);
            Employee.MODEL employee = WX.Request.rEmpolyee; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(userId);
            if (employee.LoadSucceed || true)
            {
                WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany, null, null, usermodel.CompanyID.ToString());
                for (int i = 0; i < Employee.eduarray.Length; i++)
                {
                    ui_edu.Items.Add(new ListItem(Employee.eduarray[i], Employee.eduarray[i]));
                }
                ui_edu.SelectedValue = "大专";
                //this.ddlCompany.SelectedItem.Value = employee.CompanyID.ToString();
                this.txtRealName.Text = usermodel.RealName.ToString();
                this.txtIDCard.Text = employee.IDCard.ToString();
                //this.ddlPosition.SelectedItem.Value = employee.DutyId.ToString();
                this.txtBirthday.Text = employee.Birthday.ToString();
                this.txtMobile.Text = employee.Mobile.ToString();
                if (Convert.ToBoolean(employee.Sex.ToString()))
                {
                    this.rblSex.SelectedValue = "1";
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
                if (addrarry.Length > 1)
                {
                    this.ui_jg.Text = addrarry[1];
                    this.ui_hkd.Text = addrarry[2];
                }
                this.txtAddress.Text = addrarry[0];
                if (employee.UserFace.isEmpty)
                {
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"\" alt=\"\" style=\"width: 100%; height: 100%; display: none;\" />";
                }
                else
                {
                    this.liPreZoomImage.Text = "<img id=\"preZoomImage\" src=\"" + (employee.UserFace.ToString()) + "\" alt=\"\" style=\"width: 100%; height: 100%; \" />";
                }
                this.txtSort.Text = employee.Sort.ToString();
                this.txtContent.Text = employee.Introduction.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            //if (!this.Master.A_Edit)
            //{
            //    Response.Write("你没有权限访问此功能！");
            //    Response.End();
            //    return;
            //}
            //2.取得用户变量
            string companyId = this.ddlCompany.SelectedItem.Value;
            string realName = this.txtRealName.Text.Trim();
            string idCard = this.txtIDCard.Text.Trim();
            string birthday = this.txtBirthday.Text;
            string mobile = this.txtMobile.Text.Trim();
            bool sex = rblSex.SelectedValue == "1" ? true : false;
            //if (this.rblSex.SelectedItem.Value == "1")
            //{
            //    sex = false;
            //}
            string qq = this.txtQQNumber.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim();
            string address = this.txtAddress.Text.Trim();
            string sort = this.txtSort.Text.Trim();
            string content = this.txtContent.Text.Trim();
            string userId = Request.QueryString["id"];
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
            Employee.MODEL employee = WX.Request.rEmpolyee; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(userId);
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
            usermodel.CompanyID.value = companyId;
            usermodel.RealName.value = realName;
            int iR = usermodel.Update();

            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
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
                employee.Address.value = address + "|" + ui_jg.Text + "|" + ui_hkd.Text;
                employee.Update();
                //6.登记日志
                WX.Model.Company.AddLogs(Convert.ToInt32(usermodel.CompanyID.ToString()), 6, usermodel.RealName.ToString() + "的档案信息修改成功！" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);

                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "档案信息修改成功！", "User_Skill.aspx?UserID=" + usermodel.UserID.ToString() + "&companyid=" + usermodel.CompanyID.ToString());
            }
            else
            {
                usermodel.RestoreInitial();
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}