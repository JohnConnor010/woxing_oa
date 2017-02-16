using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using WX.Model;

namespace wwwroot.Manage.include
{
    public partial class KeyXmlEdit : System.Web.UI.Page
    {
        private bool rReadOnly
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["ReadOnly"]) == 1;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request["bu_submit"] != null && Request["bu_submit"] != "")
                    Add();
                else if (Request["bu_submiteidt"] != null && Request["bu_submiteidt"] != "")
                    Edit();
                else
                    Delete();
            }
            PageInit();
        }
        private void Delete()
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[Request["appid"]].ToString().Split('|');
            ULCode.KeyXmlString kxs10 = new ULCode.KeyXmlString();
            int iR = 0;
            if (Request["table"] == "TU_Employees")
            {
                string userId = Request["keyvalue"];
                Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                try
                {
                    kxs10.LoadData(employee.DFields[Request["column"]].ToString());
                }
                catch { }
                ULCode.KeyXmlString kxsnew = new ULCode.KeyXmlString();
                int n = 0;
                foreach (String s in kxs10.GetItemValues("Node"))
                {
                    if (Request["bu_submitdel" + n] == null)
                    {
                        ULCode.KeyXmlString kxs9 = new ULCode.KeyXmlString();
                        //kxs9.LoadData(s);
                        for (int i = 0; i < items.Length; i++)
                        {
                            string[] item_2 = items[i].Split(';');
                            string uiname = "";
                            if (item_2[0].IndexOf("{") > -1)
                                uiname = item_2[0].Substring(0, item_2[0].IndexOf("{"));
                            else
                                uiname = item_2[0];
                            kxs9.SetItemValue(uiname, Request["ui_" + uiname + n]);
                        }
                        kxsnew.AddItem("Node", kxs9.GetSavedData());
                    }
                    n++;
                }


                employee.DFields[Request["column"]].value = kxsnew.GetSavedData();
                iR = employee.Update();
            }
            //5.（用户及业务对象）统计与状态
            //7.返回处理结果或返回其它页面。
            string pagename = "";
            string nexturl = "";
            switch (Request["column"])
            {
                case "Education": pagename = "员工教育经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Skill": pagename = "员工个人技能"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Family": pagename = "员工家庭成员"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Work": pagename = "员工工作经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "UrgentLink": pagename = "员工紧急联络人"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                default: break;
            }
            if (iR > 0)
            {//6.登记日志
                WX.Main.AddLog(WX.LogType.Default, pagename + "删除成功！", "");
               // ULCode.Debug.Alert(this, pagename + "删除成功！");
            }
            else
            {
                ULCode.Debug.Alert(Page, pagename + "删除失败！");
            }
        }
        private void Edit()
        {
            ULCode.KeyXmlString kxs10 = new ULCode.KeyXmlString();
            int iR = 0;
            if (Request["table"] == "TU_Employees")
            {
                string userId = Request["keyvalue"];
                Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                try
                {
                    kxs10.LoadData(employee.DFields[Request["column"]].ToString());
                }
                catch { }
                employee.DFields[Request["column"]].value = getnew(kxs10).GetSavedData();
                iR = employee.Update();
            }
            //5.（用户及业务对象）统计与状态
            //7.返回处理结果或返回其它页面。
            string pagename = "";
            string nexturl = "";
            switch (Request["column"])
            {
                case "Education": pagename = "员工教育经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Skill": pagename = "员工个人技能"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Family": pagename = "员工家庭成员"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Work": pagename = "员工工作经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "UrgentLink": pagename = "员工紧急联络人"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                default: break;
            }
            if (iR > 0)
            {//6.登记日志
                WX.Main.AddLog(WX.LogType.Default, pagename + "编辑成功！", "");
                ULCode.Debug.Alert(this, pagename + "编辑成功！");
            }
            else
            {
                ULCode.Debug.Alert(Page, pagename + "编辑失败！");
            }

        }
        private ULCode.KeyXmlString getnew(ULCode.KeyXmlString kxs10)
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[Request["appid"]].ToString().Split('|');

            ULCode.KeyXmlString kxsnew = new ULCode.KeyXmlString();
            int n = 0;
            foreach (String s in kxs10.GetItemValues("Node"))
            {
                ULCode.KeyXmlString kxs9 = new ULCode.KeyXmlString();
                //kxs9.LoadData(s);
                for (int i = 0; i < items.Length; i++)
                {
                    string[] item_2 = items[i].Split(';');
                    string uiname = "";
                    if (item_2[0].IndexOf("{") > -1)
                        uiname = item_2[0].Substring(0, item_2[0].IndexOf("{"));
                    else
                        uiname = item_2[0];
                    kxs9.SetItemValue(uiname, Request["ui_" + uiname + n]);
                }
                kxsnew.AddItem("Node", kxs9.GetSavedData());
                n++;
            }
            return kxsnew;
        }
        private void Add()
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[Request["appid"]].ToString().Split('|');
            ULCode.KeyXmlString kxs0 = new ULCode.KeyXmlString();
            if (Request["table"] == "TU_Employees")
            {
                string userId = Request["keyvalue"];
                Employee.MODEL employee = Employee.GetModel("SELECT * FROM " + Request["table"] + " WHERE UserID='" + userId + "'");
                if (employee.DFields[Request["column"]].ToString().IndexOf("<KeyXmlString>") > -1)
                {
                    kxs0.LoadData(employee.DFields[Request["column"]].ToString().Replace("&nbsp;", ""));
                }
            }
            ULCode.KeyXmlString kxs = new ULCode.KeyXmlString();
            for (int i = 0; i < items.Length; i++)
            {
                string[] item_2 = items[i].Split(';');
                string uiname = "";
                if (item_2[0].IndexOf("{") > -1)
                    uiname = item_2[0].Substring(0, item_2[0].IndexOf("{"));
                else
                    uiname = item_2[0];
                kxs.SetItemValue(uiname, Request["ui_" + uiname]);
            }
            kxs0.AddItem("Node", kxs.GetSavedData());
            int iR = 0;
            if (Request["table"] == "TU_Employees")
            {
                string userId = Request["keyvalue"];
                Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                employee.DFields[Request["column"]].value = kxs0.GetSavedData();
                iR = employee.Update();
            }
            //5.（用户及业务对象）统计与状态
            //7.返回处理结果或返回其它页面。
            string pagename = "";
            string nexturl = "";
            switch (Request["column"])
            {
                case "Education": pagename = "员工教育经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Skill": pagename = "员工个人技能"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Family": pagename = "员工家庭成员"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "Work": pagename = "员工工作经历"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                case "UrgentLink": pagename = "员工紧急联络人"; nexturl = "User_UserList.aspx?CompanyID=11"; break;
                default: break;
            }
            if (iR > 0)
            {//6.登记日志
                WX.Main.AddLog(WX.LogType.Default, pagename + "添加成功！", "");
               // ULCode.Debug.Alert(this, pagename + "添加成功！");
            }
            else
            {
                ULCode.Debug.Alert(Page, pagename + "添加失败！");
            }
        }
        public string pagestr = "";
        public string pagetitle = "";
        public string pagecontent = "";
        private void PageInit()
        {
            string[] items = System.Configuration.ConfigurationManager.AppSettings[Request["appid"]].ToString().Split('|');
            pagetitle = "<tr>";
            if (!rReadOnly)
                pagestr += "<tr>";
            for (int i = 0; i < items.Length; i++)
            {
                string[] item_2 = items[i].Split(';');
                if (item_2[0].IndexOf("{") > -1)
                {
                    pagetitle += "<td width='" + item_2[1] + "'>" + item_2[0].Substring(0, item_2[0].IndexOf("{")) + "</td>";
                    if (!rReadOnly)
                    {
                        string[] item_2_1 = item_2[0].Substring(item_2[0].IndexOf("{") + 1).Replace("}", "").Split(',');
                        pagestr += "<td width='" + item_2[1] + "'><select name='ui_" + item_2[0].Substring(0, item_2[0].IndexOf("{")) + "' style='width:99%;'>";
                        for (int j = 0; j < item_2_1.Length; j++)
                            pagestr += "<option value='" + item_2_1[j] + "'>" + item_2_1[j] + "</option>";
                        pagestr += "</select></td>";
                    }
                }
                else
                {
                    pagetitle += "<td width='" + item_2[1] + "'>" + item_2[0] + "</td>";
                    if (!rReadOnly)
                        pagestr += "<td width='" + item_2[1] + "'><input type='text' style='width:99%;' name='ui_" + item_2[0] + "'></td>";
                }
            }
            if (!rReadOnly)
            {
                pagetitle += "<td style='width:90px;'>操作</td></tr>";
                pagestr += "<td><input type='submit' value='添加' name='bu_submit' class='button'></td></tr>";
            }
            //装载
            ULCode.KeyXmlString kxs10 = new ULCode.KeyXmlString();
            if (Request["table"] == "TU_Employees")
            {
                string userId = Request["keyvalue"];
                Employee.MODEL employee = Employee.GetModel("SELECT * FROM TU_Employees WHERE UserID='" + userId + "'");
                if (employee.DFields[Request["column"]].ToString().IndexOf("<KeyXmlString>") > -1)
                {
                    kxs10.LoadData(employee.DFields[Request["column"]].ToString().Replace("&nbsp;", ""));
                }
            }
            int n = 0;
            foreach (String s in kxs10.GetItemValues("Node"))
            {
                pagestr += "<tr>";
                ULCode.KeyXmlString kxs9 = new ULCode.KeyXmlString();
                kxs9.LoadData(s);
                for (int i = 0; i < items.Length; i++)
                {
                    string[] item_2 = items[i].Split(';');
                    if (item_2[0].IndexOf("{") > -1)
                    {
                        pagestr += "<td width='" + item_2[1] + "'><select name='ui_" + item_2[0].Substring(0, item_2[0].IndexOf("{")) + n + "' " + (rReadOnly ? "disabled='disabled'" : "") + " style='width:99%;' >";
                        string[] item_2_1 = item_2[0].Substring(item_2[0].IndexOf("{") + 1).Replace("}", "").Split(',');
                        for (int j = 0; j < item_2_1.Length; j++)
                            pagestr += "<option value='" + item_2_1[j] + "' " + (kxs9.GetItemValue(item_2[0].Substring(0, item_2[0].IndexOf("{"))) == item_2_1[j] ? "selected" : "") + ">" + item_2_1[j] + "</option>";
                        pagestr += "</select></td>";
                    }
                    else
                        pagestr += "<td width='" + item_2[1] + "'><input type='text' style='width:99%;' name='ui_" + item_2[0] + n + "' "+(rReadOnly?"disabled='disabled'":"")+" value='" + kxs9.GetItemValue(item_2[0]) + "'></td>";
                }
                if (!rReadOnly)
                    pagestr += "<td><input type='submit' value='修改' name='bu_submiteidt' class='button'>&nbsp;<input type='submit' value='删除' name='bu_submitdel" + n + "' class='button'></td>";
                pagestr += "</tr>";
                n++;
            }
        }
    }
}