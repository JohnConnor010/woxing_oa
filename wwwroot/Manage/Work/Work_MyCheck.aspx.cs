using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Work
{
    public partial class Work_MyCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.pageinit();
                if (Request["mes"] != null)
                    WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Work_MyCheck.aspx?flag=0&mes=1%'", WX.Main.CurUser.UserID));
            }
        }
        private void pageinit()
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = ULCode.QDA.XSql.GetDataTable("Select Id,Name from FL_Flows where IsVisible=0 order by Sort");
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Add(new ListItem("全部",""));
                DropDownList1.SelectedValue = "";
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
            WX.Main.CurUser.LoadDutyUser();
            WX.Main.CurUser.LoadMyDepartment();
            string sSql = "select A.ID,A.FlowID,A.Name,BeginUser,BeginTime,tuser.RealName,Deal_Flag,A.StepNo,B.Name StepName from FL_Run A"
    + " left join Tu_Users tuser on A.BeginUser=tuser.UserID"
    + " left join TE_Departments dept on dept.ID=tuser.DepartmentID"
    + " left join FL_Process B on A.FlowID=B.FlowID and A.StepNo=B.StepNo"
    + " left join FL_Flows flow on A.FlowID=flow.ID"
+ " where A.FlowId in(select distinct FlowId from FL_Process where Priv_UserList like '%" + WX.Main.CurUser.UserID + "%'	or Priv_DutyList like'%" + WX.Main.CurUser.UserModel.DutyId.ToString() + "%' or Priv_DeptList like'%" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + "%'"
            +" or (A.BeginUser='" + WX.Main.CurUser.UserID + "' and Auto_Type=1)"
            +" or (Auto_Type=2 and tuser.DepartmentID=" + WX.Main.CurUser.MyDepartMent.ID.ToInt32() + " and dept.Host like '%" + WX.Main.CurUser.UserID + "%')"
            + ")"
            +(DropDownList1.SelectedValue==""?"":" and A.FlowId="+DropDownList1.SelectedValue)
            +" and flow.IsVisible=0";
            //Response.Write(WX.Main.CurUser.DutyUser.ID.ToInt32() + "---" + sSql);
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 20;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by BeginTime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
            string sSql = String.Format("select * from FL_Process where FlowId={0}", id);
            if (ULCode.QDA.XSql.IsHasRow(sSql))
            {
                ULCode.Debug.Alert(this, "此流程已经应用，不能删除！");
                return;
            }
            sSql = String.Format("Delete FL_Flows where Id={0}", id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindData(true);
        }
    }
}