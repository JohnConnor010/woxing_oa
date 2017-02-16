using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.Manage.Flow
{
    public partial class Form_Modi : System.Web.UI.Page
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
                //初始化控件
                this.LoadData();
            }
        }
        private void LoadData()
        {
            int id = WX.Request.rFormID;
            WX.Flow.Model.Form.MODEL f = WX.Request.rForm; //WX.Flow.Model.Form.NewDataModel(id);
            //填充控件
            this.Name.Text = f.Name.ToString();
            WX.Data.Dict.BindListCtrl_FormCatagory(this.ddlCatagory,null, null, f.CatagoryId.ToString());
            WX.Data.Dict.BindListCtrl_DeptList(this.ddlDept,null, null,f.DepartmentId.ToString());            
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
            int id = WX.Request.rFormID;
            String name = Convert.ToString(this.Name.Text);
            string catagoryId = this.ddlCatagory.SelectedValue;
            string deptId = this.ddlDept.SelectedValue;

            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到<br/>name:{0}<br/>type:{1}<br/>dept:{2}", name, catagoryId, deptId));
            //return;

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            //填写主要业务逻辑代码
            WX.Flow.Model.Form.MODEL f = WX.Request.rForm; //WX.Flow.Model.Form.NewDataModel(id);
            f.Name.set(name);
            f.DepartmentId.set(deptId);
            f.CatagoryId.set(catagoryId);
            int iR = f.Update();

            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR!=0)
            {
                WX.Main.AddLog(LogType.Default, "修改表单信息成功！", String.Format("{0}-{1}", id, name));
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                ULCode.Debug.Confirm(this, "表单修改成功！是否返回列表？", "Form_List.aspx", this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert(this, "表单修改失败！");
            }
        }
    }
}