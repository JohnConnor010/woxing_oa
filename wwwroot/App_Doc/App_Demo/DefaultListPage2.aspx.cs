using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.App_Demo
{
    public partial class DefaultListPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                this.lbDelSel.Visible = this.Master.A_Del;
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string keyWords = tbKeyWords.Text;
            //UI专用测试数据
            string[] datas = new String[]
                {
                    "id,name,tel,address",
                    "1,szp,85071163,",
                    "2,zdx,12343211233,",
                    "3,yjy,838402349,",
                    "4,wxd,9234e2329,",
                    "5,ssz,85071163,",
                    "6,eed,12343211233,",
                    "7,ffd,838402349,",
                    "8,dde,9234e2329,"            
                 };
            if (start) {
                AspNetPager1.RecordCount = 99;  //第一次需要初始化
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetTestDataTable(datas);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            Response.Write(this.Request.Form["checksel"]); return;
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
            ULCode.Debug.we(String.Format("已经收到id:{0}",id));
            return;

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
                 //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除用户({0})成功！",id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                //

                ULCode.Debug.Alert(this,"删除用户成功！");
            }
            else
            {
                ULCode.Debug.Alert(this,"删除用户失败！");
            }
        }
        //批量删除处理过程
        protected void DelSel(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Del)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string idList = this.Request.Form["checksel"];
            if (String.IsNullOrEmpty(idList))
            {
                ULCode.Debug.we(String.Format("没有收到任何idList"));
                return;
            }
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            ULCode.Debug.we(String.Format("已经收到idList:{0}", idList));
            return;

            //以下是程序开发者的任务
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "删除用户信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                //重新绑定数据代码
                //

                ULCode.Debug.Alert(this,"删除用户成功！");
            }
            else
            {
                ULCode.Debug.Alert(this,"删除用户失败！");
            }
        }
        //控件事件
        protected void selAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbSelAll=(CheckBox)sender;
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
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
    }
}