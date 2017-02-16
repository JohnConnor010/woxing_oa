using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wwwroot.App_Services
{
    /// <summary>
    /// DeleteFromDb 的摘要说明
    /// </summary>
    public class DeleteFromDb : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.QueryString.Count == 0)
            {
                context.Response.Write("-1");
                return;
            }
            string query = context.Request.QueryString.Keys[0].ToString().ToLower();
            string value = context.Request.QueryString[0];
            string sSql = null;
            switch (query)
            {
                case "fl_attach_id":
                    int runId = Convert.ToInt32(context.Request.QueryString["Run_Id"]);
                    sSql = String.Format("Select NewFileName from fl_runattachs where Id={0}", value);
                    WX.Main.DeleteFiles(sSql);
                    sSql = String.Format("Delete from fl_runattachs where Id={0};", value);
                    int iR = ULCode.QDA.XSql.Execute(sSql);
                    if (iR >= 0)
                    {
                        sSql = String.Format("Select Id,OldFileName from fl_runattachs where RunId={0} Order By Id", runId);
                        string idList = ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList(",", 0);
                        string nameList = ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList(",", 1);
                        sSql = String.Format("Update fl_run set Attach_IdList='{0}',Attach_NameList='{1}' where Id={2}"
                            , idList
                            , nameList
                            , runId);
                        ULCode.QDA.XSql.Execute(sSql);
                    }
                    context.Response.Write(iR);
                    return;
            }
            context.Response.Write("-2");
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