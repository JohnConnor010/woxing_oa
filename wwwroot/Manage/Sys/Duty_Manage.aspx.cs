using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Func_Manage : System.Web.UI.Page
    {
        string offstr = "off";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(Request.Url.Query))
                {
                    Response.Redirect(String.Format("{0}?CompanyId={1}", this.Request.Url, WX.Main.DefaultCompanyId), true);
                    return;
                }
                WX.Data.Dict.BindListCtrl_DutyCatagory(DutyCatagory, null, null, null);
                WX.Data.Dict.BindListCtrl_DutyGrade(ddlGradeId, null, null, null);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //WX.Model.Duty.MODEL model = WX.Model.Duty.GetModel("select * from TE_Duties where ID=" + DutyID.Text.Trim());
            //1.验证用户权限

            //2.取得用户变量
            int companyId =WX.Request.rCompanyId;
            int dutyNo = Convert.ToInt32(DutyNO.Text.Trim());
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (WX.Model.Duty.Caches.Find(delegate(WX.Model.Duty.MODEL dele) { return dele.CompanyID.ToInt32() == companyId && dele.NO.ToInt32() == dutyNo; }) != null)
            {
                ULCode.Debug.AjaxAlert(this, "新增职务失败,职务编号已存在！");
                return;
            }
            WX.Model.Duty.MODEL model = WX.Model.Duty.NewDataModel();
            model.NO.value = dutyNo;
            model.Name.value = DutyName.Text.Trim();
            model.CompanyID.value = companyId;
            model.DutyCatagoryID.value = DutyCatagory.SelectedValue;
            model.GradeID.value = ddlGradeId.SelectedValue;
            model.Description.value = FORM_CONTENT.Value;
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (model.Insert(true) > 0)
            {
                model.SaveIntoCaches();
                bDeal = true;
            }
            else
            {
                model = null;
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "添加职务信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "成功添加职务信息，是否继续添加？", this.Request.RawUrl, "Duty_List.aspx?CompanyID="+companyId);
                //Response.Redirect("Duty_List.aspx?CompanyID=11");
            }
            else
            {
                ULCode.Debug.Alert("新增职务失败,请重试！");
            }
        }
    }
}