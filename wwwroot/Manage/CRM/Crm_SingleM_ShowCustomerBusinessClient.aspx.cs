using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_SingleM_ShowCustomerBusinessClient : System.Web.UI.Page
    {
        public string mes = "";
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
            WX.Main.CurUser.LoadDutyDetailUser();
            StringBuilder sqlBuilder = new StringBuilder();
            if (Request["StageID"] != null && Request["StageID"] != "")
                sqlBuilder.Append(" AND C.StageID" + (Request["StageID"] == "1" ? "<2" : "=" + Request["StageID"]));

                
              WX.Main.CurUser.LoadMyDepartment();
              if (WX.CommonUtils.GetBossUserID != WX.Main.CurUser.UserID && WX.Main.CurUser.MyDepartMent.ID.ToString() != System.Configuration.ConfigurationManager.AppSettings["Dept_CA"])
              {
                  string ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);
                  sqlBuilder.Append(" and tu2.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ")");
              }
            string sql = "SELECT C.ID,C.CustomerID,C.StageId,C.CustomerName,CA.CategoryName,CN.CompanyNature,CI.IndustryName,CS.SourceName,CB.LevelName,CStage.StageName,tu.RealName CheckUser,tu2.RealName EmployeeUser,(select max(TrackTime) from CRM_Track where CustomerID=C.ID) TrackTime FROM CRM_Customers AS C "
                       + " INNER JOIN CRM_InnerCategory AS CA ON C.CategoryID=CA.ID "
                       + " left JOIN CRM_CompanyNature AS CN ON C.NatureID=CN.ID"
                       + " Left join CRM_Source As CS On C.SourceId=CS.Id"
                       + " Left Join CRM_Industry As CI On C.IndustryID=CI.Id"
                       + " Left Join CRM_BusinessLevel As CB On C.BusinessLevel=CB.Id"
                       + " Left Join CRM_Stage As CStage On C.StageId=CStage.Id"
                       + " Left Join TU_Users As tu On C.CheckUserId=tu.UserID"
                       + " Left Join TU_Users As tu2 On C.EmployeeID=tu2.UserID"
                       + " where C.State>-1"
                       + sqlBuilder.ToString();
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            DataTable dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY TrackTime desc", this.AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            var Customers = dataTable.AsEnumerable().Select(customer => new
            {
                ID = customer.Field<Nullable<int>>("ID"),
                CustomerID = customer.Field<string>("CustomerID"),
                StageID = customer.Field<Nullable<int>>("StageID"),
                CustomerName = customer.Field<string>("CustomerName"),
                CustomerCategory = customer.Field<string>("CategoryName"),
                CompanyNature = customer.Field<string>("CompanyNature"),
                SourceName = customer.Field<string>("SourceName"),
                LevelName = customer.Field<string>("LevelName"),
                IndustryName = customer.Field<string>("IndustryName"),
                EmployeeUser = customer.Field<string>("EmployeeUser"),
                StageName = customer.Field<string>("StageName")
            });
            this.CustomerRepeater.DataSource = Customers;
            this.CustomerRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
        }
        public string GetTrackTime(string CustomerID)
        {
            DataTable dt =ULCode.QDA.XSql.GetDataTable("select top 1 TrackTime from CRM_Track where CustomerID="+CustomerID+" order by TrackTime desc");
            if (dt == null || dt.Rows.Count == 0)
                return "<span style='color:#888;'>无跟踪</span>";
            else
                return "<b>"+WX.Main.GetTimeEslapseStr(Convert.ToDateTime(dt.Rows[0][0]), "", "")+"</b>" ;
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string customerId = e.CommandArgument.ToString();
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(customerId);
            customer.State.value = -1;
            customer.UpTime.value = DateTime.Now;
            int row = customer.Update();
            WX.CRM.Customer.AddLog(customer.ID.ToInt32(), customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 8, "");
            if (row > 0)
            {
                mes = "window.alert('客户信息已成功废弃！');"; InitCustomerRepeater(false);
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string userId = WX.Authentication.GetUserID();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            ProcessPaged(userId);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.InitCustomerRepeater(true);
        }
        private void ProcessPaged(string userId)
        {
            InitCustomerRepeater(false);
        }
    }
}