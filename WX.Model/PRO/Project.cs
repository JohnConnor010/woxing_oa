
namespace WX.PRO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Project
    {
        public static string[] statearray = new string[] { "编辑中", "申请中", "通过", "退回","进行中","结束"};
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Project : XDataEntity
    {
        public Project(string tableName)
            : base(tableName)
        {
        }
        public Project(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Project(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
       
        private static Project _entity;
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
        public static Project NewEntity()
        {
            return new Project("PRO_Projects", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Project Entity
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
            public XDataField UserID;
            public XDataField ProjectName;
            public XDataField Content;
            public XDataField Annex;
            public XDataField Days;
            public XDataField Persons;
            public XDataField Fee;
            public XDataField Imagine;
            public XDataField Manage;
            public XDataField Opinion;
            public XDataField Stime;
            public XDataField State;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Project parentEntity) : base(parentEntity) { }
            public MODEL(Project parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Project parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Project parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int64);
                this.UserID=new XDataField("UserID",DbType.String);
                this.ProjectName=new XDataField("ProjectName",DbType.String);
                this.Content=new XDataField("Content",DbType.String);
                this.Annex=new XDataField("Annex",DbType.String);
                this.Days=new XDataField("Days",DbType.Int32);
                this.Persons=new XDataField("Persons",DbType.Int32);
                this.Fee=new XDataField("Fee",DbType.Int32);
                this.Imagine=new XDataField("Imagine",DbType.String);
                this.Manage=new XDataField("Manage",DbType.String);
                this.Opinion=new XDataField("Opinion",DbType.String);
                this.Stime=new XDataField("Stime",DbType.DateTime);
                this.State=new XDataField("State",DbType.Int32);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.UserID,this.ProjectName,this.Content,this.Annex,this.Days,this.Persons,this.Fee,this.Imagine,this.Manage,this.Opinion,this.Stime,this.State,this.Addtime });
            }
        }
    }
}
