using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CTR
{
    public partial class AddProductDept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            if (WX.Main.GetConfigItem("Product_ISDept") == "1")
            {
                WX.PDT.Product.MODEL product = WX.Request.rProduct;
                liproductName.Text = product.ProductName.ToString();
                BindDropList();
                divdept.Visible = true;
                PageInit();
                if (WX.Main.GetConfigItem("Product_OneDept") == "0")
                {
                    GridView1.Visible = true;
                }
            }
            string sSql = "select pp.*,td.Name DeptName from PDT_ProductDept pp left join TE_Departments td on pp.DeptID=td.ID where pp.ProductID=" + WX.Request.rProductId;
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by ID asc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
            if (Request["ProductDeptID"] != null && Request["ProductDeptID"] != "")
                btnSave.Text = "编辑";
        }
        private void PageInit()
        {
            WX.PDT.ProductDept.MODEL productdept = null;
            if (Request["ProductDeptID"] != null && Request["ProductDeptID"] != "")
                productdept = WX.PDT.ProductDept.NewDataModel(Request["ProductDeptID"]);
            else if (WX.Main.GetConfigItem("Product_ISDept") == "1" && WX.Main.GetConfigItem("Product_OneDept") == "1")
                productdept = WX.PDT.ProductDept.GetModel("select top 1 * from PDT_ProductDept where ProductID=" + WX.Request.rProductId + " order by ID desc");
            if (productdept != null)
            {
                ProductDeptID.SelectedValue = productdept.DeptID.ToString();
                MonthFee.Text = productdept.MonthFee.ToString();
                Fee.Text = productdept.Fee.ToString();
                Feetype1.SelectedValue = productdept.MonthFeeType.ToString();
                Feetype2.SelectedValue = productdept.FeeType.ToString();
                txtDeptRemarks.Text = productdept.Remarks.ToString();
            }
        }
        private void BindDropList()
        {
            string[] str = WX.PDT.ProductDept.FeeTypestr;
            for (int i = 0; i < str.Length; i++)
            {
                Feetype1.Items.Add(new ListItem(str[i], i.ToString()));
                Feetype2.Items.Add(new ListItem(str[i], i.ToString()));
            }

            WX.Data.Dict.BindListCtrl_DeptList(this.ProductDeptID, null, null, null);
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            string sSql = String.Format("Delete PDT_ProductDept where id={0}", id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            //5.（用户及业务对象）统计与状态
            if (iR > 0)
            {
                //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除产品部门({0})成功！", id), "");
                //7.返回处理结果或返回其它页面。
                if (Request["ProductDeptID"] != null && Request["ProductDeptID"] == id.ToString())
                    Response.Redirect("AddProductDept.aspx?ProductID=" + WX.Request.rProductId);
                else
                    this.BindData(false);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除失败！");
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            WX.PDT.ProductDept.MODEL productdept;
            if (Request["ProductDeptID"] != null && Request["ProductDeptID"] != "")
                productdept = WX.PDT.ProductDept.NewDataModel(Request["ProductDeptID"]);
            else
            {
                productdept = WX.PDT.ProductDept.NewDataModel();
                productdept.ProductID.value = WX.Request.rProductId;
            }
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            productdept.DeptID.value = ProductDeptID.SelectedValue;
            try
            {
                productdept.MonthFee.value = Convert.ToDecimal(MonthFee.Text.Trim());
            }
            catch { }
            productdept.MonthFeeType.value = Feetype1.SelectedValue;
            try
            {
                productdept.Fee.value = Convert.ToDecimal(Fee.Text.Trim());
            }
            catch { }
            productdept.FeeType.value = Feetype2.SelectedValue;
            productdept.Remarks.value = txtDeptRemarks.Text;
            int row;
            string logstr = "产品信息提交成功！";
            if (Request["ProductDeptID"] != null && Request["ProductDeptID"] != "")
            {
                row = productdept.Update();
                logstr = "产品信息修改成功！";
            }
            else
                row = productdept.Insert(true);
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            if (row > 0)
            {
                //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, logstr, null);
                ULCode.Debug.Alert(logstr, "AddProductDept.aspx?ProductID=" + WX.Request.rProductId);
            }
            else
            {
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert("产品信息提交失败！");
            }
        }
    }
}