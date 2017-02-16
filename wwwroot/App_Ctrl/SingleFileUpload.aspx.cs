using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wwwroot.App_Ctrl
{
    public partial class SingleFileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool fileOK = false;
            string path = Server.MapPath("~/UploadFiles/UserPhotos/");
            if (this.FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".bmp", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                    string houzui = Path.GetExtension(FileUpload1.FileName);

                    // 服务器上保存的文件名称                
                    string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + houzui;
                    FileUpload1.SaveAs(path + filename);
                    this.imagePath.Value = "UploadFiles/UserPhotos/" + filename;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>UploadCompleted('0')</script>");
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>UploadCompleted('1')</script>");
                }
            }
        }
    }
}