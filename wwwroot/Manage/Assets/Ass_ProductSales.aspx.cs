using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_ProductSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtOpTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtOpUserName.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.Authentication.GetUserID());
                this.txtOpUserID.Value = WX.Authentication.GetUserID();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string type = "出售";
            string opUserID = this.txtOpUserID.Value;
            string opTime = this.txtOpTime.Text.Trim();
            string opIP = Request.UserHostAddress;
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
                    XSql.Execute("EXEC Assets_ProductSales " + this.PID.Value + "," + this.txtQuantity.Text.Trim());
                    if (row > 0)
                    {
                        WX.Main.AddLog(WX.LogType.Default, "产品出售成功！", null);
                    }
                }
            }
            if (singleRow > 0)
            {
                ULCode.Debug.Confirm("产品出售成功！", "Ass_ProductSales.aspx", "Ass_LogList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("产品出售失败！", "Ass_ProductSales.aspx");
            }
        }
    }
}