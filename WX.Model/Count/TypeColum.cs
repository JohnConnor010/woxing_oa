
namespace WX.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;
    using System.Web.UI;

    public partial class TypeColum
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class TypeColum : XDataEntity
    {
        public TypeColum(string tableName)
            : base(tableName)
        {
        }
        public TypeColum(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public TypeColum(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static TypeColum _entity;
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
        public static TypeColum NewEntity()
        {
            return new TypeColum("Count_TypeColum", "ID");
        }
        public static DataTable Variabledt;
        public static void loadGetVariable()
        {
            Variabledt= XSql.GetDataTable("Select * from FD_Variable where Type=0 or Type=2 order by ID asc");
        }
        public static string VarIsNone(string Typeid,string VarName)
        {
            if (Typeid == "")
                return "none";
            System.Data.DataTable dt = XSql.GetDataTable("select * from Count_TypeColum where TypeID=" + Typeid + " and Formula like '%${" + VarName + "}%'");
            return dt.Rows.Count>0?"block":"none";
        }
        public static string GetFormula(Page sPage, string str,DataTable dt)
        {
            if (str.IndexOf("{") > -1)
            {
                //if (Variabledt == null)
                loadGetVariable();
                if (Variabledt != null)
                {
                    System.Web.UI.Control cc = null;
                    for (int i = 0; i < Variabledt.Rows.Count; i++)
                    {
                        if (str.IndexOf("%{") == -1 && Variabledt.Rows[i]["Type"].ToString() == "0")
                        {
                            cc = sPage.Master.FindControl("ContentPlaceHolder").FindControl(Variabledt.Rows[i]["EnName"].ToString());
                            if (cc != null)
                            {
                                if (cc.GetType().Name == "TextBox")
                                str = str.Replace(Variabledt.Rows[i]["VarValue"].ToString(), ((System.Web.UI.WebControls.TextBox)cc).Text);
                                else if(cc.GetType().Name == "DropDownList")
                                    str = str.Replace(Variabledt.Rows[i]["VarValue"].ToString(), ((System.Web.UI.WebControls.DropDownList)cc).SelectedValue);
                            }
                        }
                        else if (str.IndexOf("%{") > -1 && Variabledt.Rows[i]["Type"].ToString() == "2")
                        {
                            if (str.IndexOf(Variabledt.Rows[i]["VarValue"].ToString())>-1 )
                            {
                                if (cc == null)
                                    cc = sPage.Master.FindControl("ContentPlaceHolder").FindControl(Variabledt.Rows[0]["EnName"].ToString());
                                string sqlstr = "select case " + Variabledt.Rows[i]["Demo"].ToString().Replace("{0}", ((System.Web.UI.WebControls.TextBox)cc).Text) + " end";
                                str = XSql.GetDataTable(sqlstr).Rows[0][0].ToString();
                            }
                        }
                    }
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    str = str.Replace("&{" + dt.Columns[i].ColumnName + "}", dt.Rows[0][i].ToString());
                }
            }
                return str;
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static TypeColum Entity
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
            public XDataField TypeID;
            public XDataField Name;
            public XDataField Mark;
            public XDataField Formula;
            public XDataField FormulaShow;
            public XDataField OrderNo;
            public XDataField Visible1;
            public XDataField Visible2;
            public XDataField Visible3;

            public MODEL() { }
            public MODEL(TypeColum parentEntity) : base(parentEntity) { }
            public MODEL(TypeColum parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(TypeColum parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(TypeColum parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            public bool IsNull(string where)
            {
                System.Data.DataTable dt = XSql.GetDataTable("select * from Count_TypeColum where " + where);
                return dt.Rows.Count > 0 ? true : false;
            }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.TypeID = new XDataField("TypeID", DbType.Int32);
                this.Name = new XDataField("Name", DbType.String);
                this.Mark = new XDataField("Mark", DbType.String);
                this.Formula = new XDataField("Formula", DbType.String);
                this.FormulaShow = new XDataField("FormulaShow", DbType.String);
                this.OrderNo = new XDataField("OrderNo", DbType.Int32);
                this.Visible1 = new XDataField("Visible1", DbType.Int32);
                this.Visible2 = new XDataField("Visible2", DbType.Int32);
                this.Visible3 = new XDataField("Visible3", DbType.Int32);
                //

                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.TypeID, this.Name,this.Mark, this.Formula, this.FormulaShow, this.OrderNo
                    , this.Visible1, this.Visible2, this.Visible3 });
            }
        }
    }
}
