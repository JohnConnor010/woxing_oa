
namespace WX.WorkOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Dept
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Dept : XDataEntity
    {
        public Dept(string tableName)
            : base(tableName)
        {
        }
        public Dept(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Dept(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Dept _entity;
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
        public static Dept NewEntity()
        {
            return new Dept("WorkOrder_Dept", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Dept Entity
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
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
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

        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField WID;
            public XDataField DeptID;
            public XDataField State;
            public XDataField SubTime;
            public XDataField FPTime;
            public XDataField YSTime;
            public XDataField StopTime;
            public XDataField StateTime;

            public MODEL() { }
            public MODEL(Dept parentEntity) : base(parentEntity) { }
            public MODEL(Dept parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Dept parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Dept parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.WID = new XDataField("WID", DbType.Int32);
                this.DeptID = new XDataField("DeptID", DbType.Int32);
                this.State = new XDataField("State", DbType.Int32);
                this.SubTime = new XDataField("SubTime", DbType.DateTime);
                this.FPTime = new XDataField("FPTime", DbType.DateTime);
                this.YSTime = new XDataField("YSTime", DbType.DateTime);
                this.StopTime = new XDataField("StopTime", DbType.DateTime);
                this.StateTime = new XDataField("StateTime", DbType.DateTime);
                //

                //                
                base.AddFields(new XDataField[] { this.ID, this.WID, this.DeptID, State, SubTime, FPTime, YSTime, this.StopTime, StateTime });
            }
        }
    }
}
