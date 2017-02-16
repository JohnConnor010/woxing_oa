using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wwwroot.App_Services
{
    /// <summary>
    /// get_item 的摘要说明
    /// </summary>
    public class get_item : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int formId = Convert.ToInt32(context.Request.QueryString["form_Id"]);
            //此处获取编号context.Request["FORM_ID"]的表单并反回列数;
            try
            {
                WX.Flow.Model.Form.MODEL model = WX.Flow.Model.Form.GetCache(formId); //WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + context.Request["FORM_ID"]);
                if (model != null && model.Items_FormFieldCollection != null)
                {
                    int max = 0;
                    int nowid = 0;
                    foreach (WX.Flow.FormField ff in model.Items_FormFieldCollection)
                    {
                        nowid = int.Parse(ff.Id.Split('_')[1]);
                        if (nowid > max)
                        {
                            max = nowid;
                        }
                    }
                    context.Response.Write(max + 1);
                    context.Response.End();
                    return;
                }
            }
            catch
            {
            }
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}