using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }
        private void BindData()
        {
            string sSql = String.Format("select Id,StepNo,Name,Next_Nodes from fl_process where FlowId={0} order by stepNo", WX.Request.rFlowID);
            GridView1.DataSource = ULCode.QDA.XSql.GetDataTable(sSql);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            //获取id
            //*******************************************************
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到id:{0}", id));
            //return;

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            String sSql = String.Format("Delete from FL_Process where Id={0}", id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Flow.Model.Process.GetCache(id).RemoveFromCaches();
                WX.Main.AddLog(WX.LogType.Default, "删除步骤成功！", String.Format("步骤编号{0}", id));
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                //ULCode.Debug.Alert(this, "删除步骤成功！");
                this.BindData();
            }
            else
            {
                ULCode.Debug.Alert(this, "添加步骤失败！");
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        public string GetUrl(string url, object oEval)
        {
            return String.Format("{0}?flowId={1}&id={2}", url, WX.Request.rFlowID, Eval("Id"));
        }
    }
}