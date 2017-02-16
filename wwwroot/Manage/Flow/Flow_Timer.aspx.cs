using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using ULCode.QDA;
using WX.Data;
using WX;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Timer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            if (!IsPostBack)
            {
                string cmdText = "SELECT * FROM Fl_FlowTimer WHERE FlowId=" + WX.Request.rFlowID;
                DataTable dataTable = XSql.GetDataTable(cmdText);
                var query = dataTable.AsEnumerable().Select(f => new
                    {
                        Id = f.Field<int>("Id"),
                        RemindType = Dict.GetItemTextByValue(Dict.GetListItems_enum_RemindType(),f.Field<Byte>("RemindType").ToString()),
                        UserList = CommonUtils.GetRealNameListByUserIdList(f.Field<string>("UserList").ToString()),
                        RemindTime = f.Field<DateTime>("RemindTime"),
                        LastTime = f.Field<string>("LastTime") == null ? "" : f.Field<DateTime>("LastTime").ToString()
                        
                    });
                this.TimerRepeater.DataSource = query;
                this.TimerRepeater.DataBind();
            }
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM Fl_FlowTimer WHERE Id=" + id);
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "定时设置删除成功！", null);
                ULCode.Debug.Alert("定时设置删除成功！", "Flow_Timer.aspx?FlowID=" + WX.Request.rFlowID);
            }
            else
            {
                ULCode.Debug.Alert("定时设置删除失败！", "Flow_Timer.aspx?FlowID=" + WX.Request.rFlowID);
            }
        }
    }
}