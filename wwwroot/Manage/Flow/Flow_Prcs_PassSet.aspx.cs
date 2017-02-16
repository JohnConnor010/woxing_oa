using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_PassSet : System.Web.UI.Page
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
                WX.Flow.Model.Process.MODEL procss = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"])); //WX.Flow.Model.Process.GetModel("select * from FL_Process where Id=" + Request["id"]);
                if (procss != null)
                {
                        WX.Data.Dict.BindListCtrl_enum_SignLookMode(this.SIGNLOOK, null, null, procss.Sign_Look.ToString());
                        WX.Data.Dict.BindListCtrl_enum_SignMode(this.FEEDBACK, null, null, procss.Sign_Mode.ToString());
                        TURN_PRIV.SelectedValue = procss.Pass_OpForce.ToString();
                    WX.Data.Dict.BindListCtrl_enum_RollBackMode(this.ALLOW_BACK, null, null, procss.Pass_RollBack.ToString());
                    WX.Data.Dict.BindListCtrl_enum_Sync_DealMode(this.SYNC_DEAL, null, null, procss.Sync_DealMode.ToString());
                    WX.Data.Dict.BindListCtrl_enum_Sync_CombineMode(this.GATHER_NODE, null, null, procss.Sync_CombineMode.ToString());
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
                prcs.Sign_Look.value = SIGNLOOK.SelectedValue;
                prcs.Sign_Mode.value = FEEDBACK.SelectedValue;
                prcs.Pass_OpForce.value = TURN_PRIV.SelectedValue;
                prcs.Pass_RollBack.value = ALLOW_BACK.SelectedValue;
                prcs.Sync_DealMode.value = SYNC_DEAL.SelectedValue;
                prcs.Sync_CombineMode.value = GATHER_NODE.SelectedValue;
               
                if (prcs.Update() != 0)
                {
                    bDeal = true;
                }
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "流程步骤流转设置成功！", "");
            }
            else
            {
                ULCode.Debug.Alert(this, "流程步骤流转设置失败！");
            }
            Response.Redirect("Flow_Prcs_List.aspx?id=" + Request["flowId"]);

        }
    }
}