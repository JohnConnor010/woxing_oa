using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_Add : System.Web.UI.Page
    {
        private WX.WXUser CurUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CurUser = WX.Main.CurUser;
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        private void PageInit()
        {
            BindDate();
        }
        private void BindDate()
        {
            for (int i = 0; i < WX.WorkOrder.Order.TypeStr.Length; i++)
            {
                Type_drop.Items.Add(new ListItem(WX.WorkOrder.Order.TypeStr[i], i.ToString()));
            }
            for (int i = 0; i < WX.WorkOrder.Order.ProjStr.Length; i++)
            {
                Proj_drop.Items.Add(new ListItem(WX.WorkOrder.Order.ProjStr[i], i.ToString()));
            }
            if (Request["OrderID"] != null && Request["OrderID"] != "")
            {
                WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
                Remarks_txt.Text = order.Remarks.ToString();
                Title_txt.Text = order.Title.ToString();
                Proj_drop.SelectedValue = order.Proj.ToString();
                Type_drop.SelectedValue = order.Type.ToString();
                YJTime_txt.Text = order.YJTime.ToString() != "" ? Convert.ToDateTime(order.YJTime.ToString()).ToString("yyyy-MM-dd") : "";
                if (order.DeptWorkID.ToString() == "")
                    otherDept.SelectedValue = "1";

                DeptBind();
                if (otherDept.SelectedValue == "-1")
                {
                    //WX.Main.CurUser.LoadUserModel(false);
                    //System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select ExecUserID from WorkOrder_Orders where PID=" + Request["OrderID"]);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    Dept_check.Items.FindByValue(dt.Rows[i]["ExecUserID"].ToString()).Selected = true;
                    //}
                    Dept_check.Visible = false;
                    DataList2.Visible = true;
                    UserBind();

                }
                else
                {
                    Dept_check.Visible = true;
                    DataList2.Visible = false;
                    string sSql = " select * from WorkOrder_Dept where WID=" + order.ID.ToString();
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Dept_check.Items.FindByValue(dt.Rows[i]["DeptID"].ToString()).Selected = true;
                    }
                }
            }
            else
            {
                DeptBind();
            }
        }
        private void DeptBind()
        {
            Dept_check.Items.Clear();
            if (otherDept.SelectedValue == "-1")
            {
                Dept_check.Visible = false;
                DataList2.Visible = true;
                UserBind();
            }
            else
            {
                Dept_check.Visible = true;
                DataList2.Visible = false;
                Dept_check.DataSource = ULCode.QDA.XSql.GetDataTable("select ID,Name from TE_Departments");
                Dept_check.DataTextField = "Name";
                Dept_check.DataValueField = "ID";
                Dept_check.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            EditOrder(0);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EditOrder(1);
        }
        private int IsNUM(string str)
        { 
        
            try
            { 
                return Convert.ToInt32(str);
            }catch{
                return 0;
            }
        }
        private void EditOrder(int state)
        {
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel();
            if (Request["OrderID"] != null && Request["OrderID"] != "")
                order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            order.UserID.value = this.CurUser.UserID;
            order.Remarks.value = Remarks_txt.Text;
            order.Title.value = Title_txt.Text;
            order.Proj.value = Proj_drop.SelectedValue;
            order.Type.value = Type_drop.SelectedValue;
            order.State.value = state;
            if (state == 1)
                order.SubTime.value = DateTime.Now;
            if (YJTime_txt.Text.Trim() != "")
                order.YJTime.value = YJTime_txt.Text;
            order.StateTime.value = DateTime.Now;
            int id = 0;
            if (otherDept.SelectedValue == "-1")
            {
                order.DeptWorkID.value = -1;
            }
            if (Request["OrderID"] != null && Request["OrderID"] != "")
            {
                id = order.ID.ToInt32();
                order.Update();
                //WX.Main.ExecuteDelete("WorkOrder_Dept", "WID", id.ToString());
            }
            else
                id = order.Insert(true);
            WX.WorkOrder.Dept.MODEL dept;
            if (otherDept.SelectedValue == "-1")
            {
                WX.Main.ExecuteDelete("WorkOrder_Orders", "PID", id.ToString());
                this.CurUser.LoadUserModel(false);
                if (!(Request["OrderID"] != null && Request["OrderID"] != ""))
                {
                    dept = WX.WorkOrder.Dept.NewDataModel();
                    dept.WID.value = id;
                    dept.DeptID.value = this.CurUser.UserModel.DepartmentID.ToString();
                    if (state == 1)
                        dept.SubTime.value = DateTime.Now;
                    dept.Insert();
                }
                else
                {
                    dept = WX.WorkOrder.Dept.GetModel("select * from WorkOrder_Dept where WID=" + id + " and DeptID=" + this.CurUser.UserModel.DepartmentID.value);
                    if (state == 1 && dept.SubTime.ToString() == "")
                    {
                        dept.SubTime.value = DateTime.Now;
                        dept.Update();
                    }
                }
                WX.WorkOrder.Order.MODEL corder;

                for (int i = 0; i < DataList2.Items.Count; i++)
                {
                    corder = WX.WorkOrder.Order.NewDataModel();
                    CheckBox cbox = (CheckBox)DataList2.Items[i].FindControl("CheckBox1");
                    HiddenField hf = (HiddenField)DataList2.Items[i].FindControl("HiddenField1");
                    TextBox tbox = (TextBox)DataList2.Items[i].FindControl("TextBox1");
                    if (cbox.Checked)
                    {
                        corder.PID.value = id;
                        corder.DeptWorkID.value = this.CurUser.UserModel.DepartmentID.value;
                        corder.UserID.value = order.UserID.value;
                        corder.AssignUserID.value = this.CurUser.UserID;
                        corder.ExecUserID.value = cbox.ToolTip;
                        corder.Remarks.value = order.Remarks.ToString();
                        corder.Title.value = order.Title.ToString();
                        corder.Proj.value = order.Proj.value;
                        corder.Type.value = order.Type.value;
                        corder.Count.value =IsNUM(tbox.Text);
                        corder.State.value = 3;
                        if (state == 1)
                            corder.SubTime.value = DateTime.Now;
                        if (order.YJTime.ToString() != "")
                            corder.YJTime.value = order.YJTime.value;
                        corder.StateTime.value = DateTime.Now;
                        corder.Insert();
                    }
                }
            }
            else
            {
                WX.Main.ExecuteDelete("WorkOrder_Dept", "WID", id.ToString());
                for (int i = 0; i < Dept_check.Items.Count; i++)
                {
                    if (Dept_check.Items[i].Selected)
                    {
                        dept = WX.WorkOrder.Dept.NewDataModel();
                        dept.WID.value = id;
                        dept.DeptID.value = Dept_check.Items[i].Value;
                        if (state == 1)
                            dept.SubTime.value = DateTime.Now;
                        dept.Insert();
                    }
                }
            }
            Response.Redirect("WorkOrder_List.aspx");
        }
        private void UserBind()
        {
            this.CurUser.LoadUserModel(false);
            System.Data.DataTable dt;
            if (Request["OrderID"] != null && Request["OrderID"] != "")
                dt = ULCode.QDA.XSql.GetDataTable("select tu.UserID,tu.RealName,orders.ID,orders.Count cCount from TU_Users tu left join WorkOrder_Orders orders on tu.UserID=orders.ExecUserID and orders.PID=" + Request["OrderID"] + " where DepartmentID=" + this.CurUser.UserModel.DepartmentID.ToString());
            else
                dt = ULCode.QDA.XSql.GetDataTable("select UserID,RealName,'' ID,0 cCount from TU_Users where DepartmentID=" + this.CurUser.UserModel.DepartmentID.ToString());

            DataList2.DataSource = dt;
            DataList2.DataBind();
        }
        protected void otherDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeptBind();
        }
    }
}