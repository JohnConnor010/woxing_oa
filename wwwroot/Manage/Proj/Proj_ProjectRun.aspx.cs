using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectRun : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
            }
        }
        private void InitComponent(bool start)
        {
            string state = "1";
            string sql = "SELECT ps.*,te.RealName ManageName,te2.RealName,pp.ProjectName FROM [PRO_State] ps left join TU_Users te on ps.Manage=te.UserID left join PRO_Projects pp on ps.ProjID=pp.ID left join TU_Users te2 on pp.UserID=te2.UserID where ps.State=";
            if (Request["state"] != null && Request["state"] == "2")
            {
                state= "2";
                MenuBar1.CurIndex = 4;
            }
            sql += state;
            var supplierData = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID desc", 20, AspNetPager1.CurrentPageIndex);

            this.SupplierRepeater.DataSource = supplierData;
            this.SupplierRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
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
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();
            WX.PRO.Project.MODEL model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + Id);
            int row = ULCode.QDA.XSql.Execute("DELETE FROM PRO_Projects WHERE ID=" + Id);
            ULCode.QDA.XSql.Execute("DELETE FROM PRO_Process WHERE ProjID=" + Id);
            if (row > 0)
            {
                WX.PRO.Log.AddLog(3, Convert.ToInt32(Id), model.ProjectName.ToString() + "-删除项目。", Request.UserHostAddress);
                InitComponent(false);
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
    }
}