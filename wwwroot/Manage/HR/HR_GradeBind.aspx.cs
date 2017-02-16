using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.HR
{
    public partial class HR_GradeBind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {//1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select ID,Name,GradeID from TE_Duties");
            ListItem li = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                li = new ListItem(dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ID"].ToString());
                CheckBoxList1.Items.Add(li);
                if (dt.Rows[i]["GradeID"].ToString() == Request["id"])
                li.Selected = true;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    ULCode.QDA.XSql.Execute("update TE_Duties set GradeID="+Request["id"]+" where ID="+CheckBoxList1.Items[i].Value);
                }
            }
            WX.Main.AddLog(WX.LogType.Default, "级别管理 >> 绑定职务操作", "");
            Response.Redirect("HR_Grade.aspx");
        }
    }
}