using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.DownloadFile("/app/网上济宁新闻阅读器.apk",null);
        }
        public void DownloadFile(string downloadUrl, string renameFile)
        {
            string path = HttpContext.Current.Server.MapPath(downloadUrl);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //  添加头信息，为"文件下载/另存为"对话框指定默认文件名
            if (String.IsNullOrEmpty(renameFile)) renameFile = file.Name;
            renameFile = HttpContext.Current.Server.UrlPathEncode(renameFile);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + renameFile);
            //  添加头信息，指定文件大小，让浏览器能够显示下载进度
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //  指定返回的是一个不能被客户端读取的流，必须被下载
            //if (String.IsNullOrEmpty(contentType))
            //    contentType = "application/ms-excel";
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //  把文件流发送到客户端
            //HttpContext.Current.Response.TransmitFile(file.FullName);
            HttpContext.Current.Response.WriteFile(file.FullName);
            //  停止页面的执行
            HttpContext.Current.Response.End();
            file = null;
        }
    }
}