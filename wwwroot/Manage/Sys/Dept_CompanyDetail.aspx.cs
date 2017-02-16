using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using WX;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanyDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!WX.Request.IsNumber("CompanyID", true)) return;
                int companyId = WX.Request.rCompanyId;
                Company.MODEL company =Company.GetRequestedModel();//Company.GetModel("SELECT TOP 1 * FROM TE_Companys WHERE ID=" + companyId);
                if (company != null)
                {
                   // this.liTitle.Text = company.Name.ToString();
                    this.txtCompanyNO.Text = company.NO.ToString();
                    this.txtCompanyName.Text = company.Name.ToString();
                    this.li_Manage.Text = WX.CommonUtils.GetRealNameListByUserIdList(company.Manage.ToString());
                    this.txtCtype.Text = company.Ctype.ToString();
                    try
                    {
                        this.txtSetuptime.Text = Convert.ToDateTime(company.Setuptime.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch { }
                    try { this.txtOperatetime.Text = Convert.ToDateTime(company.Operatetime.ToString()).ToString("yyyy-MM-dd"); }
                    catch { }
                    this.txtOperate.Text = company.Operate.ToString();
                    this.txtUptime.Text = Convert.ToDateTime(company.Uptime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    this.txtRoute.Text = company.Route.ToString().Replace("\n", "<br/>");
                    this.txtIntroduction.Text = company.Introduction.ToString();
                    this.txtTelephone.Text = company.Tel.ToString();
                    this.txtFax.Text = company.Fax.ToString();//Dept_CompanysPartnerEdit.aspx?companyID=11&id=14
                    DataTable frdt = ULCode.QDA.XSql.GetDataTable("select tcp.ID,te.RealName from TE_Companys_Partner tcp left join TU_Users te on tcp.EmployeeID=te.UserID where tcp.CompanyId=" + companyId + " and Legal=1 order by ID asc");
                    for (int i = 0; i < frdt.Rows.Count; i++)
                        this.txtFRManage.Text += "<a href='Dept_CompanysPartnerEdit.aspx?companyID=" + companyId + "&id=" + frdt.Rows[i]["ID"] + "'>" + frdt.Rows[i]["RealName"] + "</a>&nbsp;&nbsp;";
                    frdt = ULCode.QDA.XSql.GetDataTable("select tcp.ID,te.RealName from TE_Companys_Partner tcp left join TU_Users te on tcp.EmployeeID=te.UserID where tcp.CompanyId=" + companyId + " and Directors=1 order by ID asc");
                    for (int i = 0; i < frdt.Rows.Count; i++)
                        this.txtDSHList.Text += "<a href='Dept_CompanysPartnerEdit.aspx?companyID=" + companyId + "&id=" + frdt.Rows[i]["ID"] + "'>" + frdt.Rows[i]["RealName"] + "</a>&nbsp;&nbsp;";
                    DataTable list = ULCode.QDA.XSql.GetDataTable("Select tcl.ID,tcl.Title,tcl.Valid,tcl.Validstop,tcl.CheckTime,tcl.CheckstopTime,tcl.IsCheck,tcl.CheckManage,tcl.CompanyId,te.RealName from [TE_Companys_license] tcl left join TU_Users te on tcl.Manage=te.UserID where tcl.CompanyId=" + companyId + " and tcl.Type in(1,4) order by ID asc");
                    DataList1.DataSource = list;
                    DataList1.DataBind();
                    this.txtbankname.Text = company.BankName.ToString();
                    this.txtbankaccount.Text = company.BankAccount.ToString();
                    this.txtZip.Text = company.Zip.ToString();
                    this.txtSite.Text = company.Site.ToString();
                    this.txtEmail.Text = company.Email.ToString();
                    this.txtAddress.Text = company.Address.ToString();
                    this.txtAccount.Text = company.BankAccount.ToString();
                }
            }
        }
    }
}