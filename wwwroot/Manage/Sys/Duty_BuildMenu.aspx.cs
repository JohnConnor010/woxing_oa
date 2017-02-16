using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
namespace wwwroot.Manage.Sys
{
    public partial class Duty_BuildMenu : System.Web.UI.Page
    {
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
                pageinit();
            }
        }
        private void pageinit()
        {
            int id = WX.Request.rDutyId;
            int companyId = WX.Request.rCompanyId;
            try
            {
                WX.Model.Duty.MODEL model = WX.Request.rDuty; //WX.Model.Duty.GetModel("select * from TE_Duties where ID="+id);
                if (!model.Menus.isEmpty && model.Menus.value.ToString() != "")
                {
                    ui_Htmls.Value = model.Menus.value.ToString();
                    return;
                }
            }
            catch
            {
                Response.Write("参数错误，你没有权限访问此功能！");
                Response.End();
                return;
            }

            //string menustr = WX.Main.CreateMenu(id);
            //if (menustr == "-1")
            //{
            //    Response.Write("参数错误或您没有权限，数据找不到！");
            //    Response.End();
            //    return;

            //}
            //ui_Htmls.Value = menustr;

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            
            //1.验证用户权限
         
            //2.取得用户变量
            int id = WX.Request.rDutyId;
            int companyId = WX.Request.rCompanyId;
            string menus = this.ui_Htmls.Value;
            WX.Model.Duty.MODEL model =WX.Request.rDuty; //WX.Model.Duty.GetModel("select * from TE_Duties where ID="+id);
            //WX.Model.Duty.MODEL model = WX.Model.Duty.GetModel("select * from TE_Duties where ID=" + Request["id"]);
         
            //3.验证用户变量，包含Request.QueryString及Request.Form
            List<WX.Json.UserMenu> l_u = WX.Json.JsonConvert.GetJsonObject<List<WX.Json.UserMenu>>(menus, false);
            if (l_u == null)
            {
                ULCode.Debug.Alert(this, "你的文档不合法！保存失败！");
                return;
            }
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            model.Menus.set(ui_Htmls.Value);
            if (model.Update() != 0)
            {
                //WX.Main.CurUser.LoadDutyUser(true);
                bDeal = true;
            }
            else
            {
                model.RestoreInitial();
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "职务“"+model.Name.value.ToString()+"”生成菜单成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "成功生成职务菜单，是否返回职务列表页？", "/Manage/Sys/Duty_List.aspx?CompanyID=11", this.Request.RawUrl);
                //Response.Redirect();
            }
            else
            {
                ULCode.Debug.Alert(this, "生成失败，请重试！");
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            int id = WX.Request.rDutyId;
            string menustr = new WX.Json.BuildUserMenus(id).GetUserMenu();
            //menustr = "\"menus\":" + menustr;
            if (menustr == "-1")
            {
                Response.Write("参数错误或您没有权限，数据找不到！");
                Response.End();
                return;

            }
            ui_Htmls.Value = menustr;
        }
    }
}