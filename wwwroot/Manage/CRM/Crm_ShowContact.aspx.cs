using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_ShowContact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ULCode.Validation.IsNumber(Request.QueryString["ContactID"]))
                {
                    return;
                }
                string contactId = Request.QueryString["ContactID"];
                WX.CRM.Contact.MODEL contact = WX.Request.rContact;
                WX.CRM.Customer.MODEL customer = WX.CRM.Customer.GetModel("select * from CRM_Customers where ID=" + contact.CustomerID.ToString());
                this.lblCustomerName.Text = "<a style='color:#888;' href='Crm_ShowCustomerInfo.aspx?CustomerID=" + customer.ID.ToString() + "'>" + customer.CustomerName.ToString() + " << </a>";
                this.lblContactName.Text = contact.ContactName.ToString();
                this.lblSex.Text = contact.Sex.ToString();
                this.lblAge.Text = contact.Age.ToString();
                this.lblWorkPhone.Text = contact.WorkPhone.ToString();
                this.lblMobilePhone.Text = contact.MobilePhone.ToString();
                this.lblEmail.Text = contact.Email.ToString();
                this.lblFamilyPhone.Text = contact.FamilyPhone.ToString();
                this.lblFax.Text = contact.Fax.ToString();
                this.lblBirthday.Text = contact.Birthday.ToString();
                this.lblHobby.Text = contact.Hobby.ToString();
                this.lblBabyBirthday.Text = contact.BabyBirthday.ToString();
                this.lblBabySex.Text = contact.BabySex.ToString();
                this.lblWorkAddress.Text = contact.WorkAddress.ToString();
                this.lblFamilyAddress.Text = contact.FamilyAddress.ToString();
                this.lblRemarks.Text = contact.Remarks.ToString();
                if (!string.IsNullOrEmpty(contact.CardPath.ToString()))
                {
                    this.imgPhoto.ImageUrl = "../../" + contact.CardPath.ToString();
                }
            }
        }
    }
}