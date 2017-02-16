namespace ULCode.QDA
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;
    using System.IO;
    using System.Data;
    using System.Web.UI.WebControls;
    using ULCode.QDA;
    
    public class XSql
    {
        //默认功能
        private static Dictionary<String, SqlBase> _DB = null;
        public static Dictionary<String,SqlBase> DB 
        {
            get
            {
                
                InitDB();
                return _DB;
            }
        }
        private static DbSetting MyDbSetting = new WebConfigCnSetting();
        private static DynamicSqlStatement _MyDss = null;
        private static DynamicSqlStatement MyDss
        {
            get
            {
                if (_MyDss == null)
                {
                    string dss = Convert.ToString(ConfigurationManager.AppSettings["DSS-Mode"]);
                    if (!String.IsNullOrEmpty(dss))
                    {
                        if (dss == "AppSettings-Default")
                            _MyDss = new AppSettingSqlStatement();
                    }
                } 
                return _MyDss;
            }
        }
        private static void InitDB()
        {
            if (_DB == null)
                _DB = new Dictionary<String, SqlBase>();
            if (_DB.Count > 0) return;
            foreach (ConnectionStringSettings css in MyDbSetting.GetAllConnections())
            {
                SqlBase sb = null;
                switch (MyDbSetting.GetProviderType(css.ProviderName))
                {
                    case ProviderType.MsSql: sb = new MsSql(MyDbSetting, css.Name); break;
                    case ProviderType.OleDb: sb = new OleDb(MyDbSetting, css.Name); break;
                    case ProviderType.Odbc: sb = new Odbc(MyDbSetting, css.Name); break;
                    case ProviderType.Oracle: sb = new Oracle(MyDbSetting, css.Name); break;
                    case ProviderType.MySql: sb = new MySql(MyDbSetting, css.Name); break;
                }
                sb.DSS = MyDss;
                if (sb != null)
                    if (!_DB.ContainsKey(css.Name))
                        _DB.Add(css.Name, sb);
            }
        }
        public static SqlBase GetDB(string name)
        {
            if (String.IsNullOrEmpty(name)) return DefaultDB;
            return DB[name];
        }
        public static SqlBase DefaultDB
        {
            get
            {
                string defaultName = MyDbSetting.GetDefaultName();
                if (String.IsNullOrEmpty(defaultName)) return null;
                return DB[defaultName];
            }
        }
        public static SqlBase NewDB(string name)
        {
            SqlBase sb = String.IsNullOrEmpty(name) ? DefaultDB.NewOne() : DB[name].NewOne();
            sb.DSS = MyDss;
            return sb;
        }
        public static SqlBase NewDB(ProviderType providerType, String connectionString)
        {
            SqlBase sb = null;
            switch (providerType)
            {
                case ProviderType.MsSql: sb = new MsSql(connectionString); break;
                case ProviderType.OleDb: sb = new OleDb(connectionString); break;
                case ProviderType.Odbc: sb = new Odbc(connectionString); break;
                case ProviderType.Oracle: sb = new Oracle(connectionString); break;
                case ProviderType.MySql: sb = new MySql(connectionString); break;
            }
            sb.DSS = MyDss;
            return sb;
        }
        //简捷一次性功能
        //IO行为，只适合Web下删除文件
        public int DeleteFiles(string sSql) { return this.DeleteFiles(null, sSql); }
        public int DeleteFiles(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return -1;
            if (ULCode.QDA.Debug.IS_FORM_CLIENT)
            {
                return -1;
            }
            int iR= db.DeleteFiles(sSql);
            db.Close();
            return iR;
        }
        public DbResult DeleteFiles_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            int iR = 0;
            if (!db.IsConnected)
                iR = -1;
            else
                iR = db.DeleteFiles(sSql);
            DbResult dr = new DbResult(db);
            dr.ReturnValue = iR;
            db.Close();
            return dr;
        }
        /*Execute部分*/
        public static int Execute(string sSql) { return Execute(null, sSql); }
        public static int Execute(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return -1;
            int iR = db.Execute(sSql);
            db.Close();
            return iR;
        }
        public static DbResult Execute_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            int iR = 0;
            if (!db.IsConnected)
                iR = -1;
            else
                iR = db.Execute(sSql);
            DbResult dr = new DbResult(db);
            dr.ReturnValue = iR;
            db.Close();
            return dr;
        }
        /*ExecuteNonQuery部分*/
        public static object ExecuteGeneral(CommandType cmdType, string sSql, object cmdParameters, CommandMode cmdMode)
        {
            return ExecuteGeneral(null, cmdType, sSql, cmdType, cmdMode);
        }
        public static object ExecuteGeneral(String cnName, CommandType cmdType, string sSql, object cmdParameters, CommandMode cmdMode)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            object oR = db.ExecuteGeneral(cmdType, sSql, cmdParameters, cmdMode);
            db.Close();
            return oR;
        }
        public static DbResult ExecuteGeneral_R(String cnName, CommandType cmdType, string sSql, object cmdParameters, CommandMode cmdMode)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            object oR = null;
            if (db.IsConnected)
                oR = db.ExecuteGeneral(cmdType, sSql, cmdParameters, cmdMode);
            DbResult dr = new DbResult(db);   
            db.Close();
            return dr;
        }
        //GetValue
        public static object GetValue(string sSql) { return GetValue(null, sSql); }
        public static object GetValue(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return -1;
            object oR = db.GetValue(sSql);
            db.Close();
            return oR;
        }
        public static DbResult GetValue_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetValue_R(sSql);
            db.Close();
            return dr;
        }
        //IsHasRow
        public static bool IsHasRow(string sSql)
        {
            return IsHasRow(null, sSql);
        }
        public static bool IsHasRow(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return false;
            bool blnR = db.IsHasRow(sSql);
            db.Close();
            return blnR;
        }
        public static DbResult IsHasRow_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.IsHasRow_R(sSql, cnName);
            db.Close();
            return dr;
        }
        //GetDataReader
        public static object GetDataReader(string sSql)
        {
            return GetDataReader(null, sSql);
        }
        public static object GetDataReader(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return -1;
            object oR = db.GetDataReader(sSql);
            db.Close();
            return oR;
        }
        public static DbResult GetDataReader_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetDataReader_R(sSql);     
            db.Close();
            return dr;
        }
        //GetXmlReader
        public static System.Xml.XmlReader GetXmlReader(string sSql)
        {
            return GetXmlReader(null, sSql);
        }
        public static System.Xml.XmlReader GetXmlReader(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            System.Xml.XmlReader xr = db.GetXmlReader(sSql);
            db.Close();
            return xr;
        }
        public static DbResult GetXmlReader_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetXmlReader_R(sSql);   
            db.Close();
            return dr;
        }
        //GetData部分
        public static DbValue GetData(string sSql)
        {
            return GetData(null, sSql);
        }
        public static DbValue GetData(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            DbValue dv = db.GetData(sSql);
            db.Close();
            return dv;
        }
        public static DbResult GetData_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetData_R(sSql); 
            db.Close();
            return dr;
        }
        //GetDatas部分
        public static DbValues GetDatas(string sSql) { return GetDatas(null, sSql); }
        public static DbValues GetDatas(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            DbValues dvs = db.GetDatas(sSql);
            db.Close();
            return dvs;
        }
        public static DbResult GetDatas_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetDatas_R(sSql);    
            db.Close();
            return dr;
        }
        //GetDataTable
        public static DataTable GetDataTable(string sSql) { return GetDataTable(null, sSql); }
        public static DataTable GetDataTable(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            DataTable dt = db.GetDataTable(sSql);
            db.Close();
            return dt;
        }
        public static DbResult GetDataTable_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetDataTable_R(sSql);         
            db.Close();
            return dr;
        }
        //GetDataSet部分
        public static DataSet GetDataSet(string sSql) { return GetDataSet(null, sSql); }
        public static DataSet GetDataSet(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            DataSet ds = db.GetDataSet(sSql);
            db.Close();
            return ds;
        }
        public static DbResult GetDataSet_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetDataSet_R(sSql);
            db.Close();
            return dr;
        }
        //GetXDataTable部分
        public static XDataTable GetXDataTable(string sSql) { return GetXDataTable(null, sSql); }
        public static XDataTable GetXDataTable(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return null;
            XDataTable xdt = db.GetXDataTable(sSql);
            db.Close();
            return xdt;
        }
        public static DbResult GetXDataTable_R(String cnName, string sSql)
        {
            SqlBase db = NewDB(cnName);
            db.Connect();
            if (!db.IsConnected) return new DbResult(db);
            DbResult dr = db.GetXDataTable_R(sSql);
            db.Close();
            return dr;
        }
    }   
}

