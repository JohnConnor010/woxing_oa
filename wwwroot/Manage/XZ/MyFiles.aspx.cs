using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class MyFiles : System.Web.UI.Page
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
            string sSql = "Select XZ_NotifyFiles.*,RealName CategoryName from XZ_NotifyFiles left join TU_Users on XZ_NotifyFiles.UserID=TU_Users.UserID where XZ_NotifyFiles.UserID='" + WX.Main.CurUser.UserID + "'";
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 10;
                AspNetPager1.CurrentPageIndex = 1;
            }
            GridView1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by Istop desc, PublishTime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataBind();
        }
        //删除处理过程
        protected void Del(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandName);
            WX.XZ.NotifyFiles.MODEL filemodel=WX.XZ.NotifyFiles.NewDataModel(id);
            if(filemodel.RunID.ToInt32()>0)
            ULCode.QDA.XSql.Execute("delete from FL_Run where ID=" + filemodel.RunID.ToString() + ";delete from FL_RunFeedBack where RunId=" + filemodel.RunID.ToString());

            int iR = filemodel.Delete();
            //5.（用户及业务对象）统计与状态


            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            { 
                WX.Main.AddLog(WX.LogType.Default,"删除文件通知成功！",  String.Format("{0}（{1}）",filemodel.Title.ToString(), id));
                this.BindData(false);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除失败！");
            }
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