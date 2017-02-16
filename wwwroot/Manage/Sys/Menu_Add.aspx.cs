using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace wwwroot.Manage.Sys
{
    public partial class Menu_Add : System.Web.UI.Page
    {
        public string imagesstr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此菜单！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_MenuList(this.ui_ParentID,null, "0#最顶层", null);
            }
            LoadIcon();
        }
        private void LoadIcon()
        {
            string FileName;

            ///初始化时,默认为当前页面所在的目录
            string strCurDir = Server.MapPath("../icon");
            FileInfo fi;
            DirectoryInfo dir;
            ///针对当前目录建立目录引用对象
            DirectoryInfo dirInfo = new DirectoryInfo(strCurDir);
            ///循环判断当前目录下的文件和目录
            foreach (FileSystemInfo fsi in dirInfo.GetFileSystemInfos())
            {
                FileName = "";

                ///如果是文件
                if (fsi is FileInfo)
                {
                    fi = (FileInfo)fsi;
                    ///取得文件名
                    FileName = fi.Name;
                    imagesstr += "<div class=\"icostyle\"><img src=\"/Manage/Icon/" + FileName + "\" onclick=\"selectface(this)\"nage/ic /></div>\n";
                }
            }
        }
        private int getDegree(string text)
        {
            if (!text.StartsWith("├") && !text.StartsWith("│"))
            {
                return 1;
            }
            else if (text.StartsWith("├"))
            {
                return 2;
            }
            else
                return text.Length - text.Replace("│", "").Length + 1;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //1.验证用户权限
            //2.取得用户变量
            string name = ui_Name.Value;
            int parentID = String.IsNullOrEmpty(ui_ParentID.SelectedValue) ? 0 : Convert.ToInt32(ui_ParentID.SelectedValue);
            int state = Convert.ToInt32(ui_State.Value);
            string title = Convert.ToString(ui_Title.Value);
            string url = Convert.ToString(ui_Url.Value);
            int degree = this.getDegree(this.ui_ParentID.SelectedItem.Text);
            string icon = ui_icon.Value.Substring(ui_icon.Value.LastIndexOf("/") + 1);
            int orderid=(ui_OrderID.Value.Trim()==""?0:Convert.ToInt32(ui_OrderID.Value.Trim()));
           
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            
            //4.业务处理过程
            if (ULCode.QDA.XSql.IsHasRow("select * from TE_Menus where ParentID=" + parentID + " and Name='" + name + "'") == true)
            {
                ULCode.Debug.AjaxAlert(this, "菜单名称已存在，请重新输入！");
                return;
            }
            WX.Model.Menu.MODEL funNew = WX.Model.Menu.NewDataModel();
            funNew.Name.set(name);
            funNew.ParentID.set(parentID);
            funNew.State.set(state);
            funNew.Title.set(title);
            funNew.Url.set(url);
            funNew.Degree.set(degree);
            funNew.Icon.set(icon);
            funNew.OrderID.set(orderid);
            int iR = funNew.Insert(true);
            if (iR > 0)
            {
                funNew.SaveIntoCaches();
                //5.（用户及业务对象）统计与状态

                //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, "添加菜单成功！", "");
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Confirm(this, "菜单添加成功！是否继续添加?？", this.Request.RawUrl, "Menu_List.aspx");
            }
            else
            {
                ULCode.Debug.Alert(this, "添加菜单失败,可能是重复添加！");
            }


        }
    }
}