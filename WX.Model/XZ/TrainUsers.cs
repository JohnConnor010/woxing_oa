
namespace WX.XZ
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class TrainUsers
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //TrainUsers
        }
    }
    public partial class TrainUsers : XDataEntity
    {
        public TrainUsers(string tableName)
            : base(tableName)
        {
        }
        public TrainUsers(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public TrainUsers(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        private static TrainUsers _entity;
        private static MODEL _model;

        public static MODEL NewDataModel()
        {
            return new MODEL(Entity);
        }
        public static int GeCount(string wherestr)
        {
            return ULCode.QDA.XSql.GetData("select count(*) from XZ_TrainUsers A" + (wherestr == "" ? "" : " where " + wherestr)).ToInt32();
        }
        public static DataTable GetPageList(string wherestr, int top, string orderBy, int pageSize, int pageIndex)
        {
            string sqlstr = "select B.*,tuser.RealName,(case B.Type when 1 then '考核' else '培训' end) TypeName from XZ_TrainUsers A left join XZ_Train B on A.TrainID=B.ID left join TU_Users tuser on B.UserID=tuser.UserID" + (wherestr == "" ? "" : " where " + wherestr);
            string sSql = String.Format("exec [dbo].[SYST_pGetPageRows] '{0}',{1},'{2}',{3},{4}", sqlstr.Replace("'", "''"), top, orderBy, pageSize, pageIndex);
            return ULCode.QDA.XSql.GetDataTable(sSql);
        }

        public static DataTable GetUsersList(int trainid)
        {
            string sqlstr = "select A.State,A.RunID,A.UserID,tuser.RealName from XZ_TrainUsers A left join TU_Users tuser on A.UserID=tuser.UserID where A.TrainID=" + trainid;
            return ULCode.QDA.XSql.GetDataTable(sqlstr);
        }
        public static void DeleteToTrainID(int trainid)
        {
            ULCode.QDA.XSql.Execute("delete from XZ_TrainUsers where TrainID=" + trainid);
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
        public static TrainUsers NewEntity()
        {
            return new TrainUsers("XZ_TrainUsers", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static TrainUsers Entity
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
        public static MODEL GetModelToTrainID(int trainid, string userid)
        {
            return WX.XZ.TrainUsers.GetModel("select * from XZ_TrainUsers where TrainID=" + trainid + " and UserID='" + userid + "'");
        }
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
        }
        public static MODEL GetModel(int TrainUsersID)
        {
            DataTable dt = XSql.GetDataTable("select * from XZ_TrainUsers where ID=" + TrainUsersID);
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
            public XDataField TrainID;
            public XDataField UserID;
            public XDataField RETime;
            public XDataField State;
            public XDataField RunID;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(TrainUsers parentEntity) : base(parentEntity) { }
            public MODEL(TrainUsers parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(TrainUsers parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(TrainUsers parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("id", DbType.Int64);
                this.UserID = new XDataField("UserID", DbType.String);
                this.TrainID = new XDataField("TrainID", DbType.Int32);
                this.RETime = new XDataField("RETime", DbType.DateTime);
                this.State = new XDataField("State", DbType.Int32);
                this.RunID = new XDataField("RunID", DbType.Int32);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.TrainID, this.UserID, this.RETime, this.RunID, this.State, this.Addtime });
            }
        }
    }
}
