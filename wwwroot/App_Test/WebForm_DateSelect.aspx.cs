using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Test
{
    public partial class WebForm_TestDateSelect : System.Web.UI.Page
    {
        public string rYear
        {
            get { return ULCode.XWeb.Q("Year").ToStr(); }
        }
        public string rMonth
        {
            get { return ULCode.XWeb.Q("Month").ToStr(); }
        }
        public string rDay
        {
            get { return ULCode.XWeb.Q("Day").ToStr(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_Year(this.ddlYear, null, "0#--选择年份--", DateTime.Now.Year.ToString(), -5, 3);
                WX.Data.Dict.BindListCtrl_Month(this.ddlMonth, null, "0#--选择月份--", DateTime.Now.Month.ToString());
                WX.Data.Dict.BindListCtrl_Day(this.ddlDay, null, "0#--选择日期--", DateTime.Now.Day.ToString(), DateTime.Now.Year,DateTime.Now.Month);
            }
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string year = this.ddlYear.SelectedValue;
            WX.Data.Dict.BindListCtrl_Month(this.ddlMonth, null, "0#--选择月份--", "1");
            WX.Data.Dict.BindListCtrl_Day(this.ddlDay, null, "0#--选择日期--", "1", Convert.ToInt32(year), 1);
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string year = this.ddlYear.SelectedValue;
            string month = this.ddlMonth.SelectedValue;
            WX.Data.Dict.BindListCtrl_Day(this.ddlDay, null, "0#--选择日期--", "1", Convert.ToInt32(year), Convert.ToInt32(month));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(String.Format("{0}-{1}-{2}",this.ddlYear.SelectedValue,this.ddlMonth.SelectedValue,this.ddlDay.SelectedValue));
        }
    }
}