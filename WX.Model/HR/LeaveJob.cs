
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class LeaveJob
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class LeaveJob : XDataEntity
    {
        public LeaveJob(string tableName)
            : base(tableName)
        {
        }
        public LeaveJob(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public LeaveJob(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static LeaveJob _entity;
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
        public static LeaveJob NewEntity()
        {
            return new LeaveJob("HR_Leavejobs", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static LeaveJob Entity
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

            public XDataField ID;
            public XDataField UserID;
            public XDataField reason;
            public XDataField days;
            public XDataField lasttime;
            public XDataField dempOpinion;
            public XDataField dempManager;
            public XDataField financialOpinion;
            public XDataField financialHandleManager;
            public XDataField financialManager;
            public XDataField hrOpinion;
            public XDataField hrManager;
            public XDataField bossOpinion;
            public XDataField bossManager;
            public XDataField Addtime;
            public XDataField EndTime;
            public XDataField ReceiveContent;
            public XDataField ReceiveAnnex;

            public MODEL() { }
            public MODEL(LeaveJob parentEntity) : base(parentEntity) { }
            public MODEL(LeaveJob parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(LeaveJob parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(LeaveJob parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.reason=new XDataField("reason",DbType.String);
                this.days=new XDataField("days",DbType.Int32);
                this.lasttime=new XDataField("lasttime",DbType.DateTime);
                this.dempOpinion=new XDataField("dempOpinion",DbType.String);
                this.dempManager=new XDataField("dempManager",DbType.String);
                this.financialOpinion=new XDataField("financialOpinion",DbType.String);
                this.financialHandleManager=new XDataField("financialHandleManager",DbType.String);
                this.financialManager=new XDataField("financialManager",DbType.String);
                this.hrOpinion=new XDataField("hrOpinion",DbType.String);
                this.hrManager=new XDataField("hrManager",DbType.String);
                this.bossOpinion=new XDataField("bossOpinion",DbType.String);
                this.bossManager=new XDataField("bossManager",DbType.String);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);
                this.EndTime = new XDataField("EndTime", DbType.DateTime);
                this.ReceiveContent = new XDataField("ReceiveContent", DbType.String);
                this.ReceiveAnnex = new XDataField("ReceiveAnnex", DbType.String);
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.reason, this.days, this.lasttime, this.dempOpinion, this.dempManager, this.financialOpinion, this.financialHandleManager, this.financialManager, this.hrOpinion, this.hrManager, this.bossOpinion, this.bossManager, this.Addtime, this.EndTime, this.ReceiveContent, this.ReceiveAnnex });
            }
        }
    }
}
