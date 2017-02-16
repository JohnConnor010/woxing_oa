using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Data;
using System.IO;

namespace LazyOA.Manage
{
    public partial class Default : System.Web.UI.Page
    {
        public string UserTitle
        {
            get
            {
                WX.WXUser cu = WX.Main.CurUser;
                if (cu.IsEmployeeUser)
                {
                    cu.LoadUserModel(false);
                    cu.LoadMyCompany();
                    return String.Format("{0}({1})", cu.UserModel.RealName, cu.MyCompany.Name);
                }
                else
                {
                    return String.Format("{0}({1})", cu.UserName, "系统管理员");
                }
            }
        }
        public string MenuList
        {
            get
            {
                return WX.Main.GetMenu();
            }
        }
        public string GetMes
        {
            get { 
                string messtr=getHtml("/App_Services/message.ashx"); 
                if(messtr=="NONE")
                    return "";
                return messtr;
            }
        }
        public static string getHtml(string url)//url是要访问的网站地址，charSet是目标网页的编码，如果传入的是null或者"",那就自动分析网页的编码
        {
            try
            {
                WebClient myWebClient = new WebClient(); //创建WebClient实例myWebClient
                // 需要注意的：
                //有的网页可能下不下来，有种种原因比如需要cookie,编码问题等等
                //这是就要具体问题具体分析比如在头部加入cookie
                // webclient.Headers.Add("Cookie", cookie);
                //这样可能需要一些重载方法。根据需要写就可以了
                //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。
                myWebClient.Credentials = CredentialCache.DefaultCredentials;
                //如果服务器要验证用户名，密码
                //NetworkCredential mycred = new NetworkCredential(struser, strpassword);
                //myWebClient.Credentials = mycred;
                //从资源下载数据并返回字节数组。(加@是因为网址中间有"/"符号)
                byte[] myDataBuffer = myWebClient.DownloadData(System.Web.HttpContext.Current.Server.MapPath(url));
                string strWebData = Encoding.Default.GetString(myDataBuffer);
                //获取网页字符编码描述信息

                return System.Text.Encoding.GetEncoding("gbk").GetString(myDataBuffer);

            }
            catch (Exception e) { return ""; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["logout"] != null && Request["logout"] == "1")
                {
                    WX.Authentication.LoginOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
            
        }
    }
}
