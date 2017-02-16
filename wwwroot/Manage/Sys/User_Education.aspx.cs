using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.Sys
{
    public partial class User_Education : System.Web.UI.Page
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
                Employee.MODEL employee = Employee.GetModel("select * from [TU_Employees] where UserID='" + userId + "'"); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                if (employee.LoadSucceed || true)
                {
                    if (employee.Education.ToString().Trim() == "")
                        ui_content.Value = " <table class='table3' style=\"text-align:center;line-height:200%;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\">\n<tr style=\"font-weight:bold;\">\n<td rowspan=\"4\" width=\"80\">教育经历</td>\n<td width=\"160\">时间</td>\n<td width=\"80\">学历</td>\n<td width=\"180\">专业</td>\n<td>学校</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n</table>";
                    else
                    ui_content.Value = employee.Education.ToString();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string education = this.ui_content.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            string userId = WX.Request.rUserId;
            Employee.MODEL employee = Employee.GetModel("select * from [TU_Employees] where UserID='" + userId + "'"); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.Education.value = education;
            int iR = employee.Update();
            //5.（用户及业务对象）统计与状态
            //6.登记日志
            if (iR > 0)
            {
                WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(userId);
                WX.Model.Company.AddLogs(Convert.ToInt32(usermodel.CompanyID.ToString()), 6, usermodel.RealName.ToString() + "的档案修改了教育经历！" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);

                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "教育经历信息修改成功！", "User_Work.aspx?UserID=" + usermodel.UserID.ToString() + "&companyid=" + usermodel.CompanyID.ToString());
            
            }
        }
    }
}