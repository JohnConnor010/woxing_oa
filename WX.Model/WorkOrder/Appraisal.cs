
namespace WX.WorkOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Appraisal
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Appraisal : XDataEntity
    {
        public Appraisal(string tableName)
            : base(tableName)
        {
        }
        public Appraisal(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Appraisal(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Appraisal _entity;
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
        public static Appraisal NewEntity()
        {
            return new Appraisal("WorkOrder_Appraisal", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Appraisal Entity
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

        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField WID;
            public XDataField UserID;
            public XDataField Remarks;
            public XDataField State;
            public XDataField AddTime;

            public MODEL() { }
            public MODEL(Appraisal parentEntity) : base(parentEntity) { }
            public MODEL(Appraisal parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Appraisal parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Appraisal parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.WID=new XDataField("WID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.Remarks=new XDataField("Remarks",DbType.String);
                this.State=new XDataField("State",DbType.Int32);
                this.AddTime=new XDataField("AddTime",DbType.DateTime);   
                //
                            
                //                
                base.AddFields(new XDataField[] { this.ID,this.WID,this.UserID,this.Remarks,this.State,this.AddTime });
            }
        }
    }
}
