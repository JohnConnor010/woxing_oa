using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WX;

namespace wwwroot.Manage.Sys
{
    public partial class Func_List : System.Web.UI.Page
    {
        public int companyid = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridviewBind();
            }
        }
        public void gridviewBind()
        {
            try
            {
                companyid = int.Parse(Request["CompanyID"]);
            }
            catch
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            string wherestr = " where CompanyID="+companyid;
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select TE_Duties.*,TE_DutyCatagory.Name as DutyCatagoryName,TE_DutyCatagory.Sort as DutyCatagoryNo,HR_Grade.Name as GradeName from TE_Duties "
                                +" left join TE_DutyCatagory on TE_Duties.[DutyCatagoryID]=TE_DutyCatagory.ID "
                                + " left join HR_Grade on HR_Grade.Sort=TE_Duties.GradeID"
                                + wherestr + " order by DutyCatagoryNo desc,GradeID");
            Gv_duty.DataSource = dt;
            this.Gv_duty.RowDataBound += new GridViewRowEventHandler(WX.Ctrl.GridView_DynamicColor_RowDataBound);
            Gv_duty.DataBind();
            Gv_duty.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_duty.HeaderStyle.Height = Unit.Pixel(40);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_duty.PageIndex = e.NewPageIndex;
            gridviewBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            int companyid = 11;
            try
            {
                companyid = WX.Request.rCompanyId;
            }
            catch
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (e.CommandName == "del")
            {
                if (WX.Main.ExecuteDelete("TE_Duties", "CompanyID=" + companyid + " and ID", id) > 0)
                {
                    WX.Model.Duty.GetCache( Convert.ToInt32(id)).RemoveFromCaches();
                    WX.Main.ExecuteDelete("TE_FunctionsInDuties", "DutyID", id);
                    bDeal = true;
                }
            }
            else if (e.CommandName == "ledit")
            {
                Response.Redirect(String.Format("Duty_Edit.aspx?DutyId={0}&CompanyID={1}", id, companyid), true); return;
            }
            else if (e.CommandName == "buildMenu")
            {
                Response.Redirect(String.Format("Duty_BuildMenu.aspx?DutyId={0}&CompanyID={1}", id, companyid), true); return;
            }
            else if (e.CommandName == "dutyMenu")
            {
                Response.Redirect(String.Format("Duty_Menu.aspx?DutyId={0}&CompanyID={1}", id, companyid), true); return;
            }
            else if (e.CommandName == "dutyPriv")
            {
                Response.Redirect(String.Format("Duty_Func.aspx?DutyId={0}&CompanyID={1}", id, companyid), true); return;
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(LogType.Default, String.Format("删除职务({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
           
            gridviewBind();
        }
    }
}