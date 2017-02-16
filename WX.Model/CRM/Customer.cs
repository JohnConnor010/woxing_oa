
namespace WX.CRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Customer
    {
        //以下为实体开发部分
        // //客户维护日志--0添加, 1提交修改, 2审核, 3添加联系人, 4修改联系人, 5审核联系人, 6共享, 7移交, 8废弃, 9回收，10删除联系人，11跟踪维护，12申请废弃，13审核废弃，14审核跟踪记录
        public static string[] logstype = new string[] { "添加", "提交修改", "审核", "添加联系人", "修改联系人", "审核联系人", "共享", "移交", "废弃", "回收", "删除联系人", "跟踪维护","申请废弃","审核废弃","审核跟踪记录" };

        public partial class MODEL
        {

            public string CreateCustomerID(string customerId)
            {
                if (!string.IsNullOrEmpty(customerId))
                {
                    try
                    {
                        string strNumber = customerId.Replace(String.Format("KH{0}", DateTime.Now.ToString("yyyyMM")), "");
                        return String.Format("KH{1}{0:D4}", Convert.ToInt32(strNumber) + 1, DateTime.Now.ToString("yyyyMM"));
                    }
                    catch
                    {
                        return String.Format("KH{0}0001", DateTime.Now.ToString("yyyyMM"));
                    }
                }
                else
                {
                    return String.Format("KH{0}0001", DateTime.Now.ToString("yyyyMM"));
                }
            }

        }
    }
    public partial class Customer : XDataEntity
    {
        public Customer(string tableName)
            : base(tableName)
        {
        }
        public Customer(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Customer(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Customer _entity;
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
        public static Customer NewEntity()
        {
            return new Customer("CRM_Customers", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Customer Entity
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
        public static void CheckCompMess(string bossid)
        {
            System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable("SELECT * FROM [dbo].[CRM_MessageFlag] where Type=1 and datediff(day,Edittime,getdate())=0");
            if (dt2.Rows.Count == 0)
            {
                ULCode.QDA.XSql.Execute("update CRM_MessageFlag set Edittime=getdate() where Type=1");
                System.Data.DataTable comps = ULCode.QDA.XSql.GetDataTable("SELECT comp.ID,CustomerName,EmployeeID,tuuser.RealName,tuuser.State,tuuser.DepartmentID,tedept.Name deptName,tedept.Host,teparentdept.Host ParentHost,UpTime,vv.* FROM [dbo].[CRM_Customers] comp left join view_CRM_Track vv on comp.ID=vv.CustomerID left join TU_Users tuuser on comp.EmployeeID=tuuser.UserID left join TE_Departments tedept on tuuser.DepartmentID=tedept.ID left join TE_Departments teparentdept on tedept.ParentID=teparentdept.ID where (vv.CustomerID is not null or (vv.CustomerID is null and datediff(day,UpTime,getdate()-1)>15)) and comp.State>0");
                if (comps.Rows.Count > 0)
                {

                    string messtitle = "<a href=''/Manage/CRM/CRM_LongTime.aspx?mes=1''>系统提醒：有客户超过15日未跟踪，点击查看详情</a>";
                    string messsql = "insert into TM_Messages( [ID],[Title],[RedirectToUrl],[SendToUserId],[FromUserId],[SendTime],[Type],[State],[Role]) values(newid(),'{0}','{1}','{2}','{3}',getdate(),{4},0,{5});";
                    //向公司最高领导发消息
                    string sSql = "if not exists(select * from CRM_MessageLog where UserId='" + bossid + "' and datediff(day,Addtime,getdate())=0)  begin "
                          + string.Format("insert into CRM_MessageLog(Type,UserID) values(2,'{0}');", bossid)
                          + String.Format(messsql, messtitle, "/Manage/CRM/CRM_LongTime.aspx?mes=1", bossid, bossid, "4", "0")
                        + " end"; ULCode.QDA.XSql.Execute(sSql);
                    for (int i = 0; i < comps.Rows.Count; i++)
                    {
                        if (comps.Rows[i]["Host"].ToString() != "")
                        {
                            sSql = "if not exists(select * from CRM_MessageLog where UserId='" + comps.Rows[i]["Host"] + "' and datediff(day,Addtime,getdate())=0)  begin "
                              + string.Format("insert into CRM_MessageLog(Type,UserID) values(2,'{0}');", comps.Rows[i]["Host"])
                              + String.Format(messsql, messtitle , "/Manage/CRM/CRM_LongTime.aspx?mes=1", comps.Rows[i]["Host"], comps.Rows[i]["Host"], "4", "0")
                            + " end";
                            ULCode.QDA.XSql.Execute(sSql);//主管接收
                        }
                        if (comps.Rows[i]["ParentHost"].ToString() != "")
                        {
                            sSql = "if not exists(select * from CRM_MessageLog where UserId='" + comps.Rows[i]["ParentHost"] + "' and datediff(day,Addtime,getdate())=0)  begin "
                              + string.Format("insert into CRM_MessageLog(Type,UserID) values(2,'{0}');", comps.Rows[i]["ParentHost"])
                              + String.Format(messsql, messtitle, "/Manage/CRM/CRM_LongTime.aspx?mes=1", comps.Rows[i]["ParentHost"], comps.Rows[i]["ParentHost"], "4", "0")
                            + " end";
                            ULCode.QDA.XSql.Execute(sSql);//上级领导接收
                        }
                        if (comps.Rows[i]["EmployeeID"].ToString() != "" && (comps.Rows[i]["State"].ToString() == "10" || comps.Rows[i]["State"].ToString() == "20"))
                        {
                            sSql = "if not exists(select * from CRM_MessageLog where UserId='" + comps.Rows[i]["EmployeeID"] + "' and datediff(day,Addtime,getdate())=0)  begin "
                             + string.Format("insert into CRM_MessageLog(Type,UserID) values(3,'{0}');", comps.Rows[i]["EmployeeID"])
                          + String.Format(messsql, messtitle , "/Manage/CRM/CRM_LongTime.aspx?mes=1", comps.Rows[i]["EmployeeID"], comps.Rows[i]["EmployeeID"], "4", "0")
                             + " end";
                            ULCode.QDA.XSql.Execute(sSql);//维护人接收
                        }
                        //向总经理及部门负责人发消息
                    }
                }
            }

        }
        public static void AddLog(int CustomerID, string title, string userid, int logtype, string logparaments)
        {
            ULCode.QDA.XSql.Execute("insert into CRM_Logs (Title,UserID,LogType,LogIP,LogParaments,CustomerID)values('" + title + "','" + userid + "'," + logtype + ",'" + System.Web.HttpContext.Current.Request.UserHostAddress + "','" + logparaments + "'," + CustomerID + ")");
        }
        public static void AddLogSMS(string mebiles, string content, int count, string userid)
        {
            ULCode.QDA.XSql.Execute("insert into SMS_Send (UserID,Mobile,Content,Count)values('" + userid + "','" + mebiles + "','" + content + "'," + count + ")");
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
            //1.客户基本信息
            public XDataField CustomerID;
            public XDataField CustomerName;
            public XDataField CustomerZJM;
            public XDataField NatureID;
            public XDataField IndustryID;
            public XDataField CategoryID;
            public XDataField SourceID;
            public XDataField Province;
            public XDataField City;
            public XDataField Area;
            public XDataField Address;
            public XDataField WebSite;
            public XDataField ImagePath;
            //2.客户营业信息
            public XDataField EstablishmentDate;
            public XDataField RealName;
            public XDataField BankName;
            public XDataField BankAccount;
            public XDataField BusinessCircles;
            public XDataField Products;
            //3.客户合作信息
            public XDataField BusinessLevel;
            public XDataField CoopFlag;
            public XDataField StageID;
            public XDataField BuyHabbit;
            public XDataField LastConsumptionMoney;
            public XDataField LastMaintainMoney;
            public XDataField PreMoney;
            public XDataField LastBegin;
            public XDataField LastEnd;
            public XDataField UrgerDate;
            public XDataField ConsumptionMoney;
            public XDataField MaintainMoney;
            public XDataField Mobile_D;
            public XDataField AttachFile_D;
            //4.其它客户信息
            public XDataField Remarks;
            public XDataField SpecialDesc;
            public XDataField Integral_D;
            //5.OA维护信息
            public XDataField CreateUserId;
            public XDataField CreateDate;
            public XDataField EmployeeID;
            public XDataField DeptId;
            public XDataField State;
            public XDataField CheckUserId;
            public XDataField IsShare;
            public XDataField UpTime;

            public MODEL() { }
            public MODEL(Customer parentEntity) : base(parentEntity) { }
            public MODEL(Customer parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Customer parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Customer parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
                //1.客户基本信息
                this.ID = new XDataField("ID", DbType.Int32);
                this.CustomerID = new XDataField("CustomerID", DbType.String);
                this.CustomerName = new XDataField("CustomerName", DbType.String);
                this.CustomerZJM = new XDataField("CustomerZJM", DbType.String);
                this.NatureID = new XDataField("NatureID", DbType.Int32);
                this.IndustryID = new XDataField("IndustryID", DbType.Int32);
                this.CategoryID = new XDataField("CategoryID", DbType.Int32);
                this.SourceID = new XDataField("SourceID", DbType.Int32);
                this.Province = new XDataField("Province", DbType.String);
                this.City = new XDataField("City", DbType.String);
                this.Area = new XDataField("Area", DbType.String);
                this.Address = new XDataField("Address", DbType.String);
                this.WebSite = new XDataField("WebSite", DbType.String);
                this.ImagePath = new XDataField("ImagePath", DbType.String);
                //2.客户营业信息
                this.EstablishmentDate = new XDataField("EstablishmentDate", DbType.String);
                this.RealName = new XDataField("RealName", DbType.String);
                this.BankName = new XDataField("BankName", DbType.String);
                this.BankAccount = new XDataField("BankAccount", DbType.String);
                this.BusinessCircles = new XDataField("BusinessCircles", DbType.String);
                this.Products = new XDataField("Products", DbType.String);
                //3.客户合作信息
                this.BusinessLevel = new XDataField("BusinessLevel", DbType.Byte);
                this.CoopFlag = new XDataField("CoopFlag", DbType.Int32);
                this.StageID = new XDataField("StageID", DbType.Int32);
                this.BuyHabbit = new XDataField("BuyHabbit", DbType.Int32);
                this.LastConsumptionMoney = new XDataField("LastConsumptionMoney", DbType.Decimal);
                this.LastMaintainMoney = new XDataField("LastMaintainMoney", DbType.Decimal);
                this.PreMoney = new XDataField("PreMoney", DbType.Decimal);
                this.LastBegin = new XDataField("LastBegin", DbType.DateTime);
                this.LastEnd = new XDataField("LastEnd", DbType.DateTime);
                this.UrgerDate = new XDataField("UrgerDate", DbType.DateTime);
                this.ConsumptionMoney = new XDataField("ConsumptionMoney", DbType.Decimal);
                this.MaintainMoney = new XDataField("MaintainMoney", DbType.Decimal);
                this.Mobile_D = new XDataField("Mobile_D", DbType.String);
                //4.其它客户信息
                this.AttachFile_D = new XDataField("AttachFile_D", DbType.String);
                this.Remarks = new XDataField("Remarks", DbType.String);
                this.SpecialDesc = new XDataField("SpecialDesc", DbType.String);
                this.Integral_D = new XDataField("Integral_D", DbType.Int32);
                //5.OA维护信息
                this.CreateUserId = new XDataField("CreateUserId", DbType.String);
                this.CreateDate = new XDataField("CreateDate", DbType.DateTime);
                this.EmployeeID = new XDataField("EmployeeID", DbType.String);
                this.DeptId = new XDataField("DeptId", DbType.Int32);
                this.State = new XDataField("State", DbType.Int32);
                this.CheckUserId = new XDataField("CheckUserId", DbType.String);
                this.IsShare = new XDataField("IsShare", DbType.Int32);
                this.UpTime = new XDataField("UpTime", DbType.DateTime);
                //
                this.ID.isIdentity = true;
                this.ID.isKeyField = true;
                //                
                base.AddFields(new XDataField[] { this.ID, this.CustomerID, this.CustomerName, this.CustomerZJM, this.NatureID, this.IndustryID, this.CategoryID, this.SourceID, this.Province, this.City, this.Area, this.Address, this.WebSite, this.ImagePath, this.EstablishmentDate, this.RealName, this.BankName, this.BankAccount, this.BusinessCircles, this.Products, this.BusinessLevel, this.CoopFlag, this.StageID, this.BuyHabbit, this.LastConsumptionMoney, this.LastMaintainMoney, this.PreMoney, this.LastBegin, this.LastEnd, this.UrgerDate, this.ConsumptionMoney, this.MaintainMoney, this.Mobile_D, this.AttachFile_D, this.Remarks, this.SpecialDesc, this.Integral_D, this.CreateUserId, this.CreateDate, this.EmployeeID, this.DeptId, this.State, this.CheckUserId, this.IsShare, this.UpTime });
            }
        }
    }
}
