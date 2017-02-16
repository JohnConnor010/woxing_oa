namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Data.OracleClient;
    using System.Runtime.InteropServices;
    
    public class Oracle : SqlBase
    {
        public OracleConnection GetConnection()
        {
            return (OracleConnection)base.Connection;
        }
        public Oracle(DbSetting settting, string cnName)
            : base(settting, cnName)
        {
            ;
        }
        public Oracle(String connectionString)
            : base(connectionString)
        {
            ;
        }
        ~Oracle()
        {
            this.Close();
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
            OracleConnection sql_cn = new OracleConnection(cnStr);
            try
            {
                sql_cn.Open();
                this.IsConnected = true;
                this.Connection = sql_cn;
            }
            catch (OracleException e)
            {
                this.ConnectError = e;
                string msg = String.Format("链接字符串({0})出现错误{1}！", sql_cn.ConnectionString, e.Message);
                this.DealMsg(msg, true);
            }
        }
        public override void Close()
        {
            OracleConnection oCn = this.GetConnection();
            if (oCn == null) return;
            if (oCn.State != ConnectionState.Closed)
                oCn.Close();
            oCn.Dispose();
            oCn = null;
            this.Connection = null;
        }
        public override SqlBase NewOne()
        {
            return new MsSql(this.ConnectionString);
        }

        private object FillIn(OracleCommand oCmd,CommandMode cmdMode)
        {
            if (cmdMode == CommandMode.Execute)
            {
                if ( oCmd.Parameters != null && oCmd.CommandType == CommandType.StoredProcedure)
                {
                    OracleParameter rParam = null;
                    foreach (OracleParameter para in oCmd.Parameters)
                    {
                        if (para.Direction == ParameterDirection.Output 
                            || para.Direction == ParameterDirection.ReturnValue
                            || para.Direction == ParameterDirection.InputOutput
                            )
                        {
                            rParam = para;
                            break;
                        } 
                    }
                    if (rParam != null)
                    {
                        oCmd.ExecuteNonQuery();
                        return rParam.Value;    //返回值
                    }
                }
                return oCmd.ExecuteNonQuery();  //影响多少行
            }
            else if (cmdMode == CommandMode.GetDataSet)
            {
                DataSet ds = new DataSet();
                OracleDataAdapter oDA = new OracleDataAdapter(oCmd);
                oDA.Fill(ds);
                return ds;
            }
            else if (cmdMode == CommandMode.GetDataTable)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter oDA = new OracleDataAdapter(oCmd);
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
        protected override object GetGeneObject(CommandType cmdType, string cmdText
            ,object cmdParms,CommandMode cmdMode)
        {
            //判断IsConnected，直接退出
            if (!this.IsConnected) this.Connect();
            if (!this.IsConnected) { this.ReturnValue = null; return null; }

            OracleCommand oCmd = new OracleCommand();
            oCmd.Connection = this.GetConnection();
            oCmd.CommandType = cmdType;
            oCmd.CommandText = cmdText;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in (OracleParameter[])cmdParms)
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
            catch (OracleException e)
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
            return ProviderType.Oracle;
        }
    }
}

