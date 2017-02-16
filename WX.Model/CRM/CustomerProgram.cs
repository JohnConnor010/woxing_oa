
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomerProgram
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CustomerProgram : XDataEntity
    {
        public CustomerProgram(string tableName)
            : base(tableName)
        {
        }
        public CustomerProgram(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomerProgram(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomerProgram _entity;
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
        public static CustomerProgram NewEntity()
        {
            return new CustomerProgram("CRM_CustomerProgram", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomerProgram Entity
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

            public XDataField id;
            public XDataField CustomerID;
            public XDataField TrackID;
            public XDataField Title;
            public XDataField Content;
            public XDataField ZDFee;
            public XDataField Remarks;
            public XDataField Program;
            public XDataField Type;
            public XDataField ProgramTime;
            public XDataField Addtime;
            public XDataField UserID;
            public XDataField State;
            public XDataField Updatetime;

            public MODEL() { }
            public MODEL(CustomerProgram parentEntity) : base(parentEntity) { }
            public MODEL(CustomerProgram parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomerProgram parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomerProgram parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.Int32);
                this.TrackID = new XDataField("TrackID", DbType.Int32);
                this.Title = new XDataField("Title", DbType.String);
                this.Content = new XDataField("Content", DbType.String);
                this.ZDFee = new XDataField("ZDFee", DbType.Int32);
                this.Remarks = new XDataField("Remarks", DbType.String);
                this.Program = new XDataField("Program", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.ProgramTime = new XDataField("ProgramTime", DbType.DateTime);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.UserID = new XDataField("UserID", DbType.String);
                this.State = new XDataField("State", DbType.Int32);
                this.Updatetime = new XDataField("Updatetime", DbType.DateTime);
                //

                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.CustomerID, this.TrackID, this.Title, this.Content, this.ZDFee, this.Remarks, this.Program, this.Type, this.Addtime, this.ProgramTime,this.UserID,this.State,this.Updatetime });
            }
        }
    }
}
