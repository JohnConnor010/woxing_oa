using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_UserStatus : System.Web.UI.Page
    {
        DateTime nowtime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ui_ctime.Text = DateTime.Now.ToString("yyyy-MM");
                pageinit();
            }
        }
        private void pageinit()
        {
            DateTime dt = DateTime.Now;
            try
            {
                dt = Convert.ToDateTime(ui_ctime.Text.Trim() + "-01");
                mes.InnerText = "";
            }
            catch
            {
                mes.InnerText = "日期格式错误！下列信息为" + DateTime.Now.ToString("yyyy-MM")+"数据！";
            }
            GridView1.DataSource = WX.AT.Status.GetUserList(Request["UserId"],dt);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.HeaderStyle.Height = Unit.Pixel(40);
            }
            GridView2.DataSource = WX.AT.Status.GetUserLogsList(Request["UserId"], dt);
            GridView2.DataBind(); 
            if (GridView1.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        private string Getstate(string str)
        {
            if (str == "")
            {
                if (nowtime > WX.AT.Signin.BelateStart)
                {
                    if (nowtime < WX.AT.Signin.BelateStop)
                    {
                        str = "<font color=\"red\">" + WX.AT.Signin.statearray[9] + "</font>";
                    }
                    else
                    {
                        str = "<font color=\"red\">" + WX.AT.Signin.statearray[8] + "</font>";
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(str) == 0)
                {
                    str = WX.AT.Signin.statearray[Convert.ToInt32(str)];
                }
                else
                {
                    str = "<font color=\"red\">" + WX.AT.Signin.statearray[Convert.ToInt32(str)] + "</font>";
                }
            }           
            return str;
        }
        string cd = "";
        string zt = "";
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string str = "";
            cd = "";
            zt = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = Getstate(e.Row.Cells[1].Text.Replace("&nbsp;", "").Trim());
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" :"";
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" :"";
                str = ((Label)e.Row.FindControl("Label2")).Text.Trim();
                if (str.Split('|')[0] != "")
                {
                    cd = (Convert.ToInt32(str.Split('|')[0]) >= WX.AT.Signin.KGMinutes ? "上午旷工" : "迟到" + Convert.ToInt32(str.Split('|')[0]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[0]) % 60 + "分") + "<br/>";
                }
                if (str.Split('|')[1] != "")
                {
                    zt = Convert.ToInt32(str.Split('|')[1]) >= WX.AT.Signin.KGMinutes ? "下午矿工" : "早退" + Convert.ToInt32(str.Split('|')[1]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[1]) % 60 + "分";
                }
                e.Row.Cells[4].Text = cd + zt;
                if (str.Split('|')[2] != "")
                {
                    e.Row.Cells[4].Text += "<div>" + e.Row.Cells[2].Text + "时间" + Convert.ToDateTime(str.Split('|')[2]).ToString("MM-dd HH:mm") + "</div>";
                }
                if (str.Split('|')[3] != "")
                {
                    e.Row.Cells[4].Text += "<div>截止时间" + Convert.ToDateTime(str.Split('|')[3]).ToString("MM-dd HH:mm") + "</div>";
                }
            }

        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Getstate(e.Row.Cells[0].Text.Replace("&nbsp;", "").Trim());
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            pageinit();
        }
    }
}