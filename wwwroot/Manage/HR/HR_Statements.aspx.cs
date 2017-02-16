using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class HR_Statements : System.Web.UI.Page
    {
        public string[] Day = new string[] { "日", "一", "二", "三", "四", "五", "六" };
        public int i = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ui_ctime.Text = DateTime.Now.ToString("yyyy-MM");
                pageinit();
            }
        }
        public void pageinit()
        {
            DateTime dtime = DateTime.Now;
            try
            {
                dtime = Convert.ToDateTime(ui_ctime.Text + "-01");
            }
            catch { }
            DataTable dt = WX.AT.Statement.GetList(dtime);
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            pageinit();
        }
        public string GetState(string state)
        {
            int statesign = -1;
            try
            {
                statesign = Convert.ToInt32(state);
            }
            catch
            {
            }
            if (statesign == 81)
                return WX.AT.Signin.statesign[12];
            else if(statesign==82)
                return WX.AT.Signin.statesign[13];
            else if (statesign > -1)
                return WX.AT.Signin.statesign[statesign];
            else
                return "";
        }
    }
}