
namespace WX.XZ
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class NotifyFiles
    {
        //以下为实体开发部分
        //范围分类：1"公司层面", 2"部门内部", 3"上级部门内部", 4"顶级部门内部", 5"文件上报"
        public static string[] Areaarry = new string[] { "", "公司层面", "部门内部", "上级部门内部", "顶级部门内部", "文件上报" };
        //状态：1拟写，2退回，3审批中，4发布中（行政执行），5完成
        public static string[] Statearry = new string[] { "", "拟写", "退回", "审批中", "发布中", "完成" };
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class NotifyFiles : XDataEntity
    {
        public NotifyFiles(string tableName)
            : base(tableName)
        {
        }
        public NotifyFiles(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public NotifyFiles(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }

        private static NotifyFiles _entity;
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
        public static NotifyFiles NewEntity()
        {
            return new NotifyFiles("XZ_NotifyFiles", "id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static NotifyFiles Entity
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
        public static MODEL GetModel(int NotifyFilesID)
        {
            DataTable dt = XSql.GetDataTable("select * from XZ_NotifyFiles where ID=" + NotifyFilesID);
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
            public XDataField UserID;
            public XDataField Code;
            public XDataField Title;
            public XDataField Content;
            public XDataField Users;
            public XDataField Depms;
            public XDataField Istop;
            public XDataField Addtime;
            public XDataField Annex;
            public XDataField Area;
            public XDataField FlowID;
            public XDataField StepNo;
            public XDataField StepName;
            public XDataField RunID;
            public XDataField state;
            public XDataField PublishTime;

            public MODEL() { }
            public MODEL(NotifyFiles parentEntity) : base(parentEntity) { }
            public MODEL(NotifyFiles parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(NotifyFiles parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(NotifyFiles parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            public void CheckMess()
            {
                string sql = "";
                sql = "select UserID from TU_Users where State>=10 and State<40";
                WX.Model.User.MODEL usermodel=WX.Model.User.NewDataModel(this.UserID.ToString());
                WX.Model.Department.MODEL deptmodel = WX.Model.Department.NewDataModel(usermodel.DepartmentID.ToString());
                switch (Area.ToInt32())
                {
                    case 2: sql += " and DepartmentID=" + usermodel.DepartmentID.ToString(); break;
                    case 3:
                        {
                            string deptarry = ULCode.QDA.XSql.GetXDataTable("sp_get_tree_table 'TE_Departments','ID','Name','ParentId','Sort'," + deptmodel.ParentID.ToString() + ",1,5").ToColValueList("，", 0);
                            deptarry += (deptarry == "" ? "" : ",") + deptmodel.ParentID.ToString();
                            sql += " and DepartmentID in(" + deptarry + ")";
                        }
                        break;
                    case 4:
                        {
                            string deptarry = ULCode.QDA.XSql.GetXDataTable("sp_get_tree_table 'TE_Departments','ID','Name','ParentId','Sort'," + ULCode.QDA.XSql.GetDataTable("select dbo.get_oneid(" + usermodel.DepartmentID.ToString() + ")").Rows[0][0] + ",1,5").ToColValueList("，", 0);
                            deptarry += (deptarry == "" ? "" : ",") + deptmodel.ParentID.ToString();
                            sql += " and DepartmentID in(" + deptarry + ")";
                        } break;
                    default:
                        {
                            if (this.Depms.ToString() != "")
                                sql += " and DepartmentID in(" + this.Depms.ToString() + ")";
                            else if (this.Users.ToString() != "")
                                sql += " and UserID in('" + this.Users.ToString().Replace(",", "','") + "')";
                        } break;
                }
                System.Data.DataTable users = ULCode.QDA.XSql.GetDataTable(sql);
                for (int j = 0; j < users.Rows.Count; j++)
                {
                    WX.Model.Message.MODEL model = WX.Model.Message.NewDataModel();
                    model.Title.value = this.Title.ToString();
                    model.ID.value = Guid.NewGuid();
                    model.SendToUserId.value = users.Rows[j]["UserID"].ToString();
                    model.FromUserId.value = this.UserID.ToString();
                    model.SendTime.value = DateTime.Now;
                    model.RedirectToUrl.value = "/Manage/XZ/Notifyfilesshow.aspx?NotifyFileId=" + this.ID.ToString();
                    model.Type.value = "5";
                    model.Insert();
                }
            }
            protected override void LoadFields()
            {

                this.ID = new XDataField("id", DbType.Int64);
                this.Area = new XDataField("Area", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.Code = new XDataField("Code", DbType.String);
                this.Title = new XDataField("Title", DbType.String);
                this.Content = new XDataField("Content", DbType.String);
                this.Users = new XDataField("Users", DbType.String);
                this.Depms = new XDataField("Depms", DbType.String);
                this.Istop = new XDataField("Istop", DbType.Int32);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.Annex = new XDataField("Annex", DbType.String);
                this.FlowID = new XDataField("FlowID", DbType.Int32);
                this.StepNo = new XDataField("StepNo", DbType.Int32);
                this.StepName = new XDataField("StepName", DbType.String);
                this.RunID = new XDataField("RunID", DbType.Int32);
                this.state = new XDataField("state", DbType.Int32);
                this.PublishTime = new XDataField("PublishTime", DbType.DateTime);
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.Area, this.UserID, this.Code, this.Title, this.Content, this.Users, this.Istop, this.Addtime, this.Annex, this.FlowID, this.StepNo, this.StepName, this.RunID, this.state, this.PublishTime });
            }
        }
    }
}
