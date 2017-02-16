
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;
    using System.Web;

    public partial class CompanyLicense
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class CompanyLicense : XDataEntity
    {
        public CompanyLicense(string tableName)
            : base(tableName)
        {
        }
        public CompanyLicense(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public CompanyLicense(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static CompanyLicense _entity;
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
        public static CompanyLicense NewEntity()
        {
            return new CompanyLicense("TE_Companys_license", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static CompanyLicense Entity
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
        public static MODEL GetRequestedModel()
        {
            return GetModel("Select * from [TE_Companys_license] where Id=" + HttpContext.Current.Request.QueryString["LicenseID"]);
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
            public XDataField CompanyId;//公司编号
            public XDataField Title;//证件名称
            public XDataField Type;//类型
            public XDataField Content;//内容
            public XDataField Annex;//附件
            public XDataField Addtime;//发证时间
            public XDataField LNO;//证件号码
            public XDataField Valid;//有效期起始时间
            public XDataField Validstop;//有效期结束时间
            public XDataField Checktime;//年审时间起始
            public XDataField Checkstoptime;//年审时间结束
            public XDataField Ischeck;//年审情况（0未审，1已审）
            public XDataField Checkdata;//年审所需材料
            public XDataField CheckDepartentID;//年审责任部门
            public XDataField CheckManage;//年审责任人
            public XDataField Warn;//提前多长时间提醒 单位（天）
            public XDataField Unit;//发证单位 
            public XDataField DepartentID;//证件原件保存部门 
            public XDataField Manage;//责任人
            public XDataField Investment;//投资比例
            public XDataField Share;//股份比例
            public XDataField PartnerUID;//法人/股东对应档案编号
            public XDataField LorS;//1法人/2股东

            public MODEL() { }
            public MODEL(CompanyLicense parentEntity) : base(parentEntity) { }
            public MODEL(CompanyLicense parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(CompanyLicense parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(CompanyLicense parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int64);
                this.CompanyId = new XDataField("CompanyId", DbType.Int32);
                this.Title = new XDataField("Title", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.Content = new XDataField("Content", DbType.String);
                this.Annex = new XDataField("Annex", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.LNO = new XDataField("LNO", DbType.String);
                this.Checktime = new XDataField("Checktime", DbType.DateTime);
                this.Checkstoptime = new XDataField("Checkstoptime", DbType.DateTime);
                this.Ischeck = new XDataField("Ischeck", DbType.Int32);
                this.Validstop = new XDataField("Validstop", DbType.DateTime);
                this.Valid = new XDataField("Valid", DbType.DateTime);
                this.Unit = new XDataField("Unit", DbType.String);
                this.DepartentID = new XDataField("DepartentID", DbType.Int32);
                this.CheckDepartentID = new XDataField("CheckDepartentID", DbType.Int32);
                this.Warn = new XDataField("Warn", DbType.Int32);
                this.Manage = new XDataField("Manage", DbType.String);
                this.Investment = new XDataField("Investment", DbType.Decimal);
                this.Share = new XDataField("Share", DbType.Decimal);
                this.Checkdata = new XDataField("Checkdata", DbType.String);
                this.CheckManage = new XDataField("CheckManage", DbType.String);
                this.PartnerUID = new XDataField("PartnerUID", DbType.Int32);
                this.LorS = new XDataField("LorS", DbType.Int32);
                //

                this.Id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.CompanyId, this.Title, this.Type, this.Content, this.Annex, this.Addtime, this.LNO, this.Checktime, this.Checkstoptime, this.Ischeck, this.Checkdata, this.CheckDepartentID, this.CheckManage, this.Warn, this.Valid, this.Validstop, this.Unit, this.DepartentID, this.Manage, this.Investment, this.Share, this.PartnerUID, this.LorS });
            }
        }
    }
}
