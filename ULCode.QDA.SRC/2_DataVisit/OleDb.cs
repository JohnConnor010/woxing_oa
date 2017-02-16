namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class OleDb : SqlBase
    {
        public OleDb(DbSetting setting, string cnName)
            : base(setting, cnName)
        {
            ;
        }
        public OleDb(String connectionString)
            : base(connectionString)
        {
            ;
        }  
        ~OleDb()
        {
            this.Close();
        }
        private OleDbConnection GetConnection()
        {
            return (OleDbConnection) base.Connection;
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
            OleDbConnection sql_cn = new OleDbConnection(cnStr);
            try
            {
                sql_cn.Open();
                this.IsConnected = true;
                this.Connection = sql_cn;
            }
            catch (OleDbException e)
            {
                this.ConnectError = e;
                string msg = String.Format("链接字符串({0})出现错误{1}！", sql_cn.ConnectionString, e.Message);
                this.DealMsg(msg, true);
            }
        }
        public override void Close()
        {
            OleDbConnection oCn = this.GetConnection();
            if (oCn == null) return;
            if (oCn.State != ConnectionState.Closed)
                oCn.Close();
            oCn.Dispose();
            oCn = null;
            this.Connection = null;
        }
        public override SqlBase NewOne()
        {
            return new OleDb(this.ConnectionString);
        }
        private object FillIn(OleDbCommand oCmd, CommandMode cmdMode)
        {
            if (cmdMode == CommandMode.Execute)
            {
                return oCmd.ExecuteNonQuery();
            }
            else if (cmdMode == CommandMode.GetDataSet)
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter oDA = new OleDbDataAdapter(oCmd);
                oDA.Fill(ds);
                return ds;
            }
            else if (cmdMode == CommandMode.GetDataTable)
            {
                DataTable dt = new DataTable();
                OleDbDataAdapter oDA = new OleDbDataAdapter(oCmd);
                oDA.Fill(dt);
                return dt;
            }
            else if (cmdMode == CommandMode.GetValue)
            {
                return oCmd.ExecuteScalar();
            }
            else if (cmdMode == CommandMode.HasRow)
            {
                oCmd.CommandText = String.Format("{0}", oCmd.CommandText);
                return (oCmd.ExecuteScalar() != null);
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

            OleDbCommand oCmd = new OleDbCommand();
            oCmd.Connection = this.GetConnection();
            oCmd.CommandType = cmdType;
            oCmd.CommandText = cmdText;
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in (OleDbParameter[])cmdParms)
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
            catch (OleDbException e)
            {
                this.ExecError = e;
                string errorMsg = "MySql.GetGeneObject:［" + cmdText + "］（" + e.Message + "）,Faild,错误信息已经复制到剪切版！";
                this.DealMsg(errorMsg, true);
            }
            oCmd = null;
            this.ReturnValue = r;
            return r;
        }
        public override ProviderType GetProviderType()
        {
            return ProviderType.OleDb;
        }
    }
}

