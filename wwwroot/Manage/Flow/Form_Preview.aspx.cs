using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Data;

namespace wwwroot.Manage.Flow
{
    public partial class Form_Preview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Flow.Model.Form.MODEL fm = WX.Request.rForm;
            if (fm != null)
            {
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
                    foreach (WX.Flow.FormField ff in fm.Items_FormFieldCollection)
                    {
                        ff.Value = Request[ff.Id] == null ? "" : Request[ff.Id];
                        ffc.Add(ff);
                    }
                    fm.Items_FormFieldCollection = ffc;
                    Literal1.Text = fm.GenerateHtmls(ffc, ffedit, ffhidden,"").Replace("-SYS_IP-", getIp());
                }
                else
                {
                    Literal1.Text = fm.GenerateHtmls(fm.Items_FormFieldCollection, ffedit, ffhidden,"").Replace("-SYS_IP-", getIp());
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(Literal1.Text + "---" + Request.Form["DATA_1"]);
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