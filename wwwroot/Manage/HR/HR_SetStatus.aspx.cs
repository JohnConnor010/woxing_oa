using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_SetStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                string userId = WX.Request.rUserId;
                WX.Model.User.MODEL usermodel = WX.Request.rUser;
                ui_name.Text = usermodel.RealName.ToString();
                WX.AT.Signin.MODEL signin = WX.AT.Signin.GetModel("select * from [AT_Signin] where UserID='" + userId + "' and  datediff(day,Addtime,getdate())=0");
                WX.AT.Status.MODEL status = WX.AT.Status.GetModel("select * from [AT_Status] where UserID='" + userId + "'");
                ui_demo.Text = signin.Demo.ToString();
                if (Convert.ToInt32(status.State.value) > 0 && Convert.ToInt32(status.State.value) <= 7)
                {
                    switch (status.State.ToString())
                    {
                        case "1": ui_state.Items.Add(new ListItem("销假", "0")); break;
                        case "3": ui_state.Items.Add(new ListItem("销假", "0")); break;
                        case "6": ui_state.Items.Add(new ListItem("返回", "0")); break;
                        case "7": ui_state.Items.Add(new ListItem("返差", "0")); break;
                        default: break;
                    }
                }
                else
                {
                    if (signin.Ontime.isEmpty)
                    {
                        if (DateTime.Now<WX.AT.Signin.BelateStart.AddMinutes(WX.AT.Signin.KGMinutes))
                        ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[9] + "签到", "9"));
                    }
                    else
                    {
                        if (DateTime.Now > WX.AT.Signin.OffWork.AddMinutes(-WX.AT.Signin.KGMinutes)&&DateTime.Now < WX.AT.Signin.OffWork)
                            ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[10] + "签到", "10"));
                    }
                    ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[6] + "签到", "6"));
                    ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[7] + "签到", "7"));
                    ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[2], "1"));
                    ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[3], "3"));
                    ui_state.Items.Add(new ListItem(WX.AT.Signin.statearray[8], "8"));
                }
                //ui_ctime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ui_stoptime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 08:30:00");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.AT.Status.MODEL status = WX.AT.Status.GetModel("select * from [AT_Status] where UserID='" + WX.Request.rUserId + "'");
            WX.AT.Signin.MODEL model = WX.AT.Signin.GetModel("select * from [AT_Signin] where UserID='" + WX.Request.rUserId + "' and datediff(day,Addtime,getdate())=0");
            status.Uptime.value = DateTime.Now;
            status.State.value = Convert.ToInt32(ui_state.SelectedValue);
            string content = "";
            content = ui_state.SelectedItem.Text;
            if (ui_state.SelectedValue != "0")
            {
                if (ui_state.SelectedValue == "10")
                {
                    model.Offtime.value = status.Uptime.value;
                    TimeSpan ts = WX.AT.Signin.OffWork - (DateTime)model.Offtime.value;
                    model.Leaveearly.value = ts.Hours * 60 + ts.Minutes;
                    content += "——早退" + ts.Hours + "小时" + ts.Minutes + "分钟";
                }
                else if (ui_state.SelectedValue == "9")
                {
                    TimeSpan ts = (DateTime)status.Uptime.value - WX.AT.Signin.BelateStart;
                    model.Belate.value = ts.Hours * 60 + ts.Minutes;
                    model.Ontime.value = status.Uptime.value;
                    content += "——迟到" + ts.Hours + "小时" + ts.Minutes + "分钟";
                }
                else if (ui_state.SelectedValue == "8")
                {
                    TimeSpan ts = (DateTime)status.Uptime.value - WX.AT.Signin.BelateStart;
                    if (model.Ontime.isEmpty)
                    {
                        model.Belate.value = ts.Hours * 60 + ts.Minutes;
                        model.Ontime.value = status.Uptime.value;
                        content += "——上午旷工";
                    }
                    else if (model.Offtime.isEmpty)
                    {
                        model.Leaveearly.value = WX.AT.Signin.KGMinutes + 10;
                        model.Offtime.value = status.Uptime.value;
                        content += "——下午旷工";
                    }
                }
                else
                {
                    if (ui_state.SelectedValue == "6")
                    {
                        model.Type.value = ui_type.SelectedValue;
                        content = (ui_type.SelectedValue=="1"?"因公":"因私") + content;
                    }
                    model.Ontime.value = model.Ontime.isEmpty ? status.Uptime.value : model.Ontime.value;
                    model.Starttime.value = status.Uptime.value;
                    model.Stoptime.value = Convert.ToDateTime(ui_stoptime.Text);
                    model.IsSet.value=ui_isset.Checked?1:0;
                    content += ui_demo.Text;
                }

            }
            else
            {
                //if (model.Ontime.isEmpty && (DateTime)status.Uptime.value > WX.AT.Signin.OnStop)
                //{
                    model.Ontime.value = status.Uptime.value;
                //}
            }
            if (ui_demo.Text.Trim()!="")
            model.Demo.value = ui_demo.Text;
            model.Update();
            status.Update();
            WX.AT.Signin.AddLogs(status.UserID.ToString(), (int)status.State.value, content);
            //日志
            Response.Redirect("HR_Status.aspx");
        }
    }
}