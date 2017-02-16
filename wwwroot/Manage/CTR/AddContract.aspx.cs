using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX.Data;
using WX.Flow.Model;
using System.IO;

namespace wwwroot.Manage.CTR
{
    public partial class AddContract : System.Web.UI.Page
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
            string productId = NumberRule.NewDataModel(17).GetValue(17);
            this.txtContractID.Text = productId;

            DataTable contractCategoryData = XSql.GetDataTable("SELECT * FROM CTR_Category");
            this.ddlCategory.DataSource = contractCategoryData;
            this.ddlCategory.DataValueField = "ID";
            this.ddlCategory.DataTextField = "Name";
            this.ddlCategory.DataBind();
            this.ddlCategory.Items.Insert(0, new ListItem("--请选择--", ""));

            DataTable projectData = XSql.GetDataTable("SELECT * FROM Pro_Projects");
            this.ddlProject.DataSource = projectData;
            this.ddlProject.DataValueField = "ID";
            this.ddlProject.DataTextField = "ProjectName";
            this.ddlProject.DataBind();

            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
            this.ddlDepartment.Items.Insert(0, new ListItem("--请选择--", ""));

            this.txtSignedDate.Text = DateTime.Now.ToString("yyyy-M-dd");
            this.txtStartDate.Text = DateTime.Now.ToString("yyyy-M-dd");
            this.txtEndDate.Text = DateTime.Now.ToString("yyyy-M-dd");
            this.txtInputDate.Text = DateTime.Now.ToString("yyyy-M-dd");
            this.txtManager.Text = WX.WXUser.GetRealNameByUserID(WX.Authentication.GetUserID());
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
            WX.CTR.Contract.MODEL contractModel = WX.CTR.Contract.NewDataModel();
            contractModel.ContractID.value = this.txtContractID.Text.Trim();
            contractModel.ContractName.value = this.txtContractName.Text.Trim();
            contractModel.CategoryID.value = this.ddlCategory.SelectedItem.Value;
            contractModel.ProjectID.value = this.ddlProject.SelectedItem.Value;
            contractModel.ContractAmount.value = Convert.ToDecimal(this.txtPrice.Text.Trim());
            contractModel.Currency.value = this.ddlCurrency.SelectedItem.Value;
            contractModel.SignedDate.value = this.txtSignedDate.Text.Trim();
            contractModel.DepartmentID.value = this.ddlDepartment.SelectedItem.Value;
            contractModel.EmployeeID.value = this.hidden_ddlEmployee.Value;
            contractModel.PaymentType.value = this.ddlPaymentType.SelectedItem.Value;
            contractModel.ReceivablesPayment.value = this.ddlReceivablesPayment.SelectedItem.Value;
            contractModel.StartDate.value = this.txtStartDate.Text.Trim();
            contractModel.EndDate.value = this.txtEndDate.Text.Trim();
            contractModel.ContractContent.value = this.txtContractContent.Text.Trim();
            contractModel.ContractAbnormal.value = this.txtContractAbnormal.Text.Trim();
            contractModel.PartyA.value = this.txtPartyA.Text.Trim();
            contractModel.PartyAPerson.value = this.txtPartyAPerson.Text.Trim();
            contractModel.PartyB.value = this.txtPartyB.Text.Trim();
            contractModel.PartyBPerson.value = this.txtPartyBPerson.Text.Trim();
            string digitPath = string.Empty;
            if (this.FileUpload1.HasFile)
            {
                string extension = Path.GetExtension(this.FileUpload1.FileName);
                string path = Server.MapPath("~/UploadFiles/doc/");
                string fileName = DateTime.Now.ToString(DateTime.Now.ToString("yyyyMMddHHmmss")) + extension;
                this.FileUpload1.SaveAs(path + fileName);
                digitPath = "/UploadFiles/doc/" + fileName;
            }
            contractModel.DigitPath.value = digitPath;
            contractModel.Implementation.value = this.ddlImplementation.SelectedItem.Value;
            contractModel.InputDate.value = this.txtInputDate.Text.Trim();
            contractModel.Managers.value = this.txtManager.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = contractModel.Save();
            //int row = 1;
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "合同信息添加成功！", null);
            }
            if (row > 0)
            {
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Confirm(this, "合同信息添加成功！是否添加产品？", "AddContractProject.aspx", "ContractList.aspx");
            }
            else
            {
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert("合同信息添加失败！", "AddContract.aspx");
            }
        }
    }
}