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
    public partial class Dept_myCompany : System.Web.UI.Page
    {
        public int companyId = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Company.MODEL company = Company.GetCache(companyId);//Company.GetModel("SELECT TOP 1 * FROM TE_Companys WHERE ID=" + companyId);
                if (company != null)
                {
                    this.txtCompanyNO.Text = company.NO.ToString();
                    this.txtCompanyName.Text = company.Name.ToString();
                    this.li_Manage.Text = WX.CommonUtils.GetRealNameListByUserIdList(company.Manage.ToString());
                    this.txtCtype.Text = company.Ctype.ToString();
                    this.txtSetuptime.Text = company.Setuptime.f("{0:yyyy年MM月dd日}");
                    this.txtOperatetime.Text = company.Operatetime.f("{0:yyyy年MM月dd日}"); 
                    this.txtOperate.Text = company.Operate.ToString();
                    this.txtUptime.Text = company.Uptime.f("{0:yyyy年MM月dd日 HH:mm:ss}");
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
                    frdt = ULCode.QDA.XSql.GetDataTable("Select ID,Title from [TE_Companys_license] where CompanyId=" + companyId + " and Type=4 order by ID asc");
                    for (int i = 0; i < frdt.Rows.Count; i++)
                        this.txtqzsp.Text += "<a href='Dept_CompanyslicenseEdit.aspx?companyID=" + companyId + "&id=" + frdt.Rows[i]["ID"] + "'>" + frdt.Rows[i]["Title"] + "</a>&nbsp;&nbsp;";
                    frdt = ULCode.QDA.XSql.GetDataTable("Select top 8 ID,Type,content from [TE_Companys_Logs] where CompanyId=" + companyId + " and type=3 order by ID desc");
                    for (int i = 0; i < frdt.Rows.Count; i++)
                        this.txtgsbg.Text += frdt.Rows[i]["content"] + "&nbsp;&nbsp;&nbsp;";
                    this.txtgsbg.Text += "<b><a href='Dept_Companyslog.aspx?companyID=" + companyId + "&type=3'>>>更多</a></b>";
                    frdt = ULCode.QDA.XSql.GetDataTable("Select ID,Name from [TE_Companys] where LinkID=" + companyId + " and ID!=" + companyId + " order by Linktype asc");
                    for (int i = 0; i < frdt.Rows.Count; i++)
                        this.txtglcmp.Text += "<a href='Dept_CompanyDetail.aspx?id=" + frdt.Rows[i]["ID"] + "'>" + frdt.Rows[i]["Name"] + "</a>&nbsp;&nbsp;";
                    DataTable list = ULCode.QDA.XSql.GetDataTable("Select tcl.ID,tcl.Title,tcl.Valid,tcl.Validstop,tcl.CheckTime,tcl.CheckstopTime,tcl.IsCheck,tcl.CheckManage,tcl.CompanyId,te.RealName from [TE_Companys_license] tcl left join TU_Users te on tcl.Manage=te.UserID where tcl.CompanyId=" + companyId + " and tcl.Type in(1,4) order by ID asc");
                    DataList1.DataSource = list;
                    DataList1.DataBind();
                    this.txtbankname.Text = company.BankName.ToString();
                    this.txtbankaccount.Text = company.BankAccount.ToString();
                    this.txtZip.Text = company.Zip.ToString();
                    this.txtSite.Text = company.Site.ToString();
                    this.txtEmail.Text = company.Email.ToString();
                    this.txtAddress.Text = company.Address.ToString();
                    //this.txtAccount.Text = company.BankAccount.ToString();
                }
            }
        }
    }
}