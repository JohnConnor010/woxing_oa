using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Hand_Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                this.InitCustomerRepeater(true);
            }
        }
        private void BindData()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("Select UserID,td.Name+'--'+RealName RealName from Tu_Users tu left join TE_Departments td on tu.DepartmentID=td.ID where tu.DepartmentID>0 order by tu.DepartmentID asc");
            ddlUser.DataSource = dt;
            ddlUser.DataTextField = "RealName";
            ddlUser.DataValueField = "UserID";
            ddlUser.DataBind();
                
        }
        private void InitCustomerRepeater(bool start)
        {

            string sql = "SELECT C.ID,C.CustomerID,C.StageId,C.CustomerName,C.EmployeeID,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CS.SourceName,CB.LevelName,CStage.StageName,C.State,tu.RealName EmployeeName FROM CRM_Customers AS C "
                       + " INNER JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                       + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                       + " Left join CRM_Source As CS On C.SourceId=CS.Id"
                       + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                       + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                       + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                       + " Left Join TU_Users As tu On C.EmployeeID=tu.UserID"
                       + " where C.State>0";
            if (Request["UserID"] != null && Request["UserID"] != "")
                sql = sql + " and C.EmployeeID='"+Request["UserID"]+"'";
            System.Data.DataTable dataTable = WX.Main.GetPagedRows(sql, 0, " ORDER BY ID desc", 20, AspNetPager1.CurrentPageIndex);
            Gv_customer.DataSource = dataTable;
            Gv_customer.DataBind();
            if (Gv_customer.Rows.Count > 0)
            {
                Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
            }
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitCustomerRepeater(false);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string idlist = this.Request.Form["checksel"];
            string[] ids = idlist.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(ids[i]);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 7, "由（" + WX.CommonUtils.GetDeptNameListByDeptIdList(customer.DeptId.ToString()) + "--" + WX.CommonUtils.GetRealNameListByUserIdList(customer.EmployeeID.ToString()) + "）移交给（" + ddlUser.SelectedItem.Text + "）");
            }
            WX.Main.ExcuteUpdate("CRM_Customers", "EmployeeID='" + ddlUser.SelectedValue + "'", "ID in(" + idlist + ")");
            InitCustomerRepeater(true);
        }
    }
}