
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Contact
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Contact : XDataEntity
    {
        public Contact(string tableName)
            : base(tableName)
        {
        }
        public Contact(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Contact(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Contact _entity;
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
        public static Contact NewEntity()
        {
            return new Contact("CRM_Contact", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Contact Entity
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
            public XDataField CustomerID;
            public XDataField ContactName;
            public XDataField IsMain;
            public XDataField Sex;
            public XDataField Age;
            public XDataField Email;
            public XDataField FamilyPhone;
            public XDataField MobilePhone;
            public XDataField Fax;
            public XDataField WorkPhone;
            public XDataField Birthday;
            public XDataField Hobby;
            public XDataField BabySex;
            public XDataField BabyBirthday;
            public XDataField WorkAddress;
            public XDataField FamilyAddress;
            public XDataField CardPath;
            public XDataField PhotoPath;
            public XDataField Remarks;
            public XDataField Dept;
            public XDataField Duty;

            public MODEL() { }
            public MODEL(Contact parentEntity) : base(parentEntity) { }
            public MODEL(Contact parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Contact parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Contact parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.String);
                this.ContactName = new XDataField("ContactName", DbType.String);
                this.IsMain = new XDataField("IsMain", DbType.Int32);
                this.Sex = new XDataField("Sex", DbType.String);
                this.Age = new XDataField("Age", DbType.Int32);
                this.Email = new XDataField("Email", DbType.String);
                this.FamilyPhone = new XDataField("FamilyPhone", DbType.String);
                this.MobilePhone = new XDataField("MobilePhone", DbType.String);
                this.Fax = new XDataField("Fax", DbType.String);
                this.WorkPhone = new XDataField("WorkPhone", DbType.String);
                this.Birthday = new XDataField("Birthday", DbType.DateTime);
                this.Hobby = new XDataField("Hobby", DbType.String);
                this.BabySex = new XDataField("BabySex", DbType.String);
                this.BabyBirthday = new XDataField("BabyBirthday", DbType.String);
                this.WorkAddress = new XDataField("WorkAddress", DbType.String);
                this.FamilyAddress = new XDataField("FamilyAddress", DbType.String);
                this.CardPath = new XDataField("CardPath", DbType.String);
                this.PhotoPath = new XDataField("PhotoPath", DbType.String);
                this.Remarks = new XDataField("Remarks", DbType.String);
                this.Dept = new XDataField("Dept", DbType.String);
                this.Duty = new XDataField("Duty", DbType.String);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.CustomerID, this.ContactName,this.IsMain, this.Sex, this.Age
                    , this.Email, this.FamilyPhone, this.MobilePhone, this.Fax, this.WorkPhone, this.Birthday, this.Hobby
                    , this.BabySex, this.BabyBirthday, this.WorkAddress, this.FamilyAddress, this.CardPath, this.PhotoPath
                    , this.Remarks, this.Dept, this.Duty });
            }
        }
    }
}
