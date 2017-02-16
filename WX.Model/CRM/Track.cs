
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Track
    {
        //以下为实体开发部分
        //// //客户维护日志--0电话沟通, 1短信沟通, 2初次上门, 3二次跟踪", 4方案递送", 5协议签订", 6发票寄送", 7客户合作项目分配", 8合同执行", 9维护", 10余款催缴"
        public static string[] ProcessState = new string[] { "电话沟通", "短信沟通", "初次上门", "二次跟踪", "方案递送", "协议签订", "发票寄送", "客户合作项目分配", "合同执行", "维护", "余款催缴" };

        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Track : XDataEntity
    {
        public Track(string tableName)
            : base(tableName)
        {
        }
        public Track(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Track(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Track _entity;
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
        public static Track NewEntity()
        {
            return new Track("CRM_Track", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Track Entity
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

            public XDataField id;
            public XDataField CustomerID;
            public XDataField UserID;
            public XDataField ProcessState;
            public XDataField TrackNo;
            public XDataField Remarks;
            public XDataField Fee;
            public XDataField State;
            public XDataField TrackTime;
            public XDataField Addtime;
            public XDataField IP;
            public XDataField LogParaments;
            public XDataField Demo;

            public XDataField Type;
            public MODEL() { }
            public MODEL(Track parentEntity) : base(parentEntity) { }
            public MODEL(Track parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Track parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Track parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.ProcessState = new XDataField("ProcessState", DbType.Int32);
                this.TrackNo = new XDataField("TrackNo", DbType.Int32);
                this.Remarks = new XDataField("Remarks", DbType.String);
                this.Fee = new XDataField("Fee", DbType.Decimal);
                this.State = new XDataField("State", DbType.Int32);
                this.TrackTime = new XDataField("TrackTime", DbType.DateTime);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.IP = new XDataField("IP", DbType.String);
                this.LogParaments = new XDataField("LogParaments", DbType.String);
                this.Type = new XDataField("Type", DbType.Int32);
                this.Demo = new XDataField("Demo", DbType.String);
                //

                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.CustomerID, this.UserID, this.ProcessState, this.TrackNo, this.Remarks, this.Fee, this.State, this.TrackTime, this.Addtime, this.IP, this.LogParaments,this.Type,this.Demo });
            }
        }
    }
}
