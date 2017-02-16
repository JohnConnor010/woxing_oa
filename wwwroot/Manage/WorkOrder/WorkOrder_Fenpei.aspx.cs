using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_Fenpei : System.Web.UI.Page
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
                Title_li.Text = porder.Title.ToString();
                Proj_li.Text = WX.WorkOrder.Order.ProjStr[porder.Proj.ToInt32()];
                Type_li.Text = WX.WorkOrder.Order.TypeStr[porder.Type.ToInt32()];
                YJTime_li.Text = porder.YJTime.ToString();
                StateTime_li.Text = porder.StateTime.ToString();
                Remarks_li.Text = porder.Remarks.ToString();
               
            }
            UserBind();
        }
        private void UserBind()
        {
            WX.Main.CurUser.LoadUserModel(false);
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select tu.UserID,tu.RealName,orders.ID,orders.Count cCount from TU_Users tu left join WorkOrder_Orders orders on tu.UserID=orders.ExecUserID and orders.PID=" + Request["OrderID"] + " where DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString());
            DataList2.DataSource = dt;
            DataList2.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Main.CurUser.LoadUserModel(false);
            WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            WX.WorkOrder.Order.MODEL order;
            WX.WorkOrder.Dept.MODEL deptmodel = WX.WorkOrder.Dept.GetModel("select * from WorkOrder_Dept where WID=" + porder.ID.ToString() + " and DeptID=" + WX.Main.CurUser.UserModel.DepartmentID.value);
            if (deptmodel != null && deptmodel.SubTime.ToString() == "")
            {
                deptmodel.SubTime.value = DateTime.Now;
                deptmodel.Update();
            }
            for (int i = 0; i < DataList2.Items.Count; i++)
            {
                order = WX.WorkOrder.Order.NewDataModel();
                CheckBox cbox = (CheckBox)DataList2.Items[i].FindControl("CheckBox1");
                HiddenField hf = (HiddenField)DataList2.Items[i].FindControl("HiddenField1");
                TextBox tbox = (TextBox)DataList2.Items[i].FindControl("TextBox1");
                if (cbox.Checked)
                {
                    if (hf.Value == "")
                    {
                        order.PID.value = Request["OrderID"];
                        order.DeptWorkID.value = WX.Main.CurUser.UserModel.DepartmentID.value;
                        order.UserID.value = porder.UserID.value;
                        order.AssignUserID.value = WX.Main.CurUser.UserID;
                        order.ExecUserID.value = cbox.ToolTip;
                        order.Remarks.value = porder.Remarks.ToString();
                        order.Title.value = porder.Title.ToString();
                        order.Proj.value = porder.Proj.value;
                        order.Type.value = porder.Type.value;
                        order.Count.value = tbox.Text;
                        order.State.value = 3;
                        order.SubTime.value = DateTime.Now;
                        order.YJTime.value = porder.YJTime.value;
                        order.StateTime.value = DateTime.Now;
                        order.Insert();
                    }
                    else
                    {
                        order = WX.WorkOrder.Order.NewDataModel(hf.Value);
                        order.Count.value = tbox.Text;
                        order.StateTime.value = DateTime.Now;
                        order.Update();
                    }
                }
                else
                {
                    if (hf.Value != "")
                    {
                        WX.Main.ExecuteDelete("WorkOrder_Orders","ID",hf.Value);
                    }
                }
            }
            System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable("select (select count(*) from WorkOrder_Dept where WID=" + porder.ID.ToString() + ") ccount,(select count(*) from WorkOrder_Dept where WID=" + porder.ID.ToString() + " and State>=2) scount");
            if (dt2.Rows[0]["ccount"].ToString() == dt2.Rows[0]["scount"].ToString()&&porder.State.ToInt32() < 2)
            {
                porder.State.value = 2;
                porder.Update();
            }
            Response.Write("<script type=\"text/javascript\">window.parent.Dialog.close(); </script>");
        }
    }
}