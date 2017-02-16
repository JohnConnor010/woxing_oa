using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Dept_CompanyWebSite : System.Web.UI.Page
    {
        public int companyId = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["companyID"] != null && Request["companyID"] != "")
                companyId = Convert.ToInt32(Request["companyID"]);
            if (!IsPostBack)
            {
                gridviewBind();
            }
        }
        private void gridviewBind()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select tcw.*,tc.Name cmpname,te.RealName ManageName from TE_Companys_WebSite tcw left join TE_Companys tc on tcw.companyID=tc.ID left join TU_Users te on tcw.Manage=te.UserID where tcw.companyID=" + companyId);
            Gv_company.DataSource = dt;
            Gv_company.DataBind();
            if (Gv_company.Rows.Count > 0)
            {
                Gv_company.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_company.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void Gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (e.CommandName == "editc")
            {
                Response.Redirect("Dept_CompanyWebSiteEdit.aspx?companyID=" + companyId + "&id=" + id);
            }
        }
        public string getState(object oState, object oValid, object oWarn)
        {
            try
            {
                int state = Convert.ToInt32(oState);
                DateTime valid = Convert.ToDateTime(oValid);
                int warn = Convert.ToInt32(oWarn);
                switch (state)
                {
                    case 0: return "<span style='color:#888;'>已停用</span>";
                    case 1:
                        TimeSpan ts = valid - DateTime.Now;
                        if (ts.TotalDays <= warn)
                            return "<span style='color:Red;'>将续费</span>";
                        else
                            return "<span style='color:Green;'>使用中</span>";

                }
            }
            catch
            {
                return "<span style='color:Red;'>未知错误</span>";
            }
            return "<span style='color:Red;'>数据不全</span>";
        }
    }
}