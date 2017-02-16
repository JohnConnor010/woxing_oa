namespace ULCode.QDA
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;

    //public class ConnectErrorObject
    //{
    //    public object CnObject;
    //    public ConnectErrorObject(object cnObject) { CnObject = cnObject; }
    //}
    public abstract class SqlBase
    {
        /// <summary>
        /// 此类是所有数据访问类的基类！
        /// 直接输入Connection对象
        /// 直接输入cnName对象
        /// </summary>
        public object Connection;
        public bool IsConnected = false;
        public Exception ConnectError = null;
        public Exception ExecError = null;
        public string ConnectionString = null;
        public string CmdText = null;
        public object ReturnValue = null;
        
        public delegate void SetConnectStateCallback(String msg);
        public SetConnectStateCallback ShowConnectState;

        public DynamicSqlStatement DSS = null;
        //public DbSetting CurDbSetting = null;
        //public ConnectionStringSettings CurConnectionString = null;
        
        public SqlBase(DbSetting setting, String name)
        {
            //this.CurDbSetting = setting;
            //this.CurConnectionString = setting.Find(name);
            this.ConnectionString = setting.Find(name).ConnectionString;
        }
        public SqlBase(String connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public string GetSql(string sSql)
        {
            if (this.DSS != null && Regex.IsMatch(sSql, @"[\#]\w{1,50}"))
            {
                return this.DSS.GetSql(this.GetProviderType(), sSql.Substring(1));
            }
            this.CmdText = sSql;
            return sSql;
        }
        protected abstract object GetGeneObject(CommandType cmdType, string cmdText
            , object cmdParams, CommandMode cmdMode);
        protected DbResult GetGeneObject_R(CommandType cmdType, string cmdText
            , object cmdParams, CommandMode cmdMode)
        {
            this.GetGeneObject(cmdType, cmdText, cmdParams, cmdMode);
            return new DbResult(this);
        }
        public abstract void Connect();
        public abstract void Close();
        public abstract SqlBase NewOne();
        public abstract ProviderType GetProviderType();
        public bool FoundError()
        {
            return this.ConnectError != null && this.ExecError != null;
        }
        /*输出部分*/
        //IO行为，只适合Web下删除文件
        public int DeleteFiles(string sSql) { return this.DeleteFiles(sSql, null); }
        public int DeleteFiles(string sSql, object cmdParameters)
        {
            sSql = this.GetSql(sSql);
            if (ULCode.QDA.Debug.IS_FORM_CLIENT)
            {
                return -1;
            }
            object[] o = this.GetXDataTable(sSql, cmdParameters).ToObjectArray();
            for (int i = 0; i < o.Length; i++)
            {
                string path = Convert.ToString(o[i]);
                if (path.Contains("://"))
                {
                    break;
                }
                path = HttpContext.Current.Server.MapPath(path);
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                    break;
                }
            }
            return o.Length;
        }
        /*Execute部分*/
        public int Execute(string sSql)  //实现
        {
            return this.Execute(sSql, null);
        }
        public int Execute(string sSql,object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            int iR = Convert.ToInt32(this.GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.Execute));
            return Convert.ToInt32(iR);
        }
        public DbResult Execute_R(string sSql)  //实现
        {
            return this.Execute_R(sSql, null);
        }
        public DbResult Execute_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject_R(CommandType.Text, sSql, cmdParameters, CommandMode.Execute);
        }
        /*ExecuteNonQuery部分*/
        public object ExecuteGeneral(CommandType cmdType, string sSql, object cmdParameters, CommandMode cmdMode)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject(cmdType, sSql, cmdParameters, cmdMode);
        }
        public DbResult ExecuteGeneral_R(CommandType cmdType, string sSql, object cmdParameters, CommandMode cmdMode)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject_R(cmdType, sSql, cmdParameters, cmdMode);
        }
        //GetValue
        public object GetValue(string sSql)  //实现
        {
            return this.GetValue(sSql, null);
        }
        public object GetValue(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.GetValue);
        }
        public DbResult GetValue_R(string sSql)  //实现
        {
            return this.GetValue_R(sSql, null);
        }
        public DbResult GetValue_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return GetGeneObject_R(CommandType.Text, sSql, cmdParameters, CommandMode.GetValue);
        }
        //IsHasRow
        public bool IsHasRow(string sSql)  //实现
        {
            return this.IsHasRow(sSql, null);
        }
        public bool IsHasRow(string sSql,object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return Convert.ToBoolean(this.GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.HasRow));
        }
        public DbResult IsHasRow_R(string sSql)  //实现
        {
            return this.IsHasRow_R(sSql, null);
        }
        public DbResult IsHasRow_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject_R(CommandType.Text, sSql, cmdParameters, CommandMode.HasRow);
        }
        //GetDataReader
        public object GetDataReader(string sSql)  //实现
        {
            return this.GetDataReader(sSql, null);
        }
        public object GetDataReader(string sSql,object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.DataReader);
        }
        public DbResult GetDataReader_R(string sSql)  //实现
        {
            return this.GetDataReader_R(sSql, null);
        }
        public DbResult GetDataReader_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return this.GetGeneObject_R(CommandType.Text, sSql, cmdParameters, CommandMode.DataReader);
        }
        //GetXmlReader
        public XmlReader GetXmlReader(string sSql)  //实现
        {
            return this.GetXmlReader(sSql, null);
        }
        public XmlReader GetXmlReader(string sSql,object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return (XmlReader)GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.XmlReader);
        }
        public DbResult GetXmlReader_R(string sSql)  //实现
        {
            return this.GetXmlReader_R(sSql, null);
        }
        public DbResult GetXmlReader_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return GetGeneObject_R(CommandType.Text, sSql, cmdParameters, CommandMode.XmlReader);
        }
        //GetData部分
        public DbValue GetData(string sSql)
        {
            return this.GetData(sSql, null);
        }
        public DbValue GetData(string sSql, object cmdParameters)
        {
            return new DbValue(this.GetValue(sSql,cmdParameters));
        }
        public DbResult GetData_R(string sSql)
        {
            return this.GetData_R(sSql, null);
        }
        public DbResult GetData_R(string sSql, object cmdParameters)
        {
            this.ReturnValue = new DbValue(this.GetValue(sSql, cmdParameters));
            return new DbResult(this);            
        }
        //GetDatas部分
        public DbValues GetDatas(string sSql)
        {
            return this.GetDatas(sSql, null);
        }
        public DbValues GetDatas(string sSql, object cmdParameters)
        {
            return new DbValues(this.GetDataTable(sSql, cmdParameters));
        }
        public DbResult GetDatas_R(string sSql)
        {
            return this.GetDatas_R(sSql, null);
        }
        public DbResult GetDatas_R(string sSql, object cmdParameters)
        {
            this.ReturnValue = new DbValues(this.GetDataTable(sSql, cmdParameters));
            return new DbResult(this);
        }
        //GetDataTable
        public DataTable GetDataTable(string sSql)  //实现
        {
            return this.GetDataTable(sSql, null);
        }
        public DataTable GetDataTable(string sSql,object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            return (DataTable)GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.GetDataTable);
        }
        public DbResult GetDataTable_R(string sSql)  //实现
        {
            return this.GetDataTable_R(sSql, null);
        }
        public DbResult GetDataTable_R(string sSql, object cmdParameters)  //实现
        {
            sSql = this.GetSql(sSql);
            this.ReturnValue = (DataTable)GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.GetDataTable);
            return new DbResult(this);
        }
        //GetDataSet部分
        public DataSet GetDataSet(string sSql) //实现
        {
            return this.GetDataSet(sSql, null);
        }
        public DataSet GetDataSet(string sSql,object cmdParameters) //实现
        {
            sSql = this.GetSql(sSql);
            return (DataSet)GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.GetDataSet);
        }
        public DbResult GetDataSet_R(string sSql) //实现
        {
            return this.GetDataSet_R(sSql, null);
        }
        public DbResult GetDataSet_R(string sSql, object cmdParameters) //实现
        {
            sSql = this.GetSql(sSql);
            this.ReturnValue = (DataSet)GetGeneObject(CommandType.Text, sSql, cmdParameters, CommandMode.GetDataSet);
            return new DbResult(this);
        }
        //GetXDataTable部分
        public XDataTable GetXDataTable(string sSql)
        {
            return this.GetXDataTable(sSql, null);
        }
        public XDataTable GetXDataTable(string sSql, object cmdParameters)
        {
            return new XDataTable(this.GetDataTable(sSql,cmdParameters));
        }
        public DbResult GetXDataTable_R(string sSql)
        {
            return this.GetXDataTable_R(sSql, null);
        }
        public DbResult GetXDataTable_R(string sSql, object cmdParameters)
        {
            this.ReturnValue = new XDataTable(this.GetDataTable(sSql, cmdParameters));
            return new DbResult(this);
        }
        //其它
        protected void DealMsg(string msg, bool errFound)
        {
            if (this.ShowConnectState != null && errFound)
            {
                this.ShowConnectState(msg);
            }
            switch (Debug.DEBUG)
            {
                case DebugType.NoDebug: if (errFound) throw new ApplicationException(msg); break;
                case DebugType.Debug: if (errFound) Debug.PRINT(msg); break;
                case DebugType.DebugAll: Debug.PRINT(msg); break;
                case DebugType.HiddenDebug: break;
            }
        }
    }
}

