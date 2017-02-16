
namespace WX.Down.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Annex
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Annex : XDataEntity
    {
        public Annex(string tableName)
            : base(tableName)
        {
        }
        public Annex(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Annex(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }

        private static Annex _entity;
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
        public static Annex NewEntity()
        {
            return new Annex("Down_Annex", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Annex Entity
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
        public static MODEL GetModel(int AnnexID)
        {
            DataTable dt = XSql.GetDataTable("select * from Down_Annex where Id=" + AnnexID);
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
            public XDataField Name;
            public XDataField UserID;
            public XDataField DeptID;
            public XDataField Annex;
            public XDataField Demo;
            public XDataField Count;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Annex parentEntity) : base(parentEntity) { }
            public MODEL(Annex parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Annex parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Annex parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int16);
                this.Name = new XDataField("Name", DbType.String);
                this.UserID = new XDataField("UserID", DbType.String);
                this.DeptID = new XDataField("DeptID", DbType.Int32);
                this.Annex = new XDataField("Annex", DbType.String);
                this.Demo = new XDataField("Demo", DbType.String);
                this.Count = new XDataField("Count", DbType.Int32);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.Id.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.Name, this.UserID, this.DeptID, this.Annex, this.Demo, this.Count, this.Addtime });
            }
        }
    }
}
