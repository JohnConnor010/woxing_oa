
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class TransferKong
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class TransferKong : XDataEntity
    {
        public TransferKong(string tableName)
            : base(tableName)
        {
        }
        public TransferKong(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public TransferKong(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static TransferKong _entity;
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
        public static TransferKong NewEntity()
        {
            return new TransferKong("HR_TransferKong", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static TransferKong Entity
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
            DataTable dt = ULCode.QDA.XSql.GetDataTable("exec Get_HR_TransferKongList '"+UserID+"'");
           if (dt == null || dt.Rows.Count == 0) return null;
           return dt;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField UserID;
            public XDataField BackDutyID;
            public XDataField NowDutyID;
            public XDataField dempOpinion;
            public XDataField dempManager;
            public XDataField hrOpinion;
            public XDataField hrManager;
            public XDataField bossOpinion;
            public XDataField bossManager;
            public XDataField Addtime;
            public XDataField BackDempID;
            public XDataField NowDempID;
            public XDataField BackGrade;
            public XDataField NowGrade;
            public XDataField type;

            public MODEL() { }
            public MODEL(TransferKong parentEntity) : base(parentEntity) { }
            public MODEL(TransferKong parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(TransferKong parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(TransferKong parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.BackDutyID=new XDataField("BackDutyID",DbType.Int32);
                this.NowDutyID=new XDataField("NowDutyID",DbType.Int32);
                this.dempOpinion=new XDataField("dempOpinion",DbType.String);
                this.dempManager=new XDataField("dempManager",DbType.String);
                this.hrOpinion=new XDataField("hrOpinion",DbType.String);
                this.hrManager=new XDataField("hrManager",DbType.String);
                this.bossOpinion=new XDataField("bossOpinion",DbType.String);
                this.bossManager=new XDataField("bossManager",DbType.String);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);
                this.BackDempID=new XDataField("BackDempID",DbType.Int32);
                this.NowDempID=new XDataField("NowDempID",DbType.Int32);
                this.BackGrade = new XDataField("BackGrade", DbType.Int32);
                this.NowGrade = new XDataField("NowGrade", DbType.Int32);
                this.type=new XDataField("type",DbType.Int32);   
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.BackDutyID, this.NowDutyID, this.dempOpinion, this.dempManager, this.hrOpinion, this.hrManager, this.bossOpinion, this.bossManager, this.Addtime, this.BackDempID, this.NowDempID, this.type, this.BackGrade, this.NowGrade });
            }
        }
    }
}
