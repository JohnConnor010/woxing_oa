using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class Duty_Edit : System.Web.UI.Page
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
            int companyId = WX.Request.rCompanyId;
            int dutyId =WX.Request.rDutyId;
            if (!IsPostBack)
            {
                WX.Model.Duty.MODEL model = WX.Request.rDuty; //WX.Model.Duty.GetModel("select * from [TE_Duties] where ID="+Request["id"]);
                if (dutyId == 0)
                    this.DutyCatagory.Items.Add("无分类");
                else
                    WX.Data.Dict.BindListCtrl_DutyCatagory(DutyCatagory, null, null, model.DutyCatagoryID.ToString());
                DutyNO.Text = model.NO.value.ToString();
                DutyName.Text = model.Name.value.ToString();
                FORM_CONTENT.Value = model.Description.ToString();
                //DutyCatagory.SelectedValue = model.DutyCatagoryID.ToString();
                //ddlGradeId.SelectedValue = model.GradeID.ToString();
                WX.Data.Dict.BindListCtrl_DutyGrade(ddlGradeId, null, null, model.GradeID.ToString());
                btnSave.Visible = dutyId != 0;
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int companyId = WX.Request.rCompanyId;
            int Id = WX.Request.rDutyId;
            //2.取得用户变量
            int dutyNO = Convert.ToInt32(DutyNO.Text.Trim());
            string name = DutyName.Text.Trim();
            WX.Model.Duty.MODEL model = WX.Request.rDuty;// WX.Model.Duty.GetModel("select * from [TE_Duties] where ID=" + Request["id"]);
            //WX.Model.Duty.MODEL model2 = WX.Model.Duty.GetModel("select * from TE_Duties where ID!=" + Request["id"] + " and ID=" + DutyID.Text.Trim());

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (dutyNO != Id && WX.Model.Duty.Caches.Find(delegate(WX.Model.Duty.MODEL dele) { return dele.CompanyID.ToInt32() == companyId && dele.ID.ToInt32() == dutyNO; }) != null)
            {
                ULCode.Debug.AjaxAlert(this, "编辑职务失败,职务编号已存在！");
                return;
            }
            model.NO.set(dutyNO);
            model.Name.set(name);
            model.DutyCatagoryID.value = DutyCatagory.SelectedValue;
            model.GradeID.value = ddlGradeId.SelectedValue;
            model.Description.value = FORM_CONTENT.Value;
            //model.CompanyID.value = (Request["cmp"] != null && Request["cmp"] != "" ? Request["cmp"] : "11");
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (model.Update() != 0)
            {
                bDeal = true;
            }
            else
            {
                model.RestoreInitial();
            }
            //5.（用户及业务对象）统计与状态
            if (bDeal)
            {
                WX.Main.CurUser.LoadDutyUser(true);
            }
            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, "编辑职务信息成功！", "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "已成功修改职务信息!是否返回职务列表页？", "Duty_List.aspx?CompanyID=11", this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert(this,"编辑职务失败,请重试！");
            }
        }
    }
}