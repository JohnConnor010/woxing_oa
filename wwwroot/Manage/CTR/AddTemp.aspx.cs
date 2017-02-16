using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CTR
{
    public partial class AddTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.pageInit(true);
            }
        }
        private void pageInit(bool start)
        {
            for (int i = 1; i < WX.CRM.Customer_Temp.TypeStr.Length; i++)
                txtType.Items.Add(new ListItem(WX.CRM.Customer_Temp.TypeStr[i], i.ToString()));
            BindLabel();
        }
        private void BindLabel()
        {
            System.Data.DataTable dataTable = ULCode.QDA.XSql.GetDataTable("select * from CRM_Customer_Label where Type=" +txtType.SelectedValue);
            DataList1.DataSource = dataTable;
            DataList1.DataBind();
        }
        protected void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLabel();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.CRM.Customer_Temp.MODEL temp;
            if (Request["TempID"] != null && Request["TempID"] != "")
                temp = WX.CRM.Customer_Temp.NewDataModel(Request["LableID"]);
            else
                temp = WX.CRM.Customer_Temp.NewDataModel();

            temp.Title.value = txtTitle.Text;
            temp.Content.value = fckContent.Value;
            temp.Type.value = txtType.SelectedValue;
            if (Request["TempID"] != null && Request["TempID"] != "")
                temp.Update();
            else
            {
                temp.UserID.value = WX.Main.CurUser.UserID;
                temp.Addtime.value = DateTime.Now;
                temp.Insert();
            }
            Response.Redirect("CTR_Modules.aspx");
        }
    }
}