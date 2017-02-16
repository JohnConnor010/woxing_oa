using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_Companyslicense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WX.Request.IsNumber("CompanyId", true)) return;
            if (!IsPostBack)
            {
               // this.liTitle.Text = WX.Model.Company.GetCache(WX.Q.rCompanyId).Name.ToString();
                pageinit();
                MenuBar1.CurIndex = Convert.ToInt32(Request["type"]) + 2;
            }
        }
        private void pageinit()
        {
            DataTable list;
            list = ULCode.QDA.XSql.GetDataTable("Select tcl.*,td.Name DeptName,te.RealName from [TE_Companys_license] tcl left join TE_Departments td on tcl.DepartentID=td.ID left join TU_Users te on tcl.Manage=te.UserID where tcl.CompanyId=" + WX.Request.rCompanyId + " and tcl.Type=" + Request["Type"] + " order by ID asc");
            DataList1.DataSource = list;
            DataList1.DataBind();
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                Response.Redirect("Dept_CompanyslicenseEdit.aspx?companyID=" + WX.Request.rCompanyId + "&id=" + e.CommandArgument + "&del=1");
                //ULCode.QDA.XSql.Execute("delete from [TE_Companys_license] where Id=" + e.CommandArgument);
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
                        returnstr += "<a href='Dept_AnnexDetail.aspx?id=" + id.ToString() + "&aid=" + i + "&companyID=" + WX.Request.rCompanyId + "'>查看附件" + (i + 1) + "：" + annexarry[i].Split('|')[0] + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";//<a href=\"javascript:setspan('Literal" + i + "');\">删除</a>
                    }
                }
            }
            return returnstr;
        }
    }
}