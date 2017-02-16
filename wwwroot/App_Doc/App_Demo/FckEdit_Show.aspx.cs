using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Data;

namespace wwwroot.App_Demo
{
    public partial class FckEdit_Show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Flow.Model.Form.MODEL fm = null;
            if (!IsPostBack && Request["FORM_CONTENT"] != null && Request["FORM_CONTENT"] != "")
            {
                fm.Module.value = Request["FORM_CONTENT"].Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;","&");
                fm.Module_Short.value = fm.GetShortModule();
                fm.UpdateItems();
                Session["formmodel"] = fm;
            }
            else
            {
                fm = (WX.Flow.Model.Form.MODEL)Session["formmodel"];
            }
            WX.Flow.FormFieldCollection oldffc = fm.FetchItems();
            WX.Flow.FormFieldCollection ffedit = new WX.Flow.FormFieldCollection();
            //ffedit.Add(new WX.Flow.FormField("DATA_1", "item10"));
            //ffedit.Add(new WX.Flow.FormField("DATA_2", "item8"));
            //ffedit.Add(new WX.Flow.FormField("DATA_3", "item9"));
            WX.Flow.FormFieldCollection ffhidden = new WX.Flow.FormFieldCollection();
            //ffhidden.Add(new WX.Flow.FormField("DATA_1", "item7"));
            //ffhidden.Add(new WX.Flow.FormField("DATA_2", "item8"));
            //ffhidden.Add(new WX.Flow.FormField("DATA_3", "item9"));
            if (Request["sub_add"] != null && Request["sub_add"] != "")
            {
                WX.Flow.FormFieldCollection ffc = new WX.Flow.FormFieldCollection();
                foreach (WX.Flow.FormField ff in oldffc)
                {
                    ff.Value = Request[ff.Id]==null?"":Request[ff.Id];
                    ffc.Add(ff);
                }
                fm.Items_FormFieldCollection = ffc;
                Session["formmodel"] = fm;               
                Literal1.Text = fm.GenerateHtmls(ffc, ffedit, ffhidden,"").Replace("-SYS_IP-", getIp());
            }
            else
            {
                Literal1.Text = fm.GenerateHtmls(oldffc, ffedit, ffhidden,"").Replace("-SYS_IP-", getIp());
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(Literal1.Text + "---" + Request.Form["DATA_1"]);
        }
        private string GetSourt(string formstr)
        {
            string resolvestr = formstr;
            string[] regstr = new string[] {
                "<input(.*?)name=\"(.*?)\"(.*?)/>",//input表单
                "<textarea(.*?)name=\"(.*?)\"(.*?)></textarea>",//多行文本框
                "<select(.*?)name=\"(.*?)\"([\\s\\S]*?)</select>",//下拉列表
                "<img(.*?)name=\"(.*?)\"(.*?)/>",//图片
                "<button(.*?)name=\"(.*?)\"([\\s\\S]*?)</button>"//按钮
            };
            for (int i = 0; i < regstr.Length; i++)
            {
                resolvestr = Regex.Replace(resolvestr, regstr[i], "{$2}", RegexOptions.Compiled);
            }
            return resolvestr;
        }
        private string getIp()
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "202.96.134.133";
                }
                else
                    Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = Request.UserHostAddress;
            }
            return Ip;
        }
    }
}