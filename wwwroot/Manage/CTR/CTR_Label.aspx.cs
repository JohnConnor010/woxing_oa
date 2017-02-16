using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CTR
{
    public partial class CTR_Label : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.pageInit(true);
            }

        }
        private void InitVar()
        {
            txtVarP.Visible = false; txtVarA.Visible = false; txtContent.Visible = false;
            if (txtStyle.SelectedValue == "0")
            {
                if (txtType.SelectedValue == "1")
                    txtVarP.Visible = true;
                else
                    txtVarA.Visible = true;
            }
            else if (txtStyle.SelectedValue == "1")
                txtContent.Visible = true;
        }
        private void pageInit(bool start)
        {
            for (int i = 1; i < WX.CRM.Customer_Temp.TypeStr.Length; i++)
                txtType.Items.Add(new ListItem(WX.CRM.Customer_Temp.TypeStr[i], i.ToString()));
            for (int i = 0; i < WX.CRM.Customer_Label.StyleStr.Length; i++)
                txtStyle.Items.Add(new ListItem(WX.CRM.Customer_Label.StyleStr[i], i.ToString()));
            if (Request["LableID"] != null && Request["LableID"] != "")
            {
                WX.CRM.Customer_Label.MODEL label = WX.CRM.Customer_Label.NewDataModel(Request["LableID"]);
                if (label != null)
                {
                    txtTitle.Text = label.Title.ToString();
                    txtName.Text = label.Name.ToString();
                    txtContent.Text = label.Content.ToString();
                    txtStyle.SelectedValue = label.Style.ToString();
                    txtType.SelectedValue = label.Type.ToString();
                    if (txtStyle.SelectedValue == "0")
                    {
                        if(txtType.SelectedValue == "1")
                        txtVarP.SelectedValue=label.Content.ToString();
                        else
                            txtVarA.SelectedValue = label.Content.ToString();
                    }
                    else if (txtStyle.SelectedValue == "2")
                        txtContent.Text= label.Content.ToString() ;
                    fckFormat.Value = label.Format.ToString();
                }
            }
            InitVar();
            dataBind(start);


        }
        private void dataBind(bool start)
        {
            string sql = "select * from CRM_Customer_Label" + (Gv_customer.Rows.Count > 0 && ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).SelectedValue != "0" ? " where Type=" + ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).SelectedValue : "");// + ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).SelectedValue;

            var supplierData = WX.Main.GetPagedRows(sql, 0, "ORDER BY id desc", 50, AspNetPager1.CurrentPageIndex);
            System.Data.DataTable dataTable = supplierData;
            Gv_customer.DataSource = dataTable;
            Gv_customer.DataBind();

            if (Gv_customer.Rows.Count > 0)
            {
                Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
            }

            this.AspNetPager1.AlwaysShow = true;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 50;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;
            }
            for (int i = 0; i < WX.CRM.Customer_Temp.TypeStr.Length; i++)
                ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).Items.Add(new ListItem(WX.CRM.Customer_Temp.TypeStr[i], i.ToString()));
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            pageInit(false);
        }

        protected void txtStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVar();
        }

        protected void GVType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectstr = ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).SelectedValue;
            dataBind(false);
            ((DropDownList)Gv_customer.HeaderRow.FindControl("DropDownList1")).SelectedValue = selectstr;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.CRM.Customer_Label.MODEL label;
            if (Request["LableID"] != null && Request["LableID"] != "")
                label = WX.CRM.Customer_Label.NewDataModel(Request["LableID"]);
            else
                label = WX.CRM.Customer_Label.NewDataModel();
            
            label.Title.value = txtTitle.Text;
            label.Name.value = txtName.Text;
            if (txtStyle.SelectedValue == "0")
                label.Content.value = (txtType.SelectedValue == "1" ? txtVarP.SelectedValue : txtVarA.SelectedValue);
            else if (txtStyle.SelectedValue == "2")
                label.Content.value = txtContent.Text;
            label.Style.value = txtStyle.SelectedValue;
            label.Format.value = fckFormat.Value;
            label.Type.value = txtType.SelectedValue;
            if (Request["LableID"] != null && Request["LableID"] != "")
                label.Update();
            else
            {
                label.UserID.value = WX.Main.CurUser.UserID;
                label.Addtime.value = DateTime.Now;
                label.Insert();
            }
            Response.Redirect("CTR_Label.aspx");
        }
    }
}