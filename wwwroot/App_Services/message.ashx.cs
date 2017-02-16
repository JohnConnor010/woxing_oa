using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// message 的摘要说明
    /// </summary>
    public class message : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //string sid=Guid.NewGuid().ToString();
            //ULCode.QDA.SqlErr.StartCapture(sid);
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Write("LOGIN_OUT");
                return;
            }
            if (ULCode.QDA.XSql.GetData("Select 1").ToInt32()!=1)
            {
                context.Response.Write("CN_ERROR");
                return;
            }            
            WX.Model.Message.MODEL model = WX.Model.Message.GetModel("select top 1 * from TM_Messages where State=0 and SendToUserId='" + WX.Authentication.GetUserID() + "' order by SendTime asc");
            //ULCode.QDA.SqlErr.StopCapture(sid);           

            if (model != null)
            {
                switch (Convert.ToString(model.Type.value))
                { 
                    case "1": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + "?id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条短消息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "2": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + "?id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条审核信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "3": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + "?id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条催办信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "4": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条提醒信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "5": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条公告信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "6": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条考核培训信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "7": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条面试信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "8": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条入职信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "9": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条转正信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "10": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条调岗信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "11": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条离职信息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    case "12": context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + (model.RedirectToUrl.ToString().IndexOf('?') > -1 ? "&" : "?") + "id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一流程审批，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                    default: context.Response.Write("<center><a onclick=addTab('消息内容','" + model.RedirectToUrl.ToString() + "?id=" + model.ID.value.ToString() + "','icon-data3') href='#'>您有一条短消息，请注意查收。</a><br/>" + model.SendTime.value.ToString() + "</center>"); break;
                }
            }
            else
            {
                context.Response.Write("NONE");//表示无效
            }
            // context.Response.Write("sdfsdf");
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