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
    public partial class Func_EditFunctions : System.Web.UI.Page
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
        }
        private void LoadParentData()
        {
            id = WX.Request.rFunctionId;
            if (id == 0)
            {
                Response.Redirect("Func_ListFunctions.aspx");
            }
            WX.Model.Function.MODEL funNew = WX.Request.rFunction; 
                WX.Data.Dict.BindListCtrl_FuncList(this.ui_ParentID,null, "0#最顶层", funNew.ParentID.ToString());
                DataTable dt = WX.Model.Function.GetTypeList("");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropType.Items.Add(new ListItem(dt.Rows[i]["TypeName"].ToString(), dt.Rows[i]["ID"].ToString()));
                }
                DropType.SelectedValue = funNew.TypeID.ToString();
                ui_Name.Value = funNew.Name.ToString();
                try
                {
                    ui_State.SelectedIndex = int.Parse(funNew.State.value.ToString());
                }
                catch
                {
                }
                ui_Urls.Value = funNew.Urls.ToString();
               // ui_HTMLS.Value = funNew.Htmls.value.ToString();
                ui_OrderID.Value = funNew.OrderID.value.ToString();
            //}
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //1.验证用户权限
            //2.取得用户变量
            int id = WX.Request.rFunctionId;
            string name = ui_Name.Value;
            int parentID = String.IsNullOrEmpty(ui_ParentID.SelectedValue) ? 0 : Convert.ToInt32(ui_ParentID.SelectedValue);
            int state = Convert.ToInt32(ui_State.Value);
            string urls = Convert.ToString(ui_Urls.Value);
            //if (ui_ParentID.SelectedItem.Text.IndexOf("│├") == -1)
            //{
            //    urls = "";
            //}
            int degree = Convert.ToInt32(ui_degree.Value);
            int orderid = (ui_OrderID.Value.Trim() == "" ? 0 : Convert.ToInt32(ui_OrderID.Value.Trim()));
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            WX.Model.Function.MODEL funNew =WX.Model.Function.NewDataModel(WX.Request.rFunctionId); //WX.Model.Function.GetModel("select * from TE_Functions where ID=" + Request["id"]);
            //if (ULCode.QDA.XSql.IsHasRow("select * from TE_Functions where ParentID=" + parentID + " and Name='" + name + "' and ID!=" + funNew.ID.value.ToString())==true)
            if (WX.Model.Function.Caches.Find(delegate(WX.Model.Function.MODEL dele) { return dele.ParentID.ToInt32() == parentID && dele.ID.ToInt32() != id && dele.Name.ToString() == name; }) != null)
            {
                ULCode.Debug.Alert(this, "功能名称已存在，请重新输入！");
                return;
            }
            bool bDeal = false;
            funNew.Name.set(name);
            funNew.ParentID.set(parentID);
            funNew.State.set(state);
            funNew.Urls.set(urls);
            funNew.Degree.set(degree);
            funNew.OrderID.set(orderid);
            funNew.TypeID.set(DropType.SelectedValue);
            if (CheckBox1.Checked)
                funNew.UpdateChild();
            int iR = funNew.Update();
            funNew.SaveIntoCaches();
            if (iR != 0)
            {
                bDeal = true;
                //6.登记日志
                if (bDeal)
                {
                    WX.Main.AddLog(WX.LogType.Default, "编辑功能成功！", "");
                }
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Confirm(this, "成功修改功能，是否返回功能列表页？", "Func_ListFunctions.aspx", this.Request.RawUrl);
                //Response.Redirect("Func_ListFunctions.aspx");
            }
            else
            {
                funNew.RestoreInitial();
                ULCode.Debug.Alert(this, "编辑功能失败,可能是重复添加！");
            }


        }
    }
}