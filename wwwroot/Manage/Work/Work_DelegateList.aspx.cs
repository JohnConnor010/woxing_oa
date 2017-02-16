using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
using WX.Model;
using System.Data;
using ULCode.QDA;
using WX.Data;

namespace wwwroot.Manage.Work
{
    public partial class Work_DelegateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string delegateStatus = DropDownList1.SelectedItem.Value;
                BindDelegateRepeater(delegateStatus);
            }
        }
        private void BindDelegateRepeater(string delegateStatus)
        {
            WXUser curUser = new WXUser();
            string userId = curUser.UserID;
            switch (delegateStatus)
            {
                case "0":
                    string cmdText1 = "SELECT * FROM Fl_FlowAuthorization where FromUserId='" + userId + "'";
                    DataTable dataTable1 = XSql.GetDataTable(cmdText1);
                    var query1 = dataTable1.AsEnumerable().Select(fa => new
                        {
                            Id = fa.Field<Int32>("Id"),
                            FlowName = GetFlowName(fa.Field<Int16>("FlowId")),
                            Principal = CommonUtils.GetRealNameListByUserIdList(fa.Field<Guid>("FromUserId").ToString()),
                            BeThePrincipal = CommonUtils.GetRealNameListByUserIdList(fa.Field<Guid>("ToUserId").ToString()),
                            StartTime = fa.Field<Nullable<DateTime>>("BeginDate") == null? "" : fa.Field<DateTime>("BeginDate").ToString(),
                            EndTime = fa.Field<Nullable<DateTime>>("EndDate") == null? "" : fa.Field<DateTime>("EndDate").ToString(),
                            ImageUrl = GetPictureUrl(fa.Field<bool>("Status"))
                        });
                    this.DelegateRepeater.DataSource = query1;
                    this.DelegateRepeater.DataBind();
                    break;
                case "1":
                    string cmdText2 = "SELECT * FROM Fl_FlowAuthorization where ToUserId='" + userId + "'";
                    DataTable dataTable2 = XSql.GetDataTable(cmdText2);
                    var query2 = dataTable2.AsEnumerable().Select(fa => new
                    {
                        Id = fa.Field<Int32>("Id"),
                        FlowName = GetFlowName(fa.Field<Int16>("FlowId")),
                        Principal = CommonUtils.GetRealNameListByUserIdList(fa.Field<Guid>("FromUserId").ToString()),
                        BeThePrincipal = CommonUtils.GetRealNameListByUserIdList(fa.Field<Guid>("ToUserId").ToString()),
                        StartTime = fa.Field<Nullable<DateTime>>("BeginDate") == null ? "" : fa.Field<DateTime>("BeginDate").ToString(),
                        EndTime = fa.Field<Nullable<DateTime>>("EndDate") == null ? "" : fa.Field<DateTime>("EndDate").ToString(),
                        ImageUrl = GetPictureUrl(fa.Field<bool>("Status"))
                    });
                    this.DelegateRepeater.DataSource = query2;
                    this.DelegateRepeater.DataBind();
                    break;
            }
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM Fl_FlowAuthorization WHERE Id=" + id);
            if (row > 0)
            {
                WX.Main.AddLog(LogType.Default, "工作委托删除成功！", null);
                ULCode.Debug.Alert("工作委托删除成功！", "Work_DelegateList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("工作委托删除成功！", "Work_DelegateList.aspx");
            }
        }
        private string GetPictureUrl(bool status)
        {
            string src = string.Empty;
            switch (status)
            {
                case true:
                    src = "<img src=\"../images/ico_right.png\" />";
                    break;
                case false:
                    src = "<img src=\"../images/ico_wrong.png\" />";
                    break;
            }
            return src;
        }
        private string GetFlowName(int flowId)
        {
            string name = string.Empty;
            if (flowId == 0)
            {
                return "全部流程";
            }
            else
            {
                return(string)XSql.GetValue("SELECT Name FROM Fl_Flows Where Id=" + flowId);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string delegateStatus = DropDownList1.SelectedItem.Value;
            BindDelegateRepeater(delegateStatus);
        }
    }
}