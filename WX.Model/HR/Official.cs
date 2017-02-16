
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Official
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Official : XDataEntity
    {
        public Official(string tableName)
            : base(tableName)
        {
        }
        public Official(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Official(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Official _entity;
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
        public static Official NewEntity()
        {
            return new Official("HR_Official", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Official Entity
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
            public XDataField DutyID;
            public XDataField salary;
            public XDataField imagine;
            public XDataField dempOpinion;
            public XDataField demptype;
            public XDataField dempUserID;
            public XDataField HROpinion;
            public XDataField HRtype;
            public XDataField HRUserID;
            public XDataField adminOpinion;
            public XDataField admintype;
            public XDataField adminUserID;
            public XDataField bossOpinion;
            public XDataField bosstype;
            public XDataField bossUserID;
            public XDataField EndTime;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Official parentEntity) : base(parentEntity) { }
            public MODEL(Official parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Official parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Official parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.DutyID=new XDataField("DutyID",DbType.Int32);
                this.salary=new XDataField("salary",DbType.Int32);
                this.imagine=new XDataField("imagine",DbType.String);
                this.dempOpinion=new XDataField("dempOpinion",DbType.String);
                this.demptype=new XDataField("demptype",DbType.Int32);
                this.dempUserID=new XDataField("dempUserID",DbType.String);
                this.HROpinion=new XDataField("HROpinion",DbType.String);
                this.HRtype=new XDataField("HRtype",DbType.Int32);
                this.HRUserID = new XDataField("HRUserID", DbType.String);
                this.adminOpinion=new XDataField("adminOpinion",DbType.String);
                this.admintype=new XDataField("admintype",DbType.Int32);
                this.adminUserID=new XDataField("adminUserID",DbType.String);
                this.bossOpinion=new XDataField("bossOpinion",DbType.String);
                this.bosstype=new XDataField("bosstype",DbType.Int32);
                this.bossUserID=new XDataField("bossUserID",DbType.String);
                this.EndTime = new XDataField("EndTime", DbType.DateTime);  
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.DutyID, this.salary, this.imagine, this.dempOpinion, this.demptype, this.dempUserID, this.adminOpinion, this.admintype, this.adminUserID, this.bossOpinion, this.bosstype, this.bossUserID, this.HROpinion, this.HRtype, this.HRUserID, this.EndTime, this.Addtime });
            }
        }
    }
}
