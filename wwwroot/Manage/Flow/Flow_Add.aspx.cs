using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Add : System.Web.UI.Page
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
                WX.Data.Dict.BindListCtrl_FlowCatagory(this.ddlFlowCategory, null, "#--请选择流程目录--", null);
                WX.Data.Dict.BindListCtrl_enum_FlowAuthorizeMode(this.ddlDelegateType, null, "#--请选择委托类型--", null);
                WX.Data.Dict.BindListCtrl_enum_FlowType(this.ddlFlowType, null, "#--请选择流程类型--", null);
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDepartment, null, "0#--系统管理员--", null);
                WX.Data.Dict.BindListCtrl_Forms(this.ddlForm, null, "#--请选择表单--", null);
                WX.Data.Dict.BindListCtrl_NumberRules(this.ddlNumberRules, null, "#--请选择流水号规则--", null);
            }
        }
        public void SubmitData(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string name = this.txtFlowName.Text;
            string desc = this.txtDesc.Text;
            string sort = this.txtSort.Text;
            bool allowView = this.chkAllowView.Checked;
            string allowUpload = this.ddlAllowUpload.SelectedValue;
            string dele = this.ddlDelegateType.SelectedValue;
            string dept = this.ddlDepartment.SelectedValue;
            string flowCatagory = this.ddlFlowCategory.SelectedValue;
            string flowType = this.ddlFlowType.SelectedValue;
            string form = this.ddlForm.SelectedValue;
            string numberRule = this.ddlNumberRules.SelectedValue;
            string viewUsers = this.hidden_users.Value;
            //lbAllowView.Items;
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。
            //ULCode.Debug.we(String.Format("已经收到<br/>id:{0}<br/>name:{1}<br/>type:{2}", id, name, dutyType));
            //ULCode.Debug.w(String.Format("已经收到<br/>name:{0}", name));
            //ULCode.Debug.w(String.Format("<br/>desc:{0}", desc));
            //ULCode.Debug.w(String.Format("<br/>sort:{0}", sort));
            //ULCode.Debug.w(String.Format("<br/>allowView:{0}", allowView));
            //ULCode.Debug.w(String.Format("<br/>dele:{0}", dele));
            //ULCode.Debug.w(String.Format("<br/>dept:{0}", dept));
            //ULCode.Debug.w(String.Format("<br/>flowCatagory:{0}", flowCatagory));
            //ULCode.Debug.w(String.Format("<br/>flowtype:{0}", flowType));
            //ULCode.Debug.w(String.Format("<br/>form:{0}", form));
            //ULCode.Debug.w(String.Format("<br/>numberRule:{0}", numberRule));
            //ULCode.Debug.we(String.Format("<br/>viewUsers:{0}", viewUsers));


            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            //填写主要业务逻辑代码
            WX.Flow.Model.Flow.MODEL f = WX.Flow.Model.Flow.NewDataModel();
            f.Name.set(name);
            f.Description.set(desc);
            f.Sort.set(sort);
            f.AllowView.set(allowView);
            f.AllowAttach.set(allowUpload == "1" ? true : false);
            f.AuthorizeMode.set(dele);
            f.DepartmentId.set(dept);
            f.CatagoryId.set(flowCatagory);
            f.Type.set(flowType);
            f.FormId.set(form);
            f.NumberRuleId.set(numberRule);
            f.ViewPriv.set(viewUsers);
            f.IsVisible.set(ddlIsVisible.SelectedValue);
            int iR = f.Insert(true);
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (iR > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "添加流程信息成功！", String.Format("{0}-{1}", iR, name));
            }

            //7.返回处理结果或返回其它页面。
            if (iR > 0)
            {
                f.SaveIntoCaches();
                ULCode.Debug.Confirm(this, "添加流程成功！是否继续添加？", this.Request.RawUrl, "Flow_List.aspx");
            }
            else
            {
                ULCode.Debug.Alert(this, "添加流程失败！");
            }
        }
    }
}