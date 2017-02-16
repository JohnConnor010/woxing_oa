
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomerAgreement
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CustomerAgreement : XDataEntity
    {
        public CustomerAgreement(string tableName)
            : base(tableName)
        {
        }
        public CustomerAgreement(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomerAgreement(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomerAgreement _entity;
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
        public static CustomerAgreement NewEntity()
        {
            return new CustomerAgreement("CRM_CustomerAgreement", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomerAgreement Entity
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
            public XDataField TempID;
            public XDataField Content;
            public XDataField AllFee;
            public XDataField Fee;
            public XDataField OverFee;
            public XDataField OverTime;
            public XDataField Invoice;
            public XDataField OverInvoice;
            public XDataField StartTime;
            public XDataField StopTime;
            public XDataField Addtime;
            public XDataField UserID;
            public XDataField ProgramID;
            public XDataField IsCheck;
            public XDataField CheckUser;
            public XDataField CheckTime;

            public MODEL() { }
            public MODEL(CustomerAgreement parentEntity) : base(parentEntity) { }
            public MODEL(CustomerAgreement parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomerAgreement parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomerAgreement parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.Int32);
                this.TrackID = new XDataField("TrackID", DbType.Int32);
                this.TempID = new XDataField("TempID", DbType.Int32);
                this.Content = new XDataField("Content", DbType.String);
                this.AllFee = new XDataField("AllFee", DbType.Decimal);
                this.Fee = new XDataField("Fee", DbType.Decimal);
                this.OverFee = new XDataField("OverFee", DbType.Decimal);
                this.OverTime = new XDataField("OverTime", DbType.DateTime);
                this.Invoice = new XDataField("Invoice", DbType.Decimal);
                this.OverInvoice = new XDataField("OverInvoice", DbType.Decimal);
                this.StartTime = new XDataField("StartTime", DbType.DateTime);
                this.StopTime = new XDataField("StopTime", DbType.DateTime);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.UserID = new XDataField("UserID", DbType.String);
                this.ProgramID = new XDataField("ProgramID", DbType.Int32);
                this.IsCheck = new XDataField("IsCheck", DbType.Int32);
                this.CheckUser = new XDataField("CheckUser", DbType.String);
                this.CheckTime = new XDataField("CheckTime", DbType.DateTime);
                //

                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.CustomerID, this.TrackID, this.TempID, this.Content, this.AllFee, this.Fee, this.OverFee, this.OverTime, this.Invoice, this.OverInvoice, this.StartTime, this.StopTime, this.Addtime, this.UserID, this.ProgramID, this.IsCheck, this.CheckUser, this.CheckTime });
            }
        }
    }
}
