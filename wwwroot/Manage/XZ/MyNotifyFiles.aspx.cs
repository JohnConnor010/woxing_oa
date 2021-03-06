﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class MyNotifyFiles : System.Web.UI.Page
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
            WX.Main.CurUser.LoadMyDepartment(true);

            string sSql = "Select XZ_NotifyFiles.*,RealName CategoryName from XZ_NotifyFiles left join TU_Users on XZ_NotifyFiles.UserID=TU_Users.UserID left join TE_Departments dept on dept.ID=TU_Users.DepartmentID " +
"where ((XZ_NotifyFiles.Area=1 and Depms like '%" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "%') " +
"or (XZ_NotifyFiles.Area=1 and Depms is null) " +
"or (XZ_NotifyFiles.Area=2 and dept.ID= " + WX.Main.CurUser.UserModel.DepartmentID.ToString() + ") " +
"or (XZ_NotifyFiles.Area=3 and dept.ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + ") " +
"or (XZ_NotifyFiles.Area=5 and Users like '%" + WX.Main.CurUser.UserID + "%') " +
"or (XZ_NotifyFiles.Area=4 and dbo.get_oneid(dept.ID) = dbo.get_oneid(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "))" +
 ") and XZ_NotifyFiles.State=5";
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 15;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by PublishTime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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