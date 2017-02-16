using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class MyCheckFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            WX.Main.CurUser.LoadUserModel(true);
            string sSql = "Select XZ_NotifyFiles.*,RealName CategoryName from XZ_NotifyFiles left join TU_Users on XZ_NotifyFiles.UserID=TU_Users.UserID  left join TE_Departments dept on dept.ID=TU_Users.DepartmentID where XZ_NotifyFiles.FlowId in" +
                "(select distinct FlowId from FL_Process where Priv_UserList like '%" + WX.Main.CurUser.UserID + "%'	or Priv_DutyList like'%" + WX.Main.CurUser.UserModel.DutyId.ToString() + "%' or Priv_DeptList like'%" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "%'";
            sSql += " or (XZ_NotifyFiles.UserID='" + WX.Main.CurUser.UserID + "' and Auto_Type=1)";

            sSql += " or (Auto_Type=2 and TU_Users.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + " and dept.Host like '%" + WX.Main.CurUser.UserID + "%')";
            sSql += ") and XZ_NotifyFiles.State>1";
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by Istop desc, PublishTime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
    }
}