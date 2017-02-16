using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Flow
{
    public partial class Form_Module : System.Web.UI.Page
    {
        public int fid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
                    fid =WX.Request.rFormID;
            if (!IsPostBack)
            {
                if (fid > 0)
                {
                    WX.Flow.Model.Form.MODEL model = WX.Request.rForm;//WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + fid);
                    if (model != null && model.Module.value != null)
                    {
                        FORM_CONTENT.Value = model.Module.value.ToString();
                        return;
                    }
                }
                else
                {
                    Response.Redirect("Form_List.aspx");
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Flow.Model.Form.MODEL model = WX.Request.rForm ; //WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + Request["id"]);
            model.Module.value = FORM_CONTENT.Value;
            model.Module_Short.value = model.GetShortModule();
            model.UpdateItems();
            model.Update();
            //Response.Redirect("Form_List.aspx");
        }
    }
}