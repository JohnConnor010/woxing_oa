using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using WX.Flow;
using WX.Data;
using ULCode.QDA;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_AddTimer : System.Web.UI.Page
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
            }
        }
        private void InitData()
        {
            if (!ULCode.Validation.IsNumber(Request.QueryString["id"]))
            {
                ULCode.Debug.Alert("ID格式不正确！", "Flow_Timer.aspx?ID=" + Request.QueryString["ID"]);
                return;
            }
            int Id = Convert.ToInt32(Request.QueryString["id"]);
            var flows = WX.Flow.Model.Flow.Caches.FindAll(delegate(WX.Flow.Model.Flow.MODEL dele) { return dele.Id.ToInt32() == Id; });//WX.Flow.Model.Flow.GetModels("SELECT * FROM FL_Flows WHERE Id=" + Id);
            var query = flows.Select(f => new ListItem()
                {
                    Text = f.GetFieldValue("Name").ToString(),
                    Value = f.GetFieldValue("Id").ToString()
                });
            foreach (var q in query)
            {
                this.ddlFlowName.Items.Add(q);
            }
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
                    Value = Convert.ToInt32(w).ToString(),
                    Selected = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)w) == selectedWeekday ? true : false

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
                Value = m.ToString(),
                Selected = m == DateTime.Now.Month ? true : false
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
                Value = d.ToString(),
                Selected = d == DateTime.Now.Day ? true : false
            });
            foreach (var day in days)
            {
                ddlDate.Items.Add(day);
                ddlDay.Items.Add(day);
            }

        }
        public int GetDateTimeString(string weekday)
        {
            var items = Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().First(w => w.ToString() == weekday);
            return Convert.ToInt32(items.ToString());
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
            string cmdText = "SELECT RemindType FROM Fl_FlowTimer WHERE RemindType=" + remindType + " AND FlowId=" + flowId;
            if (XSql.IsHasRow(cmdText))
            {
                ULCode.Debug.Alert("此提醒类型已经添加，不能重复添加", "Flow_Timer.aspx?id=" + flowId);
                Response.End();
            }
            string sql = "INSERT INTO Fl_FlowTimer (FlowId,UserList,RemindType,RemindTime) VALUES (" + flowId + ",'" + userList + "'," + remindType + ",'" + dateTime + "')";
            int row = XSql.Execute(sql);
            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "添加定时设置成功！", null);
            }
            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("定时设置添加成功！", "Flow_Timer.aspx?id=" + flowId);
            }
            else
            {
                ULCode.Debug.Alert("提醒类型添加失败", "Flow_Timer.aspx?id=" + flowId);
            }

        }
    }
}