
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Company_WebSite
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Company_WebSite : XDataEntity
    {
        public Company_WebSite(string tableName)
            : base(tableName)
        {
        }
        public Company_WebSite(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Company_WebSite(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Company_WebSite _entity;
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
        public static Company_WebSite NewEntity()
        {
            return new Company_WebSite("TE_Companys_WebSite", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Company_WebSite Entity
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
            public XDataField CompanyID;
            public XDataField Name;
            public XDataField IP;
            public XDataField Url;
            public XDataField RecordNo;
            public XDataField Valid;
            public XDataField Feetime;
            public XDataField Warn;
            public XDataField Manage;
            public XDataField state;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Company_WebSite parentEntity) : base(parentEntity) { }
            public MODEL(Company_WebSite parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Company_WebSite parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Company_WebSite parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int64);
                this.CompanyID = new XDataField("CompanyID", DbType.Int32);
                this.Name=new XDataField("Name",DbType.String);
                this.IP=new XDataField("IP",DbType.String);
                this.Url=new XDataField("Url",DbType.String);
                this.RecordNo=new XDataField("RecordNo",DbType.String);
                this.Valid=new XDataField("Valid",DbType.DateTime);
                this.Feetime=new XDataField("Feetime",DbType.DateTime);
                this.Warn=new XDataField("Warn",DbType.Int32);
                this.Manage=new XDataField("Manage",DbType.String);
                this.state=new XDataField("state",DbType.Int32);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.CompanyID,this.Name, this.IP, this.Url, this.RecordNo, this.Valid, this.Feetime, this.Warn, this.Manage, this.state, this.Addtime });
            }
        }
    }
}
