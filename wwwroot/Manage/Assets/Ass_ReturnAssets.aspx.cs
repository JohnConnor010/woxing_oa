using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using ULCode.QDA;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_ReturnAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dict.BindListCtrl_DeptList(this.ddlDepartment, null, null, null);
                this.txtOpTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtOpUserName.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.Authentication.GetUserID());
                this.txtOpUserID.Value = WX.Authentication.GetUserID();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string type = "归还";
            string opUserID = this.txtOpUserID.Value;
            string opTime = this.txtOpTime.Text.Trim();
            string opIP = Request.UserHostAddress;
            string userId = this.hidden_ddlUser.Value;
            string departmentId = this.ddlDepartment.SelectedItem.Value;
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
                    XSql.Execute("EXEC Assets_UpdateQuantity " + this.PID.Value + "," + this.txtQuantity.Text + ", '" + this.txtProductID.Text + "','---"+content+"'");
                    if (row > 0)
                    {
                        WX.Main.AddLog(WX.LogType.Default, "产品归还成功！", null);
                    }
                }
            }
            if (singleRow > 0)
            {
                ULCode.Debug.Confirm("产品归还成功！", "Ass_ReturnAssets.aspx", "Ass_ConsumingList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("产品归还失败！", "Ass_ReturnAssets.aspx");
            }
        }
    }
}