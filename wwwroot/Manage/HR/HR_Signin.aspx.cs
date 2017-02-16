using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            DateTime nowtime = DateTime.Now;
            WX.AT.Signin.MODEL model = WX.AT.Signin.GetModel("select * from [AT_Signin] where UserID='" + WX.Main.CurUser.UserID + "' and datediff(day,Addtime,getdate())=0");
            WX.AT.Status.MODEL status = WX.AT.Status.GetModel("select * from [AT_Status] where UserID='" + WX.Main.CurUser.UserID + "'");
            Button1.Enabled = model.Ontime.ToString() == "";
            Button2.Enabled = model.Offtime.ToString() == "";
            Literal2.Text = model.Ontime.ToString() == "" ? "<font color=\"red\">未签</font>" : model.Ontime.ToString()+"&nbsp;&nbsp;"+( status.State.ToString() == "" ? (nowtime < WX.AT.Signin.BelateStart ? "" : (nowtime < WX.AT.Signin.BelateStop ? WX.AT.Signin.statearray[9] : WX.AT.Signin.statearray[8])) : ((status.State.ToString()=="6"?model.Type.ToString()=="2"?"因私":"因公":"")+WX.AT.Signin.statearray[Convert.ToInt32(status.State.ToString())]));
            Literal3.Text = model.Offtime.ToString() == "" ? "<font color=\"red\">未签</font>" : model.Offtime.ToString() + WX.AT.Signin.statearray[Convert.ToInt32(status.NoonState.ToString())];
            Literal4.Text = model.Demo.ToString();
            Literal4.Text += (model.Starttime.ToString() != "" ? "<div>开始时间：" + Convert.ToDateTime(model.Starttime.value).ToString("yyyy-MM-dd HH:mm:ss") + "</div>" : "") + (model.Stoptime.ToString() == "" ? "" : "<div>截止时间：" + Convert.ToDateTime(model.Stoptime.value).ToString("yyyy-MM-dd HH:mm:ss") + "</div>");
            Literal4.Text += model.IsSet.ToString() == "1" ? "<div><b>注：超期按“请假”或“旷工”执行</b></div>" : "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime nowtime = DateTime.Now;
            WX.AT.Signin.MODEL model = getSigninModel();
            WX.AT.Status.MODEL status = getStatusModel();
           
            string content = "";
            //switch (status.State.ToString())
            //{
            //    case "1": content = "销假"; break;
            //    case "2": content = "销事假"; break;
            //    case "3": content = "销病假"; break;
            //    case "4": content = "销婚假"; break;
            //    case "5": content = "销产假"; break;
            //    case "6": content = "返回"; break;
            //    case "7": content = "返差"; break;
            //}
            //WX.AT.Signin.AddLogs(status.UserID.ToString(), (int)status.State.value, content);
            content = "上班签到";
            //if (status.State.ToString() == "")
            //{
                if (nowtime < WX.AT.Signin.BelateStart)
                {
                    status.State.value = 0;
                }
                else if (nowtime > WX.AT.Signin.BelateStart && nowtime <= WX.AT.Signin.BelateStop)
                {
                    status.State.value = 9;
                    TimeSpan ts = nowtime - WX.AT.Signin.BelateStart;
                    model.Belate.value = ts.TotalMinutes;
                    //日志
                    content = WX.AT.Signin.statearray[status.State.ToInt32()] + ts.Hours + "小时" + ts.Minutes + "分钟";
                }
                else
                {
                    status.State.value = 12;
                    model.Belate.value = 180;
                    //写旷工日志
                    content = WX.AT.Signin.statearray[status.State.ToInt32()];
                if (nowtime > WX.AT.Signin.NoonStart)
                {
                    if (nowtime <= WX.AT.Signin.NoonStop)
                    {
                        status.NoonState.value = 14;
                        TimeSpan ts = nowtime - WX.AT.Signin.NoonStart;
                        model.Belate.value = ts.TotalMinutes;
                        //日志
                        content = content + "&nbsp;&nbsp;" + WX.AT.Signin.statearray[status.State.ToInt32()] + ts.Hours + "小时" + ts.Minutes + "分钟";
                    }
                    else
                    {
                        status.NoonState.value = 13;
                        model.Belate.value = 360;
                        //写旷工日志
                        content = WX.AT.Signin.statearray[8];
                    }
                }
                }
            //}
            status.Uptime.value = DateTime.Now;     
            status.Update();
            model.Ontime.value = nowtime;
            model.Demo.value = content;
            model.Update();
            WX.AT.Signin.AddLogs(status.UserID.ToString(), (int)status.State.value, content);
            pageinit();
        }
        private WX.AT.Status.MODEL getStatusModel()
        {
            WX.AT.Status.MODEL status = WX.AT.Status.GetModel("select * from [AT_Status] where UserID='" + WX.Main.CurUser.UserID + "'");
            if (status == null||status.ID.ToString()=="")
            {
                status = WX.AT.Status.NewDataModel();
                status.UserID.value = WX.Main.CurUser.UserID;
                status.Insert();
            }
            return status;
        }
        private WX.AT.Signin.MODEL getSigninModel()
        {
            WX.AT.Signin.MODEL model = WX.AT.Signin.GetModel("select * from [AT_Signin] where UserID='" + WX.Main.CurUser.UserID + "' and datediff(day,Addtime,getdate())=0");

            if (model == null||model.ID.ToString()=="")
            {
                model = WX.AT.Signin.NewDataModel();
                model.UserID.value = WX.Main.CurUser.UserID;
                model.Addtime.value = DateTime.Now;
                model.Insert();
            }
            return model;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DateTime nowtime = DateTime.Now;
            WX.AT.Signin.MODEL model = getSigninModel();
            WX.AT.Status.MODEL status = getStatusModel();
            string content = "";
            if (nowtime < WX.AT.Signin.OffWork)
            {
                TimeSpan ts = WX.AT.Signin.OffWork - nowtime;
                model.Leaveearly.value = ts.TotalMinutes;
                //写早退日志
                if (nowtime > WX.AT.Signin.NoonStop||ts.TotalMinutes>WX.AT.Signin.KGMinutes)
                {
                    int state = 13;
                    status.NoonState.value = state;
                    content =status.State.ToInt32()==12? content = WX.AT.Signin.statearray[8]: WX.AT.Signin.statearray[status.NoonState.ToInt32()];
                }
                else
                {
                    status.NoonState.value = 10;
                    content =model.Demo.ToString()+"&nbsp;&nbsp;"+ WX.AT.Signin.statearray[status.NoonState.ToInt32()] + ts.Hours + "小时" + ts.Minutes + "分钟";
                }
            }
            status.Uptime.value = DateTime.Now;
            status.Update();
            model.Offtime.value = DateTime.Now;
            model.Demo.value =content;
            model.Update();
            WX.AT.Signin.AddLogs(status.UserID.ToString(), (int)status.State.value, content);
            pageinit();
        }
    }
}