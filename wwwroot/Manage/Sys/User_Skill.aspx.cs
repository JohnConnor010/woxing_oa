using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.Sys
{
    public partial class User_Skill : System.Web.UI.Page
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
                string userId = WX.Request.rUserId;
                //if (!ULCode.Validation.IsGuid(userId))
                //{
                //    ULCode.Debug.we("你没有权限访问此页面！");
                //    return;
                //}
                Employee.MODEL employee = Employee.GetModel("select * from [TU_Employees] where UserID='" + userId + "'"); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                if (employee.LoadSucceed||true)
                {
                    ui_content.Value = employee.Skill.ToString();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string skill = this.ui_content.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            string userId = WX.Request.rUserId;
            Employee.MODEL employee = Employee.GetModelToID(userId ); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            employee.Skill.value = skill;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            //6.登记日志
            if (iR > 0)
            {

                WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(userId);
                //6.登记日志
                WX.Model.Company.AddLogs(Convert.ToInt32(usermodel.CompanyID.ToString()), 6, usermodel.RealName.ToString() + "的档案修改了个人技能！" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);

                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "个人技能信息修改成功！", "User_Education.aspx?UserID=" + employee.UserID.ToString() + "&companyid=" + usermodel.CompanyID.ToString());
            

            }
        }
    }
}