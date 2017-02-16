using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace ULCode.QDA
{
    /// <summary>
    /// 此类暂未用
    /// </summary>
	public class CnStrSettings
	{
        public String Name = null;
        public String ConnectionString = null;
        public ProviderType ProviderType = ProviderType.UnDefined;
        public CnStrSettings(String name,String connectionString,ProviderType providerType)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderType = providerType;
        }
        public CnStrSettings(ConnectionStringSettings cnStrSettings)
        {
            this.ConnectionString = cnStrSettings.ConnectionString;
            this.Name = cnStrSettings.Name;
            this.ProviderType = this.GetProviderType(cnStrSettings.ProviderName);
        }
        private ProviderType GetProviderType(string name)
        {
            switch (name)
            {
                case "System.Data.SqlClient": return ProviderType.MsSql;
                case "System.Data.OleDb": return ProviderType.OleDb;
                case "System.Data.Odbc": return ProviderType.Odbc;
                case "System.Data.OracleClient": return ProviderType.Oracle;
                case "MySql.Data.MySqlClient": return ProviderType.MySql;
            }
            return ProviderType.UnDefined;
        }
	}
    public class CnStrSettingsCollection
    {      
    }
}
