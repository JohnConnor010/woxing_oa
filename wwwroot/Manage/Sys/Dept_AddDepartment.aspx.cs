using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
using ULCode;
using WX;

namespace wwwroot.Manage.Sys
{
	public partial class Dept_AddDepartment : System.Web.UI.Page
	{
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
                int companyId = Convert.ToInt32(Request.QueryString["CompanyId"]);
                int parentDeptId =WX.Request.rDepartmentId;
                //List<Company.MODEL> companys = Company.Caches;
                //foreach (Company.MODEL company in companys)
                //{
                //    this.ddlCompany.Items.Add(new ListItem(company.Name.ToString(), company.ID.ToString()));
                //};
                WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany, null, null, companyId.ToString());
                this.ddlCompany.Disabled = true;
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlParentId, null, "0#无上级部门", parentDeptId.ToString());
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
            //2.获取用户变量
            string companyId = this.ddlCompany.Value;
            string departmentNO = this.txtDepartmentNO.Text;
            string departmentName = this.txtDepartmentName.Text;
            string telephone = this.txtPhone.Text;
            string fax = this.txtFax.Text;
            string parentId = this.ddlParentId.Text;
            string address = this.txtAddress.Text;
            string sort = this.txtSort.Text;
            string content = this.txtContent.Text;
            int isSubOrgan = Convert.ToInt32(this.chkIsSubOrgan.Checked);
            string host = this.hidden_txtHost.Value;
            string subHosts = this.hidden_txtSubHosts.Value;
            string assistants = this.hidden_txtAssistants.Value;
            string upHost = this.hidden_txtUpHost.Value;
            string upSubHosts = this.hidden_txtUpSubHosts.Value;
            
            //3.验证用户变量

            //4.处理业务
            WX.Model.Department.MODEL department = WX.Model.Department.NewDataModel();
            department.CompanyID.value = companyId;
            department.NO.value = departmentNO;
            department.ParentID.value = parentId;
            department.Name.value = departmentName;
            department.Tel.value = telephone;
            department.Fax.value = fax;            
            department.Address.value = address;
            department.Content.value = content;
            department.IsSubOrgan.value = isSubOrgan;
            if (!string.IsNullOrEmpty(host))
            {
                department.Host.value = host;
            }
            department.SubHosts.value = subHosts;
            department.Assistants.value = assistants;
            if (!string.IsNullOrEmpty(upHost))
            {
                department.UpHost.value = upHost;
            }
            department.UpSubHosts.value = upSubHosts;
            int row = department.Insert(true);
            //5.（用户及业务对象）统计与状态
            if (row > 0)
            {
                department.SaveIntoCaches();
                //6.登记日志
                WX.Main.AddLog(LogType.Default, "部门信息添加成功！", "");
                //7.跳转页面
                Debug.Confirm(this, "部门信息添加成功！是否继续添加?？", this.Request.RawUrl, "Dept_DepartmentList.aspx?companyId=11");
            }
            else
            {
                Debug.Alert(this, "部门信息添加失败！可能是重复添加！");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dept_AddDepartment.aspx");
        }
	}
}