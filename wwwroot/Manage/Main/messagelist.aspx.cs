using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage
{
    public partial class messagelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    this.lbDelSel.Visible = this.Master.A_Del;
                    this.BindData(true);
                }
                int state = Convert.ToInt32(Request.QueryString["State"]);
                if (state == 0)
                    this.MenuBar1.CurIndex = 1;
                else
                    this.MenuBar1.CurIndex = 2;
                //if (Request["id"] != null && Request["id"] != "")
                //{
                //    WX.Main.ExcuteUpdate("TM_HistoryMessages", "state=1", "ID='" + Request["id"] + "'");
                //}
                //pageinit();
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string keyWords = tbKeyWords.Text.Trim();
            //UI专用测试数据
            string stateCon = Convert.ToInt32(Request.QueryString["State"]) == 0 ? String.Empty : " and State=0";
            string keyWordsCon = !String.IsNullOrEmpty(keyWords) ? " and Title like '%"+keyWords+"%'" : "";
            String sSql = "Select * from view_Messages where SendToUserId='" + WX.Authentication.GetUserID() + "'" + keyWordsCon + stateCon;
            if (start)
            {
                AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sSql);  //第一次需要初始化
                AspNetPager1.PageSize = 15;
                AspNetPager1.CurrentPageIndex = 1;
            }
            DataTable dt = WX.Main.GetPagedRows(sSql, 0, "order by SendTime Desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            //1.验证提示信息权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得提示信息变量
            LinkButton lb = (LinkButton)sender;
            string id = Convert.ToString(lb.CommandName);
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到id:{0}", id));
            //return;

            //以下代码由后台开发人员填写
            //3.验证提示信息变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            string sSql = String.Format("Delete from TM_Messages Where ID='{0}';Delete from TM_HistoryMessages Where ID='{0}';",id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            bDeal = iR > 0;
            //5.（提示信息及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除提示信息({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                this.BindData(true);
                //ULCode.Debug.Alert(this, "删除提示信息成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "删除提示信息失败！");
            }
        }
        //批量设为已读
        protected void ReadSel(object sender, EventArgs e)
        {
            //1.验证提示信息权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得提示信息变量
            string idList = this.Request.Form["checksel"];
            string idList1 = String.Format("'{0}'",idList.Replace(",","','"));
            if (String.IsNullOrEmpty(idList))
            {
                ULCode.Debug.we(String.Format("没有收到任何idList"));
                return;
            }
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到idList:{0}", idList));
            //return;

            //以下是程序开发者的任务
            //3.验证提示信息变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = true;
            WX.Main.MessageToHistory(idList1);
            //填写主要业务逻辑代码
            //5.（提示信息及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "设置已读成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                this.BindData(true);
                //ULCode.Debug.Alert(this, "设置已读成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "设置已读失败！");
            }
        }
        //批量删除处理过程
        protected void DelSel(object sender, EventArgs e)
        {
            //1.验证提示信息权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得提示信息变量
            string idList = this.Request.Form["checksel"];
            //idList = String.Format("'{0}'", idList.Replace(",", "','"));
            if (String.IsNullOrEmpty(idList))
            {
                ULCode.Debug.we(String.Format("没有收到任何idList"));
                return;
            }
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到idList:{0}", idList));
            //return;

            //以下是程序开发者的任务
            //3.验证提示信息变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = true;
            //填写主要业务逻辑代码
            WX.Main.ExecuteDeleteAll("TM_Messages", "ID", idList);
            WX.Main.ExecuteDeleteAll("TM_HistoryMessages", "ID", idList);
            //5.（提示信息及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "删除提示信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                this.BindData(true);
                //ULCode.Debug.Alert(this, "删除提示信息成功！");
            }
            else
            {
                ULCode.Debug.Alert(this, "删除提示信息失败！");
            }
        }
        //控件事件
        protected void selAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbSelAll = (CheckBox)sender;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)this.GridView1.Rows[i].FindControl("sel");
                cb.Checked = cbSelAll.Checked;
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
        protected void QueryAll(object sender, EventArgs e)
        {
            this.tbKeyWords.Text = String.Empty;
            this.BindData(true);
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
        public string gRealName(object oEval)
        {
            try
            {
                return WX.WXUser.GetRealNameByUserID(oEval);
            }
            catch { return ""; }
        }
        public string getEslapseStr(object oEval)
        {
            return WX.Main.GetTimeEslapseStr(Convert.ToDateTime(oEval), null, null);
        }
    }
}