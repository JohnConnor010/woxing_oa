using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class NotifyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
             string sSql = "Select XZ_Notify.*,RealName,XZ_NotifyCategory.Name CategoryName from XZ_Notify left join TU_Users on XZ_Notify.UserID=TU_Users.UserID left join XZ_NotifyCategory on XZ_Notify.CategoryID=XZ_NotifyCategory.ID";
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by Istop desc, Starttime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            string sSql = String.Format("Delete XZ_Notify where id={0}", id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除公告({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                this.BindData(false);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除失败！");
            }
        }
        protected void edittime(object sender, EventArgs e)
        {
           
            //2.取得用户变量
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument);
            WX.XZ.Notify.MODEL model = WX.XZ.Notify.GetModel(id);
            string str = "";
            if (lb.Text == "终止")
            {
                model.Stoptime.value = DateTime.Now.ToString("yyyy-MM-dd");
                str = "终止";
                model.Update();
            }
            else if (lb.Text == "立即生效")
            {
                model.Starttime.value = DateTime.Now.ToString("yyyy-MM-dd");
                str = "生效";
                model.Update();
            }
            else
            {
                WX.XZ.Notify.SetState(model);
                str = "生效";
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
           
                WX.Main.AddLog(WX.LogType.Default, String.Format("公告信息({0}-"+model.Title.ToString()+")"+str+"！", id), "");
                this.BindData(false);
            //7.返回处理结果或返回其它页面。
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }
    }
}