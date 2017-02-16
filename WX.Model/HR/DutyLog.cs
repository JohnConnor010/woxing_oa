
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class DutyLog
    {
        //0入职，1转正，2调岗，3升迁，4离职,5加职，6撤职,7简历登记
        public static string[] tablesarry = new string[] { "入职", "转正", "调岗", "升迁", "离职", "加职", "撤职", "简历登记" };
        //0入职，1转正，2调岗，3升迁，4离职  职务状态对应页面
        public static string[] tablesurlarry = new string[] { "HR_AddIntojobs.aspx", "HR_Official.aspx", "HR_AddTransferKong.aspx", "HR_AddTransferKong.aspx", "HR_Leavejobs.aspx", "HR_AddUserjobs.aspx", "HR_Userjobs.aspx", "User_Resume.aspx" };
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class DutyLog : XDataEntity
    {
        public DutyLog(string tableName)
            : base(tableName)
        {
        }
        public DutyLog(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public DutyLog(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static DutyLog _entity;
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
        public static DutyLog NewEntity()
        {
            return new DutyLog("HR_DutyLogs", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static DutyLog Entity
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
        public static DataTable GetList(string UserID)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("exec Get_HR_DutyLogsList '" + UserID + "'");
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField UserID;
            public XDataField BackDutyID;
            public XDataField NowDutyID;
            public XDataField BackDempID;
            public XDataField NowDempID;
            public XDataField Backtableid;
            public XDataField Backcolumid;
            public XDataField Nowtableid;
            public XDataField Nowcolumid;
            public XDataField Starttime;
            public XDataField stoptime;
            public XDataField Content;
            public XDataField GradeID;

            public MODEL() { }
            public MODEL(DutyLog parentEntity) : base(parentEntity) { }
            public MODEL(DutyLog parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(DutyLog parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(DutyLog parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.BackDutyID=new XDataField("BackDutyID",DbType.Int32);
                this.NowDutyID=new XDataField("NowDutyID",DbType.Int32);
                this.BackDempID=new XDataField("BackDempID",DbType.Int32);
                this.NowDempID=new XDataField("NowDempID",DbType.Int32);
                this.Backtableid = new XDataField("Backtableid", DbType.Int32);
                this.Backcolumid = new XDataField("Backcolumid", DbType.Int32);
                this.Nowtableid = new XDataField("Nowtableid", DbType.Int32);
                this.Nowcolumid = new XDataField("Nowcolumid", DbType.Int32);
                this.Starttime=new XDataField("Starttime",DbType.DateTime);
                this.stoptime=new XDataField("stoptime",DbType.DateTime);
                this.Content=new XDataField("Content",DbType.String);
                this.GradeID = new XDataField("GradeID", DbType.Int32);
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.BackDutyID, this.NowDutyID, this.BackDempID, this.NowDempID, this.Starttime, this.stoptime, this.Content, this.Backtableid, this.Backcolumid, this.Nowtableid, this.Nowcolumid,this.GradeID });
            }
        }
    }
}
