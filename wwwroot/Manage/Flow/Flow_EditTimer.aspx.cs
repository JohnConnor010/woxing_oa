using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using ULCode.QDA;
using System.Data;
using WX.Data;
using WX;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_EditTimer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            if (!IsPostBack)
            {
                InitData();
                if (!ULCode.Validation.IsNumber(Request.QueryString["Id"]) && !ULCode.Validation.IsNumber(Request.QueryString["timerId"]))
                {
                    Response.Write("你没有权限访问此功能！");
                    Response.End();
                }
                int flowId = Convert.ToInt32(Request.QueryString["Id"]);
                string Id = Request.QueryString["timerId"];
                string cmdText = "SELECT * FROM Fl_FlowTimer WHERE FlowId=" + flowId + " AND Id=" + Id;
                DataTable dataTable = XSql.GetDataTable(cmdText);
                foreach (DataRow row in dataTable.Rows)
                {
                    //string sql = "SELECT * FROM FL_Flows Where Id=" + flowId;
                    var flows = WX.Flow.Model.Flow.Caches.FindAll(delegate(WX.Flow.Model.Flow.MODEL dele) { return dele.Id.ToInt32() == flowId; }); //WX.Flow.Model.Flow.GetModels(sql);
                    var query = flows.Select(f => new ListItem()
                    {
                        Text = f.GetFieldValue("Name").ToString(),
                        Value = f.GetFieldValue("Id").ToString()
                    });
                    foreach (var q in query)
                    {
                        this.ddlFlowName.Items.Add(q);
                    }
                    this.hidden_SponsorList.Value = row["UserList"].ToString();
                    this.txtSponsorList.Text = CommonUtils.GetRealNameListByUserIdList(row["UserList"].ToString());
                    Dict.BindListCtrl_enum_RemindType(this.ddlRemindType, null, null, row["RemindType"].ToString());
                    switch (row["RemindType"].ToString())
                    {
                        case "1":
                            this.txtTimer1.Text = row["RemindTime"].ToString();
                            break;
                        case "2":
                            this.txtTimer2.Text = Convert.ToDateTime(row["RemindTime"].ToString()).ToString("HH:mm");
                            break;
                        case "3":
                            const string Day = "日一二三四五六";
                            string selectedWeekday = "星期" + Day[Convert.ToInt16(DateTime.Now.DayOfWeek)];

                            //填充Weekday
                            var items = Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().Select(w =>
                                new ListItem
                                {
                                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)w),
                                    Value = w.ToString(),
                                    Selected = w.ToString() == Convert.ToDateTime(row["RemindTime"]).DayOfWeek.ToString() ? true : false

                                });
                            foreach (var item in items)
                            {
                                this.ddlWeekday.Items.Add(item);
                            }
                            this.txtTimer3.Text = Convert.ToDateTime(row["RemindTime"].ToString()).ToString("HH:mm");
                            break;
                        case "4":
                            //填充日
                            var days = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Select(d => new
                                ListItem()
                            {
                                Text = d + "日",
                                Value = d.ToString(),
                                Selected = d == Convert.ToDateTime(row["RemindTime"]).Day ? true : false
                            });
                            foreach (var day in days)
                            {
                                ddlDate.Items.Add(day);
                            }
                            this.txtTimer4.Text = Convert.ToDateTime(row["RemindTime"].ToString()).ToString("HH:mm");
                            break;
                        case "5":
                            var monthes = Enumerable.Range(1, 12).Select(m => new ListItem
                                {
                                    Text = m + "月",
                                    Value = m.ToString(),
                                    Selected = m == Convert.ToDateTime(row["RemindTime"]).Month ? true : false
                                });
                            foreach (var month in monthes)
                            {
                                this.ddlMonth.Items.Add(month);
                            }
                            var days1 = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Select(d => new
                                ListItem()
                            {
                                Text = d + "日",
                                Value = d.ToString(),
                                Selected = d == Convert.ToDateTime(row["RemindTime"]).Day ? true : false
                            });
                            foreach (var day in days1)
                            {
                                ddlDay.Items.Add(day);
                            }
                            this.txtTimer5.Text = Convert.ToDateTime(row["RemindTime"].ToString()).ToString("HH:mm");
                            break;
                    }
                }


            }
        }
        private void InitData()
        {
            
            //填充时间
            this.txtTimer1.Text = DateTime.Now.ToString();
            this.txtTimer2.Text = DateTime.Now.ToString("HH:mm");
            this.txtTimer3.Text = DateTime.Now.ToString("HH:mm");
            this.txtTimer4.Text = DateTime.Now.ToString("HH:mm");
            this.txtTimer5.Text = DateTime.Now.ToString("HH:mm");

            const string Day = "日一二三四五六";
            string selectedWeekday = "星期" + Day[Convert.ToInt16(DateTime.Now.DayOfWeek)];

            //填充Weekday
            var items = Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().Select(w =>
                new ListItem
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)w),
                    Value = Convert.ToInt32(w).ToString()

                });
            foreach (var item in items)
            {
                this.ddlWeekday.Items.Add(item);
            }
            //填充RemindType
            Dict.BindListCtrl_enum_RemindType(this.ddlRemindType, null, null, "2");
            //填充月
            var monthes = Enumerable.Range(1, 12).Select(m => new ListItem
            {
                Text = m + "月",
                Value = m.ToString()
            });
            foreach (var month in monthes)
            {
                this.ddlMonth.Items.Add(month);
            }
            //填充日
            var days = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Select(d => new
                ListItem()
            {
                Text = d + "日",
                Value = d.ToString()
            });
            foreach (var day in days)
            {
                ddlDate.Items.Add(day);
                ddlDay.Items.Add(day);
            }

        }
        public DateTime GetRemindTime(int remindType)
        {
            switch (remindType)
            {
                case 3:
                    string time3 = this.txtTimer3.Text;
                    DayOfWeek week = (DayOfWeek)Convert.ToInt32(this.ddlWeekday.SelectedValue);
                    System.DateTime dt3 = System.DateTime.Parse(String.Format("{0:yyyy-MM-dd} {1}", DateTime.Now, time3));
                    while (true)
                    {
                        if (dt3 > DateTime.Now)
                        {
                            if (dt3.DayOfWeek == week)
                            {
                                return dt3;
                            }
                        }
                        dt3 = dt3.AddDays(1);
                    }
                    break;
                case 4:
                    string time4 = this.txtTimer4.Text;
                    int day = Convert.ToInt32(this.ddlDate.SelectedItem.Value);
                    DateTime dt4 = DateTime.Parse(String.Format("{0:yyyy-MM}-{1} {2}", DateTime.Now, day, time4));
                    while (true)
                    {
                        if (dt4 > DateTime.Now)
                        {
                            if (dt4.Day == day)
                            {
                                return dt4;
                            }
                        }
                        dt4 = dt4.AddDays(1);
                    }
                    break;
                case 5:
                    string time5 = this.txtTimer5.Text;
                    int month = Convert.ToInt32(this.ddlMonth.SelectedItem.Value);
                    int day1 = Convert.ToInt32(this.ddlDay.SelectedItem.Value);
                    DateTime dt5 = DateTime.Parse(String.Format("{0:yyyy}-{1}-{2} {3}", DateTime.Now, month, day1, time5));
                    while (true)
                    {
                        if (dt5 > DateTime.Now)
                        {
                            if (dt5.Month == month && dt5.Day == day1)
                            {
                                return dt5;
                            }
                        }
                        dt5 = dt5.AddDays(1);
                    }
                    break;
            }
            return DateTime.Now;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string timerId = Request.QueryString["timerId"];
            string flowId = this.ddlFlowName.SelectedItem.Value;
            string userList = this.hidden_SponsorList.Value;
            int remindType = Convert.ToInt32(this.ddlRemindType.SelectedItem.Value);
            string dateTime = DateTime.Now.ToString();
            switch (remindType)
            {
                case 1:
                    dateTime = this.txtTimer1.Text;
                    break;
                case 2:
                    dateTime = String.Format("{0:yyyy-MM-dd} {1}", DateTime.Now, this.txtTimer2.Text);
                    if (Convert.ToDateTime(dateTime) < DateTime.Now) dateTime = String.Format("{0:yyyy-MM-dd HH:mm}",Convert.ToDateTime(dateTime).AddDays(1));
                    break;
                case 3:
                    dateTime = GetRemindTime(3).ToString();
                    break;
                case 4:
                    dateTime = GetRemindTime(4).ToString();
                    break;
                case 5:
                    dateTime = GetRemindTime(5).ToString();
                    break;
            }
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            //填写主要业务逻辑代码
            string sql = "UPDATE Fl_FlowTimer SET FlowId=" + flowId + ",UserList='" + userList + "',RemindType=" + remindType + ",RemindTime='" + dateTime + "' WHERE Id=" + timerId;
            int row = XSql.Execute(sql);
            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "修改定时设置成功！", null);
            }
            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("定时设置修改成功！", "Flow_Timer.aspx?id=" + flowId);
            }
            else
            {
                ULCode.Debug.Alert("提醒类型修改失败", "Flow_Timer.aspx?id=" + flowId);
            }
        }
    }
}