
namespace WX.Flow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Flow
    {
        private static List<MODEL> _Caches = null;
        public static List<MODEL> Caches
        {
            get
            {
                if (_Caches == null)
                {
                    _Caches = GetModels("Select * from FL_Flows order by Id");
                }
                return _Caches;
            }
        }
        public static MODEL GetCache(int id)
        {
            return Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == id; });
        }
        public partial class MODEL
        {
            public int SaveIntoCaches()
            {
                if (Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == this.Id.ToInt32(); }) == null)
                {
                    Caches.Add(this);
                    return 1;
                }
                else
                    return 0;
            }
            public int RemoveFromCaches()
            {
                MODEL m = Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == this.Id.ToInt32(); });
                if (m != null)
                {
                    Caches.Remove(m);
                    return 1;
                }
                else
                    return 0;
            }
            /// <summary>
            /// 获取步骤列表，默认为空，需要LoadProcessList初始化后才可使用
            /// </summary>
            public List<Process.MODEL> ProcessList = null;
            /// <summary>
            /// 运行后，ProcessList方可使用
            /// </summary>
            /// <param name="reload"></param>
            public void LoadProcessList(bool reload)
            {
                if (reload && this.ProcessList != null)
                {
                    this.ProcessList.Clear();
                    this.ProcessList = null;
                }
                if (this.ProcessList == null)
                {
                    //string sSql = String.Format("Select * from FL_Process where FlowId={0} order by StepNo", this.Id);
                    ProcessList = WX.Flow.Model.Process.Caches.FindAll(delegate(WX.Flow.Model.Process.MODEL dele) { return dele.FlowId.ToInt32() == this.Id.ToInt32(); }); //Process.GetModels(sSql);
                    ProcessList.Sort();
                    //？如何排序
                }
            }
            public int GetLastStepNo()
            {
                this.LoadProcessList(false);
                this.ProcessList.Sort();
                int iCount = this.ProcessList.Count;
                return this.ProcessList[iCount - 1].StepNo.ToInt32(); 
            }
            /// <summary>
            /// 流程所用表单，默认为空，只有LoadForm初始化后方可使用
            /// </summary>
            public Form.MODEL Form = null;
            /// <summary>
            /// LoadForm运行后,Form方可使用
            /// </summary>
            /// <param name="reload"></param>
            public void LoadForm(bool reload)
            {
                if (reload && this.Form != null)
                {
                    this.Form = null;
                }
                if (this.Form == null)
                {
                    Form = WX.Flow.Model.Form.GetCache(this.FormId.ToInt32());//WX.Flow.Model.Form.NewDataModel(this.FormId.ToInt32());
                }
            }
            /// <summary>
            /// 流水号规则，默认为空，需要LoadNumberRule初始化
            /// </summary>
            public NumberRule.MODEL NumberRule = null;
            /// <summary>
            /// 初始化NumberRule
            /// </summary>
            /// <param name="reload"></param>
            public void LoadNumberRule(bool reload)
            {
                if (reload && this.NumberRule != null)
                {
                    this.NumberRule = null;
                }
                if (this.NumberRule == null)
                {
                    NumberRule = WX.Flow.Model.NumberRule.NewDataModel(this.NumberRuleId.ToInt32());
                }
            }
            /// <summary>
            /// 获取单个步骤
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public Process.MODEL GetProcessByStep(int step)
            {
                this.LoadProcessList(false);
                return this.ProcessList.Find(delegate(Process.MODEL prcs_dele) { return prcs_dele.StepNo.ToInt32() == step; });
            }
            /// <summary>
            /// 新建工作
            /// 返回工作编号
            /// </summary>
            /// <param name="name">工作流水号名称</param>
            /// <returns></returns>
            public int NewWork(string name)
            {
                //添加工作
                Run.MODEL runNew = Run.NewDataModel();
                runNew.Name.set(name);
                runNew.FlowId.set(this.Id);
                runNew.ParentId.set(0);
                runNew.BeginUser.set(WX.Public.CurUser.UserID);
                runNew.StepNo.set(1);
                runNew.Deal_Flag.set(DealFlag.NotReceived);
                runNew.BeginTime.set_DateTime_Now();
                int newRunId = runNew.Insert(true);
                if (newRunId == 0)
                {
                    return 0;
                }
                //添加步骤状态
                WX.Public.CurUser.LoadUserModel(false);
                string sSql = String.Format("insert into FL_RunProcess(RunId,StepNo,ParentNo,UserId,WorkTime,DeliverTime,Deal_Flag,OP_Flag,Comment) "
                    + " values({0},{1},0,'{2}',GetDate(),NULL,{3},{4},'{5}')"
                    , newRunId, 1, WX.Public.CurUser.UserID, runNew.Deal_Flag.ToInt32(), 1, String.Format("主办人：{0}，{1}第1步,{2}", WX.Public.CurUser.UserModel.RealName, this.Name, DateTime.Now));
                int iR = ULCode.QDA.XSql.Execute(sSql);
                if (iR == 0)
                {
                    //步骤状态添加失败
                    runNew.Delete();
                    return -1;
                }
                else
                    return newRunId;
            }
        }
    }
    public partial class Flow : XDataEntity
    {
        public Flow(string tableName)
            : base(tableName)
        {
        }
        public Flow(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Flow(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Flow _entity;
        private static MODEL _model;
        public static DataTable GetDataList(string wherestr)
        {
            return ULCode.QDA.XSql.GetDataTable("select * from FL_Flows "+(wherestr!=""?" where "+wherestr:""));
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
        public static Flow NewEntity()
        {
            return new Flow("FL_Flows", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Flow Entity
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

            public XDataField Id;
            public XDataField CatagoryId;
            public XDataField Type;
            public XDataField FormId;
            public XDataField Name;
            public XDataField AllowAttach;
            public XDataField Sort;
            public XDataField Description;
            public XDataField AuthorizeMode;
            public XDataField ExtendFields;
            public XDataField AllowView;
            public XDataField ViewPriv;
            public XDataField NumberRuleId;
            public XDataField AttachIdList;
            public XDataField AttachNameList;
            public XDataField DepartmentId;
            public XDataField IsVisible;

            public MODEL() { }
            public MODEL(Flow parentEntity) : base(parentEntity) { }
            public MODEL(Flow parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Flow parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Flow parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int16);
                this.CatagoryId = new XDataField("CatagoryId", DbType.Int16);
                this.Type = new XDataField("Type", DbType.Byte);
                this.FormId = new XDataField("FormId", DbType.Int16);
                this.Name = new XDataField("Name", DbType.String);
                this.AllowAttach = new XDataField("AllowAttach", DbType.Boolean);
                this.Sort = new XDataField("Sort", DbType.Int16);
                this.Description = new XDataField("Description", DbType.String);
                this.AuthorizeMode = new XDataField("AuthorizeMode", DbType.Byte);
                this.ExtendFields = new XDataField("ExtendFields", DbType.String);
                this.AllowView = new XDataField("AllowView", DbType.Boolean);
                this.ViewPriv = new XDataField("ViewPriv", DbType.String);
                this.NumberRuleId = new XDataField("NumberRuleId", DbType.Int16);
                this.AttachIdList = new XDataField("AttachIdList", DbType.String);
                this.AttachNameList = new XDataField("AttachNameList", DbType.String);
                this.DepartmentId = new XDataField("DepartmentId", DbType.String);
                this.IsVisible = new XDataField("IsVisible", DbType.Int16);
                //

                this.Id.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.CatagoryId, this.Type, this.FormId, this.Name, this.AllowAttach, this.Sort, this.Description, this.AuthorizeMode, this.ExtendFields, this.AllowView, this.ViewPriv, this.NumberRuleId, this.AttachIdList, this.AttachNameList, this.DepartmentId, this.IsVisible });
            }
        }
    }
}
