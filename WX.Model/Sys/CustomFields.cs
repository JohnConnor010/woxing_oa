
namespace WX.Sys
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class CustomFields
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CustomFields : XDataEntity
    {
        public CustomFields(string tableName)
            : base(tableName)
        {
        }
        public CustomFields(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CustomFields(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CustomFields _entity;
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
        public static CustomFields NewEntity()
        {
            return new CustomFields("Sys_CustomFields", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CustomFields Entity
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
            public XDataField name;
            public XDataField Cloname;
            public XDataField ColType;
            public XDataField Demo;

            public MODEL() { }
            public MODEL(CustomFields parentEntity) : base(parentEntity) { }
            public MODEL(CustomFields parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CustomFields parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CustomFields parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.name = new XDataField("name", DbType.String);
                this.Cloname = new XDataField("Cloname", DbType.String);
                this.ColType = new XDataField("ColType", DbType.Int32);
                this.Demo = new XDataField("Demo", DbType.String);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.name, this.Cloname, this.ColType, this.Demo });
            }
        }
    }
}
