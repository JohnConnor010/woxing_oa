using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WX.Model;
using Newtonsoft.Json;
using ULCode.QDA;
using System.Data;
using WX;

namespace wwwroot.App_Services
{
    /// <summary>
    /// Summary description for GetAllEmployee
    /// </summary>
    public class GetAllEmployee : IHttpHandler
    {
        private int PermissionNum = 0;
        private bool A_Read = false;
        public void ProcessRequest(HttpContext context)
        {
            this.PermissionNum = WX.Main.GetPermission(true);
            if (this.PermissionNum == -1)
            {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }
            this.A_Read = this.PermissionNum >= Convert.ToInt32(PermissionType.Read);

            //1.验证当前用户页面权限
            if (!A_Read)
            {
                context.Response.Write("您没有读取的权限");
                context.Response.End();
                return;
            }
            context.Response.ContentType = "json";
            //2.获取用户变量
            int companyId = 11;
            //3.验证用变量
            if (!int.TryParse(context.Request["companyId"], out companyId))
            {
                return;
            }
            int page = Convert.ToInt32(context.Request["page"]);
            int rows = Convert.ToInt32(context.Request["rows"]);
            string word = context.Request["word"];
            string key = context.Request["key"];
            string where = string.Empty;
            if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(key))
            {
                where = "AND " + key + " LIKE '%" + word + "%'";
            }
            //DataTable table1 = XSql.GetDataTable("SELECT * FROM vw_employees");
            //string cmdText = "SELECT TOP " + rows + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Sort asc) AS RowNumber,* FROM vw_employees WHERE CompanyID=" + companyId + " " + where + ") A WHERE RowNumber > " + rows * (page - 1);
            string cmdText = String.Format("Select * from vw_employees WHERE State>6 and State<40 and CompanyID={0}{1}", companyId, where);
            DataTable dataTable = WX.Main.GetPagedRows(cmdText, 0, "ORDER BY Sort asc", rows, page);
            int count = WX.Main.GetPagedRowsCount(cmdText);
            var employees = dataTable.AsEnumerable().Select(e => new
                {
                    UserID = e.Field<Guid>("UserID"),
                    CompanyID = e.Field<int>("CompanyID"),
                    DepartmentName = e.Field<string>("DepartmentName"),
                    DutyName = e.Field<string>("DutyName"),
                    Grade = e.Field<string>("GradeName") + "(" + e.Field<Nullable<int>>("Grade") + ")",
                    Sex = e.Field<Nullable<bool>>("Sex") == true ? "男" : "女",
                    IDCard = e.Field<string>("IDCard"),
                    RealName = e.Field<string>("RealName"),
                    Birthday = this.GetBirth(e.Field<Nullable<DateTime>>("Birthday")),
                    Mobile = e.Field<string>("Mobile"),
                    QQ = e.Field<string>("QQ"),
                    Email = e.Field<string>("Email"),
                    Telephone = e.Field<string>("Tel"),
                    Address = e.Field<string>("Address"),
                    UserFace = e.Field<string>("UserFace"),
                    Content = e.Field<string>("Introduction"),
                    State = e.Field<bool>("IsLockedOut") == true ? "0" : "1",
                    Lock = e.Field<bool>("IsLockedOut") == true ? "<a href=\"User_AccountState.aspx?UserId=" + e.Field<Guid>("UserID") + "\"><img src=\"../icon/lock.png\" /></a>" : "<a href=\"User_AccountState.aspx?UserId=" + e.Field<Guid>("UserID") + "\"><img src=\"../icon/user.png\" /></a>",
                    ReSetPwd = "【<a href=\"User_ResetPwd.aspx?UserId=" + e.Field<Guid>("UserID")+ "\">重置密码</a>】"
                   
                });
            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            string result = "{\"total\":" + count + ",\"rows\":" + json + "}";
            context.Response.Write(result);
        }
        public string GetBirth(object f)
        {
            if(f==null || f==Convert.DBNull)
                return String.Empty;
            else
                return String.Format("{0:yyyy年MM月dd日}",f);

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}