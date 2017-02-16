using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// GetCode_Login 的摘要说明
    /// </summary>
    public class GetCode : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/Jpeg";
            WX.Verify.DrawImage(5);
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