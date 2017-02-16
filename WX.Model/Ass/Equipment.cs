
namespace WX.Ass
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Equipment
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Equipment : XDataEntity
    {
        public Equipment(string tableName)
            : base(tableName)
        {
        }
        public Equipment(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Equipment(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Equipment _entity;
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
        public static Equipment NewEntity()
        {
            return new Equipment("Ass_Equipment", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Equipment Entity
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
            public XDataField DepartmentID;
            public XDataField UserID;
            public XDataField ProductID;
            public XDataField Quantity;
            public XDataField AddDate;
            public XDataField Price;
            public XDataField Unit;
            public XDataField Remark;

            public MODEL() { }
            public MODEL(Equipment parentEntity) : base(parentEntity) { }
            public MODEL(Equipment parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Equipment parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Equipment parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.DepartmentID = new XDataField("DepartmentID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.ProductID = new XDataField("ProductID", DbType.String);
                this.Quantity = new XDataField("Quantity", DbType.Int32);
                this.AddDate = new XDataField("AddDate", DbType.DateTime);
                this.Price = new XDataField("Price", DbType.Decimal);
                this.Unit = new XDataField("Unit", DbType.String);
                this.Remark = new XDataField("Remark", DbType.String);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.DepartmentID,this.UserID, this.ProductID, this.Quantity, this.AddDate, this.Price, this.Unit,this.Remark });
            }
        }
    }
}
