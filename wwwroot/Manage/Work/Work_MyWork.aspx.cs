using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Work
{
    public partial class Work_MyWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.pageinit();
            }
        }
        private void pageinit()
        {
            if (!IsPostBack)
            {
                int flag = Convert.ToInt32(Request["flag"]);
                //switch (flag)
                //{
                //    case 1: Literal1.Text = "未处理"; MenuBar1.CurIndex = 1; break;
                //    case 2: Literal1.Text = "办理中"; MenuBar1.CurIndex = 2; break;
                //    case 3: Literal1.Text = "已办结"; MenuBar1.CurIndex = 3; break;
                //    case 4: Literal1.Text = "已挂起"; MenuBar1.CurIndex = 4; break;
                //    case 0: Literal1.Text = "全部"; MenuBar1.CurIndex = 5; break;
                //}
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            int flag = Convert.ToInt32(Request["flag"]);
            string flagStr = null;
            //UI专用测试数据
            switch (flag)
            {
                case 1: flagStr = " and Deal_Flag in (0)"; break;
                case 2: flagStr = " and Deal_Flag in (1)"; break;
                case 3: flagStr = " and Deal_Flag in (2,3)"; break;
                case 4: flagStr = " and Deal_Flag in (5)"; break;
                case 0: flagStr = ""; break;
            }
            string sSql = String.Format("SELECT A.ID,A.FlowID,A.Name,BeginUser,BeginTime,A.Deal_Flag,A.StepNo,B.Name PName,C.Name FlowName from FL_Run A"
                + " left join FL_Process B on A.FlowId=B.FlowId and A.StepNo=B.StepNo"
                 + " left join FL_Flows C on A.FlowId=C.Id"
 + " where BeginUser='{0}'", WX.Main.CurUser.UserID);
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 20;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by Id desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
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
            bool bDeal = false;
            //填写主要业务逻辑代码
            WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.NewDataModel(id);
            if (runmodel != null && runmodel.Deal_Flag.ToInt32()>0)
            {
                ULCode.Debug.Alert(this, "此流程已经应用，不能删除！");
                return;
            }

            int iR = runmodel.Del();
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除流程({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                this.BindData(true);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除流程失败！");
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void Query(object sender, EventArgs e)
        {
            this.BindData(true);
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
        public string getState(object oEval)
        {
            WX.Flow.DealFlag df = (WX.Flow.DealFlag)Convert.ToInt32(oEval);
            switch (df)
            {
                case WX.Flow.DealFlag.NotReceived: return "未接收";
                case WX.Flow.DealFlag.Operating: return "办理中";
                case WX.Flow.DealFlag.Operated: return "已办理";
                case WX.Flow.DealFlag.HasOperated: return "已办结";
                case WX.Flow.DealFlag.HungUp: return "已挂起";
            }
            return "未知错误!";
        }
    }
}