
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

     public partial class IntoJob
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class IntoJob : XDataEntity
    {
        public IntoJob(string tableName)
            : base(tableName)
        {
        }
        public IntoJob(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public IntoJob(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static IntoJob _entity;
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
        public static IntoJob NewEntity()
        {
            return new IntoJob("HR_Intojobs", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static IntoJob Entity
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
            public XDataField jobsname;
            public XDataField salary;
            public XDataField PSalary;
            public XDataField dempOpinion;
            public XDataField Addtime;
            public XDataField GradeID;
            public XDataField deptid;
            public XDataField SignUserID;

            public MODEL() { }
            public MODEL(IntoJob parentEntity) : base(parentEntity) { }
            public MODEL(IntoJob parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(IntoJob parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(IntoJob parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.jobsname=new XDataField("jobsname",DbType.String);
                this.salary=new XDataField("salary",DbType.Int32);
                this.PSalary = new XDataField("PSalary", DbType.Int32);
                this.dempOpinion=new XDataField("dempOpinion",DbType.String);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);
                this.GradeID = new XDataField("GradeID", DbType.Int32);
                this.deptid = new XDataField("deptid", DbType.Int32);
                this.SignUserID = new XDataField("SignUserID", DbType.String);
                //
            
                this.ID.isIdentity=true;
                this.ID.isKeyField=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.jobsname, this.salary, this.PSalary, this.dempOpinion, this.Addtime, this.GradeID, this.deptid, this.SignUserID });
            }
        }
    }
}
