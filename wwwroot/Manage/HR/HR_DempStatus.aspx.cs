using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_DempStatus : System.Web.UI.Page
    {
        DateTime nowtime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ui_ctime.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                pageinit();
            }
        }
        private void pageinit()
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            try
            {
                dt = Convert.ToDateTime(ui_ctime.Text.Trim());
                mes.InnerText = "";
            }
            catch
            {
                mes.InnerText = "日期格式错误！下列信息为" +dt.ToString("yyyy-MM-dd") + "数据！";
            }
            GridView1.DataSource = WX.AT.Status.GetDempList(Request["DempId"], dt);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.HeaderStyle.Height = Unit.Pixel(40);
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
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" : e.Row.Cells[2].Text.Trim();
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&nbsp;", "").Trim() == "" ? "<font color=\"red\">未签</font>" : e.Row.Cells[3].Text.Trim();
                str = ((Label)e.Row.FindControl("Label2")).Text.Trim();
                if (str.Split('|')[0] != "")
                {
                    cd = (Convert.ToInt32(str.Split('|')[0]) >= WX.AT.Signin.KGMinutes ? "上午旷工" : "迟到" + Convert.ToInt32(str.Split('|')[0]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[0]) % 60 + "分钟") + "<br/>";
                }
                if (str.Split('|')[1] != "")
                {
                    zt = Convert.ToInt32(str.Split('|')[1]) >= WX.AT.Signin.KGMinutes ? "下午矿工" : "早退" + Convert.ToInt32(str.Split('|')[1]) / 60 + "小时" + Convert.ToInt32(str.Split('|')[1]) % 60 + "分钟";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            pageinit();
        }
    }
}