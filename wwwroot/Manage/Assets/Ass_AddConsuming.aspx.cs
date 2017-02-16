using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using ULCode.QDA;
using WX.Ass;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_AddConsuming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string deptid = null;
            if (Request["mes"] != null)
            {
                WX.Model.User.MODEL usermodel = WX.Request.rUser;
                deptid = usermodel.DepartmentID.ToString();
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%Ass_AddConsuming.aspx?UserID=" + WX.Request.rUserId + "%'", WX.Main.CurUser.UserID, WX.Request.rUserId));
            }
            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, deptid);
            this.txtOpTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtOpUserName.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.Authentication.GetUserID());
            this.txtOpUserID.Value = WX.Authentication.GetUserID();
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string type = "领用";
            string opUserID = this.txtOpUserID.Value;
            string opTime = this.txtOpTime.Text.Trim();
            string opIP = Request.UserHostAddress;
            string userId = this.hidden_ddlUser.Value;
            string departmentId = this.ddlDepartment.SelectedItem.Value;
            //string deadline = this.txtDeadline.Text.Trim();
            string maturityDate = this.hidden_MaturityDate.Value;
            string quantity = this.txtQuantity.Text.Trim();
            string productID = this.txtProductID.Text.Trim();
            string content = this.txtRemarks.Text.Trim();
            string unit = this.txtUnit.Text.Trim();
            string price = this.txtPrice.Text.Trim();
            WX.Ass.Log.MODEL logModel = WX.Ass.Log.NewDataModel();
            logModel.Type.value = type;
            logModel.OpUserID.value = opUserID;
            logModel.OpTime.value = opTime;
            logModel.OpIP.value = opIP;
            logModel.UserID.value = userId;
            logModel.DepartmentID.value = departmentId;
            //logModel.Deadline.value = deadline;
            if (string.IsNullOrEmpty(maturityDate))
            {
                logModel.MaturityDate.set(DBNull.Value);
            }
            else
            {
                logModel.MaturityDate.value = maturityDate;
            }
            logModel.Quantity.value = quantity;
            logModel.ProductID.value = productID;
            logModel.Content.value = content;
            logModel.Unit.value = unit;
            logModel.Price.value = price;
            int singleRow = logModel.Save();
            int row = 0;
            if (singleRow > 0)
            {
                if (this.PID.Value != "0")
                {
                    XSql.Execute("EXEC Assets_AddConsuming " + this.PID.Value + "," + this.txtQuantity.Text + ",'" + this.txtProductID.Text + "'");
                    Equipment.MODEL equipmentModel = Equipment.NewDataModel();
                    equipmentModel.DepartmentID.value = departmentId;
                    equipmentModel.UserID.value = userId;
                    equipmentModel.ProductID.value = productID;
                    equipmentModel.Quantity.value = quantity;
                    equipmentModel.AddDate.value = opTime;
                    equipmentModel.Price.value = price;
                    equipmentModel.Unit.value = unit;
                    equipmentModel.Remark.value = content;
                    equipmentModel.Save();
                    if (row > 0)
                    {
                        WX.Main.AddLog(WX.LogType.Default, "产品领用添加成功！", null);
                    }
                }
            }
            if (singleRow > 0)
            {
                ULCode.Debug.Confirm("产品领用添加成功！", "Ass_AddConsuming.aspx", "Ass_AssetsList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("产品领用添加失败！", "Ass_AddConsuming.aspx");
            }
        }
    }
}