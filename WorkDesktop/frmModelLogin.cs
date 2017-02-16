using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace WorkDesktop
{
    public partial class frmModelLogin : Form
    {
        public String FetchedCookie = null;
        public frmModelLogin()
        {
            InitializeComponent();
        }
        private void frmModelLogin_Load(object sender, EventArgs e)
        {            
            ULCode.TextFile tf = new ULCode.TextFile(Application.StartupPath + "/User.txt");
            string s = tf.Text();
            tf = null;
            if (!String.IsNullOrEmpty(s))
            {
                frmWorkDesktop fwd = new frmWorkDesktop();
                fwd.Show();
                this.Close();
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.webBrowser1.Url.ToString().ToLower() == "http://jnooe.com/Manage/Default.aspx".ToLower())
            {
                this.FetchedCookie = this.webBrowser1.Document.Cookie;
                ULCode.TextFile tf = new ULCode.TextFile(Application.StartupPath+"/User.txt");
                tf.Save(this.FetchedCookie);
                tf = null;
                MessageBox.Show("登录设置成功！");
                frmWorkDesktop fwd = new frmWorkDesktop();
                fwd.Show();
                this.Hide();
            }
        }

        private void frmModelLogin_Shown(object sender, EventArgs e)
        {
            this.Text = "第一次登录设置";//String.Format("{0} 登录在 {1}", Main.LoginUser.Name, this.CurTeam.Name);  
            this.webBrowser1.Navigate("http://jnooe.com/login.aspx?client=true");
        }
    }
}