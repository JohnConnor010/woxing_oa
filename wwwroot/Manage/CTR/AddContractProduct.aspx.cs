using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX;

namespace wwwroot.Manage.CTR
{
    public partial class AddContractProduct : System.Web.UI.Page
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
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string cid = WX.Request.rContractID.ToString();
            string cid_real = ULCode.QDA.XSql.GetData("Select ContractID from CTR_Contracts where ID='" + cid + "'").ToStr();
            DataTable productList = XSql.GetDataTable("SELECT * FROM CTR_ContractProduct WHERE ContractID='" + cid_real + "' ORDER BY ID DESC");
            this.Repeater1.DataSource = productList;
            this.Repeater1.DataBind();

            this.lblContractID.Text = cid_real;
            DataTable unitData = XSql.GetDataTable("SELECT * FROM Ass_Unit");
            this.ddlUnits.DataSource = unitData;
            this.ddlUnits.DataValueField = "ID";
            this.ddlUnits.DataTextField = "UnitName";
            this.ddlUnits.DataBind();
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM CTR_ContractProduct WHERE ID=" + id);
            if (row > 0)
            {
                ULCode.Debug.Alert("合同产品删除成功！", "AddContractProduct.aspx?ContractID=" + WX.Request.rContractID);
                WX.Main.AddLog(LogType.Default, "合同产品删除成功！", null);
            }
            else
            {
                ULCode.Debug.Alert("合同产品删除失败！", "AddContractProduct.aspx?ContractID=" + WX.Request.rContractID);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            //2.取得用户变量
            string contractId = this.lblContractID.Text.Trim();
            string productName = this.txtProductName.Text.Trim();
            string specification = this.txtSpecification.Text.Trim();
            string units = this.ddlUnits.SelectedItem.Text.Trim();
            string price = this.txtPrice.Text.Trim();
            string quantity = this.txtQuantity.Text.Trim();
            string remark = this.txtRemark.Text.Trim();
            decimal amount = Convert.ToDecimal(Convert.ToInt32(quantity) * Convert.ToDecimal(price));
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            string cmdText = "INSERT INTO CTR_ContractProduct (ContractID,ProductName,Specification,Units,Price,Quantity,Amount,Remark) VALUES ('" + contractId + "','" + productName + "','" + specification + "','" + units + "'," + Convert.ToDecimal(price) + "," + quantity + "," + amount + ",'" + remark + "')";
            //填写主要业务逻辑代码
            int row = XSql.Execute(cmdText);
            //5.（用户及业务对象）统计与状态

            if (row > 0)
            {                
                //6.登记日志
                WX.Main.AddLog(LogType.Default, "合同产品录入成功！", null);
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert("合同产品录入成功！", "AddContractProduct.aspx?ContractID=" + WX.Request.rContractID);
            }
            else
            {
                ULCode.Debug.Alert("合同产品录入失败！", "AddContractProduct.aspx?ContractID=" + WX.Request.rContractID);
            }
        }
    }
}