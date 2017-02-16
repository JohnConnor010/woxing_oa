
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Customer_Laber
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Customer_Laber : XDataEntity
    {
        public Customer_Laber(string tableName)
            : base(tableName)
        {
        }
        public Customer_Laber(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Customer_Laber(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Customer_Laber _entity;
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
        public static Customer_Laber NewEntity()
        {
            return new Customer_Laber("CRM_Customer_Laber", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Customer_Laber Entity
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
            public MODEL(Customer_Laber parentEntity) : base(parentEntity) { }
            public MODEL(Customer_Laber parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Customer_Laber parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Customer_Laber parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
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
