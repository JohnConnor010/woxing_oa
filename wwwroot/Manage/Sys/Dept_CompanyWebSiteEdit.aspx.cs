using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanyWebSiteEdit : System.Web.UI.Page
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
                if (Request["id"] != null && Request["id"] != "")
                {
                    WX.Model.Company_WebSite.MODEL model = WX.Model.Company_WebSite.GetModel("select * from TE_Companys_WebSite where ID=" + Request["ID"]);
                    ui_name.Text = model.Name.ToString();
                    ui_IP.Text = model.IP.ToString();
                    ui_Url.Text = model.Url.ToString();
                    ui_RecordNo.Text = model.RecordNo.ToString();
                    ui_Valid.Text = Convert.ToDateTime(model.Valid.ToString()).ToString("yyyy-MM-dd");
                    ui_Feetime.Text = Convert.ToDateTime(model.Feetime.ToString()).ToString("yyyy-MM-dd");
                    ui_Warn.Text = model.Warn.ToString();
                    ui_Manage.Value = model.Manage.ToString();
                    li_Manage.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.Manage.ToString());
                    ui_state.SelectedValue = model.state.ToString();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.Company_WebSite.MODEL model = WX.Model.Company_WebSite.NewDataModel();
           
            if (Request["id"] != null&&Request["id"]!="")
            {
                model = WX.Model.Company_WebSite.GetModel("select * from TE_Companys_WebSite where ID="+Request["ID"]);
            }
            model.Name.value = ui_name.Text;
            model.IP.value = ui_IP.Text;
            model.Url.value = ui_Url.Text;
            model.RecordNo.value = ui_RecordNo.Text;
            model.Valid.value = ui_Valid.Text;
            model.Feetime.value = ui_Feetime.Text;
            model.Warn.value = ui_Warn.Text;
            model.Manage.value = ui_Manage.Value;
            string ss = "";
            if (Request["id"] != null && Request["id"] != "")
            {
                ss = "域名信息修改-" + (model.state.ToString() == ui_state.SelectedValue ? "" : (model.state.ToString() == "1" ? "启用-" : "暂停-")) + model.Url.ToString();
                model.state.value = ui_state.SelectedValue;
                model.Update();
            }
            else
            {
                ss = "域名添加-" + (model.state.ToString() == "1" ? "启用-" : "暂停-") + model.Url.ToString();
                model.state.value = ui_state.SelectedValue;
                model.CompanyID.value = Convert.ToInt32(Request["companyID"]);
                model.Insert();
            }
            WX.Model.Company.AddLogs(Convert.ToInt32(model.CompanyID.ToString()), 11, ss + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
            Response.Redirect("Dept_CompanyWebSite.aspx?companyID="+model.CompanyID.ToString());
        }
    }
}