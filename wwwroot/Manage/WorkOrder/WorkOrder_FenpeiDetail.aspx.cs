using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_FenpeiDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        private void PageInit()
        {
            if (Request["OrderID"] != null && Request["OrderID"] != "")
            {
                WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);

                if (Request["POrderID"] != null && Request["POrderID"] != "")
                {
                    WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["POrderID"]);
                    Title_txt.Text = order.Title.ToString();
                    YJTime_txt.Text = order.YJTime.ToString();
                    Remarks_txt.Text = order.Remarks.ToString();
                    ExecUserID_li.Text = WX.CommonUtils.GetRealNameListByUserIdList(order.ExecUserID.ToString());
                    Count_txt.Text = order.Count.ToString();
                }
                else
                {
                    Title_txt.Text = porder.Title.ToString();
                    YJTime_txt.Text = porder.YJTime.ToString();
                    Remarks_txt.Text = porder.Remarks.ToString();
                    ExecUserID_li.Text = WX.CommonUtils.GetRealNameListByUserIdList(Request["UserID"]);
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Main.CurUser.LoadUserModel(false);
            WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel();
            if (Request["POrderID"] != null && Request["POrderID"] != "")
                order = WX.WorkOrder.Order.NewDataModel(Request["POrderID"]);
            else
            {
                order.ExecUserID.value = Request["UserID"];
                order.PID.value = Request["OrderID"];
            }
            order.DeptWorkID.value = WX.Main.CurUser.UserModel.DepartmentID.value;
            order.UserID.value = porder.UserID.value;
            order.AssignUserID.value = WX.Main.CurUser.UserID;
            order.Remarks.value = Remarks_txt.Text;
            order.Title.value = Title_txt.Text;
            order.Proj.value = porder.Proj.value;
            order.Type.value = porder.Type.value;
            order.Count.value = Count_txt.Text;
            order.State.value = 3;
            order.YJTime.value = YJTime_txt.Text;
            order.StateTime.value = DateTime.Now;
            if (Request["POrderID"] != null && Request["POrderID"] != "")
                order.Update();
            else
                order.Insert();
            if (porder.State.ToInt32() < 2)
            {
                porder.State.value = 2;
                porder.Update();
            }
            Response.Redirect("WorkOrder_Fenpei.aspx?OrderID=" + Request["OrderID"]);
        }
    }
}