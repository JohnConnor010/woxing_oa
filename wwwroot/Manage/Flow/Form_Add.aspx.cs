using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow;

namespace wwwroot.Manage.Flow
{
    public partial class Form_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_FormCatagory(this.ddlCatagory,null, null, null);
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDept,null, null, null);
            }
        }
        protected void SubmitData(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            String name = Convert.ToString(this.Name.Text);
            string catagoryId = this.ddlCatagory.SelectedValue;
            string deptId = this.ddlDept.SelectedValue;

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            //填写主要业务逻辑代码
            WX.Flow.Model.Form.MODEL f = WX.Flow.Model.Form.NewDataModel();
            f.Name.set(name);
            f.DepartmentId.set(deptId);
            f.CatagoryId.set(catagoryId);
            int newId = f.Insert(true);
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (newId > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "添加用户信息成功！", String.Format("{0}-{1}", newId, name));
            }

            //7.返回处理结果或返回其它页面。
            if (newId > 0)
            {
                f.SaveIntoCaches();
                Response.Redirect(String.Format("Form_Module.aspx?id={0}", newId));
                //ULCode.Debug.Confirm(this, "添加用户成功！是否继续添加？", this.Request.RawUrl, "List.aspx");
            }
            else
            {
                ULCode.Debug.Alert(this, "添加用户失败！");
            }
        }
    }
}