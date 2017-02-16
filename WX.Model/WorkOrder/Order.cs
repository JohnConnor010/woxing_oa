
namespace WX.WorkOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using ULCode;
    using ULCode.QDA;

    public partial class Order
    {
        //以下为实体开发部分
        //工作项目-------------------------------------0网上济宁，1山东频道，2OA
        public static string[] ProjStr = new string[] { "网上济宁", "山东频道", "OA项目","App项目" };
        //工作分类-------------------------------------0开发，1维护，2其它
        public static string[] TypeStr = new string[] { "开发", "维护", "其它" };
        //工作项目-------------------------------------0未提交，1未分配，2已分配，3未接收，4已接收，5执行中，6已暂停，7待验收，8已完成，9已取消
        public static string[] StateStr = new string[] { "编辑中", "未分配", "已分配", "未接收", "已接收", "执行中", "已暂停","待验收", "已完成", "已取消" };

        public partial class MODEL
        {
            //以下为模型开发部分
            //
        }
    }
    public partial class Order : XDataEntity
    {
        public Order(string tableName)
            : base(tableName)
        {
        }
        public Order(string tableName, string keyField)
            : base(tableName, keyField)
        {
        }
        public Order(string cnName, string tableName, string keyField)
            : base(cnName, tableName, keyField)
        {
        }
        //静态部分
        private static Order _entity;
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
        public static MODEL GetModel(string sSql)
        {
            DataTable dt = XSql.GetDataTable(sSql);
            if (dt == null || dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return NewDataModel(dr);
        }
        public static string EnCoding(string str)
        {
            str = str.Replace("\n", "<br/>");
            str = str.Replace(" ", "&nbsp;");
            return str;
        }
        public static DataTable GetListTables(string states,string userid)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select State,count(ID) scount from WorkOrder_Orders where PID is null and UserID='"+userid+"' and State in(" + states + ") group by state");
            return dt;
        }
        public static DataTable GetMyTables(string states, string userid)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select State,count(ID) scount from WorkOrder_Orders where PID >0 and State in(" + states + ") and ExecUserID='" + userid + "' group by state");
            return dt;
        }
        public static DataTable GetAssignTables(string states, string deptid)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select corder.State,count(corder.ID) scount from WorkOrder_Orders corder left join WorkOrder_Orders porder on corder.PID=porder.ID where porder.State>0 and corder.DeptWorkID=" + deptid + " and corder.State in(" + states + ") group by corder.state");
            return dt;
        }
        public static DataTable GetAssign2Tables(string states, string deptid)
        {
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select dept.State,count(dept.ID) scount from WorkOrder_Dept dept inner join WorkOrder_Orders porder on dept.WID=porder.ID where porder.State>0 and DeptID=" + deptid + " and dept.State in(" + states + ") group by dept.state");
            return dt;
        }
        public static MODEL NewDataModel(DataTable dtCache, params object[] keyValues)
        {
            return new MODEL(Entity, dtCache, keyValues);
        }
        public static Order NewEntity()
        {
            return new Order("WorkOrder_Orders", "ID");
        }
        public static MODEL NewModel()
        {
            return new MODEL();
        }
        public static Order Entity
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
            public XDataField PID;
            public XDataField DeptWorkID;
            public XDataField UserID;
            public XDataField AssignUserID;
            public XDataField ExecUserID;
            public XDataField Remarks;
            public XDataField Title;
            public XDataField Count;
            public XDataField Proj;
            public XDataField Type;
            public XDataField State;
            public XDataField YJTime;
            public XDataField AddTime;
            public XDataField SubTime;
            public XDataField FPTime;
            public XDataField YSTime;
            public XDataField StopTime;
            public XDataField StateTime;
            public XDataField IsTop;
            public XDataField TopOrder;
            public XDataField TopTime;
            public XDataField TopCount;

            public MODEL() { }
            public MODEL(Order parentEntity) : base(parentEntity) { }
            public MODEL(Order parentEntity, DataRow drCache) : base(parentEntity, drCache) { }
            public MODEL(Order parentEntity, params object[] keyValues) : base(parentEntity, keyValues) { }
            public MODEL(Order parentEntity, DataTable dtCache, params object[] keyValues) : base(parentEntity, dtCache, keyValues) { }
            protected override void LoadFields()
            {
            
                this.ID=new XDataField("ID",DbType.Int32);
                this.PID=new XDataField("PID",DbType.Int32);
                this.DeptWorkID=new XDataField("DeptWorkID",DbType.Int32);
                this.UserID=new XDataField("UserID",DbType.String);
                this.AssignUserID=new XDataField("AssignUserID",DbType.String);
                this.ExecUserID=new XDataField("ExecUserID",DbType.String);
                this.Remarks=new XDataField("Remarks",DbType.String);
                this.Title=new XDataField("Title",DbType.String);
                this.Count=new XDataField("Count",DbType.Int32);
                this.Proj=new XDataField("Proj",DbType.Int32);
                this.Type=new XDataField("Type",DbType.Int32);
                this.State=new XDataField("State",DbType.Int32);
                this.YJTime=new XDataField("YJTime",DbType.DateTime);
                this.AddTime=new XDataField("AddTime",DbType.DateTime);
                this.SubTime = new XDataField("SubTime", DbType.DateTime);
                this.FPTime = new XDataField("FPTime", DbType.DateTime);
                this.YSTime = new XDataField("YSTime", DbType.DateTime);
                this.StopTime=new XDataField("StopTime",DbType.DateTime);
                this.StateTime=new XDataField("StateTime",DbType.DateTime);
                this.IsTop = new XDataField("IsTop", DbType.Int32);
                this.TopOrder = new XDataField("TopOrder", DbType.Int32);
                this.TopTime = new XDataField("TopTime", DbType.DateTime);
                this.TopCount = new XDataField("TopCount", DbType.String);
                //
                            
                //                
                base.AddFields(new XDataField[] { this.ID, this.PID, this.DeptWorkID, this.UserID, this.AssignUserID, this.ExecUserID, this.Remarks, this.Title, this.Count, this.Proj, this.Type, this.State, this.YJTime, this.AddTime, SubTime, FPTime, YSTime, this.StopTime, this.StateTime, this.IsTop, this.TopOrder, this.TopTime, this.TopCount });
            }
        }
    }
}
