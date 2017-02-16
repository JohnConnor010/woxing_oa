
namespace WX.Flow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;
    using System.Web;
    using Dates;

    public partial class NumberRule
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            public NumberAutoMode AutoMode_Enum
            {
                set { this.AutoMode.set(Convert.ToInt32(value)); }
                get { return (NumberAutoMode)this.AutoMode.ToInt32(); }
            }
            public NumberUserMode UserMode_Enum
            {
                set { this.UserMode.set(Convert.ToInt32(value)); }
                get { return (NumberUserMode)this.UserMode.ToInt32(); } 
            }
            //获取值
            public string GetValue()
            {
                string f = this.Format.ToString().Replace("{Y}", "{0:yyyy}")
                    .Replace("{M}", "{0:MM}")
                    .Replace("{D}", "{0:dd}")
                    .Replace("{H}", "{0:HH}")
                    .Replace("{I}", "{0:mm}")
                    .Replace("{S}", "{0:ss}")
                    .Replace("{F}", "{1}")
                    .Replace("{U}", "{2}")
                    .Replace("{BM}", "{3}")
                    .Replace("{R}", "{4}")
                    .Replace("{RUN}", "{5}")
                    .Replace("{N}", "{6:D" + (this.AutoLength.ToString()) + "}");
                string result = String.Format(f
                    , DateTime.Now
                    , this.GetFlowName()
                    , this.GetUserName()
                    , this.GetDepartmentName()
                    , this.GetDutyName()
                    , this.GetRunNo()
                    , this.GetAutoNum());
                return result;
            }
            public string GetValue(int id)
            {
                string f = this.Format.ToString().Replace("{Y}", "{0:yyyy}")
                    .Replace("{M}", "{0:MM}")
                    .Replace("{D}", "{0:dd}")
                    .Replace("{H}", "{0:HH}")
                    .Replace("{I}", "{0:mm}")
                    .Replace("{S}", "{0:ss}")
                    .Replace("{U}", "{1}")
                    .Replace("{BM}", "{2}")
                    .Replace("{R}", "{3}")
                    .Replace("{RUN}", "{4}")
                    .Replace("{N}", "{5:D" + (this.AutoLength.ToString()) + "}");
                string result = String.Format(f
                    , DateTime.Now
                    , this.GetUserName()
                    , this.GetDepartmentName()
                    , this.GetDutyName()
                    , this.GetRunNo()
                    , this.GetAutoNum());
                return result;
            }
            private string GetAutoNum()
            {
                int autoLength = this.AutoLength.ToInt32();
                int autoMode = this.AutoMode.ToInt32();
                int baseNumber = this.BaseNumber.ToInt32();
                DateTime startDate = this.StartDate.ToDateTime();
                DateTime start = new DateTime(startDate.Year, startDate.Month, startDate.Day);
                DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                //DateTime end = new DateTime(2012, 07, 27);
                DateSpan span = new DateSpan(startDate, end);
                int number = 0;
                switch (autoMode)
                {
                    case 0:
                        number = baseNumber + 1;
                        this.BaseNumber.value = number;
                        this.Update();
                        break;
                    case 1:                                                 //每年一次 
                        if (span.Years > 0)
                        {
                            number = 1;
                            this.BaseNumber.value = number;
                            this.Update();
                        }
                        else
                        {
                            //更新数据库中的值为1
                            number = baseNumber + 1;
                            this.BaseNumber.value = number;
                            this.StartDate.set_DateTime_Now();
                            this.Update();
                        }
                        break;
                    case 2:                                                     //每月一次
                        if (span.Months > 0)
                        {
                            number = 1;
                            this.BaseNumber.value = number;
                            this.Update();
                        }
                        else
                        {
                            //更新数据库中的值为1
                            number = baseNumber + 1;
                            this.BaseNumber.value = number;
                            this.StartDate.set_DateTime_Now();
                            this.Update();
                        }
                        break;
                    case 3:                                                     //每日一次
                        if (span.Days > 0)
                        {
                            number = 1;
                            this.BaseNumber.value = number;
                            this.StartDate.set_DateTime_Now();
                            this.Update();
                        }
                        else
                        {
                            //更新数据库中的值为1
                            number = baseNumber + 1;
                            this.BaseNumber.value = number;
                            this.Update();
                        }
                        break;
                }
                string str = String.Format("{0:D" + autoLength + "}", number);
                return str;
            }
            private string GetUserName()
            {
                this.ThisUser.LoadUserModel(false);
                return this.ThisUser.UserModel.RealName.ToString();
            }
            private string GetFlowName()
            {
                string flowId = HttpContext.Current.Request.QueryString["Flow_ID"];
                //string flowId = "1";
                return XSql.GetData("SELECT Name FROM FL_Flows WHERE ID=" + flowId).ToStr();
            }
            private string GetDepartmentName()
            {
                this.ThisUser.LoadUserModel(false);
                return XSql.GetData("SELECT Name FROM TE_Departments WHERE ID=" + (ThisUser.UserModel.DepartmentID.ToString())).ToStr();
            }
            public string GetDutyName()
            {
                this.ThisUser.LoadDutyUser();
                return this.ThisUser.DutyUser.Name.ToString();
            }
            public string GetRunNo()
            {
                return Convert.ToString(HttpContext.Current.Request["Run_ID"]);
            }
        }
    }
    public partial class NumberRule : XDataEntity
    {
        public NumberRule(string tableName)
            : base(tableName)
        {
        }
        public NumberRule(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public NumberRule(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static NumberRule _entity;
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
        public static NumberRule NewEntity()
        {
            return new NumberRule("FL_NumberRules", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static NumberRule Entity
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

            public XDataField Id;
            public XDataField Name;
            public XDataField Format;
            public XDataField AutoLength;
            public XDataField AutoMode;
            public XDataField UserMode;
            public XDataField BaseNumber;
            public XDataField StartDate;
            public WX.WXUser ThisUser = WX.Public.CurUser;

            public MODEL() { }
            public MODEL(NumberRule parentEntity) : base(parentEntity) { }
            public MODEL(NumberRule parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(NumberRule parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(NumberRule parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int32);
                this.Name = new XDataField("Name", DbType.String);
                this.Format = new XDataField("Format", DbType.String);
                this.AutoLength = new XDataField("AutoLength", DbType.Byte);
                this.AutoMode = new XDataField("AutoMode", DbType.Byte);
                this.UserMode = new XDataField("UserMode", DbType.Byte);
                this.BaseNumber = new XDataField("BaseNumber", DbType.Int32);
                this.StartDate = new XDataField("StartDate", DbType.Date);
                //
                
                this.UserMode.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.Name, this.Format, this.AutoLength, this.AutoMode, this.UserMode, this.BaseNumber,this.StartDate });
            }
        }
    }
}
