using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using WX.Ass;
using WX;
using ULCode.QDA;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_AddEquipment : System.Web.UI.Page
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
                InitComponent();
            }
        }
        private void InitComponent()
        {
            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
            this.txtInputDate.Text = DateTime.Now.ToString("yyyy-M-d");
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
            string departmentId = this.ddlDepartment.SelectedItem.Value;
            string userId = this.hidden_ddlUser.Value;
            string inputDate = this.txtInputDate.Text.Trim();
            string productId = this.txtProductID.Text.Trim();
            string price = this.txtPrice.Text.Trim();
            string unit = this.txtUnit.Text.Trim();
            string quantity = this.txtQuantity.Text.Trim();
            string remark = this.txtRemark.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            Equipment.MODEL equipmentModel = Equipment.NewDataModel();
            equipmentModel.DepartmentID.value = departmentId;
            equipmentModel.UserID.value = userId;
            equipmentModel.AddDate.value = inputDate;
            equipmentModel.ProductID.value = productId;
            equipmentModel.Price.value = price;
            equipmentModel.Unit.value = unit;
            equipmentModel.Quantity.value = quantity;
            equipmentModel.Remark.value = remark;
            //填写主要业务逻辑代码

            int row = equipmentModel.Save();
            //5.（用户及业务对象）统计与状态

            if (row > 0)
            {
                XSql.Execute("EXEC Assets_AddConsuming " + this.PID.Value + "," + this.txtQuantity.Text + ",'" + this.txtProductID.Text + "'");
                //6.登记日志
                WX.Main.AddLog(LogType.Default, "个人装备录入成功！", null);
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert("个人装备录入成功！", "Ass_EquipmentList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("个人装备录入失败！", "Ass_EquipmentList.aspx");
            }
        }
    }
}