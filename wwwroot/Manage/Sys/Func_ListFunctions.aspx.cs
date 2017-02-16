using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WX;

namespace wwwroot.Manage.Sys
{
    public partial class Func_ListFunctions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            string sSql = "exec [dbo].[sp_get_tree_multi_table] 'TE_Functions','ID','Name','ParentID','OrderID',0,1,5";
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (e.CommandName == "del")
            {
                if (WX.Main.ExecuteDelete("TE_Functions", "ID", id) > 0)
                {
                    try
                    {
                        WX.Main.ExecuteDelete("TE_FunctionsInDuties", "FunctionID", id);
                        WX.Model.Function.GetCache(id).RemoveFromCaches();
                    WX.Main.AddLog(LogType.Default, String.Format("删除功能({0})成功！", id), "");
                    }
                    catch
                    {
                        ;
                    }
                }

            }
            else if (e.CommandName == "editstate")
            {
                if (WX.Main.ExcuteUpdate("TE_Functions", "State=" + (id.Split('|')[1] == "1" ? "0" : "1"), "ID=" + id.Split('|')[0]) > 0)
                {
                    WX.Model.Function.NewDataModel(id.Split('|')[0]).SaveIntoCaches();
                    WX.Main.AddLog(LogType.Default, String.Format("功能状态{1}({0})成功！", id, id.Split('|')[1] == "1"?"关闭":"打开"), "");
                }
            }

            //7.返回处理结果或返回其它页面。  
            pageinit();
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lbl_node_level");
                int degree = Convert.ToInt32(lbl.Text);
                string defaultColor = String.Empty;
                if (degree == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                    defaultColor = "whitesmoke";
                }
                e.Row.Attributes.Add("onmouseover", "style.backgroundColor='lightyellow';style.cursor='hand';");
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='" + defaultColor + "';style.cursor='';");
                e.Row.Cells[2].Text = (e.Row.Cells[2].Text.Trim() == "" || e.Row.Cells[2].Text.Trim() == "0" ? "免费" : WX.Model.Function.GetTypeName(Convert.ToInt32(e.Row.Cells[2].Text.Trim())))+"功能";
            }
        }
    }
}