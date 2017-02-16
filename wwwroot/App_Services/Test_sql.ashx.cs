using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AjaxPro;
namespace wwwroot.App_Services
{
    /// <summary>
    /// Test_sql 的摘要说明
    /// </summary>
    public class Test_sql : IHttpHandler
    {
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public void ProcessRequest(HttpContext context)
        {
            #region 测试Ajax访问Session
            //context.Response.ContentType = "text/plain";
            //context.Application["UserName"] = "yjy";
            //context.Response.Write(WX.Model.Company.Caches[0].Name.ToString());
            //try
            //{
            //    context.Response.Write(context.Session["UserName"]);
            //}
            //catch( Exception e)
            //{
            //    context.Response.Write(e.Message);
            //}
            #endregion
            if (context.Request["sql"] == null || context.Request["sql"] == "")
            {
                context.Response.Write("失败:SQL语句为空！");
            }
            else
            {
                try
                {
                    if (ULCode.QDA.XSql.Execute(context.Request["sql"]) > 0)
                    {
                        context.Response.Write("成功:SQL语句测试正常！");
                    }
                    else
                    {
                        context.Response.Write("提醒:SQL语句执行正常，但无数据返回！");
                    }
                }
                catch
                {
                    context.Response.Write("失败：SQL语句执行失败，请联系管理员！");
                }
            }
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