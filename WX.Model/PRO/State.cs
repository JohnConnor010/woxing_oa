
namespace WX.PRO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class State
    {
        public static string[] statearray = new string[] { "未启动", "进行中", "结束"};
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class State : XDataEntity
    {
        public State(string tableName)
            : base(tableName)
        {
        }
        public State(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public State(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static State _entity;
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
        public static State NewEntity()
        {
            return new State("PRO_State", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static State Entity
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
            public XDataField ProjID;
            public XDataField ProcID;
            public XDataField Manage;
            public XDataField Fee;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Percnt;
            public XDataField Percnttime;
            public XDataField State;
            public XDataField YJStarttime;
            public XDataField Addtime;

            public MODEL() { }
            public MODEL(State parentEntity) : base(parentEntity) { }
            public MODEL(State parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(State parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(State parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int64);
                this.ProjID=new XDataField("ProjID",DbType.Int32);
                this.ProcID=new XDataField("ProcID",DbType.Int32);
                this.Manage=new XDataField("Manage",DbType.String);
                this.Fee=new XDataField("Fee",DbType.Int32);
                this.Starttime=new XDataField("Starttime",DbType.DateTime);
                this.Stoptime=new XDataField("Stoptime",DbType.DateTime);
                this.Percnt=new XDataField("Percnt",DbType.Int32);
                this.Percnttime=new XDataField("Percnttime",DbType.Int32);
                this.State=new XDataField("State",DbType.Int32);
                this.YJStarttime = new XDataField("YJStarttime", DbType.DateTime);   
                this.Addtime=new XDataField("Addtime",DbType.DateTime);   
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.ProjID,this.ProcID,this.Manage,this.Fee,this.Starttime,this.Stoptime,this.Percnt,this.Percnttime,this.State,this.YJStarttime,this.Addtime });
            }
        }
    }
}
