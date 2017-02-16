using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanysPartner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!WX.Request.IsNumber("CompanyId", true)) return;
                //this.liTitle.Text = WX.Model.Company.GetCache(WX.Q.rCompanyId).Name.ToString();
                pageinit();
            }
        }
        private void pageinit()
        {
            DataTable list;
            string where = "";
            if (ui_type.SelectedValue != "")
                where = " and " + ui_type.SelectedValue + "=1";
            list = ULCode.QDA.XSql.GetDataTable("Select tcp.*,te.IDCard,te.Sex,tu.RealName from [TE_Companys_Partner] tcp left join TU_Users tu on tcp.EmployeeID=tu.UserID left join TU_Employees te on tcp.EmployeeID=te.UserID where tcp.State=0 and tcp.CompanyId=" + Request["CompanyId"] + where + " order by ID asc");
            DataList1.DataSource = list;
            DataList1.DataBind();
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                Response.Redirect("Dept_CompanysPartnerEdit.aspx?companyID= "+Request["CompanyId"]+"&del=1&id="+e.CommandArgument);
                //pageinit();
            }
        }
        public string GetUrl(object annex, object id)
        {
            string returnstr = "";
            if (annex != null)
            {
                string[] annexarry = annex.ToString().Split(',');
                for (int i = 0; i < annexarry.Length; i++)
                {
                    if (annexarry[i] != "" && annexarry[i].Split('|').Length > 1)
                    {
                        returnstr += "<a href='Dept_AnnexDetail.aspx?id=" + id.ToString() + "&aid=" + i + "&companyID=" + Request["companyID"] + "'>查看附件" + (i + 1) + "：" + annexarry[i].Split('|')[0] + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";//<a href=\"javascript:setspan('Literal" + i + "');\">删除</a>
                    }
                }
            }
            return returnstr;
        }
    }
}