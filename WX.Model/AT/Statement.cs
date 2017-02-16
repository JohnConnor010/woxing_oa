
namespace WX.AT
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Statement
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Statement : XDataEntity
    {
        public Statement(string tableName)
            : base(tableName)
        {
        }
        public Statement(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Statement(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Statement _entity;
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
        public static Statement NewEntity()
        {
            return new Statement("AT_Statements", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Statement Entity
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
        public static DataTable GetList(DateTime sdate)
        {
            string sSql = "exec [dbo].[Get_AT_StatementsList] '" + sdate + "'";
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt;
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField day1;
            public XDataField day2;
            public XDataField day3;
            public XDataField day4;
            public XDataField day5;
            public XDataField day6;
            public XDataField day7;
            public XDataField day8;
            public XDataField day9;
            public XDataField day10;
            public XDataField day11;
            public XDataField day12;
            public XDataField day13;
            public XDataField day14;
            public XDataField day15;
            public XDataField day16;
            public XDataField day17;
            public XDataField day18;
            public XDataField day19;
            public XDataField day20;
            public XDataField day21;
            public XDataField day22;
            public XDataField day23;
            public XDataField day24;
            public XDataField day25;
            public XDataField day26;
            public XDataField day27;
            public XDataField day28;
            public XDataField day29;
            public XDataField day30;
            public XDataField day31;
            public XDataField UserID;
            public XDataField LCD;
            public XDataField LZT;
            public XDataField LSJ;
            public XDataField LBJ;
            public XDataField LKG;
            public XDataField Other;
            public XDataField Isall;
            public XDataField Count;
            public XDataField Sdate;

            public MODEL() { }
            public MODEL(Statement parentEntity) : base(parentEntity) { }
            public MODEL(Statement parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Statement parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Statement parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.day1=new XDataField("day1",DbType.Int32);
                this.day2=new XDataField("day2",DbType.Int32);
                this.day3=new XDataField("day3",DbType.Int32);
                this.day4=new XDataField("day4",DbType.Int32);
                this.day5=new XDataField("day5",DbType.Int32);
                this.day6=new XDataField("day6",DbType.Int32);
                this.day7=new XDataField("day7",DbType.Int32);
                this.day8=new XDataField("day8",DbType.Int32);
                this.day9=new XDataField("day9",DbType.Int32);
                this.day10=new XDataField("day10",DbType.Int32);
                this.day11=new XDataField("day11",DbType.Int32);
                this.day12=new XDataField("day12",DbType.Int32);
                this.day13=new XDataField("day13",DbType.Int32);
                this.day14=new XDataField("day14",DbType.Int32);
                this.day15=new XDataField("day15",DbType.Int32);
                this.day16=new XDataField("day16",DbType.Int32);
                this.day17=new XDataField("day17",DbType.Int32);
                this.day18=new XDataField("day18",DbType.Int32);
                this.day19=new XDataField("day19",DbType.Int32);
                this.day20=new XDataField("day20",DbType.Int32);
                this.day21=new XDataField("day21",DbType.Int32);
                this.day22=new XDataField("day22",DbType.Int32);
                this.day23=new XDataField("day23",DbType.Int32);
                this.day24=new XDataField("day24",DbType.Int32);
                this.day25=new XDataField("day25",DbType.Int32);
                this.day26=new XDataField("day26",DbType.Int32);
                this.day27=new XDataField("day27",DbType.Int32);
                this.day28=new XDataField("day28",DbType.Int32);
                this.day29=new XDataField("day29",DbType.Int32);
                this.day30=new XDataField("day30",DbType.Int32);
                this.day31=new XDataField("day31",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.LCD=new XDataField("LCD",DbType.Int32);
                this.LZT=new XDataField("LZT",DbType.Int32);
                this.LSJ=new XDataField("LSJ",DbType.Int32);
                this.LBJ=new XDataField("LBJ",DbType.Int32);
                this.LKG=new XDataField("LKG",DbType.Int32);
                this.Other=new XDataField("Other",DbType.Int32);
                this.Isall=new XDataField("Isall",DbType.Int32);
                this.Count=new XDataField("Count",DbType.Int32);
                this.Sdate=new XDataField("Sdate",DbType.Date);   
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID,this.day1,this.day2,this.day3,this.day4,this.day5,this.day6,this.day7,this.day8,this.day9,this.day10,this.day11,this.day12,this.day13,this.day14,this.day15,this.day16,this.day17,this.day18,this.day19,this.day20,this.day21,this.day22,this.day23,this.day24,this.day25,this.day26,this.day27,this.day28,this.day29,this.day30,this.day31,this.UserID,this.LCD,this.LZT,this.LSJ,this.LBJ,this.LKG,this.Other,this.Isall,this.Count,this.Sdate });
            }
        }
    }
}
