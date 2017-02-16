using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Data;
using ULCode.QDA;
using System.Xml;

namespace wwwroot.Manage.CTR
{
    public partial class ProductSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                RadioButtonList1.SelectedValue =WX.Main.GetConfigItem("Product_ISDept");
                RadioButtonList2.SelectedValue = WX.Main.GetConfigItem("Product_OneDept");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            WX.Main.SaveConfig(RadioButtonList1.SelectedValue, "Product_ISDept", Server.MapPath("/App.config"));
            WX.Main.SaveConfig(RadioButtonList2.SelectedValue, "Product_OneDept", Server.MapPath("/App.config"));
            //2.取得用户变量
            WX.Main.AddLog(WX.LogType.Default, "产品部门环境变量设置成功！", null);
            ULCode.Debug.Alert("参数已保存成功！", "ProductSetting.aspx");
        }
    }
}