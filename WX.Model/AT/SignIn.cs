
namespace WX.AT
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Signin
    {
        //以下为实体开发部分
        //
        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
        //"0出勤", "1请假", "2事假", "3病假", "4婚假", "5产假", "6外出", "7出差", "8旷工", "9迟到", "10早退", "11加班"，12上午旷工，13下午旷工，14下午迟到
        public static string[] statearray = new string[] { "出勤", "请假", "事假", "病假", "婚假", "产假", "外出", "出差", "全天旷工", "上午迟到", "早退", "加班", "上午旷工", "下午旷工", "下午迟到" };
        //"0出勤√", "1请假－", "2事假－", "3病假℃", "4婚假◇", "5产假♀", "6外出→", "7出差☆", "8旷工×", "9迟到△", "10早退¤", "11加班＋", "12上午旷工＋", "13下午旷工＋"
        public static string[] statesign = new string[] { "√", "－", "－", "℃", "◇", "○", "→", "☆", "×", "△", "¤", "＋", "♂", "♀", "¤" };
        public static DateTime OnStart = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 07:30"));//上班签到起始时间
        public static DateTime OnStop = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 09:30"));//上班签到结束时间
        public static DateTime OffStart = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 17:00"));//下班签到起始时间
        public static DateTime OffStop = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59"));//下班签到结束时间
        public static DateTime BelateStart = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 09:01"));//迟到开始时间，迟到开始时间到迟到终止时间状态为“迟到”
        public static DateTime BelateStop = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 09:30"));//迟到终止时间，超过迟到终止时间判定为“旷工”
        public static DateTime OffWork = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 17:00"));//下班时间，下班签到小于下班时间为“早退”
        public static DateTime NoonStart = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 13:00"));//下午上班时间，签到小于下午上班时间为“下午迟到”
        public static DateTime NoonStop = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 13:30"));//下午上班迟到结束时间，签到小于小于下午上班迟到结束时间为“矿工一天”
        public static int KGMinutes = 120;//迟到或早退大于等于120分钟视为旷工半天
    }
    public partial class Signin : XDataEntity
    {
        public Signin(string tableName)
            : base(tableName)
        {
        }
        public Signin(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Signin(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Signin _entity;
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
        public static Signin NewEntity()
        {
            return new Signin("AT_Signin", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Signin Entity
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
            MODEL model = null;
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0)
            {
                WX.AT.Signin.SetState();
                XSql.Execute("exec ADD_AT_Signin");
                dt = XSql.GetDataTable(sSql);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                model = NewDataModel(dr);
            }
            else
            {
                model = NewDataModel();
            }
            return model;
        }
        public static void SetState()
        {
            string sqlstr = "";
            string statestr = "";
            DataTable list = XSql.GetDataTable("select * from AT_Signin where datediff(day,Addtime,(select top 1 Addtime from AT_Signin where datediff(day,Addtime,getdate())>0 order by Addtime desc))=0");
            DataTable liststatus = XSql.GetDataTable("select * from AT_Status");
            if (list != null)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    statestr = "";
                    string kgstate = "";
                    DataRow drr = liststatus.Select("UserID='" + list.Rows[i]["UserID"].ToString() + "'")[0];
                    statestr = drr["State"].ToString().Trim();
                    if (statestr == "8")
                    {
                        if (list.Rows[i]["Belate"].ToString().Trim()!=""&&Convert.ToInt32(list.Rows[i]["Belate"].ToString().Trim()) >= WX.AT.Signin.KGMinutes &&list.Rows[i]["Leaveearly"].ToString().Trim()!=""&& Convert.ToInt32(list.Rows[i]["Leaveearly"].ToString().Trim()) >= WX.AT.Signin.KGMinutes)
                            kgstate = statestr;
                        else if (list.Rows[i]["Belate"].ToString().Trim() != "" && Convert.ToInt32(list.Rows[i]["Belate"].ToString().Trim()) >= WX.AT.Signin.KGMinutes)
                            kgstate = "81";
                        else if (list.Rows[i]["Leaveearly"].ToString().Trim() != "" && Convert.ToInt32(list.Rows[i]["Leaveearly"].ToString().Trim()) >= WX.AT.Signin.KGMinutes)
                            kgstate = "82";
                        else
                            kgstate = statestr;
                    }
                    if (list.Rows[i]["isset"].ToString() == "1" && DateTime.Now > Convert.ToDateTime(list.Rows[i]["stoptime"]))
                    {
                        if (DateTime.Now > Convert.ToDateTime(list.Rows[i]["stoptime"]).AddMinutes(WX.AT.Signin.KGMinutes))
                        {
                            WX.AT.Signin.AddLogs(list.Rows[i]["UserID"].ToString(), 8, "超出截止时间置为旷工");
                            statestr = "8";
                        }
                        else
                        {
                            WX.AT.Signin.AddLogs(list.Rows[i]["UserID"].ToString(), 1, "超出截止时间置为请假");
                            statestr = "1";
                        }
                    }
                    else
                        if ((list.Rows[i]["Ontime"].ToString() == "" || list.Rows[i]["Offtime"].ToString() == "") && (statestr == "0" || statestr == "9" || statestr == ""))
                        {
                            WX.AT.Signin.AddLogs(list.Rows[i]["UserID"].ToString(), 8, "签到不全置为旷工");
                            statestr = "8";
                        }
                        else
                        {
                            statestr = statestr == "" ? "8" : statestr;
                            WX.AT.Signin.AddLogs(list.Rows[i]["UserID"].ToString(), Convert.ToInt32(statestr), WX.AT.Signin.statearray[Convert.ToInt32(statestr)]);
                        }
                    XSql.Execute("update AT_Signin set State=" + statestr + " where ID=" + list.Rows[i]["ID"]);
                    sqlstr = "update AT_Statements set day" + Convert.ToDateTime(list.Rows[i]["Addtime"]).Day + "=" +(kgstate!=""?kgstate: statestr);
                    if (statestr == "9")
                        sqlstr += ",LCD=LCD+1";
                    if (statestr == "10")
                        sqlstr += ",LZT=LZT+1";
                    if (statestr == "1" || statestr == "8")
                        sqlstr += ",Isall=0";
                    if (statestr == "1")
                        sqlstr += ",LSJ=LSJ+1";
                    if (statestr == "3")
                        sqlstr += ",LBJ=LBJ+1";
                    if (statestr == "8" || kgstate != "")
                        sqlstr += ",LKG=LKG+1";

                    if (statestr == "0" || statestr == "6" || statestr == "7" || statestr == "9" || statestr == "10" || statestr == "11")
                        sqlstr += ",Count=Count+1";
                    sqlstr += " where datediff(month,Sdate,getdate())=0 and UserID='" + list.Rows[i]["UserID"] + "'";
                    XSql.Execute(sqlstr);
                }
            }
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
        public static void AddLogs(string UserId, int State, string Content)
        {
            WX.AT.Log.MODEL log = WX.AT.Log.NewDataModel();
            log.UserID.value = UserId;
            log.State.value = State;
            log.Conent.value = Content;
            log.Addtime.value = DateTime.Now;
            log.Insert(true);
        }
        public partial class MODEL : XDataModel
        {

            public XDataField ID;
            public XDataField UserID;
            public XDataField State;
            public XDataField Ontime;
            public XDataField Belate;
            public XDataField Leaveearly;
            public XDataField Offtime;
            public XDataField Addtime;
            public XDataField Demo;
            public XDataField Starttime;
            public XDataField Stoptime;
            public XDataField Type;//1因公，2因私
            public XDataField IsSet;//0无效，1有效

            public MODEL() { }
            public MODEL(Signin parentEntity) : base(parentEntity) { }
            public MODEL(Signin parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Signin parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Signin parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {

                this.ID = new XDataField("ID", DbType.Int32);
                this.UserID = new XDataField("UserID", DbType.String);
                this.State = new XDataField("State", DbType.Int32);
                this.Ontime = new XDataField("Ontime", DbType.DateTime);
                this.Belate = new XDataField("Belate", DbType.Int32);
                this.Leaveearly = new XDataField("Leaveearly", DbType.Int32);
                this.Offtime = new XDataField("Offtime", DbType.DateTime);
                this.Addtime = new XDataField("Addtime", DbType.DateTime);
                this.Starttime = new XDataField("Starttime", DbType.DateTime);
                this.Stoptime = new XDataField("Stoptime", DbType.DateTime);
                this.Type = new XDataField("Type", DbType.Int32);//1因公，2因私
                this.Demo = new XDataField("Demo", DbType.String);
                this.IsSet = new XDataField("IsSet", DbType.Int32);//0无效，1有效
                //

                this.ID.isIdentity = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.UserID, this.State, this.Ontime, this.Belate, this.Leaveearly, this.Offtime, this.Addtime, this.Demo, this.Starttime, this.Stoptime, this.Type, this.IsSet });
            }
        }
    }
}
