using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class WorkOrder_My : System.Web.UI.Page
    {
        public int r2count = 0;
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
            System.Data.DataTable dt = WX.WorkOrder.Order.GetMyTables("3,4,5,6,7,8", this.CurUser.UserID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label1.Text += "<a" + (Request["State"] != null && Request["State"] == dt.Rows[i]["State"].ToString() ? " style='text-decoration:underline;background-color:#ccc;'" : "") + " href='"+WX.Main.DealWithUrlForClient("WorkOrder_My.aspx?State=" +dt.Rows[i]["State"])+ "'>" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt.Rows[i]["State"])] + "(" + dt.Rows[i]["scount"] + ")</a>&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            Bindrepeater2();
            string sSql = "select * from WorkOrder_Orders where ID in( select PID from WorkOrder_Orders where ExecUserID='" + this.CurUser.UserID + "'";
            if (Request["State"] != null)
            {
                sSql += " and State=" + Request["State"];
            }
            else
            {
                sSql += " and State=3" ;
            }
            sSql += ") and State>0";
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

        private void Bindrepeater2()
        {
            string sSql = "select * from WorkOrder_Orders where  ExecUserID='" + this.CurUser.UserID + "' and State>3 and State<8 order by TopTime desc";
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            if (dt.Rows.Count> 0)
            {
                int rowhours = 0;
                int hours = 0;
                System.Data.DataTable Schedules;
                dt.Columns.Add("pl");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowhours = 0;
                    try
                    {
                        if (dt.Rows[i]["TopCount"].ToString().IndexOf("天") > -1 || dt.Rows[i]["TopCount"].ToString().IndexOf("d") > -1 || dt.Rows[i]["TopCount"].ToString().IndexOf("D") > -1)
                            rowhours = Convert.ToInt32(dt.Rows[i]["TopCount"].ToString().Substring(0, dt.Rows[i]["TopCount"].ToString().Length - 1)) * WX.Main.Whours;
                        else if (dt.Rows[i]["TopCount"].ToString().IndexOf("小时") > -1)
                            rowhours = Convert.ToInt32(dt.Rows[i]["TopCount"].ToString().Substring(0, dt.Rows[i]["TopCount"].ToString().Length - 2));
                        else if (dt.Rows[i]["TopCount"].ToString().IndexOf("h") > -1 || dt.Rows[i]["TopCount"].ToString().IndexOf("H") > -1)
                            rowhours = Convert.ToInt32(dt.Rows[i]["TopCount"].ToString().Substring(0, dt.Rows[i]["TopCount"].ToString().Length - 1));
                        hours += rowhours;
                    }
                    catch { }
                    Schedules = ULCode.QDA.XSql.GetDataTable("select top " + (hours / WX.Main.Whours + (hours % WX.Main.Whours > 0 ? 1 : 0)) + " sDate from TE_Schedules where sHours>0 and sDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");

                    if (Schedules.Rows.Count > 0)
                    {
                        int p = Schedules.Rows.Count > 1 && i > 0 ? (hours - rowhours) / WX.Main.Whours : 0;
                        p = p == Schedules.Rows.Count ? p - 1 : p;
                        DateTime stoptime = Convert.ToDateTime(Schedules.Rows[Schedules.Rows.Count - 1]["sDate"]);
                        dt.Rows[i]["pl"] = Convert.ToDateTime(Schedules.Rows[p]["sDate"]).ToString("MM-dd") + "至" + (dt.Rows[i]["YJTime"].ToString() != "" && Convert.ToDateTime(dt.Rows[i]["YJTime"].ToString()) < stoptime ? "<font color='red'>" + stoptime.ToString("MM-dd") + "</font>" : stoptime.ToString("MM-dd"));
                    }
                }
            }
            r2count = dt.Rows.Count;
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.Pageinit(false);
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
            string imgstr = time1 != "" ? "<span title='" + titlestr + "\n" + time1 + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time1), Convert.ToDateTime(time2), "", "") + "'><img src='/images/ok51.gif' alt='" + titlestr + "\n" + time1 + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time1), Convert.ToDateTime(time2), "", "") + "'/></span>" : (statestr == 1 || time2 != "" ? "<span title='" + titlestr + "\n" + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time2), DateTime.Now, "", "") + "'><img src='/images/ok52.gif' alt='" + titlestr + "\n" + WX.Main.GetTime_X_EslapseStr(Convert.ToDateTime(time2), DateTime.Now, "", "") + "'/></span>" : "");
            return imgstr;
        }
        public string Getchilds(int Id)
        {
            string sSql = "";
            string bodystr = "";
            string mydept = "";
            WX.WorkOrder.Order.MODEL model = WX.WorkOrder.Order.NewDataModel(Id);
            if (model.DeptWorkID.ToString() == "-1")
            {
                this.CurUser.LoadMyDepartment(false);
                sSql = "select *,(select count(*) from WorkOrder_Message where WID=WorkOrder_Orders.ID and State=0 and ToUserID='" + this.CurUser.UserID + "') mescount from WorkOrder_Orders where DeptWorkID=" + this.CurUser.UserModel.DepartmentID.ToString() + " and PID=" + Id;
                if (Request["State"] != null && Convert.ToInt32(Request["State"]) >= 3)
                {
                    sSql += " and State=" + Request["State"];
                }
                System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sSql);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    mydept = dt2.Rows[j]["ExecUserID"].ToString() == this.CurUser.UserID ? "" : "color:#aaa;";
                    bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:100px;'>├ <a style='" + mydept + "' href=\"javascript:PopupIFrame('"+WX.Main.DealWithUrlForClient("WorkOrder_" + (dt2.Rows[j]["ExecUserID"].ToString() == this.CurUser.UserID ? "My" : "Assign") + "_Show.aspx?OrderID=" + dt2.Rows[j]["ID"]) + "','查看任务','','',850,550)\">" + WX.CommonUtils.GetRealNameListByUserIdList(dt2.Rows[j]["ExecUserID"].ToString()) + "&nbsp;：&nbsp;" + dt2.Rows[j]["Title"] + (Convert.ToInt32(dt2.Rows[j]["mescount"]) > 0 ? "<img src='/images/4.gif' alt='您有新消息'/>" : "") + "</a></div></td><td>&nbsp;</td><td>" + Convert.ToDateTime(dt2.Rows[j]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt2.Rows[j]["SubTime"].ToString(), dt2.Rows[j]["AddTime"].ToString(), 1, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["FPTime"].ToString(), dt2.Rows[j]["FPTime"].ToString(), 2, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["YSTime"].ToString(), dt2.Rows[j]["SubTime"].ToString(), 3, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["StopTime"].ToString(), dt2.Rows[j]["YSTime"].ToString(), 4, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td class=\"state" + dt2.Rows[j]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt2.Rows[j]["State"])] + "</td></tr>";
                }
            }
            else
            {
                sSql = " select wdept.DeptID,dept.Name DeptName,wdept.*,worder.AddTime from WorkOrder_Dept wdept left join WorkOrder_Orders worder on wdept.WID=worder.ID left join TE_Departments dept on wdept.DeptID=dept.ID where WID=" + Id;
                if (Request["State"] != null)
                {
                    this.CurUser.LoadUserModel(false);
                    sSql += " and wdept.DeptID=" + this.CurUser.UserModel.DepartmentID.ToString();
                }
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sSql = "select *,(select count(*) from WorkOrder_Message where WID=WorkOrder_Orders.ID and State=0 and ToUserID='" + this.CurUser.UserID + "') mescount from WorkOrder_Orders where DeptWorkID=" + dt.Rows[i]["DeptID"] + " and PID=" + dt.Rows[i]["WID"];
                    if (Request["State"] != null && Convert.ToInt32(Request["State"]) >= 3)
                    {
                        sSql += " and State=" + Request["State"];
                    }
                    System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sSql);
                    if (Request["State"] != null && dt2.Rows.Count == 0 && Convert.ToInt32(Request["State"]) >= 2)
                    {
                        continue;
                    }
                    mydept = dt.Rows[i]["DeptID"].ToString() == this.CurUser.UserModel.DepartmentID.ToString() ? "" : "color:#aaa;";
                    bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:50px;" + mydept + "'><b>└ " + dt.Rows[i]["DeptName"] + "</b></div></td><td>&nbsp;</td><td>" + Convert.ToDateTime(dt.Rows[i]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt.Rows[i]["SubTime"].ToString(), dt.Rows[i]["AddTime"].ToString(), 1, Id, 2, 0) + "</td><td>" + GetTimeimg(dt.Rows[i]["FPTime"].ToString(), dt.Rows[i]["SubTime"].ToString(), 2, Id, 2, Convert.ToInt32(dt.Rows[i]["DeptID"].ToString())) + "</td><td>" + GetTimeimg(dt.Rows[i]["YSTime"].ToString(), dt.Rows[i]["FPTime"].ToString(), 3, Id, 2, Convert.ToInt32(dt.Rows[i]["DeptID"].ToString())) + "</td><td>" + GetTimeimg(dt.Rows[i]["StopTime"].ToString(), dt.Rows[i]["YSTime"].ToString(), 4, Id, 2, 0) + "</td><td class=\"state" + dt.Rows[i]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt.Rows[i]["State"])] + "</td></tr>";
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        mydept = dt2.Rows[j]["ExecUserID"].ToString() == this.CurUser.UserID ? "" : "color:#aaa;";
                        bodystr += "<tr style=\"display:none;\" class=\"item" + Id + "\"><td colspan='2'><div style='padding-left:100px;'>├ <a style='" + mydept + "' href=\"javascript:PopupIFrame('"+WX.Main.DealWithUrlForClient("WorkOrder_" + (dt2.Rows[j]["ExecUserID"].ToString() == this.CurUser.UserID ? "My" : "Assign") + "_Show.aspx?OrderID=" + dt2.Rows[j]["ID"]) + "','查看任务','','',850,550)\">" + WX.CommonUtils.GetRealNameListByUserIdList(dt2.Rows[j]["ExecUserID"].ToString()) + "&nbsp;：&nbsp;" + dt2.Rows[j]["Title"] + (Convert.ToInt32(dt2.Rows[j]["mescount"]) > 0 ? "<img src='/images/4.gif' alt='您有新消息'/>" : "") + "</a></div></td><td>&nbsp;</td><td>" + Convert.ToDateTime(dt2.Rows[j]["AddTime"]).ToString("MM-dd HH:mm") + "</td><td>" + GetTimeimg(dt2.Rows[j]["SubTime"].ToString(), dt2.Rows[j]["AddTime"].ToString(), 1, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["FPTime"].ToString(), dt2.Rows[j]["FPTime"].ToString(), 2, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["YSTime"].ToString(), dt2.Rows[j]["SubTime"].ToString(), 3, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td>" + GetTimeimg(dt2.Rows[j]["StopTime"].ToString(), dt2.Rows[j]["YSTime"].ToString(), 4, Convert.ToInt32(dt2.Rows[j]["ID"].ToString()), 3, 0) + "</td><td class=\"state" + dt2.Rows[j]["State"] + "\">" + WX.WorkOrder.Order.StateStr[Convert.ToInt32(dt2.Rows[j]["State"])] + "</td></tr>";
                    }
                }
            }
            return bodystr;
        }
        //上移
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            TextBox tb = (TextBox)Repeater2.Items[Convert.ToInt32(img.CommandName)].FindControl("TextBox1");
            WX.WorkOrder.Order.MODEL model = WX.WorkOrder.Order.NewDataModel(img.CommandArgument);
            model.TopTime.value = DateTime.Now;
            model.TopCount.value = tb.Text;
            model.Update();
            Bindrepeater2();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            WX.WorkOrder.Order.MODEL model = WX.WorkOrder.Order.NewDataModel(tb.ToolTip);
            model.TopCount.value = tb.Text;
            model.Update();
            Bindrepeater2();
        }
    }
}