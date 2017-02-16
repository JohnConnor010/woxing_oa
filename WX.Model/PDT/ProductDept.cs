
namespace WX.PDT
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class ProductDept
    {
        //费用计算方式 ：0固定费用，1产品成本价比例，2协议执行价比例
        public static string[] FeeTypestr = new string[] { "固定费用", "产品成本价比例", "协议执行价比例"};
       //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class ProductDept : XDataEntity
    {
        public ProductDept(string tableName)
            : base(tableName)
        {
        }
        public ProductDept(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public ProductDept(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static ProductDept _entity;
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
        public static ProductDept NewEntity()
        {
            return new ProductDept("PDT_ProductDept", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static ProductDept Entity
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
        public static DataTable GetList(string ProductID)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from PDT_ProductDept where ProductID="+ProductID);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField ProductID;
            public XDataField DeptID;
            public XDataField MonthFee;
            public XDataField MonthFeeType;
            public XDataField Fee;
            public XDataField FeeType;
            public XDataField Remarks;

            public MODEL() { }
            public MODEL(ProductDept parentEntity) : base(parentEntity) { }
            public MODEL(ProductDept parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(ProductDept parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(ProductDept parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.ProductID = new XDataField("ProductID", DbType.Int32);
                this.DeptID = new XDataField("DeptID", DbType.Int32);
                this.MonthFee = new XDataField("MonthFee", DbType.Decimal);
                this.MonthFeeType = new XDataField("MonthFeeType", DbType.Int32);
                this.Fee = new XDataField("Fee", DbType.Decimal);
                this.FeeType = new XDataField("FeeType", DbType.Int32);
                this.Remarks = new XDataField("Remarks", DbType.String);
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.ProductID, this.DeptID, this.MonthFee, this.MonthFeeType, this.Fee, this.FeeType, this.Remarks });
            }
        }
    }
}
