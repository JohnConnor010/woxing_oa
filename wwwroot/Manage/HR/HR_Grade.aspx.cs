using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_Grade : System.Web.UI.Page
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
            if (!this.IsPostBack)
            {
            }
        }
        public string getdutyname(string sort)
        {
            return ULCode.QDA.XSql.GetXDataTable("select Name from TE_Duties where GradeID=" + sort).ToColValueList();
        }
    }
}