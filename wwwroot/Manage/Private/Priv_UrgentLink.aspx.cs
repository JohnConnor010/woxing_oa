﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.Private
{
    public partial class Priv_UrgentLink : System.Web.UI.Page
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
                t3.Visible = !WX.Main.Priv_IsTemp;
                t1.Visible = WX.Main.Priv_IsTemp;
                if (t1.Visible)
                {
                    WX.Main.CurUser.LoadEmployeeUser(false);
                    WX.Main.CurUser.LoadUserModel(false);
                    Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                    Text_Template.MODEL ttM = Text_Template.NewDataModel(5);
                    if (!ttM.Apply.ToBoolean())
                    {
                        this.t1.Visible = true;
                        this.t2.Visible = false;
                        this.trSubmit.Visible = WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean();
                        this.ui_content.Value = employee.UrgentLink.isEmpty ? ttM.Template.ToString() : employee.UrgentLink.ToString();
                    }
                    else
                    {
                        this.t1.Visible = false;
                        this.t2.Visible = true;
                        this.lblContent.Text = employee.UrgentLink.ToString();
                        this.ui_content1.Value = ttM.Template.ToString();
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string urgentlink = this.ui_content.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            WX.Main.CurUser.LoadEmployeeUser(false);
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.UrgentLink.value = urgentlink;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
            //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, "紧急联络人修改成功！", "");
            //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "紧急联络人修改成功！", "Priv_UserInfo.aspx");
            }
            else
            {
                ULCode.Debug.Alert(Page, "紧急联络人修改成功！");
            }
        }
        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string urgentlink = this.ui_content1.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            WX.Main.CurUser.LoadEmployeeUser(false);
            Employee.MODEL employee = WX.Main.CurUser.EmployeeUser; //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.UrgentLink.value = urgentlink;
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
                WX.Main.AddLog(WX.LogType.Default, "个人紧急联络人修改成功！", "");
            }
            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                ULCode.Debug.Alert(this, "个人紧急联络人修改成功！");
            }
            else
            {
                ULCode.Debug.Alert(Page, "个人紧急联络人修改成功！");
            }
        }
    }
}