using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_List : System.Web.UI.Page
    {
        private WX.WXUser CurUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CurUser = WX.Main.CurUser;
            if (!IsPostBack)
            {
                Pageinit(true);
            }
        }
        private void Pageinit(bool start)
        {
            Label1.Text = "";
            System.Data.DataTable dt = WX.WorkOrder.Order.GetListTables("0,1,2,7,8,9", this.CurUser.UserID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label1.Text += "<a" + (Request["State"] != null && Request["State"] == dt.Rows[i]["State"].ToString() ? " style='text-decoration:underline;background-color:#ccc;'" : "") + " href='"+WX.Main.DealWithUrlForClient("WorkOrder_List.aspx?State=" +dt.Rows[i]["State"]) + "'>" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt.Rows[i]["State"])] + "(" + dt.Rows[i]["scount"] + ")</a>&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            string sSql = "select * from WorkOrder_Orders where PID is null and UserID='"+this.CurUser.UserID+"'";
            if (Request["State"] != null)
            {
                    sSql += " and State=" + Request["State"];
            }
            
            if (start)
            {
                int count = WX.Main.GetPagedRowsCount(sSql);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = 20;
                AspNetPager1.CurrentPageIndex = 1;
            }
            Repeater1.DataSource = WX.Main.GetPagedRows(sSql, -1, "order by AddTime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            Repeater1.DataBind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.Pageinit(false);
        }
        public string GetDateSpan(string subtime, string yjtime)
        {
            DateTime sub=Convert.ToDateTime(subtime==""?"1900-1-1":subtime);            
            DateTime yj=Convert.ToDateTime(yjtime==""?"1900-1-1":yjtime);
            return (sub.Year > 1900 ? ((TimeSpan)((yj.Year > 1900 && yj < DateTime.Now ? yj : DateTime.Now) - sub)).Days.ToString() : "0") + "/" + (yj.Year > 1900 ? (yj > DateTime.Now ? ((TimeSpan)(yj - DateTime.Now)).Days.ToString() : "0") : "0");
        }

        public string GetTimeimg(string time1, string time2, int statestr, int id, int type, int typeid)
        {
            string titlestr = "";

            switch (statestr)
            {

                case 1: titlestr = WX.CommonUtils.GetRealNameListByUserIdList(WX.WorkOrder.Order.NewDataModel(id).UserID.ToString()); break;
                case 2:
                    {
                        if (type == 1)
                            titlestr = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users where UserID in(select TE_Departments.Host from WorkOrder_Dept left join TE_Departments on WorkOrder_Dept.DeptID= TE_Departments.ID where WorkOrder_Dept.WID =" + id + ")").ToColValueList();
                        else
                            titlestr = ULCode.QDA.XSql.GetXDataTable("select RealName from TU_Users where UserID in(select TE_Departments.Host from WorkOrder_Dept left join TE_Departments on WorkOrder_Dept.DeptID= TE_Departments.ID where WorkOrder_Dept.WID =" + id + " and WorkOrder_Dept.DeptID=" + typeid + ")").ToColValueList();
                    } break;
                case 3:
                    {
                        if (type == 1)
                            titlestr = ULCode.QDA.XSql.GetXDataTable("select TU_Users.RealName from WorkOrder_Orders left join TU_Users on WorkOrder_Orders.ExecUserID= TU_Users.UserID where WorkOrder_Orders.PID =" + id).ToColValueList();
                        else if (type == 2)
                            titlestr = ULCode.QDA.XSql.GetXDataTable("select TU_Users.RealName from WorkOrder_Orders left join TU_Users on WorkOrder_Orders.ExecUserID= TU_Users.UserID where WorkOrder_Orders.PID =" + id + " and WorkOrder_Orders.DeptWorkID=" + typeid).ToColValueList();
                        else
                            titlestr = ULCode.QDA.XSql.GetXDataTable("select TU_Users.RealName from WorkOrder_Orders left join TU_Users on WorkOrder_Orders.ExecUserID= TU_Users.UserID where WorkOrder_Orders.ID =" + id).ToColValueList();
                    } break;
                default: titlestr = WX.CommonUtils.GetRealNameListByUserIdList(WX.WorkOrder.Order.NewDataModel(id).UserID.ToString()); break;
            }
            string imgstr = time1 != "" ? "<span title='" + titlestr + "\n" + time1 + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time1), time2 != "" ? Convert.ToDateTime(time2) : Convert.ToDateTime(time1), "", "") + "'><img src='/images/ok51.gif' alt='" + titlestr + "\n" + time1 + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time1), time2 != "" ? Convert.ToDateTime(time2) : Convert.ToDateTime(time1), "", "") + "'/></span>" : ((statestr == 1 && time2 != "") || time2 != "" ? "<span title='" + titlestr + "\n" + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time2), DateTime.Now, "", "") + "'><img src='/images/ok52.gif' alt='" + titlestr + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time2), DateTime.Now, "", "") + "'/></span>" : "");

            return imgstr;
        }
        public string GetDepts(int Id)
        { string sSql = "";
            string bodystr = "";
            WX.WorkOrder.Order.MODEL model = WX.WorkOrder.Order.NewDataModel(Id);
            if (model.DeptWorkID.ToString() == "-1")
            {
                sSql = "select *,(select count(*) from WorkOrder_Message where WID=WorkOrder_Orders.ID and State=0 and ToUserID='" + this.CurUser.UserID + "') mescount from WorkOrder_Orders where DeptWorkID=" + this.CurUser.UserModel.DepartmentID.ToString() + " and PID=" + Id;
                if (Request["State"] != null && Convert.ToInt32(Request["State"]) >= 3)
                {
                    sSql += " and State=" + Request["State"];
                }
                System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sSql);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:100px;'>├ <a href=\"javascript:PopupIFrame('"+WX.Main.DealWithUrlForClient("WorkOrder_Assign_Show.aspx?OrderID=" + dt2.Rows[j]["ID"]) + "','查看任务','','',850,550)\">" + WX.CommonUtils.GetRealNameListByUserIdList(dt2.Rows[j]["ExecUserID"].ToString()) + "&nbsp;：&nbsp;" + dt2.Rows[j]["Title"] + (Convert.ToInt32(dt2.Rows[j]["mescount"]) > 0 ? "<img src='/images/4.gif' alt='您有新消息'/>" : "") + "</a></div></td><td>" + Convert.ToDateTime(dt2.Rows[j]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt2.Rows[j]["SubTime"].ToString(),dt2.Rows[j]["AddTime"].ToString(), 1, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["FPTime"].ToString(), dt2.Rows[j]["FPTime"].ToString(), 2, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["YSTime"].ToString(), dt2.Rows[j]["SubTime"].ToString(), 3, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["StopTime"].ToString(), dt2.Rows[j]["YSTime"].ToString(), 4, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetDateSpan(dt2.Rows[j]["SubTime"].ToString(), dt2.Rows[j]["YJTime"].ToString()) + "</td><td class=\"state" + dt2.Rows[j]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt2.Rows[j]["State"])] + "</td><td>&nbsp;</td></tr>";
                }
            }
            else
            {
                sSql = " select wdept.DeptID,dept.Name DeptName,wdept.*,worder.AddTime,worder.YJTime from WorkOrder_Dept wdept left join WorkOrder_Orders worder on wdept.WID=worder.ID left join TE_Departments dept on wdept.DeptID=dept.ID where WID=" + Id + " and UserID='" + this.CurUser.UserID + "'";
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sSql = "select *,(select count(*) from WorkOrder_Message where WID=WorkOrder_Orders.ID and State=0 and ToUserID='" + this.CurUser.UserID + "') mescount from WorkOrder_Orders where PID=" + dt.Rows[i]["WID"] + " and DeptWorkID=" + dt.Rows[i]["DeptID"];

                    if (Request["State"] != null && Convert.ToInt32(Request["State"]) >= 3)
                    {
                        sSql += " and State=" + Request["State"];
                    }
                    System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sSql);
                    if (Request["State"] != null && dt2.Rows.Count == 0 && Convert.ToInt32(Request["State"]) >= 2)
                    {
                        continue;
                    }
                    bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:50px;'><b>└ " + dt.Rows[i]["DeptName"] + "</b></div></td><td>" + Convert.ToDateTime(dt.Rows[i]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt.Rows[i]["SubTime"].ToString(), "", 1, Id, 2, 0) + "</td><td>" + GetTimeimg(dt.Rows[i]["FPTime"].ToString(), dt.Rows[i]["SubTime"].ToString(), 2, Id, 2, Convert.ToInt32(dt.Rows[i]["DeptID"].ToString())) + "</td><td>" + GetTimeimg(dt.Rows[i]["YSTime"].ToString(), dt.Rows[i]["FPTime"].ToString(), 3, Id, 2, Convert.ToInt32(dt.Rows[i]["DeptID"].ToString())) + "</td><td>" + GetTimeimg(dt.Rows[i]["StopTime"].ToString(), dt.Rows[i]["YSTime"].ToString(), 4, Id, 2, 0) + "</td><td>" + GetDateSpan(dt.Rows[i]["SubTime"].ToString(), dt.Rows[i]["YJTime"].ToString()) + "</td><td class=\"state" + dt.Rows[i]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt.Rows[i]["State"])] + "</td><td>&nbsp;</td></tr>";
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:100px;'>├ <a href=\"javascript:PopupIFrame('"+WX.Main.DealWithUrlForClient("WorkOrder_Assign_Show.aspx?OrderID=" + dt2.Rows[j]["ID"]) + "','查看任务','','',850,550)\">" + WX.CommonUtils.GetRealNameListByUserIdList(dt2.Rows[j]["ExecUserID"].ToString()) + "&nbsp;：&nbsp;" + dt2.Rows[j]["Title"] + (Convert.ToInt32(dt2.Rows[j]["mescount"]) > 0 ? "<img src='/images/4.gif' alt='您有新消息'/>" : "") + "</a></div></td><td>" + Convert.ToDateTime(dt2.Rows[j]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt2.Rows[j]["SubTime"].ToString(), dt2.Rows[j]["AddTime"].ToString(), 1, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["FPTime"].ToString(), dt2.Rows[j]["FPTime"].ToString(), 2, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["YSTime"].ToString(), dt2.Rows[j]["SubTime"].ToString(), 3, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["StopTime"].ToString(), dt2.Rows[j]["YSTime"].ToString(), 4, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetDateSpan(dt2.Rows[j]["SubTime"].ToString(), dt2.Rows[j]["YJTime"].ToString()) + "</td><td class=\"state" + dt2.Rows[j]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt2.Rows[j]["State"])] + "</td><td>&nbsp;</td></tr>";
                    }
                }
            }
                return bodystr;
        }
    }
}