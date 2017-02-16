using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_Project : System.Web.UI.Page
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
            string sql = "SELECT * FROM [PRO_Projects] where UserID='"+WX.Main.CurUser.UserID+"'";
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
            if (e.CommandName == "del")
            {
                int row = ULCode.QDA.XSql.Execute("DELETE FROM PRO_Projects WHERE ID=" + Id);
                ULCode.QDA.XSql.Execute("DELETE FROM PRO_Process WHERE ProjID=" + Id);
                if (row > 0)
                {
                    WX.PRO.Log.AddLog(3, Convert.ToInt32(Id), model.ProjectName.ToString() + "-删除项目。", Request.UserHostAddress);
                }
            }
            else if (e.CommandName == "qidong")
            {
                model.State.value = 4;
                model.Update();
                WX.PRO.State.MODEL statemodel = WX.PRO.State.GetModel("select * from PRO_State where ProjID="+model.ID.ToString());
                statemodel.State.value = 1;
                statemodel.ProcID.value = 1;
                statemodel.Starttime.value = DateTime.Now;
                statemodel.Update();
                WX.PRO.Process.SetTime(Convert.ToDateTime(statemodel.Starttime.value), Convert.ToInt32(statemodel.ProjID.value),1);
                WX.PRO.Log.AddLog(2, Convert.ToInt32(Id), model.ProjectName.ToString() + "-启动。", Request.UserHostAddress);
                WX.PRO.Log.AddLog(5, Convert.ToInt32(Id), model.ProjectName.ToString() + "-第1步开始。", Request.UserHostAddress);
            }
            InitComponent(false);
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
    }
}