using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;

    /// <summary>
    /// Verify 的摘要说明
    /// </summary>
namespace WX
{

    public class Verify
    {
        /// <summary>
        /// 随机码认证
        /// </summary>
        /// <param name="code">生成认证长度</param>
        public static void DrawImage(int code)
        {
            HttpContext.Current.Session["CheckCode"] = Rand.Number(5);
            CreateImages(HttpContext.Current.Session["CheckCode"].ToString());
        }
        /// <summary>
        /// /// 生成验证图片
        /// /// </summary>
        /// /// <param name="checkCode">验证字符</param>
        private static void CreateImages(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 15);
            Bitmap image = new Bitmap(iwidth, 25);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.LightCyan);
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体 
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体", "Comic Sans MS" };
            Random rand = new Random();
            //随机输出噪点
            for (int i = 0; i < 150; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawPie(new Pen(Color.LightGray, 0), x, y, 6, 6, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(6);
                Font fs_font = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), fs_font, b, 3 + (i * 12), ii);
            }

            //画一个边框
            g.DrawRectangle(new Pen(Color.Red, 0), 100, 0, image.Width - 1, image.Height - 1);
            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();//Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

    }

}
