using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace Install.Install
{
    public partial class Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Convert.ToInt32(TextBox1.Text.Trim()) > 0)
            //    {
            //        Configuration((Convert.ToInt32(TextBox1.Text.Trim()) * 60).ToString(), "ApplicationServices");
            //        //mess.InnerText="设置成功！");
            //    }
            //    else
            //        mess.InnerText="参数有误！须设置大于0的整数!";
            //}
            //catch
            //{
            //    mess.InnerText="参数有误或文件无权限不可操作。";
            //}
        }
        private void Configuration(string ConnenctionString, string strKey)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                try
                {
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == strKey)
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = ConnenctionString;
                        break;
                    }
                }
                catch { }
            }
            doc.Save(strFileName);
        }
    }
}