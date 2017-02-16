
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Log
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Log : XDataEntity
    {
        public Log(string tableName)
            : base(tableName)
        {
        }
        public Log(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Log(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Log _entity;
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
        public static Log NewEntity()
        {
            return new Log("TL_Logs", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Log Entity
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
            public XDataField Title;
            public XDataField UserID;
            public XDataField LogType;
            public XDataField LogTime;
            public XDataField LogIP;
            public XDataField LogParaments;

            public MODEL() { }
            public MODEL(Log parentEntity) : base(parentEntity) { }
            public MODEL(Log parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Log parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Log parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int64);
                this.Title=new XDataField("Title",DbType.String);
                this.UserID=new XDataField("UserID",DbType.String);
                this.LogType=new XDataField("LogType",DbType.Int16);
                this.LogTime=new XDataField("LogTime",DbType.DateTime);
                this.LogIP=new XDataField("LogIP",DbType.String);
                this.LogParaments=new XDataField("LogParaments",DbType.String);   
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.Title,this.UserID,this.LogType,this.LogTime,this.LogIP,this.LogParaments });
            }
        }
    }
}
