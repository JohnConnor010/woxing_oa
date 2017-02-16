using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Check_ContactDetail : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                WX.CRM.ContactTemp.MODEL contacttemp = WX.CRM.ContactTemp.NewDataModel(Request["ContactTempID"]);
                if (contacttemp == null)
                {
                    ULCode.Debug.we("contacttemp获取失败！请联系管理！");
                    return;
                }
                WX.CRM.Contact.MODEL contact = WX.CRM.Contact.NewDataModel(contacttemp.ContactID.ToString());
                if (contact != null)
                {
                    WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(contact.CustomerID.ToString());
                    if (customer != null)
                        liCustomerName.Text = customer.CustomerName.ToString();

                    this.liContactName.Text = contact.ContactName.ToString();
                    this.liDept.Text = contact.Dept.ToString();
                    this.liDuty.Text = contact.Duty.ToString();
                    this.liSex.Text = contact.Sex.ToString();
                    this.liEmail.Text = contact.Email.ToString();
                    this.liFamilyPhone.Text = contact.FamilyPhone.ToString();
                    this.liWorkPhone.Text = contact.WorkPhone.ToString();
                    this.liFax.Text = contact.Fax.ToString();
                    this.liMobilePhone.Text = contact.MobilePhone.ToString();
                    this.liBirthday.Text = contact.Birthday.ToString();
                    this.liHobby.Text = contact.Hobby.ToString();
                    this.liBabySex.Text = contact.BabySex.ToString() + "--" + contact.BabyBirthday.ToString();
                    this.liWorkAddress.Text = contact.WorkAddress.ToString();
                    this.liFamilyAddress.Text = contact.FamilyAddress.ToString();
                    this.liCardPath.Text = "<img src='" + contact.CardPath.ToString() + "'/>";
                    this.liPhotoPath.Text = "<img src='" + contact.PhotoPath.ToString() + "'/>";
                    this.liRemarks.Text = contact.Remarks.ToString();
                }
                if (contacttemp != null)
                {
                    this.txtContactName.Text = contacttemp.ContactName.ToString();
                    this.txtDept.Text = contacttemp.Dept.ToString();
                    this.txtDuty.Text = contacttemp.Duty.ToString();
                    this.radSex.SelectedValue = contacttemp.Sex.ToString();
                    this.txtEmail.Text = contacttemp.Email.ToString();
                    this.txtFamilyPhone.Text = contacttemp.FamilyPhone.ToString();
                    this.txtWorkPhone.Text = contacttemp.WorkPhone.ToString();
                    this.txtFax.Text = contacttemp.Fax.ToString();
                    this.txtMobilePhone.Text = contacttemp.MobilePhone.ToString();
                    this.txtBirthday.Text = contacttemp.Birthday.ToString();
                    this.txtHobby.Text = contacttemp.Hobby.ToString();
                    this.ddlBabySex.SelectedValue = contacttemp.BabySex.ToString();
                    this.txtBabyBirthday.Text = contacttemp.BabyBirthday.ToString();
                    this.txtWorkAddress.Text = contacttemp.WorkAddress.ToString();
                    this.txtFamilyAddress.Text = contacttemp.FamilyAddress.ToString();
                    this.tempCardPath.Text = "<img src='" + contacttemp.CardPath.ToString() + "'/>";
                    this.tempPhotoPath.Text = "<img src='" + contacttemp.PhotoPath.ToString() + "'/>";
                    this.txtRemarks.Text = contacttemp.Remarks.ToString();
                    this.txtContactName.Enabled = contact.ContactName.ToString() != contacttemp.ContactName.ToString();
                    this.txtDept.Enabled = contact.Dept.ToString() != contacttemp.Dept.ToString();
                    this.txtDuty.Enabled = contact.Duty.ToString() != contacttemp.Duty.ToString();
                    this.radSex.Enabled = contact.Sex.ToString() != contacttemp.Sex.ToString();
                    this.txtEmail.Enabled = contact.Email.ToString() != contacttemp.Email.ToString();
                    this.txtFamilyPhone.Enabled = contact.FamilyPhone.ToString() != contacttemp.FamilyPhone.ToString();
                    this.txtWorkPhone.Enabled = contact.WorkPhone.ToString() != contacttemp.WorkPhone.ToString();
                    this.txtFax.Enabled = contact.Fax.ToString() != contacttemp.Fax.ToString();
                    this.txtMobilePhone.Enabled = contact.MobilePhone.ToString() != contacttemp.MobilePhone.ToString();
                    this.txtBirthday.Enabled = contact.Birthday.ToString() != contacttemp.Birthday.ToString();
                    this.txtHobby.Enabled = contact.Hobby.ToString() != contacttemp.Hobby.ToString();
                    this.ddlBabySex.Enabled = contact.BabySex.ToString() != contacttemp.BabySex.ToString();
                    this.txtBabyBirthday.Enabled = contact.BabyBirthday.ToString() != contacttemp.BabyBirthday.ToString();
                    this.txtWorkAddress.Enabled = contact.WorkAddress.ToString() != contacttemp.WorkAddress.ToString();
                    this.txtFamilyAddress.Enabled = contact.FamilyAddress.ToString() != contacttemp.FamilyAddress.ToString();
                    this.txtRemarks.Enabled = contact.Remarks.ToString() != contacttemp.Remarks.ToString();
                }
            }
        }
        private WX.CRM.Contact.MODEL getnew()
        {
            WX.CRM.ContactTemp.MODEL contacttemp = WX.CRM.ContactTemp.NewDataModel(Request["ContactTempID"]);
            WX.CRM.Contact.MODEL contact = WX.CRM.Contact.NewDataModel(contacttemp.ContactID.ToString());
            contact.CustomerID.value = contacttemp.CustomerID.value;
            contact.ContactName.value = txtContactName.Text;
            contact.Dept.value = txtDept.Text;
            contact.Duty.value = txtDuty.Text;
            contact.IsMain.value = contacttemp.IsMain.value;
            contact.Sex.value = radSex.SelectedValue;
            contact.Email.value = txtEmail.Text;
            contact.Birthday.value = txtBirthday.Text;
            contact.FamilyPhone.value = txtFamilyPhone.Text;
            contact.WorkPhone.value = txtWorkPhone.Text;
            contact.MobilePhone.value = txtMobilePhone.Text;
            contact.Fax.value = txtFax.Text;
            contact.BabyBirthday.value = txtBabyBirthday.Text;
            contact.BabySex.value = ddlBabySex.SelectedValue;
            contact.WorkAddress.value = txtWorkAddress.Text;
            contact.FamilyAddress.value = txtFamilyAddress.Text;
            contact.CardPath.value = contacttemp.CardPath.value;
            contact.PhotoPath.value = contacttemp.PhotoPath.value;
            contact.Remarks.value = txtRemarks.Text;
            contact.Hobby.value = this.txtHobby.Text;
            //---OA维护信息
            return contact;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.CRM.ContactTemp.MODEL contacttemp = WX.CRM.ContactTemp.NewDataModel(Request["ContactTempID"]);
            if (contacttemp.State.ToInt32() == -1)
            {
                WX.Main.ExecuteDelete("CRM_Contact", "ID", contacttemp.ContactID.ToString());
                contacttemp.Delete();
            }
            else
            {
                WX.CRM.Contact.MODEL contact = getnew();
                if (contact.ID.ToInt32() > 0)
                    contact.Update();
                else
                    contact.Insert();
            }
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(contacttemp.CustomerID.ToString());
            contacttemp.Delete();
            mes = "butsumit();";
            WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(),WX.Main.CurUser.UserID, 5, "通过");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            WX.CRM.ContactTemp.MODEL contacttemp = WX.CRM.ContactTemp.NewDataModel(Request["ContactTempID"]);
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel(contacttemp.CustomerID.ToString());
            contacttemp.CheckState.set(-1);
            contacttemp.CheckUserID.value = WX.Main.CurUser.UserID;
            contacttemp.Update();
            mes = "butsumit();";
            WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 5, "未通过");
        }
    }
}