using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_OpSet : System.Web.UI.Page
    {
        public int fid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (Request["flowid"] != null)
            {
                try
                { 
                    fid = Convert.ToInt32(Request["flowid"]);
                }
                catch { }
                if (!IsPostBack)
                {
                    WX.Flow.Model.Flow.MODEL model = WX.Flow.Model.Flow.GetCache(fid); //WX.Flow.Model.Flow.GetModel("select * from FL_Flows where ID=" + fid);
                    if (model != null)
                    {
                        model.LoadForm(false);
                        WX.Flow.Model.Form.MODEL formmodel = model.Form; //WX.Flow.Model.Form.GetModel("select * from FL_Forms where ID=" + model.FormId.value);
                        foreach (WX.Flow.FormField ff in formmodel.Items_FormFieldCollection)
                        {
                            drop_items.Items.Add(new ListItem(ff.Text, ff.Id));
                        }
                    }
                    //DataTable dt = ULCode.QDA.XSql.GetDataTable("select Name,StepNo from FL_Process where FlowId=" + fid);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    model.LoadProcessList(false);
                    if(model.ProcessList!=null)
                    foreach(WX.Flow.Model.Process.MODEL prcs in model.ProcessList)
                    {
                        AUTO_PRCS_USER.Items.Add(new ListItem(prcs.Name.ToString(), prcs.StepNo.ToString()));
                    }
                    AUTO_PRCS_USER.SelectedValue = Request["id"];
                    WX.Flow.Model.Process.MODEL procss = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request["id"]));//WX.Flow.Model.Process.GetModel("select * from FL_Process where Id="+Request["id"]);
                    if (procss != null)
                    {
                        WX.Data.Dict.BindListCtrl_enum_AutoOpMode(this.TOP_DEFAULT, null, null, procss.Auto_OPMode.ToString());
                        USER_LOCK.SelectedValue = procss.Auto_OpChangeMode.ToString();
                        WX.Data.Dict.BindListCtrl_enum_AutoSelOpFilter(this.USER_FILTER, null, null, procss.Auto_FilterMode.ToString());
                        WX.Data.Dict.BindListCtrl_enum_AutoSelOpType(this.AUTO_TYPE, null, null, procss.Auto_Type.ToString());
                        AUTO_PRCS_USER.SelectedValue = procss.Auto_BaseUnit.ToString();
                        drop_items.SelectedValue =procss.Auto_Item.ToString();
                        AUTO_USER.Value = procss.Auto_UserList.ToString();
                        if (procss.Auto_UserList.ToString()!="")
                            AUTO_USER_NAME.Text = ULCode.QDA.XSql.GetXDataTable("SELECT RealName FROM TU_Employees WHERE UserID in('" + procss.Auto_UserList.ToString().Replace(",", "','") + "')").ToColValueList();
                        AUTO_USER_OP.Value = procss.Auto_UserOP.ToString();
                        if (procss.Auto_UserOP.ToString()!="")
                            AUTO_USER_OP_NAME.Text = ULCode.QDA.XSql.GetXDataTable("SELECT RealName FROM TU_Employees WHERE UserID in('" + procss.Auto_UserOP.ToString().Replace(",", "','") + "')").ToColValueList();
                       
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            //获取id
            //*******************************************************
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            //string prcsin = PLUG_IN_type.SelectedValue + "|" + PLUG_IN.Value + "|" + PLUG_IN_demo.Text;
            //string prcsout = PLUG_OUT_type.SelectedValue + "|" + PLUG_OUT.Value + "|" + PLUG_OUT_demo.Text;
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。


            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            WX.Flow.Model.Process.MODEL prcs = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"]));//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
            if (prcs != null)
            {
                prcs.Auto_Type.value = AUTO_TYPE.SelectedValue;
                prcs.Auto_FilterMode.value = USER_FILTER.SelectedValue;
                prcs.Auto_OPMode.value = TOP_DEFAULT.SelectedValue;
                prcs.Auto_OpChangeMode.value = USER_LOCK.SelectedValue;
                if (prcs.Auto_Type.value.ToString() == "3")
                {
                    if (AUTO_USER_OP.Value != "")
                    {
                        prcs.Auto_UserOP.value =AUTO_USER_OP.Value;
                    }
                    if (AUTO_USER.Value != "")
                    {
                        prcs.Auto_UserList.value = AUTO_USER.Value;
                    }
                }
                if (prcs.Auto_Type.value.ToString() == "2" || prcs.Auto_Type.value.ToString() == "4" || prcs.Auto_Type.value.ToString() == "6" || prcs.Auto_Type.value.ToString() == "8" || prcs.Auto_Type.value.ToString() == "9" || prcs.Auto_Type.value.ToString() == "10" || prcs.Auto_Type.value.ToString() == "11")
                {
                    prcs.Auto_BaseUnit.value = AUTO_PRCS_USER.SelectedValue;
                }
                if (prcs.Auto_Type.value.ToString() == "7")
                {
                    prcs.Auto_Item.value = drop_items.SelectedValue;
                }
                if (prcs.Update() != 0) {
                    bDeal = true;
                }
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "流程步骤经办设置成功！", "");
            }
            else
            {
                ULCode.Debug.Alert(this, "流程步骤经办设置失败！");
            }
            Response.Redirect("Flow_Prcs_List.aspx?id=" + Request["flowId"]);

        }
    }
}