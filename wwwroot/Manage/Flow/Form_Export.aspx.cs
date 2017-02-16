using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.Manage.Flow
{
    public partial class Form_Export : System.Web.UI.Page
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
                this.BindDownLoadFile();
            }
        }
        private void BindDownLoadFile()
        {
            int id = WX.Request.rFormID;
            WX.Flow.Model.Form.MODEL f = WX.Request.rForm ;//WX.Flow.Model.Form.NewDataModel(id);
            string url = String.Format("/UploadFiles/Forms/{0}.html", f.Name);

            if (System.IO.File.Exists(Server.MapPath(url)))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(url));
                DateTime dt = file.LastWriteTime;
                this.liLastUpdateTime.Text = String.Format("{0:yyyy年MM月dd日 hh:mm}", dt);
                btnDownLoadFile.Enabled = true;
            }
            else
                btnDownLoadFile.Enabled = false;
            f = null;
        }
        protected void ReBuildFile(object sender, EventArgs e)
        {
            int id = WX.Request.rFormID;
            WX.Flow.Model.Form.MODEL f = WX.Request.rForm;
            string file = String.Format("/UploadFiles/Forms/{0}.html", f.Name);
            ULCode.TextFile tf = new ULCode.TextFile(Server.MapPath(file));
            tf.Save(f.Module.ToString(), false);
            tf = null;
            f = null;
            this.BindDownLoadFile();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = WX.Request.rFormID;
            WX.Flow.Model.Form.MODEL f = WX.Request.rForm;
            string url = String.Format("/UploadFiles/Forms/{0}.html", f.Name);
            if (System.IO.File.Exists(Server.MapPath(url)))
            {
                WX.Main.DownloadFile(url, null);
            }
            else
                ULCode.Debug.Alert(this,"没有找到下载文件！");
            f = null;
        }
    }
}