using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ULCode;
using ULCode.QDA;
namespace WX.Model
{
    public partial class Appraise
    {
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
        public partial class Appraise : XDataEntity
        {
            public Appraise(string tableName)
                : base(tableName)
            {
            }
            public Appraise(string tableName, string keyField)
                : base(tableName, keyField)
            {
            }
            public Appraise(string cnName, string tableName, string keyField)
                : base(cnName, tableName, keyField)
            {
            }
            //静态部分
            private static Appraise _entity;
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
            public static Appraise NewEntity()
            {
                return new Appraise("Plan_Appraise", "id");
            }
            public static MODEL NewModel()
            {
                return new MODEL();
            }
            public static Appraise Entity
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
            public static DataTable GetList(int PlanID)
            {
                return XSql.GetDataTable("select * from PLAN_Appraise where PlanID="+PlanID);
            }
            public static MODEL GetModel(int AppraiseId)
            {
                return GetModel("select * from PLAN_Appraise where id=" + AppraiseId);
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
                public XDataField Appraise;
                public XDataField Content;
                public XDataField UserID;
                public XDataField Addtime;

                public MODEL() { }
                public MODEL(Appraise parentEntity) : base(parentEntity) { }
                public MODEL(Appraise parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
                public MODEL(Appraise parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
                public MODEL(Appraise parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
                protected override void LoadFields()
                {
                    this.id = new XDataField("id", DbType.Int32);
                    this.PlanID = new XDataField("PlanID", DbType.Int32);
                    this.Appraise = new XDataField("Appraise", DbType.Int32);
                    this.UserID = new XDataField("UserID", DbType.String);
                    this.Content = new XDataField("Content", DbType.String);
                    this.Addtime = new XDataField("Addtime", DbType.DateTime);
                    //
                    this.id.isIdentity = true;
                    //                
                    base.AddFields(new XDataField[] { this.id, this.PlanID, this.Appraise, this.UserID, this.Content, this.Addtime });
                }
            }
        }
    }
