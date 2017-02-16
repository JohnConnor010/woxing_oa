using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.Sys
{
    public partial class Priv_Family : System.Web.UI.Page
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
            { t3.Visible = !WX.Main.Priv_IsTemp;
                t1.Visible = WX.Main.Priv_IsTemp;
                if (t1.Visible)
                {
                    WX.Main.CurUser.LoadEmployeeUser();
                    WX.Main.CurUser.LoadUserModel(false);
                    Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                    Text_Template.MODEL ttM = Text_Template.NewDataModel(4);
                    if (!ttM.Apply.ToBoolean())
                    {
                        this.t1.Visible = true;
                        this.t2.Visible = false;
                        this.trSubmit.Visible = WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean();
                        this.ui_content.Value = employee.Family.isEmpty ? ttM.Template.ToString() : employee.Family.ToString();
                    }
                    else
                    {
                        this.t1.Visible = false;
                        this.t2.Visible = true;
                        this.lblContent.Text = employee.Family.ToString();
                        this.ui_content1.Value = ttM.Template.ToString();
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string content = this.ui_content.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            WX.Main.CurUser.LoadEmployeeUser();
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.Family.value = content;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
            //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, "用户家庭成员修改成功！", "");
            //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "家庭成员修改成功！请完善下一步“工作经历”", "Priv_Work.aspx");
            }
            else
            {
                ULCode.Debug.Alert(Page, "家庭成员修改成功！");
            }
        }
        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string content = this.ui_content1.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            WX.Main.CurUser.LoadEmployeeUser();
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.Family.value = content;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
                //if (employee.UserID.ToString() == WX.Main.CurUser.UserID.ToString())
                //{
                //    WX.Main.CurUser.LoadEmployeeUser(true);
                //}
            }
            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "用户家庭成员修改成功！", "");
            }
            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                ULCode.Debug.Alert(this, "个人家庭成员修改成功");
            }
            else
            {
                ULCode.Debug.Alert(Page, "个人家庭成员修改成功！");
            }
        }
    }
}