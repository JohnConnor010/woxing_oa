
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Sell
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Sell : XDataEntity
    {
        public Sell(string tableName)
            : base(tableName)
        {
        }
        public Sell(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Sell(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Sell _entity;
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
        public static Sell NewEntity()
        {
            return new Sell("Count_Sell", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Sell Entity
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
        public static DataTable GetList(string typeid,string did,string userid)
        {
            string sql = "SELECT * FROM [Count_Sell] where TypeID=" + typeid + (did != "" ? " and DivisionID=" + did : "") + (userid != "" ? " and UserID='" + userid + "'" : "") + " order by payingTime";
            return XSql.GetDataTable(sql);
        }
        public static DataTable GetListUser(string typeid, string did, string userid)
        {
            string sql = "SELECT sell.Valuestr1 Valuestr,sell.payingTime,tuser.RealName RealName FROM [Count_Sell] sell left join Tu_Users tuser on sell.UserID=tuser.UserID where sell.TypeID=" + typeid + (did != "" ? " and sell.DivisionID=" + did : "") + (userid != "" ? " and sell.UserID='" + userid + "'" : "") + " order by sell.payingTime";
            return XSql.GetDataTable(sql);
        }
        public static DataTable GetListDivi(string typeid, string did, string userid)
        {
            string sql = "SELECT sell.Valuestr2 Valuestr,sell.payingTime,tedivi.Name RealName FROM [Count_Sell] sell left join TE_Departments tedivi on sell.DivisionID=tedivi.ID where sell.TypeID=" + typeid + (did != "" ? " and sell.DivisionID=" + did : "") + (userid != "" ? " and sell.UserID='" + userid + "'" : "") + " order by sell.payingTime";
            return XSql.GetDataTable(sql);
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
            public XDataField DivisionID;
            public XDataField UserID;
            public XDataField Fee;
            public XDataField TypeID;
            public XDataField PayingTime;
            public XDataField AddUserID;
            public XDataField Valuestr1;
            public XDataField Valuestr2;
            public XDataField Valuestr3;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Sell parentEntity) : base(parentEntity) { }
            public MODEL(Sell parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Sell parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Sell parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.DivisionID = new XDataField("DivisionID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.Fee = new XDataField("Fee", DbType.Decimal);
                this.TypeID = new XDataField("TypeID", DbType.Int32);
                this.PayingTime = new XDataField("PayingTime", DbType.DateTime);
                this.AddUserID = new XDataField("AddUserID", DbType.String);
                this.Valuestr1 = new XDataField("Valuestr1", DbType.String);
                this.Valuestr2 = new XDataField("Valuestr2", DbType.String);
                this.Valuestr3 = new XDataField("Valuestr3", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.DivisionID, this.UserID,this.Fee, this.TypeID, this.PayingTime
                    , this.AddUserID, this.Valuestr1, this.Valuestr2, this.Valuestr3, this.Addtime });
            }
        }
    }
}
