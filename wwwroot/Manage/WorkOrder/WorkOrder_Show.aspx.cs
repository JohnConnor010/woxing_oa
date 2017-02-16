using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Management;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_Show : System.Web.UI.Page
    {
        private WX.WXUser CurUser;
        public int state = 0;
        private string orderids = "";
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
                orderids = Request["OrderID"];
                WX.WorkOrder.Order.MODEL porder = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
                Title_li.Text = porder.Title.ToString();
                Proj_li.Text = WX.WorkOrder.Order.ProjStr[porder.Proj.ToInt32()];
                Type_li.Text = WX.WorkOrder.Order.TypeStr[porder.Type.ToInt32()];
                YJTime_li.Text = porder.YJTime.ToString();
                StateTime_la.Text = WX.CommonUtils.GetRealNameListByUserIdList(porder.UserID.ToString()) + "&nbsp;&nbsp;" + porder.AddTime.ToString();
                SubTime_li.Text = porder.SubTime.ToString();
                FPTime_li.Text = porder.YSTime.ToString();
                StopTime_li.Text = porder.StopTime.ToString();
                State_li.Text = WX.WorkOrder.Order.StateStr[porder.State.ToInt32()];
                state = porder.State.ToInt32();
                Remarks_li.Text = WX.WorkOrder.Order.EnCoding(porder.Remarks.ToString());
                Button2.Visible = false;
                if (porder.State.ToInt32() == 7)
                {
                    Button2.Visible = true;
                }
                DataList3.DataSource = ULCode.QDA.XSql.GetDataTable("select dept.Name DeptName,wdept.DeptID,worder.* from WorkOrder_Dept wdept left join WorkOrder_Orders worder on wdept.WID=worder.ID  left join TE_Departments dept on wdept.DeptID=dept.ID where worder.State>0 and wdept.WID=" + Request["OrderID"] + " order by AddTime desc");
                DataList3.DataBind();
                //if (porder.State.ToInt32() > 1)
                //{
                    MessBind();
                    mess.Visible = true;
                        AppBind();
                        pingjiafs.Visible = true;
                        pingjiafs.Width = "418px";
                //}
            }
        }
        public string Getchilds(int deptId)
        {
            string sSql = " select * from WorkOrder_Orders where DeptWorkID=" + deptId + " and PID=" + Request["OrderID"];
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            string bodystr = "";
            if (dt.Rows.Count > 0)
                bodystr = "</td></tr><tr><td>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bodystr += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"WorkOrder_Assign_Show.aspx?OrderID=" + dt.Rows[i]["ID"] + "\">" + dt.Rows[i]["Title"]+ "</a></td><td>" + WX.CommonUtils.GetRealNameListByUserIdList(dt.Rows[i]["ExecUserID"].ToString()) + "</td><td>&nbsp;</td><td class=\"state" + dt.Rows[i]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt.Rows[i]["State"])] + "</td><td>" + dt.Rows[i]["StateTime"] + "</td><td>&nbsp;</td>";
                if (dt.Rows.Count - 1 > i)
                    bodystr += "</tr><tr><td>";
                orderids += ","+ dt.Rows[i]["ID"] ;
            }
            return bodystr;
        }
        
        private void AppBind()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Appraisal where WID in(" + orderids + ") order by AddTime desc");
            DataList2.DataSource = dt;
            DataList2.DataBind();
           // WX.Main.ExcuteUpdate("WorkOrder_Appraisal", "State=1", "WID=(select ID from WorkOrder_Orders where ID=" + Request["OrderID"] + " and ExecUserID='" + WX.Main.CurUser.UserID + "') and State=0");
        }
        private void MessBind()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from WorkOrder_Message where WID in(" +orderids+ ") order by AddTime desc");
            DataList1.DataSource = dt;
            DataList1.DataBind();
           // WX.Main.ExcuteUpdate("WorkOrder_Message", "State=1", "WID=" + Request["OrderID"] + " and ToUserID='" + WX.Main.CurUser.UserID + "' and State=0");
        }
        protected void Button2_Click1(object sender, EventArgs e)
        {
            Exceup(8);
        }
        private void Exceup(int state)
        {
            this.CurUser.LoadUserModel(false);
            //WX.Main.ExcuteUpdate("WorkOrder_Orders", "State=" + state + ",StateTime=getdate()", "ID=" + Request["OrderID"]);
            WX.WorkOrder.Order.MODEL order = WX.WorkOrder.Order.NewDataModel(Request["OrderID"]);
            order.State.value = state; order.StateTime.value = DateTime.Now;
            order.StopTime.value = DateTime.Now;
            order.Update();
            WX.Main.ExcuteUpdate("WorkOrder_Dept", "State=" + state + ",StopTime=getdate()" + ",StateTime=getdate()", "WID=" + order.ID.ToString());
            WX.Main.ExcuteUpdate("WorkOrder_Orders", "State=" + state + ",StopTime=getdate()" + ",StateTime=getdate()", "PID=" + order.ID.ToString());
            WX.WorkOrder.Message.MODEL mess = WX.WorkOrder.Message.NewDataModel();
            mess.WID.value = Request["OrderID"];
            mess.Remarks.value = "本任务“已验收”完成。";
            mess.Insert();
            PageInit();
        }
    }

}