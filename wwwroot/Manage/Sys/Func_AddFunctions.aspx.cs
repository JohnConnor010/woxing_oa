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
    public partial class Func_AddFunctions : System.Web.UI.Page
    {
        public string imagesstr = "";
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
                WX.Data.Dict.BindListCtrl_FuncList(this.ui_ParentID,null, "0#最顶层", null);
                DataTable dt = WX.Model.Function.GetTypeList("");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropType.Items.Add(new ListItem(dt.Rows[i]["TypeName"].ToString(),dt.Rows[i]["ID"].ToString()));
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //1.验证用户权限
            //2.取得用户变量
            string name = ui_Name.Value;
            int parentID = String.IsNullOrEmpty(ui_ParentID.SelectedValue) ? 0 : Convert.ToInt32(ui_ParentID.SelectedValue);
            int state = Convert.ToInt32(ui_State.Value);
            string urls = Convert.ToString(ui_Urls.Value);
            int degree = parentID == 0 ? 1 : 2;
            int orderid=(ui_OrderID.Value.Trim()==""?0:Convert.ToInt32(ui_OrderID.Value.Trim()));
           
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            
            //4.业务处理过程
            if (ULCode.QDA.XSql.IsHasRow("select * from TE_Functions where ParentID=" + parentID + " and Name='" + name + "'") == true)
            {
                ULCode.Debug.AjaxAlert(this, "同一目录下功能名称重复，请重新输入功能名称！");
                return;
            }
            WX.Model.Function.MODEL funNew = WX.Model.Function.NewDataModel();
            funNew.Name.set(name);
            funNew.ParentID.set(parentID);
            funNew.State.set(state);
            funNew.Urls.set(urls);
            funNew.Degree.set(degree);;
            funNew.OrderID.set(orderid);
            funNew.TypeID.set(DropType.SelectedValue);
            int iR = funNew.Insert(true);
            if (iR > 0)
            {
                funNew.SaveIntoCaches();
                //5.（用户及业务对象）统计与状态

                //6.登记日志
                WX.Main.AddLog(WX.LogType.Default, "添加功能成功！", "");
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Confirm(this, "功能添加成功！是否继续添加?？", this.Request.RawUrl, "Func_ListFunctions.aspx");
            }
            else
            {
                ULCode.Debug.Alert(this, "添加功能失败,可能是重复添加！");
            }


        }
    }
}