namespace ULCode.QDA
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Runtime.InteropServices;

    public class AppSettingSqlStatement : DynamicSqlStatement
    {
        public override string GetSql(ProviderType providerType, string name)
        {
            string sSql = null; sSql = this.GetAppSettingSql(sSql, "Sql." + name);
            switch (providerType)
            {
                case ProviderType.MsSql: sSql = this.GetAppSettingSql(sSql, "MsSql." + name); break;
                case ProviderType.MySql: sSql = this.GetAppSettingSql(sSql, "MySql." + name); break;
                case ProviderType.Oracle: sSql = this.GetAppSettingSql(sSql, "Oracle." + name); break;
                case ProviderType.Odbc: sSql = this.GetAppSettingSql(sSql, "Odbc." + name); break;
                case ProviderType.OleDb: sSql = this.GetAppSettingSql(sSql, "OleDb." + name); break;
            }
            return sSql;
        }
        private string GetAppSettingSql(string sSql, string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
                return Convert.ToString(ConfigurationManager.AppSettings[key]);
            else
                return sSql;
        }
    }
}

