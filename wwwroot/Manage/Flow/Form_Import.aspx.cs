using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.Manage.Flow
{
    public partial class Form_Import : System.Web.UI.Page
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
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuForm.HasFile)
            {
                int id = WX.Request.rFormID;
                WX.Flow.Model.Form.MODEL f = WX.Request.rForm;//WX.Flow.Model.Form.NewDataModel(id);
                //上传
                string file = String.Format("/UploadFiles/Forms/{0}.html", f.Name);
                fuForm.SaveAs(Server.MapPath(file));
                //替换
                ULCode.TextFile tf = new ULCode.TextFile(Server.MapPath(file));
                f.Module.value = tf.Text();
                f.Module_Short.value = f.GetShortModule();
                f.UpdateItems();
                f.Update();
            }
        }
    }
}