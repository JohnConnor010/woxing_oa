using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace wwwroot.Manage.Sys
{
    public partial class Priv_Credentials : System.Web.UI.Page
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
            WX.Main.CurUser.LoadUserModel(false);
            hlInit.Visible =  WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean(); ;
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from [TU_Employees_Credentials] where UserId='" + WX.Main.CurUser.UserID + "'");
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //if (e.CommandName == "del")
            //{
            //    ULCode.QDA.XSql.Execute("delete from [TU_Employees_Credentials] where Id=" + e.CommandArgument);
            //    hid_id.Value = "";
            //    ui_name.Text = "";
            //    ui_unit.Text = "";
            //    ui_ctime.Text = "";
            //    ui_content.Text = "";
            //    pageinit();
            //}
            //if (e.CommandName == "edi")
            //{
            //    WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select * from [TU_Employees_Credentials] where Id=" + e.CommandArgument);
            //    hid_id.Value = model.Id.ToString();
            //    ui_name.Text = model.Name.ToString();
            //    ui_unit.Text = model.Unit.ToString();
            //    ui_ctime.Text = ((DateTime)model.Ctime.value).ToString("yyyy-MM-dd");
            //    ui_content.Text = model.Content.ToString();
            //}
        }

    }
}