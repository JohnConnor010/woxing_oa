
namespace WX.Flow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using System.Web;
    using ULCode;
    using ULCode.QDA;
    using System.Text;
    public partial class Run
    {
        //以下为实体开发部分
        //
        public static string GetFileCode(int fID)
        {
            DataTable dt = XSql.GetDataTable("select count(*) from FL_Run where FlowId=" + fID + " and DateDiff(mm,BeginTime,getdate())=0");
            int code = Convert.ToInt32(dt.Rows[0][0]) + 1;
            return String.Format("{0}{1}", DateTime.Now.ToString("yyyyMM"), code > 9 ? code.ToString() : "0" + code);
        }
        public partial class MODEL
        {
            public Flow.MODEL MyFlow = null;
            public void LoadMyFlow(bool reload)
            {
                if (reload && this.MyFlow != null)
                {
                    this.MyFlow = null;
                }
                if (this.MyFlow == null)
                {
                    this.MyFlow = Flow.GetCache(this.FlowId.ToInt32());//Flow.NewDataModel(this.FlowId.ToInt32());
                }
            }
            public Form.MODEL MyForm = null;
            public void LoadMyForm(bool reload)
            {
                if (reload && this.MyForm != null)
                {
                    this.MyForm = null;
                }
                if (this.MyForm == null)
                {
                    this.LoadMyFlow(false);
                    this.MyFlow.LoadForm(false);
                    MyForm = this.MyFlow.Form;
                }
            }
            private Process.MODEL GetSafeProcess(int step)
            {
                Process.MODEL prcs = Process.Caches.Find(delegate(Process.MODEL dele) { return dele.FlowId.ToInt32() == this.FlowId.ToInt32() && dele.StepNo.ToInt32() == step; });
                if (prcs == null) throw new ApplicationException(String.Format("找不到步骤！"));
                return prcs;
            }
            public int Del()
            {
                string sSql = "exec [dbo].[Delete_Run] " + this.Id.ToString();
                int rows = XSql.Execute(sSql);
                return rows;
            }
            /// <summary>
            /// 进入条件
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public bool GetInAccess(int step)
            {
                return this.GetSafeProcess(step).GetInAccess(this);
            }

            /// <summary>
            /// 进入提示
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public string GetInMsg(int step)
            {
                return this.GetSafeProcess(step).GetInMsg(this);
            }

            /// <summary>
            /// 进入插件
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public int ExecIn(int step)
            {
                return this.GetSafeProcess(step).ExecIn(this);
            }

            /// <summary>
            /// 自动获取主办人名称
            /// 返回字符串格式：UserId,RealName
            /// </summary>
            /// <returns></returns>
            public string GetAutoOpHost(int step)
            {
                return this.GetSafeProcess(step).GetAutoOpHost(this);
                //return "455868D6-3BD9-486B-BF37-75387CD5FC9E,孙战平";
            }

            /// <summary>
            /// 自动获取经办人列表
            /// 返回字符串格式：UserId,RealName|UserId,RealName|..
            /// </summary>
            /// <returns></returns>
            public string GetAutoOpList(int step)
            {
                return this.GetSafeProcess(step).GetAutoOpList(this);
                //return "455868D6-3BD9-486B-BF37-75387CD5FC9E,孙战平"
                //    + "|2B894856-9BAC-4537-82E2-4A51C5A5ADE2,孙小兵";
            }
            /// <summary>
            /// 所有经办人
            /// 返回字符串格式：UserId,UserId,UserId,
            /// </summary>
            /// <returns></returns>
            public string GetAllOpList(int step)
            {
                return this.GetSafeProcess(step).GetAllOpList(this);
                //return "455868D6-3BD9-486B-BF37-75387CD5FC9E,2B894856-9BAC-4537-82E2-4A51C5A5ADE2,3492EB60-8186-4DCF-801D-A55C53337DC8,9F2B74B2-C586-42D1-99EB-031A2C72D72E";
            }
            /// <summary>
            /// 获取下一站点列表
            /// 返回字符串格式: StepNum,PrcsName,IsCanEnter|StepNum,PrcsName,IsCanEnter|..
            /// </summary>
            /// <returns></returns>
            public string GetNextPots(int step)
            {
                return this.GetSafeProcess(step).GetNextPots(this);
                //return "3,行政审批,True|4,经理审批,False|5,主管审批,True";
            }
            /// <summary>
            /// 是否为最后一步骤
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public bool IsLastPot(int step)
            {
                string sSql = String.Format("select Max(StepNo) from FL_Process where FlowId={0}", this.FlowId);
                return step == Convert.ToInt32(ULCode.QDA.XSql.GetData(sSql).ToInt32());
            }

            /// <summary>
            /// 退出条件
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public bool GetOutAccess(int step)
            {
                return this.GetSafeProcess(step).GetOutAccess(this);
            }

            /// <summary>
            /// 退出提示
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public string GetOutMsg(int step)
            {
                return this.GetSafeProcess(step).GetOutMsg(this);
            }

            /// <summary>
            /// 退出插件
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public int ExecOut(int step)
            {
                return this.GetSafeProcess(step).ExecOut(this);
            }
            ///取得当前工作中的表单数据
            public string GetDatas_String()
            {
                this.LoadMyForm(false);
                String sSql = String.Format("select * from sysobjects where xtype='u' and name='FL_RunDatas_{0}'", this.MyForm.Id);
                if (ULCode.QDA.XSql.IsHasRow(sSql))
                {
                    sSql = String.Format("select Datas from FL_RunDatas_{0} where RunId={1}", this.MyForm.Id, this.Id);
                    return ULCode.QDA.XSql.GetData(sSql).ToString();
                }
                else
                {
                    return null;
                }
            }
            public FormFieldCollection GetDatas_FormFieldCollection()
            {
                string datas = this.GetDatas_String();
                if (String.IsNullOrEmpty(datas))
                {
                    this.LoadMyForm(false);
                    return this.MyForm.Items_FormFieldCollection;
                }
                else
                    return new FormFieldCollection(datas, FormFieldDataType.Data);
            }
            /// <summary>
            /// 生成当前工作N步中表单Html
            /// </summary>
            /// <param name="step"></param>
            /// <returns></returns>
            public String GenerateHtmls(int id)
            {
                WX.Flow.Model.Run.MODEL runmodel = WX.Flow.Model.Run.NewDataModel(id);
                FormFieldCollection datas = this.GetDatas_FormFieldCollection();
                this.MyFlow.LoadProcessList(false);
                Process.MODEL process = this.MyFlow.ProcessList.Find(delegate(Process.MODEL prcs_dele) { return prcs_dele.StepNo.ToInt32() == runmodel.StepNo.ToInt32(); });
                if (process == null) return null;
                return this.MyForm.GenerateHtmls(datas, process.Editable_FormFieldCollection, process.Hidden_FormFieldCollection,runmodel.BeginUser.ToString());
            }
            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="step">第几步</param>
            /// <param name="fldDatas">字段信息与数据</param>
            /// <param name="attacheIdList">附件id列表</param>
            /// <param name="attacheNameList">附件Name列表</param>
            /// <param name="signText"></param>
            /// <param name="signData"></param>
            /// <param name="flag">状态</param>
            /// <returns></returns>
            public int Save(int step, FormFieldCollection fldDatas, string attacheIdList, string attacheNameList, string signText, string signData, int flag)
            {
                //1.保存fldDatas到FL_RunDatas_{N}中
                this.LoadMyForm(false);
                String sSql = String.Format("select * from sysobjects where xtype='u' and name='FL_RunDatas_{0}'", this.MyForm.Id);
                if (!ULCode.QDA.XSql.IsHasRow(sSql))
                {
                    sSql = String.Format("Select * into FL_RunDatas_{0} from FL_RunDatas", this.MyForm.Id);
                    ULCode.QDA.XSql.Execute(sSql);
                }
                sSql = String.Format("Select * from FL_RunDatas_{0} where RunId={1}", this.MyForm.Id, this.Id);
                if (ULCode.QDA.XSql.IsHasRow(sSql))
                {
                    sSql = String.Format("Update FL_RunDatas_{0} set Datas='{2}' where RunId={0}", this.MyForm.Id, this.Id, fldDatas.GetSavedDatas(FormFieldDataType.Data));
                }
                else
                {
                    sSql = String.Format("insert into FL_RunDatas_{0}(Name,FormId,RunId,BeginUserId,BeginTime,Datas) Values('{2}','{0}','{1}','{3}','{4}','{5}')"
                        , this.MyForm.Id
                        , this.Id
                        , this.Name
                        , this.BeginUser.ToString()
                        , this.BeginTime
                        , fldDatas.GetSavedDatas(FormFieldDataType.Data));
                }
                int iR0 = ULCode.QDA.XSql.Execute(sSql);
                //2.保存attacheIdList, attacheNameList到当前步骤
                if (!String.IsNullOrEmpty(attacheIdList))
                {
                    if (this.Attach_IdList.isEmpty)
                        this.Attach_IdList.set_String_Append(attacheIdList);
                    else
                        this.Attach_IdList.set_String_Append("," + attacheIdList);
                }
                if (!String.IsNullOrEmpty(attacheNameList))
                {
                    if (this.Attach_NameList.isEmpty)
                        this.Attach_NameList.set_String_Append("," + attacheNameList);
                    else
                        this.Attach_NameList.set_String_Append("," + attacheNameList);
                }
                //this.Attach_IdList.set_String_AppendFormat(",{0}", attacheIdList);
                //this.Attach_NameList.set_String_AppendFormat(",{0}", attacheNameList);

                int iR1 = this.Update();
                //3.保存SignText与SignData
                sSql = String.Format("if exists(select * from FL_RunFeedBack where RunId={0} and StepNo={1} and UserId='{4}' and FeedFlag=0)"
                    + " Update FL_RunFeedBack set Content='{2}',SignData='{3}',FeedFlag={7},EditTime=GetDate(),CheckUserID='" + WX.Public.CurUser.UserID + "' where RunId={0} and StepNo={1} and UserId='{4}' and FeedFlag=0;"
                    + " else "
                    + " Insert into FL_RunFeedBack(RunId,StepNo,UserId,Content,Attach_IdList,Attach_NameList,EditTime,FeedFlag,SignData,ReplyId,CheckUserID)"
                    + "                         values({0},{1},'{4}','{2}','{5}','{6}',GetDate(),{7},'{3}',0,'{8}');"
                    , this.Id, step, signText, signData, this.BeginUser.ToString(), null, null, flag,WX.Public.CurUser.UserID);

                int iR2 = ULCode.QDA.XSql.Execute(sSql);
                WX.Flow.Model.Process.MODEL proc = WX.Flow.Model.Process.GetModel("select * from FL_Process where FlowID="+this.FlowId.ToString()+" and StepNo="+step);
                if (proc!=null&& proc.UpdateTable.ToString()!="")
                {
                    sSql = String.Format("if exists(select * from {0} where UserID='{2}')  update {0} set {1} where UserID='{2}'; else begin Insert into {0} (UserID) values('{2}');update {0} set {1} where UserID='{2}';end", proc.UpdateTable.ToString(), proc.UpdateKeyValue, this.BeginUser.ToString());
                    ULCode.QDA.XSql.Execute(sSql);
                }
                if (iR0 * iR1 * iR2 > 0)
                {
                    //Succeess; 
                }
                else
                {
                    //Faild
                }
                return iR2;
            }
            /// <summary>
            /// 流转
            /// </summary>
            /// <param name="step">第几步</param>
            /// <param name="toStep">到第几步</param>
            /// <param name="opHost">选择主办人</param>
            /// <param name="opList">选择经办人</param>
            /// <returns></returns>
            public int Pass(int step, int toStep, string opHost, string opList, bool sendToNext, bool sendToStarter, bool sendToAlloP, string sendMsg)
            {
                WX.Public.CurUser.LoadUserModel(false);
                //需要测试
                //ULCode.Debug.we(String.Format(@"收到信息<br/>Step:{0}<br/>toStep:{1}<br/>opHost:{2}<br/>opList:{3}<br/>sendToNext:{4}<br/>sendToStarter:{5}<br/>sendToAlloP:{6}<br/>sendMsg:{7}"
                //                      , step, toStep, opHost, opList, sendToNext, sendToStarter, sendToAlloP, sendMsg));
                //退出条件(From FL_Process)
                bool flag_OutAccess = this.GetOutAccess(step);
                if (!flag_OutAccess)
                {
                    return -1;
                }
                //退出插件(Fron FL_Process)
                int flag_ExecOut = this.ExecOut(step);
                if (flag_ExecOut == 0)
                {
                    return -2;
                }
                //步骤状态(In FL_RunProcess)
                string sSql = null;
                if (toStep == -1)
                {   //结束工作环节 
                    sSql = String.Format("Update FL_RunProcess set DeliverTime=GetDate(),Deal_Flag=4 where runid={0} and stepno={1};"
                        , this.Id, step);
                    ULCode.QDA.XSql.Execute(sSql);
                    //结束工作状态
                    sSql = String.Format("Update FL_Run set EndTime=GetDate() where Id={0}", this.Id);
                    ULCode.QDA.XSql.Execute(sSql);
                    //添加结束日志
                    string msg = String.Format("[{0}]结束流程", WX.Public.CurUser.UserModel.RealName);
                    this.AddRunLog(step, msg);
                }
                else
                {   //改变本环节状态
                    sSql = String.Format("Update FL_RunProcess set DeliverTime=GetDate(),Deal_Flag={2} where runid={0} and stepno={1};"
                        , this.Id, step, Convert.ToInt32(DealFlag.Operated));
                    ULCode.QDA.XSql.Execute(sSql);
                    //添加下一步环节列表
                    string[] arr_oplist = opList.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String op in arr_oplist)
                    {
                        string msg = null;
                        //获取委托人，并记录委托日志
                        sSql = String.Format("select ToUserId from fl_FlowAuthorization where FromUserId='{0}' and Status=1 and (FlowId=0 or FlowId={1})"
                                      , op, this.FlowId);
                        String op_autorization = ULCode.QDA.XSql.GetData(sSql).ToString();
                        string op_real;
                        if (String.IsNullOrEmpty(op_autorization) || op == op_autorization)
                        {
                            op_real = op;
                        }
                        else
                        {
                            op_real = op_autorization;
                            msg = String.Format("根据委托规则[{0}]把工作委托给[{1}]", WX.Public.CurUser.UserModel.RealName, WX.WXUser.GetRealNameByUserID(op_real));
                            this.AddRunLog(step, msg);
                        }
                        //流转环节记录
                        sSql = String.Format("Insert into FL_RunProcess(RunId,StepNo,ParentNo,UserId,Deal_Flag,Op_Flag,Comment) "
                            + "Values({0},{1},{2},'{3}',{4},{5},'{6}')"
                            , this.Id
                            , toStep
                            , step
                            , op_real
                            , Convert.ToInt32(WX.Flow.DealFlag.NotReceived)
                            , op == opHost ? 1 : 0
                            , String.Format("{0} 第{1}步：{2}{3} ", this.MyFlow.Name, toStep, op_real, String.IsNullOrEmpty(op_autorization) ? "" : "(受委托)"));
                        ULCode.QDA.XSql.Execute(sSql);
                        //流程日志(In FL_RunLogs)
                        msg = String.Format("[{0}]转交给第{1}步办理人[{2}]", WX.Public.CurUser.UserModel.RealName, toStep, WX.WXUser.GetRealNameByUserID(op_real));
                        this.AddRunLog(step, msg);
                    }
                }
                return 1;
            }
            private void AddRunLog(int step, string msg)
            {
                string sSql = String.Format("Insert into FL_RunLogs(RunId,Name,FlowId,StepNo,UserId,[Time],IP,Type,Content)"
                             + " Values({0},'{1}',{2},{3},'{4}',GetDate(),'{5}',0,'{6}')"
                             , this.Id
                             , this.Name
                             , this.FlowId
                             , step
                             , WX.Public.CurUser.UserID
                             , HttpContext.Current.Request.UserHostAddress
                             , msg);
                ULCode.QDA.XSql.Execute(sSql);
            }
        }
    }
    public partial class Run : XDataEntity
    {
        public Run(string tableName)
            : base(tableName)
        {
        }
        public Run(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Run(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Run _entity;
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
        public static Run NewEntity()
        {
            return new Run("FL_Run", "Id");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Run Entity
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
        public static string[] GetSignList(int runid,string MyUserID)
        {
            string[] arrsign=",,,".Split(',');
            System.Data.DataTable  dt = ULCode.QDA.XSql.GetDataTable(String.Format("select rfb.*,p.Name runname,emp.RealName username from FL_RunFeedBack rfb "
                   + " left join FL_Run run on rfb.RunId=run.Id"
                   + " left join FL_Process p on run.FlowId=p.FlowId and rfb.StepNo=p.StepNo "
                   + " left join TU_Users emp on rfb.CheckUserID=emp.UserId "
                   + " where RunId ={0} and FeedFlag=1", runid));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["FeedFlag"]) == 0 && Convert.ToString(dt.Rows[i]["UserId"]) == MyUserID)
                {
                    arrsign[0]=dt.Rows[i]["Content"].ToString();
                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["SignData"])))
                    {
                        arrsign[1]=dt.Rows[i]["SignData"].ToString();
                        arrsign[2] = "<input type=\"button\" onclick=\"PopupIFrame('/App_Ctrl/ShowSignData.aspx?Id=" + Convert.ToString(dt.Rows[i]["Id"]) + "','查看手写签章','','',250,250);\" value=\"查看手写签章\" />";
                    }
                }
                else
                {
                    arrsign[3] += " <tr>\n";
                    arrsign[3] += " <td>\n";
                    arrsign[3] += "     <fieldset>\n";
                    arrsign[3] += "    <!--<b> -->\n";
                    arrsign[3] += "      <legend>\n";
                    arrsign[3] += "      	第" + dt.Rows[i]["StepNo"] + "步 " + dt.Rows[i]["runname"] + "      <u style='cursor:hand'>" + dt.Rows[i]["username"] + "</u>\n";
                    arrsign[3] += "      <!--</b> -->\n";
                    arrsign[3] += "      </legend>\n";
                    arrsign[3] += "      <div class='content'>\n";
                    arrsign[3] += "         <div style='width:85%;float:left;'>";
                    arrsign[3] += "         <i>" + dt.Rows[i]["EditTime"] + "</i><br/>";
                    arrsign[3] += "         " + dt.Rows[i]["Content"];
                    arrsign[3] += "         </div>\n";
                    arrsign[3] += "         <div style='width:14%;float:left; text-align:left;'>";
                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["SignData"])))
                    {
                        arrsign[3] += "           <input type=\"button\" onclick=\"PopupIFrame('/App_Ctrl/ShowSignData.aspx?Id=" + Convert.ToString(dt.Rows[i]["Id"]) + "','查看手写签章','','',250,250);\" value=\"查看手写签章\" />";
                    }
                    arrsign[3] += "         </div>\n";
                    arrsign[3] += "      </div>\n";
                    arrsign[3] += "      </fieldset>\n";
                    arrsign[3] += "       </td>\n";
                    arrsign[3] += "     </tr>";
                }
            }
            return arrsign;
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
            public XDataField ParentId;
            public XDataField Name;
            public XDataField FlowId;
            public XDataField BeginUser;
            public XDataField BeginTime;
            public XDataField EndTime;
            public XDataField Attach_IdList;
            public XDataField Attach_NameList;
            public XDataField FocusUsers;
            public XDataField ViewUsers;
            public XDataField Archive;
            public XDataField Del_Flag;
            public XDataField AIP_Files;
            public XDataField Deal_Flag;
            public XDataField StepNo;
            public XDataField Next_Nodes;


            public MODEL() { }
            public MODEL(Run parentEntity) : base(parentEntity) { }
            public MODEL(Run parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Run parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Run parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.Id = new XDataField("Id", DbType.Int64);
                this.ParentId = new XDataField("ParentId", DbType.Int64);
                this.Name = new XDataField("Name", DbType.String);
                this.FlowId = new XDataField("FlowId", DbType.Int32);
                this.BeginUser = new XDataField("BeginUser", DbType.String);
                this.BeginTime = new XDataField("BeginTime", DbType.DateTime);
                this.EndTime = new XDataField("EndTime", DbType.DateTime);
                this.Attach_IdList = new XDataField("Attach_IdList", DbType.String);
                this.Attach_NameList = new XDataField("Attach_NameList", DbType.String);
                this.FocusUsers = new XDataField("FocusUsers", DbType.String);
                this.ViewUsers = new XDataField("ViewUsers", DbType.String);
                this.Archive = new XDataField("Archive", DbType.String);
                this.Del_Flag = new XDataField("Del_Flag", DbType.Boolean);
                this.AIP_Files = new XDataField("AIP_Files", DbType.String);
                this.Deal_Flag = new XDataField("Deal_Flag", DbType.Int32);
                this.StepNo = new XDataField("StepNo", DbType.Int32);
                this.Next_Nodes = new XDataField("Next_Nodes", DbType.Int32);
                //

                this.Id.isIdentity = true;
                this.Id.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.Id, this.ParentId, this.Name, this.FlowId, this.BeginUser, this.BeginTime, this.EndTime, this.Attach_IdList, this.Attach_NameList, this.FocusUsers, this.ViewUsers, this.Archive, this.Del_Flag, this.AIP_Files, this.Deal_Flag, this.StepNo, this.Next_Nodes });
            }
        }
    }
}
