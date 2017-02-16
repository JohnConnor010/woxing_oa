using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using ULCode;
using WX;
using ULCode.QDA;

namespace wwwroot.Manage.Sys
{
	public partial class Dept_EditDepartment : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End(); 
            }
            if (!IsPostBack)
            {
                int id = WX.Request.rDepartmentId;
                int companyId = WX.Request.rCompanyId;
                if (id>0)
                {
                    //string cmdText = "SELECT * FROM TE_Departments WHERE ID=" + id + " AND CompanyId=" + companyId;
                    Department.MODEL department = WX.Request.rDepartment; //Department.GetModel(cmdText);
                    Company.MODEL company = WX.Request.rCompany;
                    this.CompanyName.Text = company.Name.ToString(); //XSql.GetValue("SELECT Name FROM TE_Companys WHERE ID=" + companyId).ToString();
                    this.txtDepartmentNO.Text = department.NO.f("{0}");
                    WX.Data.Dict.BindListCtrl_DeptList(this.ddlParentId, null, "0#无上级部门", department.ParentID.ToString());
                    this.txtDepartmentName.Text = department.Name.ToString();
                    this.txtPhone.Text = department.Tel.ToString();
                    this.txtFax.Text = department.Fax.ToString();
                    this.txtAddress.Text = department.Address.ToString();
                    this.txtSort.Text = department.Sort.ToString();
                    this.txtContent.Text = department.Content.ToString();
                    this.chkIsSubOrgan.Checked = string.IsNullOrEmpty(department.IsSubOrgan.ToString()) ? false : true;
                    this.hidden_txtHost.Value = department.Host.ToString();
                    this.txtHost.Text = CommonUtils.GetRealNameListByUserIdList(department.Host.ToString());
                    this.hidden_txtSubHosts.Value = department.SubHosts.ToString();
                    this.txtSubHosts.Text=CommonUtils.GetRealNameListByUserIdList(department.SubHosts.ToString());

                    this.hidden_txtAssistants.Value = department.Assistants.ToString();
                    this.txtAssistants.Text = CommonUtils.GetRealNameListByUserIdList(department.Assistants.ToString());

                    this.hidden_txtUpHost.Value = department.UpHost.ToString();
                    this.txtUpHost.Text = CommonUtils.GetRealNameListByUserIdList(department.UpHost.ToString());

                    this.hidden_txtUpSubHosts.Value = department.UpSubHosts.ToString();
                    this.txtUpSubHosts.Text = CommonUtils.GetRealNameListByUserIdList(department.UpSubHosts.ToString());
                }
                else
                {
                    Response.Write("部门编号格式不正确！");
                    Response.End();
                    return;
                }
            }
		}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            //3.验证用户变量
            int id = WX.Request.rDepartmentId;
            int companyId = WX.Request.rCompanyId;
                //4.处理业务
                Department.MODEL department = WX.Request.rDepartment;
                //List<Department.MODEL> childs = department.GetChilds();
                //Department.GetModel("SELECT * FROM TE_Departments WHERE ID=" + id);
                department.NO.set(this.txtDepartmentNO.Text);
                department.ParentID.value = this.ddlParentId.Text;
                department.Name.value = this.txtDepartmentName.Text.Trim();
                department.Tel.value = this.txtPhone.Text.Trim();
                department.Fax.value = this.txtFax.Text.Trim();
                department.Address.value = this.txtAddress.Text.Trim();
                department.Content.value = this.txtContent.Text.Trim();
                department.Sort.value = this.txtSort.Text.Trim();

                department.IsSubOrgan.value = Convert.ToInt32(this.chkIsSubOrgan.Checked);
                if (string.IsNullOrEmpty(this.hidden_txtHost.Value))
                {
                    department.Host.set(Convert.DBNull);
                }
                else
                {
                    department.Host.value = this.hidden_txtHost.Value;
                }
                department.SubHosts.value = this.hidden_txtSubHosts.Value;
                department.Assistants.value = this.hidden_txtAssistants.Value;
                if (string.IsNullOrEmpty(this.hidden_txtUpHost.Value))
                {
                    department.UpHost.set(Convert.DBNull);
                }
                else
                {
                    department.UpHost.value = this.hidden_txtUpHost.Value;
                }
                department.UpSubHosts.value = this.hidden_txtUpSubHosts.Value;
                //bool idModified = department.ID.updated;
                int row = department.Update();
                department.SaveIntoCaches();
                if (row != 0)
                {
                    //if (idModified)
                    //{
                        
                    //    if (childs != null)
                    //    {
                    //        foreach (Department.MODEL dm in childs)
                    //        {
                    //            dm.ParentID.set(department.ID);
                    //            dm.Update();
                    //        }
                    //    }
                    //}
                    //5.（用户及业务对象）统计与状态
                    //WX.Main.CurUser.LoadMyDepartment(true);
                    //6.登记日志
                    WX.Main.AddLog(LogType.Default, "部门信息修改成功！", "");
                    //7.返回页面
                    //if (idModified)
                    //    Response.Redirect("Dept_DepartmentList.aspx?companyId=" + Request.QueryString["Companyid"], true);
                    //else
                        ULCode.Debug.Confirm(this, "部门信息修改成功！是否返回部门列表页？", "Dept_DepartmentList.aspx?companyId=" + Request.QueryString["Companyid"], this.Request.RawUrl);
                }
                else
                {
                    department.RestoreInitial();
                    ULCode.Debug.Alert(this, "部门信息修改失败！可能是重复添加！");
                }
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dept_EditDepartment.aspx");
        }
	}
}