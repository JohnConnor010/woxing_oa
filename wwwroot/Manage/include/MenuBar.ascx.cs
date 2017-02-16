using System;
using System.Linq;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Text;

namespace wwwroot.Manage.include
{
    public partial class MenuBar : System.Web.UI.UserControl
    {
        #region //参数
        public string Key { get; set; }

        public int CurIndex { get; set; }

        public string Param1 { get; set; }

        public string Param2 { get; set; }

        public string Param3 { get; set; }
        #endregion
        private static string SelCss = "Sel";
        private string xmlPath = WebConfigurationManager.AppSettings["MenuBar"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder sb = new StringBuilder();
                string path = Server.MapPath(xmlPath);
                XDocument document = XDocument.Load(path);
                var menubars = document.Descendants("Menu").Where(m => m.Attribute("key").Value == this.Key).Descendants("MenuBar").Select((item, index) => new { index, item });
                foreach (var menubar in menubars)
                {
                    var url = String.Format(menubar.item.Attribute("url").Value, g(this.Param1), g(this.Param2), g(this.Param3));
                    var title = String.Format(menubar.item.Attribute("title").Value, g(this.Param1), g(this.Param2), g(this.Param3));
                    if (menubar.index + 1 == this.CurIndex)
                    {
                        sb.AppendFormat("<a href=\"{0}\" class=\"{2}\">{1}</a>   ", url, title, SelCss);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>   ", url, title);
                    }
                }
                this.Literal1.Text = sb.ToString();

            }
        }
        private string g(string v)
        {
            if (String.IsNullOrEmpty(v)) return String.Empty;
            if (v.Contains("{") && v.Contains("}"))
            {
                try
                {
                    return new ULCode.UniqueCode(v).Value;
                }
                catch
                {
                    return null;
                }                
            }
            else
                return v;
        }
    }
}