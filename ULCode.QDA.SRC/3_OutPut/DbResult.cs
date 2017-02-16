using System;
using System.Data;
using System.Xml;

namespace ULCode.QDA
{
    public class DbResult
    {
        public bool IsConnected = false;
        public Exception ConnError = null;
        public Exception ExecError = null;
        public string ConnectionString = null;
        public string CmdText = null;
        public object ReturnValue = null;
        public bool OK
        {
            get
            {
                return this.IsConnected && this.ConnError == null && this.ExecError == null;
            }
        }
        public bool FoundError
        {
            get
            {
                return !this.IsConnected || this.ConnError != null || this.ExecError != null;
            }
        }
        public DbResult(bool isConnected, string cnStr, string cmdText, Exception connError, Exception execError)
        {
            this.IsConnected = isConnected;
            this.ConnectionString = cnStr;
            this.CmdText = cmdText;
            this.ConnError = connError;
            this.ExecError = execError;
        }
        public DbResult(SqlBase sql)
        {
            this.IsConnected = sql.IsConnected;
            this.ConnectionString = sql.ConnectionString;
            this.ExecError = sql.ExecError;
            this.ConnError = sql.ConnectError;
            this.CmdText = sql.CmdText;
            this.ReturnValue = sql.ReturnValue;
        }
        public Int32 ToInt32()
        {
            return Convert.ToInt32(this.ReturnValue);
        }
        public Object ToObject()
        {
            return this.ReturnValue;
        }
        public ULCode.QDA.DbValue ToDbValue()
        {
            return (DbValue)this.ReturnValue;
        }
        public ULCode.QDA.DbValues ToDbValues()
        {
            return (DbValues)this.ReturnValue;
        }
        public DataTable ToDataTable()
        {
            return (DataTable)this.ReturnValue;
        }
        public DataSet ToDataSet()
        {
            return (DataSet)this.ReturnValue;
        }
        public XDataTable ToXDataTable()
        {
            return (XDataTable)this.ReturnValue;
        }
        public XmlReader ToXmlReader()
        {
            return (XmlReader)this.ReturnValue;
        }
    }
}
