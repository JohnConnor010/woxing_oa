
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomerProducts
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CustomerProducts : XDataEntity
    {
        public CustomerProducts(string tableName)
            : base(tableName)
        {
        }
        public CustomerProducts(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomerProducts(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomerProducts _entity;
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
        public static CustomerProducts NewEntity()
        {
            return new CustomerProducts("CRM_CustomerProducts", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomerProducts Entity
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
            public XDataField PID;
            public XDataField ProductID;
            public XDataField ZDFee;
            public XDataField Remarks;
            public XDataField Type;
            public XDataField ProductName;
            public XDataField Specification;
            public XDataField Units;
            public XDataField SalesPrice;
            public XDataField DiscountedPrice;
            public XDataField CostPrice;
            public XDataField Remark;
            public XDataField Services;

            public MODEL() { }
            public MODEL(CustomerProducts parentEntity) : base(parentEntity) { }
            public MODEL(CustomerProducts parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomerProducts parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomerProducts parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.Int32);
                this.PID = new XDataField("PID", DbType.Int32);
                this.ProductID = new XDataField("ProductID", DbType.Int32);
                this.ZDFee = new XDataField("ZDFee", DbType.Int32);
                this.Remarks = new XDataField("Remarks", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.ProductName = new XDataField("ProductName", DbType.String);
                this.Specification = new XDataField("Specification", DbType.String);
                this.Units = new XDataField("Units", DbType.String);
                this.SalesPrice = new XDataField("SalesPrice", DbType.Decimal);
                this.DiscountedPrice = new XDataField("DiscountedPrice", DbType.Decimal);
                this.CostPrice = new XDataField("CostPrice", DbType.Decimal);
                this.Remark = new XDataField("Remark", DbType.String);
                this.Services = new XDataField("Services", DbType.String);
                //

                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.CustomerID, this.PID, this.ProductID, this.ZDFee, this.Remarks, this.Type,this.ProductName, this.Specification, this.Units, this.SalesPrice, this.DiscountedPrice, this.CostPrice, this.Remark, this.Services});
            }
        }
    }
}
