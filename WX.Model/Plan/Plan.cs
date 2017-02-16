
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Plan
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
        public class Day
        {
            private DateTime ThisDay;
            public Day(DateTime thisDay)
            {
                this.ThisDay = thisDay; 
            }
            public string ToWeekDayStr()
            {
                DateTime dFirst = this.GetFirstWeekDay();
                DateTime dEnd = this.GetLastWeekDay();
                string dayList = String.Empty;
                for (DateTime d = dFirst; d <= dEnd; d = d.AddDays(1))
                {
                    if (!String.IsNullOrEmpty(dayList)) dayList += ",";
                    dayList += d.Day.ToString();
                }
                return String.Format("{0}月({1})", dFirst.Month, dayList);
            }
            //内部接口
            private DateTime GetFirstWeekDay()
            {
                return this.ThisDay.AddDays(WX.PlanSet.Plan_WeekFirst - Convert.ToInt32(this.ThisDay.DayOfWeek));
            }
            private DateTime GetLastWeekDay()
            {
                return this.ThisDay.AddDays(WX.PlanSet.Plan_WeekEnd - Convert.ToInt32(this.ThisDay.DayOfWeek));
            }
        }
    }
    public partial class Plan : XDataEntity
    {
        public Plan(string tableName)
            : base(tableName)
        {
        }
        public Plan(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Plan(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Plan _entity;
        private static MODEL _model;

        public static MODEL NewDataModel()
        {
            return new MODEL(Entity);
        }
        public static MODEL NewDataModel(DataRow drCache)
        {
            return new MODEL(Entity, drCache);
        }
        public static MODEL NewDataModel(params object[] keyValues)
        {
            return new MODEL(Entity, keyValues);
        }
        public static MODEL NewDataModel(DataTable dtCache, params object[] keyValues)
        {
            return new MODEL(Entity, dtCache, keyValues);
        }
        public static Plan NewEntity()
        {
            return new Plan("PLAN_Plan", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Plan Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = NewEntity();
                }
                return _entity;
            }
        }
        public static MODEL Model
        {
            get
            {
                if (_model == null)
                {
                    _model = NewModel();
                }
                return _model;
            }
        }
        public static MODEL GetModel(string UserID, DateTime Starttime, int type,int rtype)
        {
            string sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType="+rtype+" and datediff(dd,Starttime,'" + Starttime + "')=0";
            if(type==2)
                sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(week,Starttime,'" + Starttime + "')=0";
            else if(type==3)
                sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(month,Starttime,'" + Starttime + "')=0";
            return GetModel(sql);
        }
        public static MODEL GetModel(int deptId, DateTime Starttime, int type, int rtype)
        {
            string sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(dd,Starttime,'" + Starttime + "')=0";
            if (type == 2)
                sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(week,Starttime,'" + Starttime + "')=0";
            else if (type == 3)
                sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(month,Starttime,'" + Starttime + "')=0";
            return GetModel(sql);
        }
        /// <summary>
        /// 成立前提，部门计划必须由部门主管来创建，而且部门主管永不变动
        /// 故作废
        /// </summary>
        public static bool CheckModel(string UserID, DateTime Starttime, int type, int rtype)
        {
            string sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(dd,Starttime,'" + Starttime + "')=0";
            if (type == 2)
                sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(week,Starttime,'" + Starttime + "')=0";
            else if (type == 3)
                sql = "select * from PLAN_Plan where UserID='" + UserID + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(month,Starttime,'" + Starttime + "')=0";
            return ULCode.QDA.XSql.IsHasRow(sql);
        }
        public static bool CheckModel(int deptId, DateTime Starttime, int type, int rtype)
        {
            string sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(dd,Starttime,'" + Starttime + "')=0";
            if (type == 2)
                sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(week,Starttime,'" + Starttime + "')=0";
            else if (type == 3)
                sql = "select * from PLAN_Plan where DepartmentId='" + deptId + "' and Type=" + type + " and RangeType=" + rtype + " and datediff(month,Starttime,'" + Starttime + "')=0";
            return ULCode.QDA.XSql.IsHasRow(sql);
        }
        public static MODEL GetModel(int PlanId)
        {
            return GetModel("select * from PLAN_Plan where id=" + PlanId);
        }
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
        }
        public static List<MODEL> GetModels(string sSql)
        {
            List<MODEL> lm = new List<MODEL>();
            DataTable dt = XSql.GetDataTable(sSql);
            foreach (DataRow dr in dt.Rows)
            {
                lm.Add(NewDataModel(dr));
            }
            return lm;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField id;
            public XDataField Title;
            public XDataField Total;
            public XDataField Current;
            public XDataField UserID;
            public XDataField DepartmentID;
            public XDataField Content;
            public XDataField Summary;
            public XDataField Appraise;
            public XDataField Type;
            public XDataField RangeType;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Addtime;
            public XDataField PlanState;
            public XDataField Reason;
            public XDataField DateKey;

            public MODEL() { }
            public MODEL(Plan parentEntity) : base(parentEntity) { }
            public MODEL(Plan parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Plan parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Plan parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.Title = new XDataField("Title", DbType.String);
                this.Total = new XDataField("Total", DbType.Int32);
                this.Current = new XDataField("Current", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.DepartmentID = new XDataField("DepartmentID", DbType.Int32);
                this.Content = new XDataField("Content", DbType.String);
                this.Summary = new XDataField("Summary", DbType.String);
                this.Appraise = new XDataField("Appraise", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.RangeType = new XDataField("RangeType", DbType.Int32);
                this.Starttime = new XDataField("Starttime", DbType.DateTime);
                this.Stoptime = new XDataField("Stoptime", DbType.DateTime);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.PlanState = new XDataField("PlanState", DbType.Int32);
                this.Reason = new XDataField("Reason", DbType.String);
                this.DateKey = new XDataField("DateKey", DbType.String);
                //
                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.Title, this.Total, this.Current, this.UserID, this.DepartmentID, this.Content, this.Summary, this.Appraise, this.Type, this.RangeType, this.Starttime, this.Stoptime, this.Addtime, this.PlanState, this.Reason, this.DateKey });
            }
        }
    }
}
