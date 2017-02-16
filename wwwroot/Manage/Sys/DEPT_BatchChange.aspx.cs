using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace wwwroot.Manage.Sys
{
    public partial class DEPT_BatchChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lbDelSel.Visible = this.Master.A_Del;
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlFrom, null, "0#--请选择部门--", null);
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlTo, null, "0#--请选择部门--", null); 
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string key = this.tbKeyWords.Text;
            string key_con = String.IsNullOrEmpty(key) ? String.Empty : String.Format(" and RealName like '%{0}%'",key);
            string deptId = this.ddlFrom.SelectedValue;
            string dept_con = String.Empty;
            if (deptId != "0")
            {
                string sSql = String.Format("exec sp_get_tree_table 'TE_Departments','ID','Name','ParentId','Sort',{0},1,5", deptId);
                
                string deptIdList = ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList();
                deptIdList = String.IsNullOrEmpty(deptIdList) ? deptId : deptIdList + "," + deptId;
                dept_con = String.Format(" and DepartmentId in ({0})", deptIdList);
            }
            string sql = "SELECT * FROM vw_Users WHERE CompanyID=" + Request["companyID"] + key_con + dept_con;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }

            //DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
            DataTable logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY Grade desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            //Response.Write(logData.Rows.Count);
            this.GridView1.DataSource = logData;
            this.GridView1.DataBind();
            this.AspNetPager1.AlwaysShow = true;
        }
        //批量转移过程
        protected void Transfer(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string idList = this.Request.Form["checksel"];
            if (String.IsNullOrEmpty(idList))
            {
                ULCode.Debug.Alert(this, String.Format("你没有选择要转移的人员！"));
                return;
            }
            //以下是程序开发者的任务
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (this.ddlFrom.SelectedValue == this.ddlTo.SelectedValue)
            {
                ULCode.Debug.Alert(this, String.Format("请设置在不同部门之间进行转移！"));
                return;
            }
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            string sSql = String.Format("Update TU_Users Set DepartmentId={1} where UserID in ('{0}')", idList.Replace(",", "','"), this.ddlTo.SelectedValue);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            bDeal = iR > 0;
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "批量转移用户到新部门信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                this.BindData(true);
                ULCode.Debug.Alert(this, "批量转移用户到新部门信息成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "批量转移用户到新部门信息失败！");
            }
        }
        //控件事件
        protected void selAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbSelAll = (CheckBox)sender;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)this.GridView1.Rows[i].FindControl("sel");
                cb.Checked = cbSelAll.Checked;
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void Query(object sender, EventArgs e)
        {
            this.BindData(true);
        }
        protected void QueryAll(object sender, EventArgs e)
        {
            this.tbKeyWords.Text = String.Empty;
            this.BindData(true);
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        protected void ddlFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindData(true);
        }
    }
}