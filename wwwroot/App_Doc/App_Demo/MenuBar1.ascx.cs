using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace wwwroot.Manage.include
{
    public partial class MenuBar1 : System.Web.UI.UserControl
    {
        #region //参数
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        private int _curIndex;
        public int CurIndex
        {
            get { return _curIndex; }
            set { _curIndex = value; }
        }
        private string _param1 = null;
        public string Param1
        {
            get { return _param1; }
            set { _param1 = value; }
        }
        private string _param2 = null;
        public string Param2
        {
            get { return _param2; }
            set { _param2 = value; }
        }
        private string _param3 = null;
        public string Param3
        {
            get { return _param3; }
            set { _param3 = value; }
        }
        #endregion
        private static string SelCss = "Sel";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String[] menus = null;
                switch (this.Key.ToLower())
                {
                    case "base":
                        menus = new String[] 
                        { 
                            "部门管理","/Manage/sys/Dept_DepartmentList.aspx?companyId=11"
                           ,"职务管理","/Manage/Sys/Duty_List.aspx?CompanyID=11"
                           ,"公司信息","/Manage/Sys/Dept_CompanyInfo.aspx?id=11"
                        };
                        break;
                    case "dept":
                        menus = new String[] 
                        { 
                            "部门管理","/Manage/sys/Dept_DepartmentList.aspx?CompanyID=11"
                           ,"修改部门","/Manage/Sys/Dept_EditDepartment.aspx?companyID={0}&id={1}"
                        };
                        break;
                    case "user":
                        menus = new String[] 
                        { 
                            "用户列表","/Manage/sys/User_UserList.aspx?CompanyID=11"
                           ,"新增用户","/Manage/sys/User_AddUser.aspx"
                        };
                        break;
                    case "func":
                        menus = new String[] 
                        { 
                           "功能列表","/Manage/Sys/Func_ListFunctions.aspx"
                           ,"新增功能","/Manage/Sys/Func_AddFunctions.aspx"
                        };
                        break;

                }
                if (menus==null) return;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < menus.Length; i += 2)
                {
                    string title = menus[i];
                    string url = menus[i + 1];
                    url = String.Format(url, g(this.Param1), g(this.Param2), g(this.Param3));
                    title = String.Format(title, g(this.Param1), g(this.Param2), g(this.Param3));
                    int index = i / 2 + 1;
                    if(i>0) sb.Append(" ");
                    if (index == this.CurIndex)
                    {
                        sb.AppendFormat("<a href=\"{0}\" class=\"{2}\">{1}</a>",url, title, SelCss);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", url, title);
                    }
                }
                this.Literal1.Text = sb.ToString();
            }
        }
        private string g(string v)
        {
            if (String.IsNullOrEmpty(v)) return String.Empty;
            if (v.Contains("{") && v.Contains("}"))
            {
                return new ULCode.UniqueCode(v).Value;
            }
            else
                return v;
        }
    }
}