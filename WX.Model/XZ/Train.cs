
namespace WX.XZ
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Train
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //Train
        }
    }
    public partial class Train : XDataEntity
    {
        public Train(string tableName)
            : base(tableName)
        {
        }
        public Train(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Train(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }

        private static Train _entity;
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
        public static Train NewEntity()
        {
            return new Train("XZ_Train", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Train Entity
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
        public static MODEL GetModel(int TrainID)
        {
            DataTable dt = XSql.GetDataTable("select * from XZ_Train where ID=" + TrainID);
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
        public static int GeCount(string wherestr)
        {
            return ULCode.QDA.XSql.GetData("select count(*) from XZ_Train" + (wherestr == "" ? "" : " where " + wherestr)).ToInt32();
        }
        public static DataTable GetPageList(string wherestr, int top, string orderBy, int pageSize, int pageIndex)
        {
            string sqlstr = "select A.*,tuser.RealName,(case A.Type when 1 then '考核' else '培训' end) TypeName from XZ_Train A left join TU_Users tuser on A.UserID=tuser.UserID" + (wherestr == "" ? "" : " where " + wherestr);
            string sSql = String.Format("exec [dbo].[SYST_pGetPageRows] '{0}',{1},'{2}',{3},{4}", sqlstr.Replace("'", "''"), top, orderBy, pageSize, pageIndex);
            return ULCode.QDA.XSql.GetDataTable(sSql);
        }

        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField Title;
            public XDataField Type;
            public XDataField UserID;
            public XDataField RunTime;
            public XDataField Addr;
            public XDataField FlowID;
            public XDataField Content;
            public XDataField UsersID;
            public XDataField UsersName;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Train parentEntity) : base(parentEntity) { }
            public MODEL(Train parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Train parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Train parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }

            public int Del()
            {
                WX.XZ.TrainUsers.DeleteToTrainID(this.ID.ToInt32());
                return this.Delete();
            }
            protected override void LoadFields()
            {

                this.ID = new XDataField("id", DbType.Int64);
                this.Title = new XDataField("Title", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.RunTime = new XDataField("RunTime", DbType.DateTime);
                this.Addr = new XDataField("Addr", DbType.String);
                this.FlowID = new XDataField("FlowID", DbType.Int32);
                this.Content = new XDataField("Content", DbType.String);
                this.UsersID = new XDataField("UsersID", DbType.String);
                this.UsersName = new XDataField("UsersName", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.Type, this.UserID, this.Title, this.Content, this.RunTime, this.Addr, this.FlowID, this.UsersID, this.UsersName, this.Addtime });
            }
        }
    }
}
