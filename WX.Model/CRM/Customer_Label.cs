
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Customer_Label
    {
        //以下为实体开发部分
        //
        public static string[] StyleStr = new string[] { "变量", "sql语句", "纯内容" };
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Customer_Label : XDataEntity
    {
        public Customer_Label(string tableName)
            : base(tableName)
        {
        }
        public Customer_Label(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Customer_Label(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Customer_Label _entity;
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
        public static Customer_Label NewEntity()
        {
            return new Customer_Label("CRM_Customer_Label", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Customer_Label Entity
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

            public XDataField id;
            public XDataField Title;
            public XDataField Name;
            public XDataField Content;
            public XDataField Style;
            public XDataField Format;
            public XDataField UserID;
            public XDataField Type;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Customer_Label parentEntity) : base(parentEntity) { }
            public MODEL(Customer_Label parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Customer_Label parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Customer_Label parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.id=new XDataField("id",DbType.Int32);
                this.Title=new XDataField("Title",DbType.String);
                this.Name=new XDataField("Name",DbType.String);
                this.Content=new XDataField("Content",DbType.String);
                this.Style=new XDataField("Style",DbType.Int32);
                this.Format=new XDataField("Format",DbType.String);
                this.UserID=new XDataField("UserID",DbType.String);
                this.Type=new XDataField("Type",DbType.Int32);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.id.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.id,this.Title,this.Name,this.Content,this.Style,this.Format,this.UserID,this.Type,this.Addtime });
            }
        }
    }
}
