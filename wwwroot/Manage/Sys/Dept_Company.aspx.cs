using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace wwwroot.Manage.Sys
{
    public partial class Dept_Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int defaultCompanyId = WX.Main.DefaultCompanyId;
                this.liAddMotherCompany.Visible = WX.Main.DefaultCompany.MotherCompany == null;
                this.liAddMotherCompany.NavigateUrl = String.Format("Dept_CompanyInfo.aspx?ltype={0}&lid={1}", Convert.ToInt32(WX.CompanyMode.MotherCompany), defaultCompanyId);
                this.liAddChildCompany.NavigateUrl = String.Format("Dept_CompanyInfo.aspx?ltype={0}&lid={1}", Convert.ToInt32(WX.CompanyMode.ChildCompany), defaultCompanyId);
                this.liAddHoldingCompany.NavigateUrl = String.Format("Dept_CompanyInfo.aspx?ltype={0}&lid={1}", Convert.ToInt32(WX.CompanyMode.HoldingCompany), defaultCompanyId);
                gridviewBind();
            }
        }
        private void gridviewBind()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from TE_Companys order by Linktype asc");
            Gv_company.DataSource = dt;

            Gv_company.DataBind();
            Gv_company.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_company.HeaderStyle.Height = Unit.Pixel(40);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*
            string str = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label l1=(Label)e.Row.FindControl("Label1");
                str = l1.Text.Trim();
                l1.Text = "";
                if (str.Split('|')[0] == "1")
                {
                    e.Row.Style.Value = "font-weight:bold;";
                    l1.Text = "";
                    if (ULCode.QDA.XSql.GetDataTable("select * from TE_Companys where LinkID=" + Gv_company.DataKeys[e.Row.RowIndex].Value + " and LinkType=2").Rows.Count <= 0)
                        l1.Text = "<a href='Dept_CompanyInfo.aspx?ltype=2&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>+母公司</a>";
                    l1.Text += "<a href='Dept_CompanyInfo.aspx?ltype=3&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>+子公司</a><a href='Dept_CompanyInfo.aspx?ltype=4&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>+控股公司</a>";
                    ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = false;
                }
                else if (str.Split('|')[0] == "2" || str.Split('|')[0] == "3" || str.Split('|')[0] == "4")
                {
                    //e.Row.Cells[2].Style.Value = "padding-left:20px;";
                }
            }*/
        }
        protected void Gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            //WX.Model.Company.MODEL model = WX.Model.Company.GetModel("select * from TE_Companys where ID="+id);
            if (e.CommandName == "del")
            {

                Response.Redirect("Dept_CompanyInfo.aspx?CompanyID=" + id + "&del=1");
                //if (WX.Main.ExecuteDelete("TE_Companys", "ID", id) > 0)
                //{
                //    bDeal = true;
                //}
            }
            else if (e.CommandName == "editc")
            {
                Response.Redirect("Dept_CompanyInfo.aspx?CompanyID=" + id);
            }
            //5.（用户及业务对象）统计与状态

           
        }
        public bool GetDelVisible(object oLinkType)
        {
            int linkType = Convert.ToInt32(oLinkType);
            return linkType != 1;
        }
    }
}