using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Private
{
    public partial class Priv_CheckEmail : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mes = "load();";
            }
            //string code = "23423";
            //HiddenField1.Value = code;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            Random ro = new Random();
            string code = ro.Next(10000,99999).ToString(); 
            HiddenField1.Value = code;
            string bodystr = "欢迎使用我行信息有限公司OA办公管理系统，验证码为：<font color='red'>" + code + "</font><br>";
            if (WX.Main.SendEmail(ui_email.Text, "我行信息有限公司-邮箱验证！", bodystr))
                Response.Write("验证码已发送到您的邮箱请登录邮箱查看！");
            else
                Response.Write("验证码发送失败请重试！");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == HiddenField1.Value)
            {
                mes = "alert('邮箱验证成功！');close();";
            }
            else
            {
                Response.Write("验证码错误！");
            }
        }
    }
}