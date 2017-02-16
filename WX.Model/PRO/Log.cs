
namespace WX.PRO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Log
    {
        public static string[] logtype = new string[] { "申请", "审批", "项目启动", "项目管理", "步骤管理", "步骤开始","步骤跳转", "步骤结束", "项目结束" };
        //以下为实体开发部分
        //
        public static void AddLog(int type, int pid, string content,string IP)
        {
            WX.PRO.Log.MODEL model = WX.PRO.Log.NewDataModel();
            model.Type.value = type;
            if (pid > 0)
            {
                model.PID.value = pid;
            }
            model.Content.value = content;
            model.IP.value = IP;
            model.Insert();
        }
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Log : XDataEntity
    {
        public Log(string tableName)
            : base(tableName)
        {
        }
        public Log(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Log(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Log _entity;
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
        public static Log NewEntity()
        {
            return new Log("PRO_Logs", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Log Entity
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
            public XDataField Type;
            public XDataField PID;
            public XDataField Content;
            public XDataField Addtime;
            public XDataField IP;

            public MODEL() { }
            public MODEL(Log parentEntity) : base(parentEntity) { }
            public MODEL(Log parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Log parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Log parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int64);
                this.Type = new XDataField("Type", DbType.Int32);
                this.PID = new XDataField("PID", DbType.Int32);
                this.Content = new XDataField("Content", DbType.String);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.IP = new XDataField("IP", DbType.String);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.Type, this.PID, this.Content,this.IP, this.Addtime });
            }
        }
    }
}
