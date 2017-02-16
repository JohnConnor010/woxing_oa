
namespace WX.HR
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;
    
    public partial class Receive
    {
        //以下为实体开发部分
        //

        public static string[] Statestr = new string[] { "未回复", "回复", "不合格", "合格" };
        public partial class MODEL
        {
            //以下为模型开发部分
            //
            public DataTable GetDeptCount()
            {
                return XSql.GetDataTable("select count(distinct NextUserID),count(ID),count(nullif([State],3)) from HR_Receive where UserID='" + this.UserID.ToString() + "'");
            }
        }
    }
    public partial class Receive : XDataEntity
    {
        public Receive(string tableName)
            : base(tableName)
        {
        }
        public Receive(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Receive(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Receive _entity;
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
        public static Receive NewEntity()
        {
            return new Receive("HR_Receive", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Receive Entity
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
        public static DataTable GetList(string wherestr)
        {
            return XSql.GetDataTable("select * from HR_Receive " + (wherestr==""?"":"where "+wherestr));
        }
       
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField UserID;
            public XDataField DeptID;
            public XDataField NextUserID;
            public XDataField Question;
            public XDataField QuestionTime;
            public XDataField Answer;
            public XDataField AnswerTime;
            public XDataField State;
            public XDataField AddUserID;
            public XDataField ConfirmUserID;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(Receive parentEntity) : base(parentEntity) { }
            public MODEL(Receive parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Receive parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Receive parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.DeptID = new XDataField("DeptID", DbType.Int32);
                this.NextUserID = new XDataField("NextUserID", DbType.String);
                this.Question = new XDataField("Question", DbType.String);
                this.QuestionTime = new XDataField("QuestionTime", DbType.DateTime);
                this.Answer = new XDataField("Answer", DbType.String);
                this.AnswerTime = new XDataField("AnswerTime", DbType.DateTime);
                this.State = new XDataField("State", DbType.Int32);
                this.AddUserID = new XDataField("AddUserID", DbType.String);
                this.ConfirmUserID = new XDataField("ConfirmUserID", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.DeptID, this.NextUserID, this.State, this.Question, this.QuestionTime, this.Answer, this.AnswerTime, this.AddUserID, this.ConfirmUserID, this.Addtime });
            }
        }
    }
}
