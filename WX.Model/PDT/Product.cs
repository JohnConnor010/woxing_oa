
namespace WX.PDT
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Product
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Product : XDataEntity
    {
        public Product(string tableName)
            : base(tableName)
        {
        }
        public Product(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Product(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Product _entity;
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
        public static Product NewEntity()
        {
            return new Product("PDT_Products", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Product Entity
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
            public XDataField CategoryID;
            public XDataField ProductID;
            public XDataField ProductName;
            public XDataField Specification;
            public XDataField Units;
            public XDataField SalesPrice;
            public XDataField DiscountedPrice;
            public XDataField CostPrice;
            public XDataField Remark;
            public XDataField Services;
            public XDataField IsEnable;

            public MODEL() { }
            public MODEL(Product parentEntity) : base(parentEntity) { }
            public MODEL(Product parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Product parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Product parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.CategoryID = new XDataField("CategoryID", DbType.Int32);
                this.ProductID = new XDataField("ProductID", DbType.String);
                this.ProductName = new XDataField("ProductName", DbType.String);
                this.Specification = new XDataField("Specification", DbType.String);
                this.Units = new XDataField("Units", DbType.String);
                this.SalesPrice = new XDataField("SalesPrice", DbType.Decimal);
                this.DiscountedPrice = new XDataField("DiscountedPrice", DbType.Decimal);
                this.CostPrice = new XDataField("CostPrice", DbType.Decimal);
                this.Remark = new XDataField("Remark", DbType.String);
                this.Services = new XDataField("Services", DbType.String);
                this.IsEnable = new XDataField("IsEnable", DbType.Int32);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.CategoryID, this.ProductID, this.ProductName, this.Specification, this.Units, this.SalesPrice, this.DiscountedPrice, this.CostPrice, this.Remark, this.Services,this.IsEnable });
            }
        }
    }
}
