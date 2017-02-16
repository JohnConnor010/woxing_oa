using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Test
{
    public partial class WebForm_TestAreaSelect : System.Web.UI.Page
    {
        public string rProv
        {
            get { return ULCode.XWeb.Q("Prov").ToStr(); }
        }
        public string rCity
        {
            get { return ULCode.XWeb.Q("City").ToStr(); }
        }
        public string rArea
        {
            get { return ULCode.XWeb.Q("Area").ToStr(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_Province(this.ddlProv, null, "0#--选择省份--", rProv);
                if (!String.IsNullOrEmpty(rProv)) WX.Data.Dict.BindListCtrl_City(this.ddlCity, null, "0#--选择城市--", rCity, rProv);
                if (!String.IsNullOrEmpty(rCity)) WX.Data.Dict.BindListCtrl_Area(this.ddlArea, null, "0#--选择区县--", rArea, rCity);
            }
        }
        protected void ddlProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prov = this.ddlProv.SelectedValue;
            WX.Data.Dict.BindListCtrl_City(this.ddlCity, null, "0#--选择城市--", null, prov);
            WX.Data.Dict.BindListCtrl_Area(this.ddlArea, null, "0#--选择区县--", null, "0");
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = this.ddlCity.SelectedValue;
            WX.Data.Dict.BindListCtrl_Area(this.ddlArea, null, "0#--选择区县--", null, city);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(String.Format("{0}-{1}-{2}",this.ddlProv.SelectedValue,this.ddlCity.SelectedValue,this.ddlArea.SelectedValue));
        }
    }
}