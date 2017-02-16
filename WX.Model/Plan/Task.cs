
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Task
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Task : XDataEntity
    {
        public Task(string tableName)
            : base(tableName)
        {
        }
        public Task(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Task(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Task _entity;
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
        public static Task NewEntity()
        {
            return new Task("PLAN_Task", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Task Entity
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
        public static MODEL GetModel(int TaskId)
        {
            return GetModel("select * from PLAN_Task where id=" + TaskId);
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
            public XDataField PlanID;
            public XDataField Title;
            public XDataField UserIDs;
            public XDataField Content;
            public XDataField Appraise;
            public XDataField State;
            public XDataField Etime;
            public XDataField Statetime;

            public MODEL() { }
            public MODEL(Task parentEntity) : base(parentEntity) { }
            public MODEL(Task parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Task parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Task parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.id = new XDataField("id", DbType.Int32);
                this.PlanID = new XDataField("PlanID", DbType.Int32);
                this.Title = new XDataField("Title", DbType.String);
                this.UserIDs = new XDataField("UserIDs", DbType.String);
                this.Content = new XDataField("Content", DbType.String);
                this.Appraise = new XDataField("Appraise", DbType.String);
                this.State = new XDataField("State", DbType.Int32);
                this.Etime = new XDataField("Etime", DbType.String);
                this.Statetime = new XDataField("Statetime", DbType.DateTime);
                //

                this.id.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.id, this.PlanID, this.Title, this.UserIDs, this.Content, this.Appraise, this.State, this.Etime, this.Statetime });
            }
        }
    }
}
