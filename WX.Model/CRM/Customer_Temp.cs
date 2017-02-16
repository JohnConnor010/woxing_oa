
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Customer_Temp
    {
        //以下为实体开发部分
        //
        public static string[] TypeStr = new string[] { "全部", "方案", "协议" };
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Customer_Temp : XDataEntity
    {
        public Customer_Temp(string tableName)
            : base(tableName)
        {
        }
        public Customer_Temp(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Customer_Temp(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Customer_Temp _entity;
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
        public static Customer_Temp NewEntity()
        {
            return new Customer_Temp("CRM_Customer_Temp", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Customer_Temp Entity
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
            public XDataField UserID;
            public XDataField Type;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Customer_Temp parentEntity) : base(parentEntity) { }
            public MODEL(Customer_Temp parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Customer_Temp parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Customer_Temp parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.id=new XDataField("id",DbType.Int32);
                this.Title=new XDataField("Title",DbType.String);
                this.Name=new XDataField("Name",DbType.String);
                this.Content=new XDataField("Content",DbType.String);
                this.UserID=new XDataField("UserID",DbType.String);
                this.Type=new XDataField("Type",DbType.Int32);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.id.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.id,this.Title,this.Name,this.Content,this.UserID,this.Type,this.Addtime });
            }
        }
    }
}
