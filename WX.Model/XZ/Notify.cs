
namespace WX.XZ
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Notify
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Notify : XDataEntity
    {
        public Notify(string tableName)
            : base(tableName)
        {
        }
        public Notify(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Notify(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }

        private static Notify _entity;
        private static MODEL _model;
        public static void CheckMess()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from XZ_Notify where Ismes=1 and Starttime<='" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + "' and (Stoptime>'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + "' or Stoptime is null)");
            string sql = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ULCode.QDA.XSql.Execute("update XZ_Notify set Ismes=2 where id=" + dt.Rows[i]["id"].ToString());
                sql = "select UserID from TU_Users where State>=10 and State<40";
                if (dt.Rows[i]["depms"].ToString() != "")
                    sql += " and DepartmentID in(" + dt.Rows[i]["depms"].ToString() + ")";
                if (dt.Rows[i]["dutys"].ToString() != "")
                    sql += " and DutyId in(" + dt.Rows[i]["dutys"].ToString() + ")";
                if (dt.Rows[i]["Users"].ToString() != "")
                    sql += " and UserID in('" + dt.Rows[i]["Users"].ToString().Replace(",", "','") + "')";
                System.Data.DataTable users = ULCode.QDA.XSql.GetDataTable(sql);
                for (int j = 0; j < users.Rows.Count; j++)
                {
                    WX.Model.Message.MODEL model = WX.Model.Message.NewDataModel();
                    model.Title.value = dt.Rows[i]["Title"].ToString();
                    model.ID.value = Guid.NewGuid();
                    model.SendToUserId.value = users.Rows[j]["UserID"].ToString();
                    model.FromUserId.value = dt.Rows[i]["UserID"].ToString();
                    model.SendTime.value = dt.Rows[i]["Starttime"].ToString();
                    model.RedirectToUrl.value = "/Manage/XZ/NotifyDetail.aspx?NotifyID=" + dt.Rows[i]["id"];
                    model.Type.value = "5";
                    model.Insert();
                }
            }
        }
        
        public static void SetState(MODEL model)
        {
            ULCode.QDA.XSql.Execute("update XZ_Notify set Starttime='" + Convert.ToDateTime(model.Starttime.ToString()).ToString("yyyy-MM-dd") + "',Stoptime=null where id=" + model.ID.ToString());
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
        public static Notify NewEntity()
        {
            return new Notify("XZ_Notify", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Notify Entity
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
        public static MODEL GetModel(int NotifyID)
        {
            DataTable dt = XSql.GetDataTable("select * from XZ_Notify where ID=" + NotifyID);
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
            public XDataField CategoryID;
            public XDataField UserID;
            public XDataField Title;
            public XDataField Content;
            public XDataField Users;
            public XDataField Depms;
            public XDataField Dutys;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Ismes;
            public XDataField Istop;
            public XDataField Addtime;
            public XDataField Annex;

            public MODEL() { }
            public MODEL(Notify parentEntity) : base(parentEntity) { }
            public MODEL(Notify parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Notify parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Notify parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("id",DbType.Int64);
                this.CategoryID = new XDataField("CategoryID", DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.Title = new XDataField("Title", DbType.String);
                this.Content=new XDataField("Content",DbType.String);
                this.Users = new XDataField("Users", DbType.String);
                this.Depms = new XDataField("Depms", DbType.String);
                this.Dutys = new XDataField("Dutys", DbType.String);
                this.Starttime = new XDataField("Starttime", DbType.DateTime);
                this.Stoptime = new XDataField("Stoptime", DbType.DateTime);
                this.Ismes = new XDataField("Ismes", DbType.Int32);
                this.Istop = new XDataField("Istop", DbType.Int32);
                this.Addtime=new XDataField("Addtime",DbType.DateTime);
                this.Annex = new XDataField("Annex", DbType.String);
                //
            
                this.ID.isIdentity=true;                
                //                
                base.AddFields(new XDataField[] { this.ID, this.CategoryID, this.UserID, this.Title, this.Content, this.Users, this.Dutys, this.Depms, this.Starttime, this.Stoptime, this.Ismes, this.Istop, this.Addtime, this.Annex });
            }
        }
    }
}
