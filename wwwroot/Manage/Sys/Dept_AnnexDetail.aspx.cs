using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
namespace wwwroot.Manage.Sys
{
    public partial class Dept_AnnexDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.Model.CompanyLicense.MODEL model;
            if (Request["id"] != null)
            {
                model = WX.Model.CompanyLicense.GetModel("Select * from [TE_Companys_license] where Id=" + Request["id"]);
                MenuBar1.CurIndex = (int)model.Type.value + 2;
                string[] annexarry = model.Annex.ToString().Split(',');
                if (annexarry[Convert.ToInt32(Request["aid"])] != "")
                {
                    string fileName = annexarry[Convert.ToInt32(Request["aid"])].Split('|')[0];//客户端保存的文件名
                    string filePath = annexarry[Convert.ToInt32(Request["aid"])].Split('|')[1];//路径
                    string hz = Path.GetExtension(filePath).ToLower();


                    //以字符流的形式下载文件
                    if (Request["zs"]!=null)
                    {
                        Bitmap image = new Bitmap(Server.MapPath(filePath));

                        Graphics g = Graphics.FromImage(image);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Response.ClearContent();//Response.ClearContent();
                        Response.ContentType = "image/Jpeg";
                        Response.BinaryWrite(ms.ToArray());
                        g.Dispose();
                        image.Dispose();

                        Response.Flush();
                        Response.End();
                        return;
                    }
                    //else 
                    if (hz == ".jpg" || hz == ".png"||hz == ".tif" || hz == ".gif" || hz == ".bmp")
                    {
                        Image1.ImageUrl = filePath;
                        return;
                    }
                    else
                    {
                        FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Open);
                        byte[] bytes = new byte[(int)fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        fs.Close();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }
}