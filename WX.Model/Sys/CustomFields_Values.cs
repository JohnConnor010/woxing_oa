
namespace WX.Sys
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomFields_Values
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CustomFields_Values : XDataEntity
    {
        public CustomFields_Values(string tableName)
            : base(tableName)
        {
        }
        public CustomFields_Values(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomFields_Values(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomFields_Values _entity;
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
        public static CustomFields_Values NewEntity()
        {
            return new CustomFields_Values("Sys_CustomFields_Values", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomFields_Values Entity
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

        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField CFID;
            public XDataField TableID;
            public XDataField valuestr;

            public MODEL() { }
            public MODEL(CustomFields_Values parentEntity) : base(parentEntity) { }
            public MODEL(CustomFields_Values parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomFields_Values parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomFields_Values parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.CFID = new XDataField("CFID", DbType.Int32);
                this.TableID = new XDataField("TableID", DbType.Int32);
                this.valuestr = new XDataField("valuestr", DbType.String);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.CFID, this.TableID, this.valuestr });
            }
        }
    }
}
