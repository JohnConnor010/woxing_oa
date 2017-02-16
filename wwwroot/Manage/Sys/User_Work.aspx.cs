using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.Sys
{
    public partial class User_Work : System.Web.UI.Page
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
                Employee.MODEL employee = Employee.GetModelToID(userId ); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                if (employee.LoadSucceed || true)
                {
                    if (employee.Work.ToString().Trim() == "")
                        ui_content.Value = "<table class='table3' style=\"text-align:center;line-height:200%;\" width=\"98%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\">\n<tr style=\"font-weight:bold;\">\n<td rowspan=\"4\" width=\"80\">工作经历</td>\n<td width=\"160\">日期</td>\n<td width=\"150\">职位</td>\n<td>公司</td>\n<td width=\"80\">工作时间</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n<tr>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n<td>&nbsp;</td>\n</tr>\n</table>";
                    else
                    ui_content.Value = employee.Work.ToString();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //2.取得用户变量
            string content = this.ui_content.Value;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            string userId = WX.Request.rUserId;
            Employee.MODEL employee = Employee.GetModelToID( userId ); //Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
            //Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + Request.QueryString["UserID"] + "'");
            employee.Work.value = content;
            int iR = employee.Update();
            //6.登记日志
            if (iR > 0)
            {
                WX.Model.User.MODEL usermodel = WX.Model.User.GetCache(userId);
                WX.Model.Company.AddLogs(Convert.ToInt32(usermodel.CompanyID.ToString()), 6, usermodel.RealName.ToString() + "的档案修改了工作经历！" + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);

                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert(this, "工作经历信息修改成功！", "User_Family.aspx?UserID=" + usermodel.UserID.ToString() + "&companyid=" + usermodel.CompanyID.ToString());
            
            }
        }
    }
}