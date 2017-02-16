
namespace WX.Ass
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Warehouse
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Warehouse : XDataEntity
    {
        public Warehouse(string tableName)
            : base(tableName)
        {
        }
        public Warehouse(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Warehouse(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Warehouse _entity;
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
        public static Warehouse NewEntity()
        {
            return new Warehouse("Ass_Warehouse", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Warehouse Entity
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
            public XDataField ProductID;
            public XDataField ProductName;
            public XDataField Unit;
            public XDataField Quantity;
            public XDataField CategoryID;
            public XDataField UsedQuantity;
            public XDataField Price;
            public XDataField Suppliers;
            public XDataField Specification;
            public XDataField Color;
            public XDataField Brand;
            public XDataField Model;
            public XDataField LastTime;
            public XDataField Remark;
            public XDataField ProductPhoto;

            public MODEL() { }
            public MODEL(Warehouse parentEntity) : base(parentEntity) { }
            public MODEL(Warehouse parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Warehouse parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Warehouse parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.ProductID = new XDataField("ProductID", DbType.String);
                this.ProductName = new XDataField("ProductName", DbType.String);
                this.Unit = new XDataField("Unit", DbType.String);
                this.Quantity = new XDataField("Quantity", DbType.Int32);
                this.CategoryID = new XDataField("CategoryID", DbType.Int32);
                this.UsedQuantity = new XDataField("UsedQuantity", DbType.Int32);
                this.Price = new XDataField("Price", DbType.Decimal);
                this.Suppliers = new XDataField("Suppliers", DbType.String);
                this.Specification = new XDataField("Specification", DbType.String);
                this.Color = new XDataField("Color", DbType.String);
                this.Brand = new XDataField("Brand", DbType.String);
                this.Model = new XDataField("Model", DbType.String);
                this.LastTime = new XDataField("LastTime", DbType.DateTime);
                this.Remark = new XDataField("Remark", DbType.String);
                this.ProductPhoto = new XDataField("ProductPhoto", DbType.String);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.ProductID, this.ProductName, this.Unit, this.Quantity,this.CategoryID, this.UsedQuantity, this.Price, this.Suppliers, this.Specification, this.Color, this.Brand, this.Model, this.LastTime, this.Remark, this.ProductPhoto });
            }
        }
    }
}
