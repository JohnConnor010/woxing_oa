
namespace WX.AT
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Status
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Status : XDataEntity
    {
        public Status(string tableName)
            : base(tableName)
        {
        }
        public Status(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Status(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Status _entity;
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
        public static Status NewEntity()
        {
            return new Status("AT_Status", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Status Entity
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
            MODEL model = null;
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0)
            {
                WX.AT.Signin.SetState();
                XSql.Execute("exec ADD_AT_Signin");
                dt = XSql.GetDataTable(sSql);

            } if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                model = NewDataModel(dr);
            }
            else
            {
                model = NewDataModel();
            }
            return model;
        }
        public static DataTable GetList(int deptid)
        {
            string sSql = "exec [dbo].[Get_AT_StatusList] " + deptid;
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0)
            {
                WX.AT.Signin.SetState();
                XSql.Execute("exec ADD_AT_Signin");
                dt = GetList(deptid);
            }
            return dt;
        }
        public static DataTable GetUserList(string uid, DateTime time)
        {
            string sSql = "exec [dbo].[Get_AT_StatusUserList] '" + uid + "','" + time.ToString("yyyy-MM-dd") + "'";
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
        public static DataTable GetUserLogsList(string uid, DateTime time)
        {
            string sSql = "exec [dbo].[Get_AT_UserLogsList] '" + uid + "','" + time.ToString("yyyy-MM-dd") + "'";
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }

        public static DataTable GetDempList(string dempid, DateTime time)
        {
            string sSql = "exec [dbo].[Get_AT_StatusDempList] '" + dempid + "','" + time.ToString("yyyy-MM-dd") + "'";
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
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
            public XDataField State;
            public XDataField Uptime;
            public XDataField NoonState;

            public MODEL() { }
            public MODEL(Status parentEntity) : base(parentEntity) { }
            public MODEL(Status parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Status parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Status parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.State = new XDataField("State", DbType.Int32);
                this.Uptime = new XDataField("Uptime", DbType.DateTime);
                this.NoonState = new XDataField("NoonState", DbType.Int32);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.State, this.Uptime, this.NoonState });
            }
        }
    }
}
