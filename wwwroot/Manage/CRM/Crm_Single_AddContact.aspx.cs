using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Single_AddContact : System.Web.UI.Page
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
                //初始化Title
                string mode = Convert.ToString(Request.QueryString["PageMode"]);
                if (mode == "my")
                {
                    this.lblTitle.Text = "我的客户";
                    this.MenuBar1.Key = "MyCustomer-Modi";
                }
                else
                {
                    this.lblTitle.Text = "我的管理";
                    this.MenuBar1.Key = "Customer-Modi";
                }

                string CustomerID;
                if (Request.QueryString["Action"] == "Add")
                {
                    CustomerID = WX.Request.rCustomerID.ToString();
                    this.lblCustomerID.Text = ULCode.QDA.XSql.GetData(String.Format("SELECT CustomerID FROM CRM_Customers where CustomerID ='{0}'", CustomerID)).ToString();
                    this.txtBirthday.Text = new DateTime(1960, 1, 1).ToString("yyyy-MM-dd");
                    this.txtBabyBirthday.Text = new DateTime(1980, 1, 1).ToString("yyyy-MM-dd");
                    this.InitContactRepeater();
                    this.btnSubmit.Enabled = true;
                    this.btnEdit.Enabled = false;
                }
                if (Request.QueryString["Action"] == "Edit")
                {
                    GetContactDataByCustomerID();
                    this.InitContactRepeater();
                    this.btnSubmit.Enabled = false;
                    this.btnEdit.Enabled = true;
                }
            }
        }
        private void GetContactDataByCustomerID()
        {
            WX.CRM.ContactTemp.MODEL contact, contactmodel=null;
            if (Request["ContactTempID"] != null && Request["ContactTempID"] != "")
            {
                contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_ContactTemp WHERE ID=" + Request["ContactTempID"]);
                contactmodel = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_Contact WHERE ID=" + (contact.ContactID.ToString() == "" ? "0" : contact.ContactID.ToString()));// WX.CRM.ContactTemp.NewDataModel(contact.ContactID.ToString());
            }
            else
            {
                contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_ContactTemp WHERE ContactID=" + WX.Request.rContactID);
                if (contact == null)
                    contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_Contact WHERE ID=" + WX.Request.rContactID);

                contactmodel = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_Contact WHERE ID=" + WX.Request.rContactID);
            }
            if (contact != null)
            {
                this.lblCustomerID.Text = contact.CustomerID.ToString();
                this.txtContactName.Text = contact.ContactName.ToString();
                this.cbIsMain.Checked = Convert.ToBoolean(contact.IsMain.value);
                this.txtDept.Text = contact.Dept.ToString();
                this.txtDuty.Text = contact.Duty.ToString();
                this.rblSex.SelectedValue = contact.Sex.ToString();
                this.txtEmail.Text = contact.Email.ToString();
                this.txtFamilyPhone.Text = contact.FamilyPhone.ToString();
                this.txtMobilePhone.Text = contact.MobilePhone.ToString();
                this.txtFax.Text = contact.Fax.ToString();
                this.txtWorkPhone.Text = contact.WorkPhone.ToString();
                if (contact.Birthday.value != null)
                {
                    this.txtBirthday.Text = Convert.ToDateTime(contact.Birthday.value).ToString("yyyy-MM-dd");
                }
                this.txtHobby.Text = contact.Hobby.ToString();
                this.ddlBabySex.SelectedValue = contact.BabySex.ToString();
                this.txtBabyBirthday.Text = contact.BabyBirthday.ToString();
                this.txtWorkAddress.Text = contact.WorkAddress.ToString();
                this.txtFamilyAddress.Text = contact.FamilyAddress.ToString();
                this.txtCardPath.Text = contact.CardPath.ToString();
                this.txtPhotoPath.Text = contact.PhotoPath.ToString();
                this.txtRemarks.Text = contact.Remarks.ToString();
                if (contactmodel != null)
                {
                    Label1.Text = contact.ContactName.ToString() + contact.IsMain.ToString() != contactmodel.ContactName.ToString() + contactmodel.IsMain.ToString() ? "已修改未审核" : "";
                    Label2.Text = contact.Sex.ToString() != contactmodel.Sex.ToString() ? "已修改未审核" : "";
                    Label3.Text = contact.Dept.ToString() != contactmodel.Dept.ToString() ? "已修改未审核" : "";
                    Label4.Text = contact.Duty.ToString() != contactmodel.Duty.ToString() ? "已修改未审核" : "";
                    Label5.Text = contact.Email.ToString() != contactmodel.Email.ToString() ? "已修改未审核" : "";
                    Label6.Text = contact.FamilyPhone.ToString() != contactmodel.FamilyPhone.ToString() ? "已修改未审核" : "";
                    Label7.Text = contact.MobilePhone.ToString() != contactmodel.MobilePhone.ToString() ? "已修改未审核" : "";
                    Label8.Text = contact.Fax.ToString() != contactmodel.Fax.ToString() ? "已修改未审核" : "";
                    Label9.Text = contact.WorkPhone.ToString() != contactmodel.WorkPhone.ToString() ? "已修改未审核" : "";
                    Label10.Text = contact.Birthday.ToString() != contactmodel.Birthday.ToString() ? "已修改未审核" : "";
                    Label11.Text = contact.Hobby.ToString() != contactmodel.Hobby.ToString() ? "已修改未审核" : "";
                    Label12.Text = contact.BabySex.ToString() + contact.BabyBirthday.ToString() != contactmodel.BabySex.ToString() + contactmodel.BabyBirthday.ToString() ? "已修改未审核" : "";
                    Label13.Text = contact.WorkAddress.ToString() != contactmodel.WorkAddress.ToString() ? "已修改未审核" : "";
                    Label14.Text = contact.FamilyAddress.ToString() != contactmodel.FamilyAddress.ToString() ? "已修改未审核" : "";
                    Label15.Text = contact.CardPath.ToString() != contactmodel.CardPath.ToString() ? "已修改未审核" : "";
                    Label16.Text = contact.PhotoPath.ToString() != contactmodel.PhotoPath.ToString() ? "已修改未审核" : "";
                    Label17.Text = contact.Remarks.ToString() != contactmodel.Remarks.ToString() ? "已修改未审核" : "";

                }
            }
        }
        private void InitContactRepeater()
        {
            string customerId = WX.Request.rCustomerID.ToString();

            string sSql = String.Format("SELECT * FROM CRM_ContactTemp WHERE CustomerID='{0}' and (ContactID is null or ContactID=0)  order by IsMain desc", customerId);
            DataTable dataTable = XSql.GetDataTable(sSql);
            this.ContactTempRepeater.DataSource = dataTable;
            this.ContactTempRepeater.DataBind();

            string sSql2 = String.Format("SELECT * FROM CRM_Contact WHERE CustomerID='{0}' order by IsMain desc", customerId);
            DataTable dataTable2 = XSql.GetDataTable(sSql2);
            this.ContactRepeater.DataSource = dataTable2;
            this.ContactRepeater.DataBind();
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            string sSql = "DELETE FROM CRM_ContactTemp WHERE ID=" + id;
            int row = XSql.Execute(sSql);
            if (row > 0)
            {
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(WX.Request.rCustomerID);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 10, "删除未审核的联系人");
                WX.Main.AddLog(WX.LogType.Default, "客户联系人信息删除成功！", null);
                ULCode.Debug.Alert("联系人信息删除成功！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID);
            }
            else
            {
                ULCode.Debug.Alert("联系人信息删除失败！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID);
            }
        }
        protected void btnDeletetemp_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            WX.CRM.ContactTemp.MODEL contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_ContactTemp WHERE ContactID=" +id);
            int row;
            if (contact == null)
            {
                contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_Contact WHERE ID=" + id);
                contact.ContactID.value = id;
                contact.State.value = -1;
                row = contact.Insert();
            }
            else
            {
                contact.State.value = -1;
                contact.CheckState.value = 0;
                row = contact.Update();
            }
            if (row > 0)
            {
                WX.CRM.Customer.MODEL customer=WX.CRM.Customer.NewDataModel(contact.CustomerID.ToString());
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID,10, "申请删除（"+contact.ContactName.ToString()+"）");
                WX.Main.AddLog(WX.LogType.Default, "客户联系人信息删除申请！", null);
                ULCode.Debug.Alert("删除信息申请成功！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID);
            }
            else
            {
                ULCode.Debug.Alert("删除信息申请失败！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            WX.CRM.ContactTemp.MODEL contact = WX.CRM.ContactTemp.NewDataModel();
            contact.CustomerID.value = WX.Request.rCustomerID;
            contact.ContactName.value = this.txtContactName.Text.Trim();
            contact.Dept.value = this.txtDept.Text.Trim();
            contact.Duty.value = this.txtDuty.Text.Trim();
            contact.IsMain.value = Convert.ToInt32(this.cbIsMain.Checked);
            contact.Sex.value = this.rblSex.SelectedItem.Value;
            contact.Email.value = this.txtEmail.Text.Trim();
            contact.FamilyPhone.value = this.txtFamilyPhone.Text.Trim();
            contact.MobilePhone.value = this.txtMobilePhone.Text.Trim();
            contact.Fax.value = this.txtFax.Text.Trim();
            contact.WorkPhone.value = this.txtWorkPhone.Text.Trim();
            contact.Birthday.value = this.txtBirthday.Text.Trim();
            contact.Hobby.value = this.txtHobby.Text.Trim();
            contact.BabySex.value = this.ddlBabySex.SelectedValue;
            contact.BabyBirthday.value = this.txtBabyBirthday.Text.Trim();
            contact.WorkAddress.value = this.txtWorkAddress.Text.Trim();
            contact.FamilyAddress.value = this.txtFamilyAddress.Text.Trim();
            contact.CardPath.value = this.txtCardPath.Text.Trim();
            contact.PhotoPath.value = this.txtPhotoPath.Text.Trim();
            contact.Remarks.value = this.txtRemarks.Text.Trim();
            contact.State.value = 0;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = contact.Save();
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(WX.Request.rCustomerID);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 3, contact.ContactName.ToString());

                ULCode.Debug.Confirm("客户联系人信息添加成功！是否继续添加联系人信息？", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID, "Crm_My_CustomerToCheck.aspx");
            }
            else
            {
                ULCode.Debug.Alert("客户信息添加失败！", null);
            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            WX.CRM.ContactTemp.MODEL contact;
            bool flag = true;
            if (Request["ContactTempID"] != null && Request["ContactTempID"] != "")
            {
                contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_ContactTemp WHERE ID=" + Request["ContactTempID"]);
            }
            else
            {
                contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_ContactTemp WHERE ContactID=" + WX.Request.rContactID);
                if (contact == null)
                {
                    contact = WX.CRM.ContactTemp.GetModel("SELECT * FROM CRM_Contact WHERE ID=" + WX.Request.rContactID);
                    contact.ContactID.value = WX.Request.rContactID;
                    flag = false;
                }
            }
            if (contact == null)
            {
                ULCode.Debug.Alert(this, "没有找到此联系！修改失败！此错误请联系管理员！");
                return;
            }
            contact.CustomerID.value = this.lblCustomerID.Text;
            contact.Dept.value = this.txtDept.Text.Trim();
            contact.Duty.value = this.txtDuty.Text.Trim();
            contact.ContactName.value = this.txtContactName.Text.Trim();
            contact.IsMain.value = Convert.ToInt32(this.cbIsMain.Checked);
            contact.Sex.value = this.rblSex.SelectedItem.Value;
            contact.Email.value = this.txtEmail.Text.Trim();
            contact.FamilyPhone.value = this.txtFamilyPhone.Text.Trim();
            contact.MobilePhone.value = this.txtMobilePhone.Text.Trim();
            contact.Fax.value = this.txtFax.Text.Trim();
            contact.WorkPhone.value = this.txtWorkPhone.Text.Trim();
            try
            {
                contact.Birthday.value = this.txtBirthday.Text.Trim();
            }
            catch { }
            contact.Hobby.value = this.txtHobby.Text.Trim();
            contact.BabySex.value = this.ddlBabySex.SelectedValue;
            contact.BabyBirthday.value = this.txtBabyBirthday.Text.Trim();
            contact.WorkAddress.value = this.txtWorkAddress.Text.Trim();
            contact.FamilyAddress.value = this.txtFamilyAddress.Text.Trim();
            contact.CardPath.value = this.txtCardPath.Text.Trim();
            contact.PhotoPath.value = this.txtPhotoPath.Text.Trim();
            contact.Remarks.value = this.txtRemarks.Text.Trim();
            contact.State.value = 1;
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程

            contact.CheckState.value = 0;
            int row;
            if (flag)
                row = contact.Update();
            else
                row = contact.Insert();
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(WX.Request.rCustomerID);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 4, contact.ContactName.ToString());
                if (Request["ContactTempID"] != null && Request["ContactTempID"] != "")
                    ULCode.Debug.Alert("客户联系人信息提交成功！", "/Manage/CRM/Crm_My_CustomerToCheck.aspx" );
                else
                    ULCode.Debug.Confirm("客户联系人信息提交成功！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Add&CustomerID=" + WX.Request.rCustomerID, "Crm_CustomerList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("客户信息提交失败！", "Crm_Single_AddContact.aspx?PageMode=" + Request["PageMode"] + "&Action=Edit&&ContactID=" + WX.Request.rContactID + "&CustomerID=" + WX.Request.rCustomerID + "");
            }
        }
    }
}