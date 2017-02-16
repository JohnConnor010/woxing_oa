using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'PDT_ProductCategory','ID','Name','ParentID','ID',0,1,5");
                this.ddlProductCategory.DataSource = categoryData;
                this.ddlProductCategory.DataTextField = "name";
                this.ddlProductCategory.DataValueField = "id";
                this.ddlProductCategory.DataBind();
                this.ddlProductCategory.Items.Add(new ListItem("--全部--","0"));
                this.ddlProductCategory.SelectedValue = "0";
                WX.Data.Dict.BindListCtrl_DeptList(this.ProductDeptID, null, "0#--全部--", null);
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string sSql = "select pp.* from PDT_Products pp where 1=1";
            if (ProductDeptID.SelectedValue != "0")
            {
                sSql = "select pp.* from PDT_Products pp inner join PDT_ProductDept ppd on pp.ID=ppd.ProductID where ppd.DeptID=" + ProductDeptID.SelectedValue;
            }
            if (ddlProductCategory.SelectedValue != "0")
            {
                sSql += " and pp.CategoryID=" + ddlProductCategory.SelectedValue;
            }
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by ProductID desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
            if (WX.Main.GetConfigItem("Product_ISDept") == "0")
            {
                GridView1.Columns[6].Visible = false;
            }
        }
        public string GetDept(string id)
        {
            string bodystr = "";
            DataTable dt = XSql.GetDataTable("select ppd.id,td.Name from PDT_ProductDept ppd left join TE_Departments td on ppd.DeptID=td.ID where ProductID=" + id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bodystr += "<a href='AddProductDept.aspx?ProductID=" + id + "&ProductDeptID=" + dt.Rows[i]["id"] + "'>" + dt.Rows[i]["Name"] + "</a>&nbsp;&nbsp;";
            }
                bodystr += "<a href='AddProductDept.aspx?ProductID=" + id + "'>添加</a>&nbsp;&nbsp;";
                return bodystr;

        }//删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            string sSql = String.Format("update PDT_Products set IsEnable={0} where id={1}",lb.CommandArgument, id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
            //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, String.Format("产品{0}({1})成功！", lb.CommandArgument=="1"?"启用":"禁用", id), "");
            //7.返回处理结果或返回其它页面。
                this.BindData(false);
            }
            else
            {
                ULCode.Debug.Alert(this, "操作失败！");
            }
        }
       
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData(true);
        }
    }
}