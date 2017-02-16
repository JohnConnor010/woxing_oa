using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (WX.Request.rProjectId != 0)
                {
                    WX.PRO.Project.MODEL model = WX.PRO.Project.GetModel("select * from PRO_Projects where ID=" + WX.Request.rProjectId);
                    if (model != null)
                    {
                        Literal1.Text = model.ProjectName.ToString();
                        Literal2.Text = model.ProjectName.ToString();
                    }
                    ReBind();
                }
            }
        }
        private void ReBind()
        {
            string sql = "SELECT * FROM PRO_Process where ProjID=" + WX.Request.rProjectId + " order by NO asc";
            var supplierData =ULCode.QDA.XSql.GetDataTable(sql);
            this.SupplierRepeater.DataSource = supplierData;
            this.SupplierRepeater.DataBind();
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();
            if(e.CommandName=="del")
            {
                WX.PRO.Process.MODEL model = WX.PRO.Process.GetModel("SELECT * FROM PRO_Process where ID=" + Id);
                int row = ULCode.QDA.XSql.Execute("DELETE FROM PRO_Process WHERE ID=" + Id);
            if (row > 0)
            {
                WX.PRO.Log.AddLog(4, Convert.ToInt32(model.ProjID.ToString()), Literal1.Text + "-删除步骤。", Request.UserHostAddress);
                ReBind();
            }
            }else{
                WX.PRO.Process.MODEL model = WX.PRO.Process.GetModel("SELECT * FROM PRO_Process where ID=" + Id);
                ui_id.Value = model.ID.ToString();
                ui_NO.Text = model.NO.ToString();
                ui_Persons.Text = model.Persons.ToString();
                ui_Days.Text = model.Days.ToString();
                ui_Percnt.Text = model.Percnt.ToString();
                ui_Percnttime.Text = model.Percnttime.ToString();
                ui_demo.Text = model.Demo.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            WX.PRO.Process.MODEL model = WX.PRO.Process.NewDataModel();
            if(ui_id.Value!="")
            model = WX.PRO.Process.GetModel("SELECT * FROM PRO_Process where ID=" + ui_id.Value);
            model.NO.value = ui_NO.Text;
            model.Persons.value = ui_Persons.Text;
            model.Days.value = ui_Days.Text;
            model.Percnt.value = ui_Percnt.Text;
            model.Percnttime.value = ui_Percnttime.Text;
            model.Demo.value = ui_demo.Text;
            //4.业务处理过程
            string logstr = "-添加";
            if (ui_id.Value != "")
            {
                logstr = "-修改";
                model.Update();
            }
            else
            {
                model.ProjID.value = WX.Request.rProjectId;
                model.State.value = 0;
                model.Insert();
            }
            //5.登记日志
            WX.PRO.Log.AddLog(4, Convert.ToInt32(model.ProjID.ToString()), Literal1.Text + logstr + "第" + model.NO.ToString() + "步", Request.UserHostAddress);
            ui_id.Value = "";
            ui_NO.Text = "";
            ui_Persons.Text = "";
            ui_Days.Text = "";
            ui_Percnt.Text = "";
            ui_Percnttime.Text = "";
            ui_demo.Text = "";
            //6.返回处理结果。
            ReBind();
        }
    }
}