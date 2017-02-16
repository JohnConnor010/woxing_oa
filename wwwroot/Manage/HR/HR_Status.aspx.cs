using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_Status : System.Web.UI.Page
    {
        DateTime nowtime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, "0#全部", null);
                pageinit();
            }
        }
        private void pageinit()
        {
            int deptid = 0;
            try
            {
                deptid = Convert.ToInt32(this.ddlDepartment.SelectedValue);
            }
            catch
            {
            }
            GridView1.DataSource = WX.AT.Status.GetList(deptid);
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.HeaderStyle.Height = Unit.Pixel(40);
            GridView1.Columns[7].Visible = this.Master.A_Edit;
        }
        private string Getstate(string str, string userid,string type)
        {
            if (type != "" && str == "6")
                type = type == "1" ? "因公" : (type == "2" ? "因私" : "");
            else
                type = "";
            int state = -1;
            if (str == "")
            {
                if (nowtime > WX.AT.Signin.BelateStart)
                {
                    if (nowtime < WX.AT.Signin.BelateStop)
                    {
                        state = 9;
                        str = "<font color=\"red\">" + WX.AT.Signin.statearray[9] + "</font>";
                    }
                    else
                    {
                        state = 8;
                        str = "<font color=\"red\">" + WX.AT.Signin.statearray[8] + "</font>";
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(str) == 0 || Convert.ToInt32(str) == 6 || Convert.ToInt32(str) == 7 || Convert.ToInt32(str) == 11)
                    str = WX.AT.Signin.statearray[Convert.ToInt32(str)];
                else if (Convert.ToInt32(str) == 9 && nowtime > WX.AT.Signin.BelateStop)
                {
                     state = 8;
                        str = "<font color=\"red\">" + WX.AT.Signin.statearray[8] + "</font>";
                }
                else
                    str = "<font color=\"red\">" + WX.AT.Signin.statearray[Convert.ToInt32(str)] + "</font>";

            }
            if (state > -1)
            {
                WX.AT.Status.MODEL status = WX.AT.Status.GetModel("select * from [AT_Status] where UserID='" + userid + "'");
                status.State.value = state;
                status.Uptime.value = DateTime.Now;
                status.Update();
            }
                    str = type + str;
            return str;
        }
        string cd = "";
        string zt = "";
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string str = "", str2 = "" ;
            //cd = "";
            //zt = "";
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    str2 = ((Label)e.Row.FindControl("Label1")).Text.Trim();
            //    e.Row.Cells[2].Text = Getstate(str2.Split('|')[0], str2.Split('|')[1], str2.Split('|')[2]);
            //    string starttime=e.Row.Cells[3].Text.Replace("&nbsp;", "").Trim();
            //    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" : e.Row.Cells[3].Text.Trim();
            //    e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" : e.Row.Cells[4].Text.Trim();
            //    str = ((Label)e.Row.FindControl("Label2")).Text.Trim();
            //    if (str.Split('|')[0] != "")
            //    {
            //        cd = "<div>" + (Convert.ToInt32(str.Split('|')[0]) >= WX.AT.Signin.KGMinutes ? "上午旷工" : "迟到" + Convert.ToInt32(str.Split('|')[0]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[0]) % 60 + "分钟") + "</div>";
            //    }
            //    if (str.Split('|')[1] != "")
            //    {
            //        zt = "<div>" + (Convert.ToInt32(str.Split('|')[1]) >= WX.AT.Signin.KGMinutes ? "下午矿工" : "早退" + Convert.ToInt32(str.Split('|')[1]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[1]) % 60 + "分钟") + "</div>";
            //    }
                
            //    e.Row.Cells[5].Text = cd + zt;
            //    //if (str.Split('|')[2] != "")
            //    //{
            //    //    e.Row.Cells[5].Text += "<div>" + e.Row.Cells[2].Text + "时间" + Convert.ToDateTime(str.Split('|')[2]).ToString("MM-dd HH:mm") + "</div>";
            //    //}
            //    if (str.Split('|')[3].Trim() != "")
            //    {
            //        DateTime dtnow = DateTime.Now;
            //        if (str2.Split('|')[0] != "" && Convert.ToInt32(str2.Split('|')[0]) > 0 && Convert.ToInt32(str2.Split('|')[0]) < 8)
            //        {
            //            TimeSpan ts = Convert.ToDateTime(str.Split('|')[3]) - dtnow;
            //            e.Row.Cells[5].Text += "<div" + (dtnow < Convert.ToDateTime(str.Split('|')[3]) ? ">还有" : " style='color:red;'>超期") + (ts.Days > -1 ? ts.Days : -ts.Days) + "天" + (ts.Hours > -1 ? ts.Hours : -ts.Hours) + "小时" + (ts.Minutes > -1 ? ts.Minutes : -ts.Minutes) + "分</div>";
            //        }
            //        else if (starttime != "")
            //        {
            //            TimeSpan ts = Convert.ToDateTime(str.Split('|')[3]) - Convert.ToDateTime(dtnow.Year + "-" + starttime + ":00");
            //            e.Row.Cells[5].Text += "<div" + (dtnow < Convert.ToDateTime(str.Split('|')[3]) ? ">还有" : " style='color:red;'>超期") + (ts.Days > -1 ? ts.Days : -ts.Days) + "天" + (ts.Hours > -1 ? ts.Hours : -ts.Hours) + "小时" + (ts.Minutes > -1 ? ts.Minutes : -ts.Minutes) + "分</div>";
            //        }
            //    }
            //}

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            pageinit();
        }
    }
}