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
    public partial class Crm_SingleM_ShowCustomerBusinessUser : System.Web.UI.Page
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
            string ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);
            if (WX.CommonUtils.GetBossUserID != WX.Main.CurUser.UserID&& WX.Main.CurUser.MyDepartMent.ID.ToString() != System.Configuration.ConfigurationManager.AppSettings["Dept_CA"])
                sqlBuilder.Append(" and tu2.DepartmentID in(" + (ids != "" ? ids : WX.Main.CurUser.UserModel.DepartmentID.ToString()) + ")");
            string sql = "SELECT distinct C.EmployeeID,tu2.RealName EmployeeUser,tu2.Grade,(select count(ID) from CRM_Track where CustomerID in(select ID from CRM_Customers where EmployeeID=C.EmployeeID) and DATEDIFF(mm,TrackTime,GETDATE())=1) Lcount,(select count(ID) from CRM_Track where CustomerID in((select ID from CRM_Customers where EmployeeID=C.EmployeeID)) and DATEDIFF(mm,TrackTime,GETDATE())=0) Ncount FROM CRM_Customers AS C "
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
            DataTable dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY Grade desc", this.AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            var Customers = dataTable.AsEnumerable().Select(customer => new
            {
                EmployeeID = customer.Field<Guid>("EmployeeID"),
                Lcount = customer.Field<Nullable<int>>("Lcount"),
                Ncount = customer.Field<Nullable<int>>("Ncount"),
                EmployeeUser = customer.Field<string>("EmployeeUser")
            });
            this.CustomerRepeater.DataSource = Customers;
            this.CustomerRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
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