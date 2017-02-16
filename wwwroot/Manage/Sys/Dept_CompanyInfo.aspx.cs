using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using WX.Model;
using WX;

namespace wwwroot.Manage
{
    public partial class CompanyInfo : System.Web.UI.Page
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
                //1.用户验证                
                //2.是否为删除按钮
                if (Request["del"] != null)
                {
                    Button1.Visible = true;
                }
                //3.装载
                if (Request["CompanyID"] != null)
                {
                    if (!WX.Request.IsNumber("CompanyID", true)) return;
                    Company.MODEL company = Company.GetRequestedModel(); //Company.GetModel("select * from TE_Companys where ID=" + companyId);//Company.GetModel("SELECT TOP 1 * FROM TE_Companys WHERE ID=" + companyId);
                    this.liTitle.Text = company.Name.ToString();
                    //if (WX.Main.CurUser.UserID != company.Manage.ToString())
                    //{
                    //    Response.Write("你没有权限访问此功能！！");
                    //    Response.End();
                    //    return;
                    //}
                    //if (company.State.ToString() == "" || Convert.ToBoolean(company.State.ToString()) == false)
                    //{
                    //    Response.Write("须按流程审批后方可修改此信息！");
                    //    Response.End();
                    //    return;
                    //}
                    this.txtCompanyNO.Text = company.NO.ToString();
                    this.txtCompanyName.Text = company.Name.ToString();
                    this.txtManage.Value = company.Manage.ToString();
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
                    this.txtRoute.Text = company.Route.ToString();
                    this.txtIntroduction.Text = company.Introduction.ToString();
                    this.txtTelephone.Text = company.Tel.ToString();
                    this.txtFax.Text = company.Fax.ToString();

                    this.txtZip.Text = company.Zip.ToString();
                    this.txtSite.Text = company.Site.ToString();
                    this.txtEmail.Text = company.Email.ToString();
                    this.txtAddress.Text = company.Address.ToString();
                    this.txtBankname.Text = company.BankName.ToString();
                    this.txtAccount.Text = company.BankAccount.ToString();
                }
                else if (Request["ltype"] != null)
                {
                    MenuBar1.Param1 = "{Q:lid}";
                    this.MenuBar1.Key = "com";
                    this.MenuBar1.CurIndex = 2;
                    switch (Convert.ToInt32(Request["ltype"]))
                    {
                        case 2: this.liTitle.Text = "你正在添加母公司"; break;
                        case 3: this.liTitle.Text = "你正在添加子公司"; break;
                        case 4: this.liTitle.Text = "你正在添加控股公司"; break;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int companyId = WX.Request.rCompanyId;
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string no = this.txtCompanyNO.Text.Trim();
            string companyName = this.txtCompanyName.Text.Trim();
            string introduction = this.txtIntroduction.Text.Trim();
            string telephone = this.txtTelephone.Text.Trim();
            string fax = this.txtFax.Text.Trim();
            string zip = this.txtZip.Text.Trim();
            string site = this.txtSite.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string address = this.txtAddress.Text.Trim();
            string account = this.txtAccount.Text.Trim();
            //3.验证用户变量
            if (Request["CompanyID"] != null)
            {
                //4.处理业务
                Company.MODEL company = Company.GetRequestedModel(); //Company.GetModel("select * from TE_Companys where ID=" + companyId);//Company.GetModel("SELECT * FROM TE_Companys WHERE ID=" + companyId);
                this.liTitle.Text = company.Name.ToString();

                if (Request["del"] != null)
                {
                    WX.Main.ExecuteDelete("TE_Companys", "ID", companyId.ToString());
                    string logstr = (company.Linktype.ToString() == "0" ? "母" : (company.Linktype.ToString() == "2" ? "子" : "")) + "公司删除！-" + "(id-" + company.ID.ToString() + ",no-" + company.NO.ToString() + ") " + company.Name.ToString();
                    WX.Model.Company.AddLogs(Convert.ToInt32(company.ID.value), 2, logstr + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
                    //7.返回页面
                    ULCode.Debug.Alert(this, "删除成功！", "Dept_Company.aspx");
                    return;
                }
                company.NO.value = no;
                company.Name.value = companyName;
                company.Manage.value = txtManage.Value;
                company.Ctype.value = txtCtype.Text;
                company.Setuptime.value = txtSetuptime.Text;
                company.Operate.value = txtOperate.Text;
                company.Operatetime.value = txtOperatetime.Text;
                company.Route.value = txtRoute.Text;
                company.Introduction.value = introduction;
                company.Tel.value = telephone;
                company.Fax.value = fax;
                company.Zip.value = zip;
                company.Site.value = site;
                company.Email.value = email;
                company.Address.value = address;
                company.BankName.value = txtBankname.Text;
                company.BankAccount.value = account;
                company.Uptime.value = DateTime.Now;
                company.Ctype.value = txtCtype.Text;
                company.State.value = false;
                int row = company.Update();

                if (row != 0)
                {
                    //5.（用户及业务对象）统计与状态
                    //6.登记日志
                    string logstr = (company.Linktype.ToString() == "0" ? "母" : (company.Linktype.ToString() == "2" ? "子" : "")) + "公司信息修改成功！-" + "(id-" + company.ID.ToString() + ",no-" + company.NO.ToString() + ")" + company.Name.ToString();
                    WX.Model.Company.AddLogs(Convert.ToInt32(company.ID.value), 1, logstr + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
                    //7.返回页面
                    ULCode.Debug.Alert(this, "公司信息修改成功！", "Dept_CompanyDetail.aspx?CompanyID=" + companyId);
                }
                else
                {
                    company.RestoreInitial();
                    ULCode.Debug.Alert(this, "公司信息修改失败！");
                }
            }
            else if (Request["ltype"] != null)
            {
                Company.MODEL company = Company.NewDataModel();//Company.GetModel("SELECT * FROM TE_Companys WHERE ID=" + companyId);
                company.NO.value = no;
                company.Name.value = companyName;
                company.Manage.value = txtManage.Value;
                company.Ctype.value = txtCtype.Text;
                company.Setuptime.value = txtSetuptime.Text;
                company.Operate.value = txtOperate.Text;
                company.Operatetime.value = txtOperatetime.Text;
                company.Route.value = txtRoute.Text;
                company.Introduction.value = introduction;
                company.Tel.value = telephone;
                company.Fax.value = fax;
                company.Zip.value = zip;
                company.Site.value = site;
                company.Email.value = email;
                company.Address.value = address;
                company.BankName.value = txtBankname.Text;
                company.BankAccount.value = account;
                company.Uptime.value = DateTime.Now;
                company.Ctype.value = txtCtype.Text;
                company.LinkID.value = Request["lid"];
                company.Linktype.value = Request["ltype"];
                company.State.value = false;
                int id = company.Insert(true);

                if (id > 0)
                {
                    //5.（用户及业务对象）统计与状态
                    //6.登记日志
                    company.SaveIntoCaches();
                    string logstr = "添加一个" + (company.Linktype.ToString() == "0" ? "母" : "子") + "公司！-" + "(id-" + id.ToString() + ",no-" + company.NO.ToString() + ")" + company.Name.ToString();
                    WX.Model.Company.AddLogs(id, 0, logstr + "[" + ui_logcontent.Text + "]", WX.Main.CurUser.UserID, ui_logmanage.Value, Request.UserHostAddress);
                    //7.返回页面

                    ULCode.Debug.Alert(this, "添加成功！", "Dept_Company.aspx");
                }
                else
                {

                    ULCode.Debug.Alert(this, "公司信息添加失败！");
                }
            }
        }
    }
}