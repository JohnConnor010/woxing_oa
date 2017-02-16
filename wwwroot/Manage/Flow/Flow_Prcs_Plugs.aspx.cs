using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_Plugs : System.Web.UI.Page
    {
        public string plugin="";
        public string plugout = "";
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
                WX.Flow.Model.Process.MODEL prcs = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"]));//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
                if (prcs != null)
                {
                    if (prcs.Plug_In.value != null)
                    {
                        string[] pin = prcs.Plug_In.value.ToString().Split('|');
                        PLUG_IN_type.SelectedValue = pin[0];
                        PLUG_IN.Value = pin[1].Replace("`", "'");
                        PLUG_IN_demo.Text = pin[2];

                        MatchCollection mc = Regex.Matches(pin[1], "\\[(.*?)\\]");
                        foreach (Match mm in mc)
                        {
                            pin[1] = pin[1].Replace(mm.Value, mm.Result(Request[mm.Value.Replace("[", "").Replace("]", "")]));
                        }
                        plugin = pin[1].Replace("`", "'");
                    }
                    if (prcs.Plug_Out.value != null)
                    {
                        string[] pout = prcs.Plug_Out.value.ToString().Split('|');
                        PLUG_OUT_type.SelectedValue = pout[0];
                        PLUG_OUT.Value = pout[1].Replace("`", "'");
                        PLUG_OUT_demo.Text = pout[2];

                        MatchCollection mc2 = Regex.Matches(pout[1], "\\[(.*?)\\]");
                        foreach (Match mm in mc2)
                        {
                            pout[1] = pout[1].Replace(mm.Value, mm.Result(Request[mm.Value.Replace("[", "").Replace("]", "")]));
                        }
                        plugout = pout[1].Replace("`", "'");
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
            string prcsin =PLUG_IN_type.SelectedValue+"|"+ PLUG_IN.Value+"|"+PLUG_IN_demo.Text;
            string prcsout = PLUG_OUT_type.SelectedValue + "|" + PLUG_OUT.Value + "|"+PLUG_OUT_demo.Text;
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。


            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            WX.Flow.Model.Process.MODEL prcs = WX.Flow.Model.Process.GetCache(Convert.ToInt32(Request.QueryString["Id"]));//WX.Flow.Model.Process.GetModel("select * from FL_Process where ID=" + Request["id"]);
            if (prcs != null)
            {
                prcs.Plug_In.set(prcsin.ToString());
                prcs.Plug_Out.set(prcsout.ToString());
                bDeal = prcs.Update() != 0;
                //string sSql = String.Format("Update FL_Process set Plug_In='{0}',Plug_Out='{1}' where ID={2}", prcsin.Replace("'", "`"), prcsout.Replace("'", "`"), Request["id"]);
                //if (ULCode.QDA.XSql.Execute(sSql) > 0)
                //{
                //    bDeal = true;
                //}
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "流程插件设置成功！", "");
            }
            else
            {
                ULCode.Debug.Alert(this, "流程插件设置失败！");
            }
            Response.Redirect("Flow_Prcs_List.aspx?id=" + Request["flowId"]);

        }

    }
}