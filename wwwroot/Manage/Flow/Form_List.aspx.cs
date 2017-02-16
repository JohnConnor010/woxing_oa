using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.Manage.Flow
{
    public partial class Form_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.ddlType.Items.Add(new ListItem("--请选择--", ""));
                WX.Data.Dict.BindListCtrl_FormCatagory(this.ddlType,null, "#--请选择--", null);
                this.BindData(true);
            }
        }
        //绑定数据
        public void BindData(bool start)
        {
            string keyWords = this.tbKeyWords.Text;
            string catagory = this.ddlType.SelectedValue;
            //UI专用测试数据
            string con1 = null; if(!String.IsNullOrEmpty(keyWords))con1 = String.Format(" and [Name] like '%{0}%'", keyWords);
            string con2 = null; if (!String.IsNullOrEmpty(catagory)) con2 = String.Format(" and CatagoryId = {0}", catagory);
            string sSql = String.Format("Select * from vw_Forms where 1=1{0}{1}", con1, con2);
            
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

            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            string sSql = String.Format("select * from FL_RunDatas where FormId={0}", id);
            if (ULCode.QDA.XSql.IsHasRow(sSql))
            {
                ULCode.Debug.Alert(this, "此表单已经开始应用，不能删除！");
                return;
            }
            sSql = String.Format("Delete FL_Forms where Id={0}", id);
            int iR = ULCode.QDA.XSql.Execute(sSql);
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Flow.Model.Form.GetCache(id).RemoveFromCaches();
                WX.Main.AddLog(LogType.Default, String.Format("删除表单({0})成功！", id), "");
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                //重新绑定数据代码
                this.BindData(true);
            }
            else
            {
                ULCode.Debug.Alert(this, "删除表单失败！");
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
        public string GetFormPrivewUrl(object oEval)
        {
            return String.Format("javascript:PopupIFrame('Form_Preview.aspx?FormId={0}','预览表单','','',650,700)", oEval);
        }
    }
}