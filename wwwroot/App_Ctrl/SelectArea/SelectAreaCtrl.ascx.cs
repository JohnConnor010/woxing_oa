using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Ctrl.SelectArea
{
    public partial class SelectAreaCtrl : System.Web.UI.UserControl
    {
        public string ProvCode { get { return this.hidden_province.Value; } set { this.hidden_province.Value = value; } }
        public string CityCode { get { return this.hidden_city.Value; } set { this.hidden_city.Value = value; } }
        public string AreaCode { get { return this.hidden_area.Value; } set { this.hidden_area.Value = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.FillProvince();
                if (this.IsEmpty(this.ProvCode)) 
                {
                    this.ddlProvince.SelectedValue = "0";
                    this.ddlCity.Disabled = true;
                    this.ddlCity.Value = "0";
                    this.ddlArea.Disabled = true;
                    return; 
                }
                else
                {
                    this.ddlProvince.SelectedValue = this.ProvCode;
                    this.ddlCity.Disabled = false;
                }
                this.FillCity();
                if (this.IsEmpty(this.CityCode))
                {
                    this.ddlCity.Value = "0";
                    this.ddlArea.Disabled = true;
                    return;
                }
                else
                {
                    this.ddlCity.Value = this.CityCode;
                    this.ddlArea.Disabled = false;
                }

                this.FillArea();
                if (this.IsEmpty(this.AreaCode))
                {
                    this.ddlArea.Value = "0";
                    return;
                }
                else
                {
                    this.ddlArea.Value = this.AreaCode;
                }
            }
        }
        private bool IsEmpty(string code)
        {
            return String.IsNullOrEmpty(code) || Convert.ToInt32(code) == 0;
        }
        private void FillProvince()
        {
            var province = ULCode.QDA.XSql.GetDataTable("SELECT code,name FROM CRM_Province");
            this.ddlProvince.DataSource = province;
            this.ddlProvince.DataTextField = "name";
            this.ddlProvince.DataValueField = "code";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        private void FillCity()
        {
            var province = ULCode.QDA.XSql.GetDataTable("SELECT code,name FROM CRM_City Where ProvinceId='"+this.ProvCode+"'");
            this.ddlCity.DataSource = province;
            this.ddlCity.DataTextField = "name";
            this.ddlCity.DataValueField = "code";
            this.ddlCity.DataBind();
            this.ddlCity.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        private void FillArea()
        {
            var province = ULCode.QDA.XSql.GetDataTable("SELECT code,name FROM CRM_Area Where CityId='"+this.CityCode+"'");
            this.ddlArea.DataSource = province;
            this.ddlArea.DataTextField = "name";
            this.ddlArea.DataValueField = "code";
            this.ddlArea.DataBind();
            this.ddlArea.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
    }
}