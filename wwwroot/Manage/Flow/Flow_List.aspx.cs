using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.ddlType.Items.Add(new ListItem("--请选择--", ""));
                WX.Data.Dict.BindListCtrl_FlowCatagory(this.ddlType, null, "#--请选择--", null);
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string keyWords = this.tbKeyWords.Text;
            string catagory = this.ddlType.SelectedValue;
            //UI专用测试数据
            string con1 = null; if (!String.IsNullOrEmpty(keyWords)) con1 = String.Format(" and [Name] like '%{0}%'", keyWords);
            string con2 = null; if (!String.IsNullOrEmpty(catagory)) con2 = String.Format(" and CatagoryId = {0}", catagory);
            string sSql = String.Format("Select * from FL_Flows where 1=1{0}{1}", con1, con2);

            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by sort", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
                WX.Flow.Model.Flow.GetCache(id).RemoveFromCaches();
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
    }
}