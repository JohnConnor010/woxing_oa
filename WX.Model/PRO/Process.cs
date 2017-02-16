
namespace WX.PRO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Process
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Process : XDataEntity
    {
        public Process(string tableName)
            : base(tableName)
        {
        }
        public Process(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Process(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Process _entity;
        private static MODEL _model;
        public static void SetTime(DateTime starttime,int Projid,int no)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from PRO_Process where ProjID="+Projid+" and NO>="+no+" order by NO asc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ULCode.QDA.XSql.Execute("update PRO_Process set Starttime='" + starttime.ToString("yyyy-MM-dd") + "',Stoptime='"+starttime.AddDays(Convert.ToInt32(dt.Rows[i]["Days"].ToString())).ToString("yyyy-MM-dd")+"' where ID="+dt.Rows[i]["ID"].ToString());
                starttime = starttime.AddDays(Convert.ToInt32(dt.Rows[i]["Days"].ToString())+1);
            }
        }
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
        public static Process NewEntity()
        {
            return new Process("PRO_Process", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Process Entity
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
            public XDataField NO;
            public XDataField Persons;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Percnt;
            public XDataField Percnttime;
            public XDataField Days;
            public XDataField State;
            public XDataField Demo;

            public MODEL() { }
            public MODEL(Process parentEntity) : base(parentEntity) { }
            public MODEL(Process parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Process parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Process parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int64);
                this.ProjID = new XDataField("ProjID", DbType.Int32);
                this.NO = new XDataField("NO", DbType.Int32);
                this.Persons = new XDataField("Persons", DbType.Int32);
                this.Starttime = new XDataField("Starttime", DbType.DateTime);
                this.Stoptime = new XDataField("Stoptime", DbType.DateTime);
                this.Percnt = new XDataField("Percnt", DbType.Int32);
                this.Percnttime = new XDataField("Percnttime", DbType.Int32);
                this.State = new XDataField("State", DbType.Int32);
                this.Days = new XDataField("Days", DbType.Int32);
                this.Demo = new XDataField("Demo", DbType.String);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.ProjID, this.NO, this.Persons, this.Starttime, this.Stoptime, this.Percnt, this.Percnttime, this.State,this.Days,this.Demo });
            }
        }
    }
}
