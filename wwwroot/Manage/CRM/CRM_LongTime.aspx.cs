using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;

namespace wwwroot.Manage.CRM
{
    public partial class CRM_LongTime : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = WX.Authentication.GetUserID();
                InitCustomerRepeater(true);
            }
        }
        private void InitCustomerRepeater(bool start)
        {
            WX.Main.CurUser.LoadMyDepartment();

            string wherestr = " (vv.CustomerID is not null or(vv.CustomerID is null and datediff(day,UpTime,getdate()-1)>15)) and comp.State>0";
            DataTable dt =ULCode.QDA.XSql.GetDataTable("SELECT [Host] FROM [dbo].[TE_Departments] where Host='"+WX.Main.CurUser.UserID+"'");
            string ids = WX.Main.GetUserDeptids(WX.Main.CurUser.UserID);

            if (WX.Main.CurUser.UserID == WX.CommonUtils.GetBossUserID)
                wherestr += "";
            else if ((ids != "" ? "," + ids : "") !="")
                wherestr += " and tuuser.DepartmentID in(" + (ids != "" ? ids : "")  + ")";
            else
                wherestr += " and comp.EmployeeID='"+WX.Main.CurUser.UserID.ToString()+"'";
            string sql = "SELECT comp.ID,CustomerName,comp.EmployeeID,tuuser.RealName,tuuser.State,tuuser.DepartmentID,tedept.Name deptName,tedept.Host,teparentdept.Host ParentHost,UpTime,vv.* FROM [dbo].[CRM_Customers] comp left join view_CRM_Track vv on comp.ID=vv.CustomerID left join TU_Users tuuser on comp.EmployeeID=tuuser.UserID left join TE_Departments tedept on tuuser.DepartmentID=tedept.ID left join TE_Departments teparentdept on tedept.ParentID=teparentdept.ID where" + wherestr;
            
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            DataTable dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID desc", this.AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
           
            this.CustomerRepeater.DataSource = dataTable;
            this.CustomerRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
            if (Request["mes"] != null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%/Manage/CRM/CRM_LongTime.aspx?mes=1%'", WX.Main.CurUser.UserID));
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string userId = WX.Authentication.GetUserID();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitCustomerRepeater(false);
        }

    }
}