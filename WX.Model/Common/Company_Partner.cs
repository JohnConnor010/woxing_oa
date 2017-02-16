
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Company_Partner
    {
        public static string[] Legalarray = new string[] { "", "法人" };
        public static string[] Shareholderarray = new string[] { "", "股东" };
        public static string[] Directorsarray = new string[] { "", "董事会" };
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Company_Partner : XDataEntity
    {
        public Company_Partner(string tableName)
            : base(tableName)
        {
        }
        public Company_Partner(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Company_Partner(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Company_Partner _entity;
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
        public static Company_Partner NewEntity()
        {
            return new Company_Partner("TE_Companys_Partner", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Company_Partner Entity
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

            public XDataField Id;
            public XDataField CompanyID;
            public XDataField EmployeeID;
            public XDataField Annex;
            public XDataField LNO;
            public XDataField Title;
            public XDataField Content;
            public XDataField PoliticalL;//政治面貌
            public XDataField Manage;
            public XDataField DepartentID;
            public XDataField Legal;
            public XDataField Shareholder;
            public XDataField Directors;
            public XDataField Share;
            public XDataField Assets;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Addtime;//政治面貌
            public XDataField State;

            public MODEL() { }
            public MODEL(Company_Partner parentEntity) : base(parentEntity) { }
            public MODEL(Company_Partner parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Company_Partner parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Company_Partner parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int32);
                this.CompanyID=new XDataField("CompanyID",DbType.Int32);
                this.EmployeeID = new XDataField("EmployeeID", DbType.String);
                this.Annex = new XDataField("Annex", DbType.String);
                this.LNO = new XDataField("LNO", DbType.String);
                this.Title = new XDataField("Title", DbType.String);
                this.Content = new XDataField("Content", DbType.String);
                this.Manage = new XDataField("Manage", DbType.String);
                this.DepartentID = new XDataField("DepartentID", DbType.Int32);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.Stoptime = new XDataField("Stoptime", DbType.DateTime);
                this.Starttime = new XDataField("Starttime", DbType.DateTime);
                this.Legal = new XDataField("Legal", DbType.Int32);
                this.PoliticalL = new XDataField("PoliticalL", DbType.String);
                this.Legal = new XDataField("Legal", DbType.Int32);
                this.Shareholder = new XDataField("Shareholder", DbType.Int32);
                this.Directors = new XDataField("Directors", DbType.Int32);
                this.Share = new XDataField("Share", DbType.Int32);
                this.Assets = new XDataField("Assets", DbType.Int32);
                this.State = new XDataField("State", DbType.Int32);
                //
                            
                //                
                base.AddFields(new XDataField[] { this.Id, this.CompanyID, this.PoliticalL, this.EmployeeID, this.Annex, this.LNO, this.Title, this.Content, this.Addtime, this.Manage, this.DepartentID, this.Legal, this.Shareholder, this.Directors, this.Share, this.Assets, this.Stoptime, this.Starttime, this.State });
            }
        }
    }
}
