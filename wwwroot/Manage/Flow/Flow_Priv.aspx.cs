using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;
using WX;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Priv : System.Web.UI.Page
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
                int Id = WX.Request.rFlowID;
                DataTable table = XSql.GetDataTable("SELECT * FROM Fl_FlowManage");
                var query = table.AsEnumerable().Where(f => f.Field<Int16>("FlowId") == Id).Select(f => new
                {
                    ManageId = f.Field<Int16>("Id"),
                    FlowId = f.Field<Int16>("FlowId"),
                    ManageType = GetManageTypeById(Convert.ToInt32(f.Field<Byte>("ManageType"))),
                    DelegateScope = GetDelegateScope(f.Field<string>("UserList"), f.Field<string>("DeptList"), f.Field<string>("DutyList")),
                    Scope = GetManageScope(f.Field<string>("Scope"))
                });
                this.Repeater1.DataSource = query;
                this.Repeater1.DataBind(); 

            }
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string manageId = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM Fl_FlowManage WHERE Id=" + manageId);
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "权限管理删除成功！",null);
                ULCode.Debug.Alert("管理权限删除成功！", "Flow_Priv.aspx?FlowID=" + WX.Request.rFlowID);
            }
        }
        //获取数据源需要重写
        public string GetManageTypeById(int id)
        {
            string type = string.Empty;
            switch (id)
            {
                case 1:
                    type = "管理";
                    break;
                case 2:
                    type = "监控";
                    break;
                case 3:
                    type = "查询";
                    break;
                case 4:
                    type = "编辑";
                    break;
                case 5:
                    type = "点评";
                    break;
            }
            return type;
        }
        public string GetManageScope(string manageScope)
        {
            string scope = string.Empty;
            switch (manageScope)
            {
                case "SELF_ORG":
                    scope = "本机构";
                    break;
                case "ALL_DEPT":
                    scope = "所有部门";
                    break;
                case "SELF_DEPT":
                    scope = "本部门及下属部门";
                    break;
                case "SELF_BRANCH":
                    scope = "本部门(不含下属部门)";
                    break;
                case "CUSTOM":
                    scope = "自定义部门";
                    break;
            }
            return scope;
        }
        public string GetDelegateScope(string userList, string deptList, string dutyList)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(userList))
            {
                builder.Append("<b>用户：</b>" + CommonUtils.GetRealNameListByUserIdList(userList));
            }
            if (!string.IsNullOrEmpty(deptList))
            {
                builder.Append("<br/><b>部门：</b>" + CommonUtils.GetDeptNameListByDeptIdList(deptList));
            }
            if (!string.IsNullOrEmpty(dutyList))
            {
                builder.Append("<br/><b>角色：</b>" + CommonUtils.GetDutyNameListByDutyIdList(dutyList));
            }
            return builder.ToString();
        }
        
    }
    
}