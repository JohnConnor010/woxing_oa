using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using ULCode.QDA;
using WX;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// Summary description for GetJsonOfAllDepartment
    /// </summary>
    
    public class GetJsonOfAllDepartment : IHttpHandler
    {
        private string companyId = "11";
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
            context.Response.ContentType = "text/plain";
            //1.获取用户变量
            int companyId = WX.Request.rCompanyId;
            //2.验证用变量
            DataTable data = XSql.GetDataTable("select * from view_Departments WHERE CompanyID=" + companyId+" order by NO asc");
            string json = GetJsonTree(data, 0);
            if (json.Length > 12)
            {
                json = json.Substring(12);
            }
            context.Response.Write(json);
        }
        public string GetJsonTree(DataTable data, int parentId)
        {
            StringBuilder json = new StringBuilder();
            string filterExpression = "ParentID=" + parentId;
            DataRow[] rows = data.Select(filterExpression);
            if (rows.Length < 1)
            {
                return "";
            }
            json.Append(",\"children\":[");
            foreach (DataRow row in rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", row["ID"].ToString());
                json.AppendFormat("\"deptid\":\"{0}\",", row["NO"].ToString());
                json.AppendFormat("\"name\":\"{0}\",", row["Name"].ToString());
                json.AppendFormat("\"telephone\":\"{0}\",", row["Tel"].ToString());
                json.AppendFormat("\"fax\":\"{0}\",", row["Fax"].ToString());
                json.AppendFormat("\"manager\":\"{0}\",", row["ManagerName"].ToString());
                json.AppendFormat("\"personcount\":\"{0}\",", row["PersonCount"].ToString());
                json.AppendFormat("\"edit\":\"{0}\",", "<a style='color:darkblue' href='Dept_EditDepartment.aspx?CompanyId=" + companyId + "&DepartmentID=" + id + "'>编辑</a>");
                json.AppendFormat("\"delete\":\"{0}\",", "<a style='color:darkblue' class='manage' href='javascript:Confirm()'>删除</a>");
                json.AppendFormat("\"addDept\":\"{0}\",", "<a style='color:darkblue' class='manage' href='Dept_AddDepartment.aspx?CompanyId=" + companyId + "&DepartmentID=" + id + "'>添加子部门</a>");
                json.Append("\"state\":\"open\"");
                json.Append(GetJsonTree(data, id).TrimEnd(','));
                json.Append("},");
            }
            if (json.ToString().EndsWith(","))
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
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