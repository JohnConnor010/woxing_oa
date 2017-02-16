namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Data.Odbc;

    public class Odbc : SqlBase
    {
        public Odbc(DbSetting setting, string cnName)
            : base(setting, cnName)
        {
            ;
        }
        public Odbc(String connectionString)
            : base(connectionString)
        {
            ;
        }  
        ~Odbc()
        {
            this.Close();
        }
        private OdbcConnection GetConnection()
        {
            return (OdbcConnection) base.Connection;
        }
        public override void Connect()
        {
            //初始化状态变量
            this.IsConnected = false;
            this.ConnectError = null;
            this.ExecError = null;
            this.ReturnValue = null;
            //开始
            if (this.Connection != null)
            {
                //throw new ApplicationException("数据连接重复打开！");
                string msg = "数据连接重复打开！";
                this.DealMsg(msg, true);
                return;
            }
            string cnStr = this.ConnectionString;
            if (String.IsNullOrEmpty(cnStr))
            {
                //throw new ApplicationException("数据连接字符串为空！");
                string msg = "数据连接字符串为空！";
                this.DealMsg(msg, true);
                return;
            }
            OdbcConnection sql_cn = new OdbcConnection(cnStr);
            try
            {
                sql_cn.Open();
                this.IsConnected = true;
                this.Connection = sql_cn;
            }
            catch (OdbcException e)
            {
                this.ConnectError = e;
                string msg = String.Format("链接字符串({0})出现错误{1}！", sql_cn.ConnectionString, e.Message);
                this.DealMsg(msg, true);
            }
        }
        public override void Close()
        {
            OdbcConnection oCn = this.GetConnection();
            if (oCn == null) return;
            if (oCn.State != ConnectionState.Closed)
                oCn.Close();
            oCn.Dispose();
            oCn = null;
            this.Connection = null;
        }
        public override SqlBase NewOne()
        {
            return new Odbc(this.ConnectionString);
        }
        private object FillIn(OdbcCommand oCmd, CommandMode cmdMode)
        {
            if (cmdMode == CommandMode.Execute)
            {
                return oCmd.ExecuteNonQuery();
            }
            else if (cmdMode == CommandMode.GetDataSet)
            {
                DataSet ds = new DataSet();
                OdbcDataAdapter oDA = new OdbcDataAdapter(oCmd);
                oDA.Fill(ds);
                return ds;
            }
            else if (cmdMode == CommandMode.GetDataTable)
            {
                DataTable dt = new DataTable();
                OdbcDataAdapter oDA = new OdbcDataAdapter(oCmd);
                oDA.Fill(dt);
                return dt;
            }
            else if (cmdMode == CommandMode.GetValue)
            {
                return oCmd.ExecuteScalar();
            }
            else if (cmdMode == CommandMode.HasRow)
            {
                oCmd.CommandText = String.Format("if exists({0})select 1; else select 0;", oCmd.CommandText);
                return (Convert.ToInt32(oCmd.ExecuteScalar()) == 1);
            }
            else if (cmdMode == CommandMode.Count)
            {
                oCmd.CommandText = String.Format("select count(*) from ({0}) as A", oCmd.CommandText);
                return Convert.ToInt32(oCmd.ExecuteScalar());
            }
            else if (cmdMode == CommandMode.DataReader)
            {
                return oCmd.ExecuteReader();
            }            
            return null;
        }
        protected override object GetGeneObject(CommandType cmdType, string cmdText, object cmdParms, CommandMode cmdMode)
        {
            //判断IsConnected，直接退出
            if (!this.IsConnected) this.Connect();
            if (!this.IsConnected) { this.ReturnValue = null; return null; }

            OdbcCommand oCmd = new OdbcCommand();
            oCmd.Connection = this.GetConnection();
            oCmd.CommandType = cmdType;
            oCmd.CommandText = cmdText;
            if (cmdParms != null)
            {
                foreach (OdbcParameter parm in (OdbcParameter[])cmdParms)
                {
                    oCmd.Parameters.Add(parm);
                }
            }
            object r = null;
            try
            {
                r = FillIn(oCmd, cmdMode);
                #if DEBUG
                this.DealMsg(cmdText, false);
                #endif
            }
            catch (OdbcException e)
            {
                this.ExecError = e;
                string errorMsg = "Odbc.GetGeneObject:［" + cmdText + "］（" + e.Message + "）,Faild,错误信息已经复制到剪切版！";
                this.DealMsg(errorMsg, true);
            }
            oCmd = null;
            this.ReturnValue = r;
            return r;
        }
        public override ProviderType GetProviderType()
        {
            return ProviderType.Odbc;
        }
    }
}

