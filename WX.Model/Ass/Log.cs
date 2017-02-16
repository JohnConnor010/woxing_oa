
namespace WX.Ass
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
            return new Log("Ass_Logs", "ID");
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
            public XDataField Type;
            public XDataField OpUserID;
            public XDataField OpTime;
            public XDataField OpIP;
            public XDataField UserID;
            public XDataField DepartmentID;
            public XDataField Deadline;
            public XDataField MaturityDate;
            public XDataField Quantity;
            public XDataField ProductID;
            public XDataField Content;
            public XDataField Unit;
            public XDataField Price;

            public MODEL() { }
            public MODEL(Log parentEntity) : base(parentEntity) { }
            public MODEL(Log parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Log parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Log parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.Type = new XDataField("Type", DbType.String);
                this.OpUserID = new XDataField("OpUserID", DbType.String);
                this.OpTime = new XDataField("OpTime", DbType.String);
                this.OpIP = new XDataField("OpIP", DbType.String);
                this.UserID = new XDataField("UserID", DbType.String);
                this.DepartmentID = new XDataField("DepartmentID", DbType.Int32);
                this.Deadline = new XDataField("Deadline", DbType.DateTime);
                this.MaturityDate = new XDataField("MaturityDate", DbType.DateTime);
                this.Quantity = new XDataField("Quantity", DbType.Int32);
                this.ProductID = new XDataField("ProductID", DbType.String);
                this.Content = new XDataField("Content", DbType.String);
                this.Unit = new XDataField("Unit", DbType.String);
                this.Price = new XDataField("Price", DbType.Decimal);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.Type, this.OpUserID, this.OpTime, this.OpIP, this.UserID, this.DepartmentID,this.Deadline,this.MaturityDate, this.Quantity, this.ProductID, this.Content, this.Unit, this.Price });
            }
        }
    }
}
