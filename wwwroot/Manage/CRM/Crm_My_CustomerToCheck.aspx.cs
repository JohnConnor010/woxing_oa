using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_My_CustomerToCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = WX.Authentication.GetUserID();
                this.InitCustomerRepeater(true);
            }
        }
        private void InitCustomerRepeater(bool start)
        {

            string sql = "SELECT C.CustomersID ID,C.CustomerID,C.CustomerName,tu.RealName CheckUser,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CS.SourceName,CB.LevelName,CStage.StageName,C.CustomersID,C.State CheckState,C.State,1 checktype,1 ContactID FROM CRM_CustomersTemp AS C "
                       + " left JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                       + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                       + " left join TU_Users tu on C.CheckUserId=tu.UserID"
                       + " Left join CRM_Source As CS On C.SourceId=CS.Id"
                       + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                       + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                       + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                       + " where C.EmployeeID='" + WX.Main.CurUser.UserID + "'"
                       + " union all"
                       + " SELECT cctemp.ID,C.CustomerID,C.CustomerName,tu.RealName CheckUser,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CS.SourceName,"
                        + " CB.LevelName,CStage.StageName,cctemp.CustomerID,cctemp.CheckState CheckState,cctemp.State,2 checktype,cctemp.ContactID FROM CRM_ContactTemp as cctemp"
                        + " left join CRM_Customers AS C  on cctemp.CustomerID=C.ID"
                        + " left join TU_Users tu on cctemp.CheckUserID=tu.UserID"
                        + " left JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID"
                        + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                        + " Left join CRM_Source As CS On C.SourceId=CS.Id"
                        + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                        + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                        + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                       + " where C.EmployeeID='" + WX.Main.CurUser.UserID + "' ORDER BY ID desc";

           System.Data.DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
           Gv_customer.DataSource = dataTable;
           Gv_customer.DataBind();
           if (Gv_customer.Rows.Count > 0)
           {
               Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
               Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
           }
        }

    }
}