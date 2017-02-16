namespace ULCode.QDA
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    
    public class MsSql : SqlBase
    {
        public SqlConnection GetConnection()
        {
            return (SqlConnection)base.Connection;
        }
        public MsSql(DbSetting settting, string cnName)
            : base(settting, cnName)
        {
            ;
        }
        public MsSql(String connectionString)
            : base(connectionString)
        {
            ;
        }        
        ~MsSql()
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
            SqlConnection sql_cn = new SqlConnection(cnStr);
            try
            {
                sql_cn.Open();
                this.IsConnected = true;
                this.Connection = sql_cn;
            }
            catch (SqlException e)
            {
                this.ConnectError = e;
                string msg = String.Format("链接字符串({0})出现错误{1}！", sql_cn.ConnectionString, e.Message);
                this.DealMsg(msg, true);
            }
            #region //将删除
            /*
            //get sql_cn
            SqlConnection sql_cn = null;
            if (this.Connection == null)
            {
                string cnStr = this.ConnectionString;
                if (!String.IsNullOrEmpty(cnStr))
                {
                    sql_cn = new SqlConnection(cnStr);
                }
                else
                {
                    string msg = String.Format("链接字符串为空！请配置好链接字符串！");
                    this.DealMsg(msg,true);
                }
            }
            else
            {
                sql_cn = this.GetConnection();
            }
            if (sql_cn != null)
            {
                if (sql_cn.State != ConnectionState.Closed)
                {
                    this.IsConnected = true;
                    this.Connection = sql_cn;
                }
                else
                {
                    try
                    {
                        sql_cn.Open();
                        this.IsConnected = true;
                        this.Connection = sql_cn;
                    }
                    catch(System.Data.Common.DbException e)
                    {
                        if (!String.IsNullOrEmpty(this.ClientKey))
                        {
                            SqlErr.Capture(this.ClientKey, e);
                        }
                        string msg = String.Format("链接字符串({0})无法登录！请配置好链接字符串！", sql_cn.ConnectionString);
                        this.DealMsg(msg,true);
                    }
                }
            }*/
            #endregion
        }
        public override void Close()
        {            
            SqlConnection oCn = this.GetConnection();
            if (oCn == null) return;
            if (oCn.State != ConnectionState.Closed)
                try
                {
                    oCn.Close();
                }
                catch { ; }
            oCn.Dispose();
            oCn = null;
            this.Connection = null;
        }
        public override SqlBase NewOne()
        {
            return new MsSql(this.ConnectionString);
        }

        private object FillIn(SqlCommand oCmd,CommandMode cmdMode)
        {
            if (cmdMode == CommandMode.Execute)
            {
                if ( oCmd.Parameters != null && oCmd.CommandType == CommandType.StoredProcedure)
                {
                    SqlParameter rParam = null;
                    foreach (SqlParameter para in oCmd.Parameters)
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
                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
                oDA.Fill(ds);
                return ds;
            }
            else if (cmdMode == CommandMode.GetDataTable)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter oDA = new SqlDataAdapter(oCmd);
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
            else if (cmdMode == CommandMode.XmlReader)
            {                
                return oCmd.ExecuteXmlReader();
            }
            return null;
        }
        protected override object GetGeneObject(CommandType cmdType, string cmdText
            ,object cmdParms,CommandMode cmdMode)
        {
            //判断IsConnected，直接退出
            if (!this.IsConnected) this.Connect();
            if (!this.IsConnected) { this.ReturnValue = null; return null; }
            #region //将删除
            /*
            SqlConnection sqlCn=this.GetConnection();
            if (sqlCn == null)
            {
                return null;
            }

            if (sqlCn.State != ConnectionState.Open)
                sqlCn.Open();
            */
            #endregion
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = this.GetConnection();
            oCmd.CommandType = cmdType;
            oCmd.CommandText = cmdText;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in (SqlParameter[])cmdParms)
                {
                    oCmd.Parameters.Add(parm);
                }
            }
            object r=null;
            try
            {
                r = FillIn(oCmd, cmdMode);
                #if DEBUG
                this.DealMsg(cmdText, false);
                #endif
            }
            catch (SqlException e)
            {
                this.ExecError = e;
                string errorMsg = "Sql.GetGeneObject:［" + cmdText + "］（" + e.Message + "）,Faild,错误信息已经复制到剪切版！";
                this.DealMsg(errorMsg,true);
            }
            oCmd = null;
            this.ReturnValue = r;
            return r;
        }
        public override ProviderType GetProviderType()
        {
            return ProviderType.MsSql;
        }
    }
}

