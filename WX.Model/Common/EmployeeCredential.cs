
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class EmployeeCredential
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class EmployeeCredential : XDataEntity
    {
        public EmployeeCredential(string tableName)
            : base(tableName)
        {
        }
        public EmployeeCredential(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public EmployeeCredential(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static EmployeeCredential _entity;
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
        public static EmployeeCredential NewEntity()
        {
            return new EmployeeCredential("TU_Employees_Credentials", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static EmployeeCredential Entity
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
            public XDataField UserId;
            public XDataField Name;
            public XDataField Unit;
            public XDataField Ctime;
            public XDataField Content;
            public XDataField Annex;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(EmployeeCredential parentEntity) : base(parentEntity) { }
            public MODEL(EmployeeCredential parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(EmployeeCredential parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(EmployeeCredential parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int64);
                this.UserId = new XDataField("UserId", DbType.String);
                this.Name = new XDataField("Name", DbType.String);
                this.Unit = new XDataField("Unit", DbType.String);
                this.Ctime = new XDataField("Ctime", DbType.DateTime);
                this.Content = new XDataField("Content", DbType.String);
                this.Annex = new XDataField("Annex", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.Id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.UserId, this.Name, this.Unit, this.Ctime, this.Content, this.Annex, this.Addtime });
            }
        }
    }
}
