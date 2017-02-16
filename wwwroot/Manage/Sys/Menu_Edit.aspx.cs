using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
namespace wwwroot.Manage.Sys
{
    public partial class Menu_Edit : System.Web.UI.Page
    {
        public string imagesstr = "";
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                this.LoadParentData();
            }
            LoadIcon();
        }
        private void LoadParentData()
        {

            id = WX.Request.rMenuId;
            if (id == 0)
            {
                Response.Redirect("Menu_List.aspx");
            }
            WX.Model.Menu.MODEL funNew = WX.Request.rMenu; //;WX.Model.Menu.GetModel("select * from TE_Menus where ID=" + id);
            WX.Data.Dict.BindListCtrl_MenuList(this.ui_ParentID, null, "0#最顶层", funNew.ParentID.ToString());
            ui_Name.Value = funNew.Name.ToString();
            ui_Title.Value = funNew.Title.ToString();
            try
            {
                ui_State.SelectedIndex = int.Parse(funNew.State.value.ToString());
            }
            catch
            {
            }
            ui_Url.Value = funNew.Url.ToString();
            try
            {
                ui_icon.Value = "/Manage/icon/" + funNew.Icon.value.ToString();
            }
            catch
            {
            }
            nowimg.Src = ui_icon.Value;
            ui_OrderID.Value = funNew.OrderID.value.ToString();
        }
        private void LoadIcon()
        {
            string FileName, FileExt;

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
                FileExt = "";

                ///如果是文件
                if (fsi is FileInfo)
                {
                    fi = (FileInfo)fsi;
                    ///取得文件名
                    FileName = fi.Name;
                    ///取得文件的扩展名
                    FileExt = fi.Extension;
                    imagesstr += "<div class=\"icostyle\"><img src=\"/Manage/icon/" + FileName + "\" onclick=\"selectface(this)\" /></div>\n";
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
            int id = WX.Request.rMenuId;
            string name = ui_Name.Value;
            int parentID = String.IsNullOrEmpty(ui_ParentID.SelectedValue) ? 0 : Convert.ToInt32(ui_ParentID.SelectedValue);
            int state = Convert.ToInt32(ui_State.Value);
            string title = Convert.ToString(ui_Title.Value);
            string url = Convert.ToString(ui_Url.Value);
            int degree = this.getDegree(ui_ParentID.SelectedItem.Text);
            string icon = ui_icon.Value.Substring(ui_icon.Value.LastIndexOf("/") + 1);
            int orderid = (ui_OrderID.Value.Trim() == "" ? 0 : Convert.ToInt32(ui_OrderID.Value.Trim()));
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (id == parentID)
            {
                ULCode.Debug.Alert(this, "自己不能选择自己为父目录!");
                return;
            }
            //4.业务处理过程
            WX.Model.Menu.MODEL funNew = WX.Request.rMenu; //WX.Model.Menu.GetModel("select * from TE_Menus where ID=" + Request["id"]);
            //if (ULCode.QDA.XSql.IsHasRow("select * from TE_Menus where ParentID=" + parentID + " and Name='" + name + "' and ID!=" + funNew.ID.value.ToString())==true)
            if (WX.Model.Menu.Caches.Find(delegate(WX.Model.Menu.MODEL dele) { return dele.ParentID.ToInt32() == parentID && dele.ID.ToInt32() != id && dele.Name.ToString() == name; }) != null)
            {
                ULCode.Debug.Alert(this, "功能名称已存在，请重新输入！");
                return;
            }
            bool bDeal = false;
            funNew.Name.set(name);
            funNew.ParentID.set(parentID);
            funNew.State.set(state);
            funNew.Title.set(title);
            funNew.Url.set(url);
            funNew.Degree.set(degree);
            funNew.Icon.set(icon);
            funNew.OrderID.set(orderid);
            //Response.Write(parentID);
            int iR = funNew.Update();
            if (iR != 0)
            {
                bDeal = true;
                //6.登记日志
                if (bDeal)
                {
                    WX.Main.AddLog(WX.LogType.Default, "编辑功能成功！", "");
                }
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Confirm(this, "成功修改功能，是否返回功能列表页？", "Menu_List.aspx", this.Request.RawUrl);
                //Response.Redirect("Menu_List.aspx");
            }
            else
            {
                //funNew.RestoreInitial();
                ULCode.Debug.Alert(this, "编辑功能失败,可能是重复添加！");
            }


        }
        protected void btnSave2_Click(object sender, EventArgs e)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select ID from TE_Duties  where CompanyID=11");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sSql = String.Format("if exists(Select * from TE_MenusInDuties where MenuId={0} and DutyId={1})"
                            + " update TE_MenusInDuties set Flag={2} where MenuId={0} and DutyId={1}"
                            + " else "
                            + " insert into TE_MenusInDuties(MenuId,DutyId,Flag) values({0},{1},{2})", WX.Request.rMenuId, dt.Rows[i]["ID"], 1);
                ULCode.QDA.XSql.Execute(sSql);
                string menustr = new WX.Json.BuildUserMenus(Convert.ToInt32(dt.Rows[i]["ID"].ToString())).GetUserMenu();
                if (menustr != "-1")
                {
                    WX.Model.Duty.MODEL model = WX.Model.Duty.NewDataModel(dt.Rows[i]["ID"]);
                    List<WX.Json.UserMenu> l_u = WX.Json.JsonConvert.GetJsonObject<List<WX.Json.UserMenu>>(menustr, false);
                    if (l_u != null)
                    {
                        model.Menus.set(menustr);
                        if (model.Update() == 0)
                        {
                            model.RestoreInitial();
                        }
                    }
                }
            }
        }
    }
}