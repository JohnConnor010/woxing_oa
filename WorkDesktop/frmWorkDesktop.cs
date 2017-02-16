using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace WorkDesktop
{
    public partial class frmWorkDesktop : Form
    {
        public frmWorkDesktop()
        {
            InitializeComponent();
        }
        private string WorkName
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["name"]);
            }
        }
        private string UserName
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["userName"]);
            }
        }
        private string MyWorkUrl 
        {
            get 
            {
                return String.Format(ConfigurationManager.AppSettings["myWorkUrl"],this.UserName);
            }
        }
        private string MySubmitUrl
        {
            get
            {
                return String.Format(ConfigurationManager.AppSettings["mySubmitUrl"], this.UserName);
            }
        }
        private void frmWorkDesktop_Load(object sender, EventArgs e)
        {
            this.Text = this.WorkName;
            webBrowser1.Navigate(this.MyWorkUrl);
            webBrowser2.Navigate(this.MySubmitUrl);
        }
        private void Navigating(object sender, WebBrowserNavigatingEventArgs e)
        { 
            WebBrowser wb = (WebBrowser)sender;
            string url = e.Url.ToString();
            if (!url.StartsWith("http")) return;
            if (!url.ToLower().Contains("user="))
            {
                if (url.Contains("?"))
                {
                    url = url + "&user=" + this.UserName;
                }
                else
                {
                    url = url + "?user=" + this.UserName;
                }
                e.Cancel = true;
                wb.Navigate(url);
            }
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //this.Navigating(sender, e);
        }
        private void webBrowser2_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //this.Navigating(sender, e);
            
        }
        private void NewWindow(object sender, CancelEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.StatusText;
            if (!url.StartsWith("http")) return;
            if (!url.ToLower().Contains("user="))
            {
                if (url.Contains("?"))
                {
                    url = url + "&user=" + this.UserName;
                }
                else
                {
                    url = url + "?user=" + this.UserName;
                }
                e.Cancel = true;
                wb.Navigate(url);
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //this.NewWindow(sender, e);
            //webBrowser1.Document.Cookie
        }
        private void webBrowser2_NewWindow(object sender, CancelEventArgs e)
        {
            //this.NewWindow(sender, e);
        }

        private void frmWorkDesktop_Shown(object sender, EventArgs e)
        {
            //frmModelLogin fml = new frmModelLogin();
            //fml.ShowDialog();
        }
    }
}
