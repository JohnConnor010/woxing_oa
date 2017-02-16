using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace wwwroot.Manage.HR
{
    public partial class User_Credentials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from [TU_Employees_Credentials] where UserId='" + Request["UserId"] + "'");
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            WX.Model.EmployeeCredential.MODEL model = null;
            if (hid_id.Value!="")
            {
                model = WX.Model.EmployeeCredential.GetModel("Select * from [TU_Employees_Credentials] where Id=" + hid_id.Value);
            }
            if (model == null)
            {
                model = WX.Model.EmployeeCredential.NewDataModel();
            }
            model.Name.value = ui_name.Text;
            model.Unit.value = ui_unit.Text;
            model.Ctime.value = ui_ctime.Text;
            if (FileUpload1.HasFile)
            {
                string filepath = "/UploadFiles/cmp/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(filepath));
                model.Annex.value = filepath;
            }
            model.Content.value = ui_content.Text;
            int modelid = 0;
            try
            {
                modelid = Convert.ToInt32(model.Id.ToString());
            }
            catch
            {
            }
            if (modelid > 0)
            {
                model.Update();
            }
            else
            {
                model.UserId.value = Request["UserId"];
                model.Save();
            }
            hid_id.Value = "";
            ui_name.Text="";
            ui_unit.Text = "";
            ui_ctime.Text = "";
            ui_content.Text = "";
            pageinit();
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                ULCode.QDA.XSql.Execute("delete from [TU_Employees_Credentials] where Id=" + e.CommandArgument);
                hid_id.Value = "";
                ui_name.Text = "";
                ui_unit.Text = "";
                ui_ctime.Text = "";
                ui_content.Text = "";
                pageinit();
            }
            if (e.CommandName == "edi")
            {
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select * from [TU_Employees_Credentials] where Id=" + e.CommandArgument);
                hid_id.Value = model.Id.ToString();
                ui_name.Text = model.Name.ToString();
                ui_unit.Text = model.Unit.ToString();
                ui_ctime.Text = ((DateTime)model.Ctime.value).ToString("yyyy-MM-dd");
                ui_content.Text = model.Content.ToString();
            }
        }

    }
}