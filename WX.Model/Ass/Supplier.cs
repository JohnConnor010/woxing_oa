
namespace WX.Ass
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Supplier
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Supplier : XDataEntity
    {
        public Supplier(string tableName)
            : base(tableName)
        {
        }
        public Supplier(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Supplier(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Supplier _entity;
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
        public static Supplier NewEntity()
        {
            return new Supplier("Ass_Suppliers", "SupplierID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Supplier Entity
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

            public XDataField SupplierID;
            public XDataField CompanyName;
            public XDataField SupplierCategoryID;
            public XDataField ContactName;
            public XDataField Telephone;
            public XDataField Fax;
            public XDataField MobilePhone;
            public XDataField QQNumber;
            public XDataField Address;
            public XDataField Email;
            public XDataField ZipCode;
            public XDataField WebSite;
            public XDataField Remark;

            public MODEL() { }
            public MODEL(Supplier parentEntity) : base(parentEntity) { }
            public MODEL(Supplier parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Supplier parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Supplier parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.SupplierID = new XDataField("SupplierID", DbType.Int32);
                this.CompanyName = new XDataField("CompanyName", DbType.String);
                this.SupplierCategoryID = new XDataField("SupplierCategoryID", DbType.Int32);
                this.ContactName = new XDataField("ContactName", DbType.String);
                this.Telephone = new XDataField("Telephone", DbType.String);
                this.Fax = new XDataField("Fax", DbType.String);
                this.MobilePhone = new XDataField("MobilePhone", DbType.String);
                this.QQNumber = new XDataField("QQNumber", DbType.String);
                this.Address = new XDataField("Address", DbType.String);
                this.Email = new XDataField("Email", DbType.String);
                this.ZipCode = new XDataField("ZipCode", DbType.String);
                this.WebSite = new XDataField("WebSite", DbType.String);
                this.Remark = new XDataField("Remark", DbType.String);
                //

                this.SupplierID.isIdentity = true;
                this.SupplierID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.SupplierID, this.CompanyName, this.SupplierCategoryID, this.ContactName, this.Telephone, this.Fax, this.MobilePhone, this.QQNumber, this.Address, this.Email, this.ZipCode, this.WebSite, this.Remark });
            }
        }
    }
}
