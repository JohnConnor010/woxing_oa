
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Text_Template
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Text_Template : XDataEntity
    {
        public Text_Template(string tableName)
            : base(tableName)
        {
        }
        public Text_Template(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Text_Template(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Text_Template _entity;
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
        public static Text_Template NewEntity()
        {
            return new Text_Template("TE_Text_Templates", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Text_Template Entity
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
            public XDataField Title;
            public XDataField Template;
            public XDataField Apply;

            public MODEL() { }
            public MODEL(Text_Template parentEntity) : base(parentEntity) { }
            public MODEL(Text_Template parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Text_Template parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Text_Template parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.Type = new XDataField("Type", DbType.Byte);
                this.Title = new XDataField("Title", DbType.String);
                this.Template = new XDataField("Template", DbType.String);
                this.Apply = new XDataField("Apply", DbType.Boolean);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.Type, this.Title, this.Template, this.Apply });
            }
        }
    }
}
