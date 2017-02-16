
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class EmployeeContract
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class EmployeeContract : XDataEntity
    {
        public EmployeeContract(string tableName)
            : base(tableName)
        {
        }
        public EmployeeContract(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public EmployeeContract(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static EmployeeContract _entity;
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
        public static EmployeeContract NewEntity()
        {
            return new EmployeeContract("TU_Employees_Contract", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static DataTable GetList(string UserID,int type)
        {
            DataTable dt = XSql.GetDataTable("Select * from TU_Employees_Contract where UserID='"+UserID+"' and Type="+type);
            return dt;
        }
        public static EmployeeContract Entity
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
            public XDataField CNO;
            public XDataField Name;
            public XDataField UserId;
            public XDataField Annex;
            public XDataField BeginTime;
            public XDataField EndTime;
            public XDataField ManageUserID;
            public XDataField Type;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(EmployeeContract parentEntity) : base(parentEntity) { }
            public MODEL(EmployeeContract parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(EmployeeContract parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(EmployeeContract parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int64);
                this.CNO = new XDataField("CNO", DbType.String);
                this.Name = new XDataField("Name", DbType.String);
                this.Annex = new XDataField("Annex", DbType.String);
                this.UserId = new XDataField("UserId", DbType.String);
                this.BeginTime = new XDataField("BeginTime", DbType.DateTime);
                this.EndTime = new XDataField("EndTime", DbType.DateTime);
                this.ManageUserID = new XDataField("ManageUserID", DbType.String);
                this.Type = new XDataField("Type", DbType.Int64);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.Id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.UserId, this.CNO,this.Name, this.EndTime, this.BeginTime, this.ManageUserID, this.Annex,this.Type, this.Addtime });
            }
        }
    }
}
