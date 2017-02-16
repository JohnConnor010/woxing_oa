
namespace WX.Flow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Text;
    using ULCode;
    using ULCode.QDA;

    public partial class Process
    {
        private static List<MODEL> _Caches = null;
        public static List<MODEL> Caches
        {
            get
            {
                if (_Caches == null)
                {
                    _Caches = GetModels("Select * from FL_Process order by FlowId,StepNo");
                }
                return _Caches;
            }
        }
        public static MODEL GetCache(int flowId, int stepId)
        {
            return Caches.Find(delegate(MODEL dele) { return dele.FlowId.ToInt32() == flowId && dele.StepNo.ToInt32() == stepId; });
        }
        public static MODEL GetCache(int id)
        {
            return Caches.Find(delegate(MODEL dele) { return dele.Id.ToInt32() == id; });
        }
        public partial class MODEL:IComparable
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
            public AutoSelOpType Auto_Type_AutoSelOpType
            {
                get { return (AutoSelOpType)this.Auto_Type.ToInt32(); }
                set { this.Auto_Type.set(value); }
            }
            /// <summary>
            /// 步骤编辑字段
            /// </summary>
            public FormFieldCollection Editable_FormFieldCollection
            {
                get { return new FormFieldCollection(this.Fields_Editable.ToString(), FormFieldDataType.Item); }
                set { this.Fields_Editable.set(value.GetSavedDatas(FormFieldDataType.Item)); }
            }
            /// <summary>
            /// 步骤隐藏字段
            /// </summary>
            public FormFieldCollection Hidden_FormFieldCollection
            {
                get { return new FormFieldCollection(this.Fields_Hidden.ToString(), FormFieldDataType.Item); }
                set { this.Fields_Hidden.set(value.GetSavedDatas(FormFieldDataType.Item)); }
            }
            /// <summary>
            /// 步骤所有下一节点，默认为空，需要LoadNextProcessList才能使用
            /// </summary>
            public List<MODEL> NextProcessList = null;
            public List<MODEL> NotbyProcessList = null;
            /// <summary>
            /// 运行后NextProcessList可以使用
            /// </summary>
            /// <param name="reload"></param>
            public void LoadNextProcessList(bool reload)
            {
                if (reload && this.NextProcessList != null)
                {
                    this.NextProcessList.Clear();
                    this.NextProcessList = null;
                }
                if (this.NextProcessList == null && !this.Next_Nodes.isEmpty)
                {
                    Flow.MODEL flowParent=Flow.Caches.Find(delegate(Flow.MODEL dele){return dele.Id.ToInt32()==this.FlowId.ToInt32();});
                    if(flowParent==null)
                    {
                        throw new ApplicationException("Not miss flowParent!");
                    }
                    flowParent.LoadProcessList(false);
                    this.NextProcessList = flowParent.ProcessList.FindAll(delegate(Process.MODEL dele) { return this.Next_Nodes.f(",{0},").Contains(dele.StepNo.f(",{0},")); });
                    this.NextProcessList.Sort();
                    //string sSql = String.Format("Select * from FL_Process where FlowId={0} and StepNo in ({1}) order by StepNo", this.FlowId, this.Next_Nodes);
                    //NextProcessList = GetModels(sSql);
                }
            }
            /// <summary>
            /// 运行后NextProcessList可以使用
            /// </summary>
            /// <param name="reload"></param>
            public void LoadNotbyProcessList(bool reload)
            {
                if (reload && this.NotbyProcessList != null)
                {
                    this.NotbyProcessList.Clear();
                    this.NotbyProcessList = null;
                }
                if (this.NotbyProcessList == null && !this.Notby.isEmpty)
                {
                    Flow.MODEL flowParent = Flow.Caches.Find(delegate(Flow.MODEL dele) { return dele.Id.ToInt32() == this.FlowId.ToInt32(); });
                    if (flowParent == null)
                    {
                        throw new ApplicationException("Not miss flowParent!");
                    }
                    flowParent.LoadProcessList(false);
                    this.NotbyProcessList = flowParent.ProcessList.FindAll(delegate(Process.MODEL dele) { return this.Notby.f(",{0},").Contains(dele.StepNo.f(",{0},")); });
                    this.NotbyProcessList.Sort();
                    //string sSql = String.Format("Select * from FL_Process where FlowId={0} and StepNo in ({1}) order by StepNo", this.FlowId, this.Next_Nodes);
                    //NextProcessList = GetModels(sSql);
                }
            }
            public bool IsLastStep()
            {
                Flow.MODEL flowParent = Flow.Caches.Find(delegate(Flow.MODEL dele) { return dele.Id.ToInt32() == this.FlowId.ToInt32(); });
                return this.StepNo.ToInt32() == flowParent.GetLastStepNo();
            }
            /// <summary>
            /// 进入条件
            /// </summary>
            /// <param name="curRun">当前工作</param>
            /// <returns></returns>
            public bool GetInAccess(Run.MODEL curRun)
            {
                if (this.Condition_In.isEmpty)
                    return true;
                else
                {
                    string sSql = this.GetConditionSql(this.Condition_In.ToString(), curRun);
                    return ULCode.QDA.XSql.IsHasRow("select 1 where " + sSql);
                }
            }
            /// <summary>
            /// 进入提示
            /// </summary>
            /// <param name="curRun">当前工作</param>
            /// <returns></returns>
            public string GetInMsg(Run.MODEL curRun)
            {
                if (this.Condition_In.isEmpty)
                {
                    return null;
                }
                else
                {
                    string[] arr = this.Condition_In.ToString().Split('|');
                    return arr[2];
                }
            }
            /// <summary>
            /// 进入插件执行
            /// </summary>
            /// <param name="curRun"></param>
            /// <returns></returns>
            public int ExecIn(Run.MODEL curRun)
            {
                return this.ExecutePlugs(this.Plug_In.ToString());
            }
            /// <summary>
            /// 退出条件
            /// </summary>
            /// <param name="curRun">当前工作</param>
            /// <returns></returns>
            public bool GetOutAccess(Run.MODEL curRun)
            {
                if (this.Condition_Out.isEmpty)
                    return true;
                else
                {
                    string sSql = this.GetConditionSql(this.Condition_Out.ToString(), curRun);
                    return ULCode.QDA.XSql.IsHasRow("select 1 where " + sSql);
                }
            }
            /// <summary>
            /// 退出提示
            /// </summary>
            /// <param name="curRun">当前工作</param>
            /// <returns></returns>
            public string GetOutMsg(Run.MODEL curRun)
            {
                if (this.Condition_Out.isEmpty)
                {
                    return null;
                }
                else
                {
                    string[] arr = this.Condition_Out.ToString().Split('|');
                    return arr[2];
                }
            }
            /// <summary>
            /// 退出插件执行
            /// </summary>
            /// <param name="curRun"></param>
            /// <returns></returns>
            public int ExecOut(Run.MODEL curRun)
            {
                return this.ExecutePlugs(this.Plug_Out.ToString());
            }
            /// <summary>
            /// 自动获取主办人名称
            /// 返回字符串格式：UserId,RealName
            /// </summary>
            /// <returns></returns>
            public string GetAutoOpHost(Run.MODEL curRun)
            {
                string sSql = "";
                switch (Auto_Type_AutoSelOpType)
                {
                    case AutoSelOpType.None:
                        return null;
                    case AutoSelOpType.AutoSelBeginner:
                        sSql = String.Format("Select UserId+','+RealName from TU_Employees where UserId='{0}'", curRun.BeginUser);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelMyHost:      //本部门主管
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.Host=D.UserID"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelMyAssistant: //本部门助理
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.Assistants like '%'+cast(D.UserID as char(36))+'%'"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelUpHost:      //上级部门领导
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.UpHost=D.UserID"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelUpLeader:    //上级部门分管领导
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.UpSubHosts like '%'+cast(D.UserID as char(36))+'%'"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelTopHost:     //最高级部门领导
                        sSql = "select * from TU_Employees A "
                            + " inner join TE_Departments B on Cast(B.host as char(36))+','+B.Assistants like '%'+Cast(A.UserId as char(36))+'%'"
                            + " where B.parentId=0";
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelOpsInMyDept:  //本部门符合条件的所有人员
                        string con_deptList = this.Priv_DeptList.isEmpty ? String.Empty : " and Cast(B.ID as varchar) in (" + this.Priv_DeptList.ToString() + ")";
                        string con_userList = this.Priv_UserList.isEmpty ? String.Empty : " and '" + (this.Priv_UserList.ToString()) + "' like '%'+Cast(A.UserId as char(36))+'%'";
                        string con_dutyList = this.Priv_DutyList.isEmpty ? String.Empty : " and Cast(DutyId as varchar) in (" + (this.Priv_DutyList.ToString()) + ")";
                        sSql = string.Format("select top 1 UserId,RealName from TU_Employees A "
                            + " inner join TE_Departments B on A.DepartmentID=B.ID "
                            + " inner join TE_Duties C on A.dutyid=C.id"
                            + " where 1=1" + con_deptList + con_dutyList + con_userList
                            + "  and B.Id in （select B.DepartmentID from FL_RunProcess A"
                            + "     inner join TU_Employees B On A.UserID=B.UserID "
                            + "     where A.RunId={0} and A.StepNo={1}）", curRun.Id, this.StepNo);
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelOpsInTopDept: //一级部门符合条件的所有人员
                        string con_deptList1 = this.Priv_DeptList.isEmpty ? String.Empty : " and Cast(B.ID as varchar) in (" + this.Priv_DeptList.ToString() + ")";
                        string con_userList1 = this.Priv_UserList.isEmpty ? String.Empty : " and '" + (this.Priv_UserList.ToString()) + "' like '%'+Cast(A.UserId as char(36))+'%'";
                        string con_dutyList1 = this.Priv_DutyList.isEmpty ? String.Empty : " and Cast(DutyId as varchar) in (" + (this.Priv_DutyList.ToString()) + ")";
                        sSql = string.Format("select top 1 UserId,RealName from TU_Employees A "
                            + " inner join TE_Departments B on A.DepartmentID=B.ID "
                            + " inner join TE_Duties C on A.dutyid=C.id"
                            + " where 1=1" + con_deptList1 + con_dutyList1 + con_userList1
                            + "  and B.ParentId=0", curRun.Id, this.StepNo);
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelStepOp:
                        return null;
                    case AutoSelOpType.SelfByField:
                        return null;
                    case AutoSelOpType.SelfDefaultOp:
                        return null;
                    default:
                        return "";
                }
            }
            /// <summary>
            /// 自动获取经办人列表
            /// 返回字符串格式：UserId,RealName|UserId,RealName|..
            /// </summary>
            /// <returns></returns>
            public string GetAutoOpList(Run.MODEL curRun)
            {
                string sSql = "";
                switch (Auto_Type_AutoSelOpType)
                {
                    case AutoSelOpType.None:
                        return null;
                    case AutoSelOpType.AutoSelBeginner:
                        sSql = String.Format("Select Cast(UserId as char(36))+','+RealName from TU_Employees where UserId='{0}'", curRun.BeginUser);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelMyHost:      //本部门主管
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.Host=D.UserID"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelMyAssistant: //本部门助理
                        sSql = String.Format("select Cast(D.UserID as char(36)),D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.Assistants like '%'+cast(D.UserID as char(36))+'%'"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelUpHost:      //上级部门领导
                        sSql = String.Format("select top 1 Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.UpHost=D.UserID"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetData(sSql).ToString();
                    case AutoSelOpType.AutoSelUpLeader:    //上级部门分管领导
                        sSql = String.Format("select Cast(D.UserID as char(36))+','+D.RealName from FL_RunProcess A"
                            + " inner join TU_Employees B On A.UserID=B.UserID"
                            + " inner join TE_Departments C on B.DepartmentID=C.ID"
                            + " inner join TU_Employees D on C.UpSubHosts like '%'+cast(D.UserID as char(36))+'%'"
                            + " where A.RunId={0} and A.StepNo={1}", curRun.Id, this.Auto_BaseUnit);
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelTopHost:     //最高级部门领导
                        sSql = "select Cast(A.UserID as char(36)),A.RealName from TU_Employees A "
                            + " inner join TE_Departments B on Cast(B.host as char(36))+','+B.Assistants like '%'+Cast(A.UserId as char(36))+'%'"
                            + " where B.parentId=0";
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelOpsInMyDept:  //本部门符合条件的所有人员
                        string con_deptList = this.Priv_DeptList.isEmpty ? String.Empty : " and Cast(B.ID as varchar) in (" + this.Priv_DeptList.ToString() + ")";
                        string con_userList = this.Priv_UserList.isEmpty ? String.Empty : " and '" + (this.Priv_UserList.ToString()) + "' like '%'+Cast(A.UserId as char(36))+'%'";
                        string con_dutyList = this.Priv_DutyList.isEmpty ? String.Empty : " and Cast(DutyId as varchar) in (" + (this.Priv_DutyList.ToString()) + ")";
                        sSql = string.Format("select A.UserId,A.RealName from TU_Employees A "
                            + " inner join TE_Departments B on A.DepartmentID=B.ID "
                            + " inner join TE_Duties C on A.dutyid=C.id"
                            + " where 1=1" + con_deptList + con_dutyList + con_userList
                            + "  and B.Id in （select B.DepartmentID from FL_RunProcess A"
                            + "     inner join TU_Employees B On A.UserID=B.UserID "
                            + "     where A.RunId={0} and A.StepNo={1}）", curRun.Id, this.StepNo);
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelOpsInTopDept: //一级部门符合条件的所有人员
                        string con_deptList1 = this.Priv_DeptList.isEmpty ? String.Empty : " and Cast(B.ID as varchar) in (" + this.Priv_DeptList.ToString() + ")";
                        string con_userList1 = this.Priv_UserList.isEmpty ? String.Empty : " and '" + (this.Priv_UserList.ToString()) + "' like '%'+Cast(A.UserId as char(36))+'%'";
                        string con_dutyList1 = this.Priv_DutyList.isEmpty ? String.Empty : " and Cast(DutyId as varchar) in (" + (this.Priv_DutyList.ToString()) + ")";
                        sSql = "select UserId,RealName from TU_Employees A "
                            + " inner join TE_Departments B on A.DepartmentID=B.ID "
                            + " inner join TE_Duties C on A.dutyid=C.id"
                            + " where 1=1" + con_deptList1 + con_dutyList1 + con_userList1
                            + "  and B.ParentId=0";
                        return ULCode.QDA.XSql.GetXDataTable(sSql).ToCellValueList(",", "|");
                    case AutoSelOpType.AutoSelStepOp:
                        return null;
                    case AutoSelOpType.SelfByField:
                        return null;
                    case AutoSelOpType.SelfDefaultOp:
                        return null;
                    default:
                        return "";
                }
            }
            /// <summary>
            /// 自动获取经办人列表
            /// 返回字符串格式：UserId,RealName|UserId,RealName|..
            /// </summary>
            /// <returns></returns>
            public string GetAllOpList(Run.MODEL curRun)
            {
                string con_deptList = this.Priv_DeptList.isEmpty ? String.Empty : " and Cast(B.ID as varchar) in (" + this.Priv_DeptList.ToString() + ")";
                string con_userList = this.Priv_UserList.isEmpty ? String.Empty : " and '" + (this.Priv_UserList.ToString()) + "' like '%'+Cast(A.UserId as char(36))+'%'";
                string con_dutyList = this.Priv_DutyList.isEmpty ? String.Empty : " and Cast(DutyId as varchar) in (" + (this.Priv_DutyList.ToString()) + ")";
                string sSql = string.Format("select A.UserId from TU_Employees A "
                    + " inner join TE_Departments B on A.DepartmentID=B.ID "
                    + " inner join TE_Duties C on A.dutyid=C.id"
                    + " where 1=1" + con_deptList + con_dutyList + con_userList
                    , curRun.Id, this.StepNo);
                return ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList();
            }
            /// <summary>
            /// 获取下一站点列表
            /// 返回字符串格式: StepNum,PrcsName,IsCanEnter|StepNum,PrcsName,IsCanEnter|..
            /// </summary>
            /// <returns></returns>
            public string GetNextPots(Run.MODEL curRun)
            {
                StringBuilder sb = new StringBuilder();
                if (!this.IsLastStep())
                {
                    this.LoadNextProcessList(false);
                    foreach (Process.MODEL prcs in this.NextProcessList)
                    {
                        bool flag = prcs.GetInAccess(curRun);
                        if (sb.Length > 0) sb.Append("|");
                        sb.AppendFormat("{0},{1},{2}", prcs.StepNo, prcs.Name, flag);
                    }
                }
                else
                {
                    sb.AppendFormat("{0},{1},{2}", -1, "结束流程", "True");
                }
                return sb.ToString();
            }
            #region 条件
            /// <summary>
            /// 获取条件形成的Sql语句
            /// </summary>
            /// <param name="inStr">条件字段字符串</param>
            /// <param name="curRun">当前工作</param>
            /// <returns></returns>
            private String GetConditionSql(string inStr, Run.MODEL curRun)
            {
                if (String.IsNullOrEmpty(inStr)) return "1=1";
                //Get FieldValues of FieldItemCollection
                FormFieldCollection datas = null;
                if (curRun != null) datas = curRun.GetDatas_FormFieldCollection();
                string prcsin = inStr;
                //获取单个表达式集合，并逐个解析它
                string[] prcslist = prcsin.Split(new String[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (prcslist.Length != 3) return "1=1";
                string[] wherelist = prcslist[0].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < wherelist.Length; i++)
                {
                    wherelist[i] = GetConditionSql_ParserSingleExpression(wherelist[i], datas);
                }
                //获取整体表达式字符串
                string sqlstr = prcslist[1];
                //生成整体表达式
                for (int i = 0; i < wherelist.Length; i++)
                {
                    sqlstr = sqlstr.Replace("[" + (i + 1) + "]", wherelist[i]);
                }

                return sqlstr;
            }
            /// <summary>
            /// 解析单个表达式
            /// </summary>
            /// <param name="wherelist"></param>
            /// <param name="datas"></param>
            /// <returns></returns>
            private string GetConditionSql_ParserSingleExpression(string where, FormFieldCollection datas)
            {
                string[] tjlist = where.Split(new String[] { "'", "`" }, StringSplitOptions.None);
                if (tjlist[1].Substring(0, 1) == "@")
                {
                    tjlist[1] = tjlist[1].Substring(1);
                    tjlist[1] = this.GetConditionSql_GetCurFormVar(tjlist[1], datas);
                }
                else if (tjlist[1].Substring(0, 1) == "[")
                {
                    tjlist[1] = tjlist[1].Substring(1, tjlist[1].Length - 2);
                    tjlist[1] = this.GetConditionSql_GetSysVariable(tjlist[1]);
                }
                if (tjlist[3].Substring(0, 1) == "@")
                {
                    tjlist[3] = tjlist[3].Substring(1);
                    tjlist[1] = this.GetConditionSql_GetCurFormVar(tjlist[1], datas);
                }
                else if (tjlist[3].Substring(0, 1) == "[")
                {
                    tjlist[3] = tjlist[3].Substring(1, tjlist[3].Length - 2);
                    tjlist[3] = this.GetConditionSql_GetSysVariable(tjlist[3]);
                }
                return "'" + tjlist[1] + "'" + tjlist[2] + "'" + tjlist[3] + "'";
            }
            /// <summary>
            /// 获取单个服务器变量
            /// </summary>
            /// <param name="Vname"></param>
            /// <returns></returns>
            private string GetConditionSql_GetSysVariable(string name)
            {
                string returnstr = "";
                WX.WXUser cu = WX.Public.CurUser;
                //WX.Flow.Model.Process.MODEL pmodel = WX.Flow.Model.Process.GetModel("select top 1 * from TE_VarDefine where Title='" + Vname + "'");
                //string name = "";
                //if (pmodel != null && pmodel.Name.value != null)
                //{
                //    name = pmodel.Name.value.ToString();
                //}
                switch (name)
                {
                    case "PRCS_Z_UserName":
                        cu.LoadUserModel(false);
                        returnstr = cu.UserModel.RealName.value.ToString();
                        break;
                    case "PRCS_Z_UserDuty":
                        cu.LoadDutyUser();
                        returnstr = cu.DutyUser.Name.value.ToString();
                        break;
                    case "PRCS_Z_UserDept":
                        cu.LoadEmployeeUser();
                        cu.LoadMyDepartment();
                        returnstr = cu.MyDepartMent.Name.ToString();
                        break;
                    case "PRCS_Z_UserSupDept":
                        cu.LoadEmployeeUser();
                        cu.LoadMyDepartment();
                        WX.Model.Department.MODEL dept2 = WX.Model.Department.GetCache(cu.MyDepartMent.ParentID.ToInt32());
                        //WX.Model.Department.GetModel("select * from TE_Departments where ID=" + cu.MyDepartMent.ParentID.ToString());
                        returnstr = dept2.Name.value.ToString();
                        break;
                    case "PRCS_ID": returnstr = Convert.ToInt32(HttpContext.Current.Request.QueryString["Step_Id"]).ToString(); break;
                    case "Datetime_Now": returnstr = DateTime.Now.ToString("yyyy-MM-dd"); break;
                    default: break;
                }
                return returnstr;
            }
            /// <summary>
            /// 获取单个表单变量
            /// </summary>
            /// <param name="Vname"></param>
            /// <returns></returns>
            private string GetConditionSql_GetCurFormVar(string Vname, FormFieldCollection datas)
            {
                if (datas == null) return String.Empty;
                FormField ff = datas.FindItemByTitle(Vname);
                if (ff != null)
                {
                    return ff.Value;
                }
                else
                {
                    return String.Empty;
                }
            }
            #endregion
            #region 插件
            private int ExecutePlugs(String plugsStr)
            {
                if (String.IsNullOrEmpty(plugsStr)) return 1;
                string[] pin = plugsStr.Split('|');
                //get type
                string type = pin[0];
                //get plugs
                string plugs = pin[1].Replace("`", "'");
                MatchCollection mc = Regex.Matches(plugs, "\\[(.*?)\\]");
                foreach (Match mm in mc)
                {
                    plugs = plugs.Replace(mm.Value, mm.Result(HttpContext.Current.Request[mm.Value.Replace("[", "").Replace("]", "")]));
                }
                plugs = pin[1].Replace("`", "'");
                //exec it
                if (type == "Proc")
                {
                    return ULCode.QDA.XSql.Execute("exec {0}", plugs);
                }
                else if (type == "Url")
                {
                    WebClient client = new WebClient();
                    string result = client.DownloadString(plugs);
                    return Convert.ToInt32(result);
                }
                else
                    return 1;
            }
            #endregion
            public int CompareTo(object obj)
            {
                return this.StepNo.ToInt32().CompareTo(((Process.MODEL)obj).StepNo.ToInt32());
            }
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
            return new Process("FL_Process", "Id");
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

            public XDataField Id;
            public XDataField FlowId;
            public XDataField StepNo;
            public XDataField Name;
            public XDataField NodeType;
            public XDataField Next_Nodes;
            public XDataField Priv_UserList;
            public XDataField Priv_DutyList;
            public XDataField Priv_DeptList;
            public XDataField Fields_Editable;
            public XDataField Fields_Hidden;
            public XDataField VML_Top;
            public XDataField VML_Left;
            public XDataField Condition_In;
            public XDataField Condition_Out;
            public XDataField Plug_In;
            public XDataField Plug_Out;
            public XDataField Auto_Type;
            public XDataField Auto_UserList;
            public XDataField Auto_UserOP;
            public XDataField Auto_FilterMode;
            public XDataField Auto_OPMode;
            public XDataField Auto_OpChangeMode;
            public XDataField Auto_BaseUnit;
            public XDataField Auto_Item;
            public XDataField Sign_Look;
            public XDataField Sign_Mode;
            public XDataField Pass_OpForce;
            public XDataField Pass_RollBack;
            public XDataField Msg_ViewMode;
            public XDataField Msg_ViewUsers;
            public XDataField Sync_DealMode;
            public XDataField Sync_CombineMode;
            public XDataField UpdateTable;
            public XDataField UpdateKeyValue;
            public XDataField Notby;

            public MODEL() { }
            public MODEL(Process parentEntity) : base(parentEntity) { }
            public MODEL(Process parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Process parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Process parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int32);
                this.FlowId = new XDataField("FlowId", DbType.Int16);
                this.StepNo = new XDataField("StepNo", DbType.Byte);
                this.Name = new XDataField("Name", DbType.String);
                this.NodeType = new XDataField("NodeType", DbType.Byte);
                this.Next_Nodes = new XDataField("Next_Nodes", DbType.String);
                this.Priv_UserList = new XDataField("Priv_UserList", DbType.String);
                this.Priv_DutyList = new XDataField("Priv_DutyList", DbType.String);
                this.Priv_DeptList = new XDataField("Priv_DeptList", DbType.String);
                this.Fields_Editable = new XDataField("Fields_Editable", DbType.String);
                this.Fields_Hidden = new XDataField("Fields_Hidden", DbType.String);
                this.VML_Top = new XDataField("VML_Top", DbType.Int16);
                this.VML_Left = new XDataField("VML_Left", DbType.Int16);
                this.Condition_In = new XDataField("Condition_In", DbType.String);
                this.Condition_Out = new XDataField("Condition_Out", DbType.String);
                this.Plug_In = new XDataField("Plug_In", DbType.String);
                this.Plug_Out = new XDataField("Plug_Out", DbType.String);
                this.Auto_Type = new XDataField("Auto_Type", DbType.Byte);
                this.Auto_UserList = new XDataField("Auto_UserList", DbType.String);
                this.Auto_UserOP = new XDataField("Auto_UserOP", DbType.String);
                this.Auto_FilterMode = new XDataField("Auto_FilterMode", DbType.Byte);
                this.Auto_OPMode = new XDataField("Auto_OPMode", DbType.Byte);
                this.Auto_OpChangeMode = new XDataField("Auto_OpChangeMode", DbType.Byte);
                this.Auto_BaseUnit = new XDataField("Auto_BaseUnit", DbType.Byte);
                this.Auto_Item = new XDataField("Auto_Item", DbType.Int32);
                this.Sign_Look = new XDataField("Sign_Look", DbType.Byte);
                this.Sign_Mode = new XDataField("Sign_Mode", DbType.Byte);
                this.Pass_OpForce = new XDataField("Pass_OpForce", DbType.Byte);
                this.Pass_RollBack = new XDataField("Pass_RollBack", DbType.Byte);
                this.Msg_ViewMode = new XDataField("Msg_ViewMode", DbType.Int16);
                this.Msg_ViewUsers = new XDataField("Msg_ViewUsers", DbType.String);
                this.Sync_DealMode = new XDataField("Sync_DealMode", DbType.Byte);
                this.Sync_CombineMode = new XDataField("Sync_CombineMode", DbType.Byte);
                this.UpdateTable = new XDataField("UpdateTable", DbType.String);
                this.UpdateKeyValue = new XDataField("UpdateKeyValue", DbType.String);
                this.Notby = new XDataField("Notby", DbType.Int16);
                //

                this.Id.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.FlowId, this.StepNo, this.Name, this.NodeType, this.Next_Nodes, this.Priv_UserList, this.Priv_DutyList, this.Priv_DeptList, this.Fields_Editable, this.Fields_Hidden, this.VML_Top, this.VML_Left, this.Condition_In, this.Condition_Out, this.Plug_In, this.Plug_Out, this.Auto_Type, this.Auto_UserList, this.Auto_UserOP, this.Auto_FilterMode, this.Auto_OPMode, this.Auto_OpChangeMode, this.Auto_BaseUnit, this.Auto_Item, this.Sign_Look, this.Sign_Mode, this.Pass_OpForce, this.Pass_RollBack, this.Msg_ViewMode, this.Msg_ViewUsers, this.Sync_DealMode, this.Sync_CombineMode, this.UpdateTable, this.UpdateKeyValue, this.Notby });
            }
        }
    }
}
