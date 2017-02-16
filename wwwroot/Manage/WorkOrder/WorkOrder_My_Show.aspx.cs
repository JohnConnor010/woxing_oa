using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_My_Show : System.Web.UI.Page
    {
        public int state = 0;
        private WX.WXUser CurUser= null;
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
            if (Request["OrderID"] != null && Request["OrderID"] != "")
            {
                WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
                YJTime_my.Text = order.YJTime.ToString();
                FPTime_my.Text = WX.CommonUtils.GetRealNameListByUserIdList(order.AssignUserID.ToString()) + "&nbsp;&nbsp;" + order.FPTime.ToString();
                AddTime_my.Text = order.AddTime.ToString();
                YSTime_my.Text = order.YSTime.ToString();
                StopTime_my.Text = order.StopTime.ToString();
                Count_my.Text = order.Count.ToString();
                State_my.Text = WX.WorkOrder.Order.StateStr[order.State.ToInt32()];
                WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(order.PID.ToString());
                Title_li.Text = porder.Title.ToString();
                Proj_li.Text = WX.WorkOrder.Order.ProjStr[porder.Proj.ToInt32()];
                Type_li.Text = WX.WorkOrder.Order.TypeStr[porder.Type.ToInt32()];
                YJTime_li.Text = porder.YJTime.ToString();
                SubTime_li.Text = porder.SubTime.ToString();
                FPTime_li.Text = porder.AddTime.ToString();
                YSTime_li.Text = porder.YSTime.ToString();
                StopTime_li.Text = porder.StopTime.ToString();
                State_li.Text = WX.WorkOrder.Order.StateStr[porder.State.ToInt32()];
                StateTime_la.Text = WX.CommonUtils.GetRealNameListByUserIdList(porder.UserID.ToString()) + "&nbsp;&nbsp;" + porder.AddTime.ToString();
                FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(porder.UserID.ToString()), porder.UserID.ToString()));
                if (order.UserID.ToString() != order.AssignUserID.ToString())
                    FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(order.AssignUserID.ToString()), order.AssignUserID.ToString()));
                state = order.State.ToInt32();
                Remarks_li.Text = WX.WorkOrder.Order.EnCoding(porder.Remarks.ToString());
                Button2.Visible = false;
                Button3.Visible = false;
                Button4.Visible = false;
                Button5.Visible = false;
                if (order.State.ToInt32() == 3)
                {
                    Button2.Visible = true;
                }
                else if (order.State.ToInt32() == 4 || order.State.ToInt32() == 6)
                {
                    System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select count(*) scount from WorkOrder_Orders where ExecUserID='" + this.CurUser.UserID + "' and State=5");
                    if (dt.Rows[0]["scount"].ToString() == "0")
                    {
                        Button3.Visible = true;
                    }
                }
                else if (order.State.ToInt32() == 5)
                {
                    Button4.Visible = true; Button5.Visible = true;
                }
                if (order.State.ToInt32() > 3)
                {
                    MessBind(porder.ID.ToInt32());
                    mess.Visible = true;
                    if (order.State.ToInt32() == 6 || order.State.ToInt32() == 8)
                    {
                        messfs.Visible = false;
                        mess.Width = "418px";
                    }
                    if (order.State.ToInt32() >= 6)
                    {
                        AppBind();
                        pingjiafs.Visible = true;
                        pingjiafs.Width = "418px";
                    }
                }
            }
        }
        private void AppBind()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Appraisal where WID=" + Request["OrderID"] + " order by AddTime desc");
            DataList2.DataSource = dt;
            DataList2.DataBind();
            WX.Main.ExcuteUpdate("WorkOrder_Appraisal", "State=1", "WID=(select ID from WorkOrder_Orders where ID=" + Request["OrderID"] + " and ExecUserID='" + this.CurUser.UserID + "') and State=0");
        }
        private void MessBind(int porderid)
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Message where WID=" + Request["OrderID"] + " or WID="+porderid+" order by AddTime desc");
            DataList1.DataSource = dt;
            DataList1.DataBind();
            WX.Main.ExcuteUpdate("WorkOrder_Message", "State=1", "WID=" + Request["OrderID"] + " and ToUserID='" + this.CurUser.UserID + "'");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.WorkOrder.Message.MODEL mess = WX.WorkOrder.Message.NewDataModel();
            mess.WID.value = Request["OrderID"];
            mess.FromUserID.value = this.CurUser.UserID;
            mess.ToUserID.value = FS_drop.SelectedValue;
            mess.Remarks.value = MessContent_txt.Text;
            mess.Insert();
            MessContent_txt.Text = "";
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            MessBind(order.PID.ToInt32());
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            LinkButton bu1 = (LinkButton)sender;
            if (FS_drop.Items.FindByValue(bu1.CommandArgument) == null)
                FS_drop.Items.Add(new ListItem("@" + WX.CommonUtils.GetRealNameListByUserIdList(bu1.CommandArgument), bu1.CommandArgument));
            FS_drop.SelectedValue = bu1.CommandArgument;
        }
        private void Exceup(int state, int deptstate)
        {
            this.CurUser.LoadUserModel(false);
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            order.State.value = state; order.StateTime.value = DateTime.Now;
            if (state == 7)
                order.YSTime.value = DateTime.Now;
            order.Update();
            WX.WorkOrder.Message.MODEL mess = WX.WorkOrder.Message.NewDataModel();
            mess.WID.value = Request["OrderID"];
            mess.Remarks.value = WX.CommonUtils.GetRealNameListByUserIdList(order.ExecUserID.ToString()) + "的工作“" + WX.WorkOrder.Order.StateStr[order.State.ToInt32()] + "”";
            mess.Insert();
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select (select count(*) from WorkOrder_Orders where PID=" + order.PID.ToString() + " and DeptWorkID=" + this.CurUser.UserModel.DepartmentID.ToString() + ") ccount,(select count(*) from WorkOrder_Orders where PID=" + order.PID.ToString() + " and DeptWorkID=" + this.CurUser.UserModel.DepartmentID.ToString() + " and State>=" + state + ") scount");

            if (dt.Rows[0]["ccount"].ToString() == dt.Rows[0]["scount"].ToString())
            {
                int n = WX.Main.ExcuteUpdate("WorkOrder_Dept", "State=" + deptstate + (deptstate == 2 ? ",FPTime=getdate()" : (deptstate == 7 ? ",YSTime=getdate()" : "")) + ",StateTime=getdate()", "DeptID=" + this.CurUser.UserModel.DepartmentID.ToString() + " and WID=" + order.PID.ToString() + " and State<" + deptstate);
                if (n > 0)
                {
                    mess = WX.WorkOrder.Message.NewDataModel();
                    mess.WID.value = Request["OrderID"];
                    mess.Remarks.value = WX.CommonUtils.GetDeptNameListByDeptIdList(order.DeptWorkID.ToString()) + "的工作“" + WX.WorkOrder.Order.StateStr[deptstate] + "”";
                    mess.Insert();
                }
            }
            System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable("select (select count(*) from WorkOrder_Dept where WID=" + order.PID.ToString() + ") ccount,(select count(*) from WorkOrder_Dept where WID=" + order.PID.ToString() + " and State>=" + deptstate + ") scount");
            if (dt2.Rows[0]["ccount"].ToString() == dt2.Rows[0]["scount"].ToString())
            {
                int n = WX.Main.ExcuteUpdate("WorkOrder_Orders", "State=" + deptstate + (deptstate == 2 ? ",FPTime=getdate()" : (deptstate == 7 ? ",YSTime=getdate()" : "")) + ",StateTime=getdate()", "ID=" + order.PID.ToString() + " and State<" + deptstate);
                if (n > 0)
                {
                    mess = WX.WorkOrder.Message.NewDataModel();
                    mess.WID.value = Request["OrderID"];
                    mess.Remarks.value = "全部参与部门的工作“" + WX.WorkOrder.Order.StateStr[deptstate] + "”";
                    mess.Insert();
                }
            }
            Response.Redirect(WX.Main.DealWithUrlForClient("WorkOrder_My_Show.aspx?OrderID=" + order.ID.ToString()));
        }
        protected void Button2_Click1(object sender, EventArgs e)
        {
            Exceup(4, 2);
        }

        protected void Button2_Click2(object sender, EventArgs e)
        {
            Exceup(5, 2);
        }
        protected void Button2_Click3(object sender, EventArgs e)
        {
            Exceup(6, 2);
        }
        protected void Button2_Click4(object sender, EventArgs e)
        {
            Exceup(7, 7);
        }
    }
}