
namespace WX.PRO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class User
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class User : XDataEntity
    {
        public User(string tableName)
            : base(tableName)
        {
        }
        public User(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public User(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static User _entity;
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
        public static User NewEntity()
        {
            return new User("PRO_User", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static User Entity
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
            public XDataField PID;
            public XDataField type;
            public XDataField UserID;

            public MODEL() { }
            public MODEL(User parentEntity) : base(parentEntity) { }
            public MODEL(User parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(User parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(User parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int64);
                this.PID=new XDataField("PID",DbType.Int32);
                this.type=new XDataField("type",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);   
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.PID,this.type,this.UserID });
            }
        }
    }
}
